using System;
using System.Linq;
using System.Threading.Tasks;
using IApplicationService;
using IApplicationService.Base.AppQuery;
using Infrastructure.EfDataAccess;
using InfrastructureBase.AuthBase;
using Oxygen.Client.ServerProxyFactory.Interface;
using InfrastructureBase.Data;
using Microsoft.EntityFrameworkCore;
using IApplicationService.TradeService.Dtos.Output;
using IApplicationService.TradeService.Dtos.Input;
using Domain.Repository;
using InfrastructureBase;
using IApplicationService.TradeService;
using IApplicationService.GoodsService;
using Domain.Services;
using DomainBase;
using System.Collections.Generic;
using Domain.Entities;
using InfrastructureBase.Object;
using Domain.Specification;
using Domain.ValueObject;
using Domain.Dtos;
using IApplicationService.GoodsService.Dtos.Input;
using InfrastructureBase.Http;
using IApplicationService.AppEvent;
using Domain.Events;

namespace ApplicationService
{
    public class OrderUseCaseService : IOrderUseCaseService
    {
        private readonly IOrderRepository repository;
        private readonly IUnitofWork unitofWork;
        private readonly IEventBus eventBus;
        private readonly IStateManager stateManager;
        private readonly IGoodsQueryService goodsQueryService;
        private readonly IGoodsActorService goodsActorService;
        public OrderUseCaseService(IOrderRepository repository, IEventBus eventBus, IStateManager stateManager, IUnitofWork unitofWork, IGoodsQueryService goodsQueryService, IGoodsActorService goodsActorService)
        {
            this.repository = repository;
            this.unitofWork = unitofWork;
            this.eventBus = eventBus;
            this.stateManager = stateManager;
            this.goodsQueryService = goodsQueryService;
            this.goodsActorService = goodsActorService;
        }

        [AuthenticationFilter]
        public async Task<ApiResult> CreateOrder(OrderCreateDto input)
        {
            //����һ�����������������ʵ��,��Զ��rpc������Ϊ�����������ݽ�ȥ
            var createOrderService = new CreateOrderService(GetGoodsListByIds, DeductionGoodsStock, UnDeductionGoodsStock);
            return await ApiResult.Ok("���������ɹ�!").RunAsync(async () =>
            {
                var order = await createOrderService.CreateOrder(HttpContextExt.Current.User.Id, HttpContextExt.Current.User.UserName, HttpContextExt.Current.User.Address, HttpContextExt.Current.User.Tel, input.Items.CopyTo<OrderCreateDto.OrderCreateItemDto, OrderItem>());//ͨ���������񴴽�����
                repository.Add(order);
                if (await new CheckOrderCanCreateSpecification(repository).IsSatisfiedBy(order))
                    await unitofWork.CommitAsync();
                await eventBus.SendEvent(EventTopicDictionary.Order.CreateOrderSucc, new CreateOrderSuccessEvent(order, HttpContextExt.Current.User.NickName));//���Ͷ��������ɹ��¼�
            },
            //ʧ�ܻع�
            createOrderService.UnCreateOrder);
        }

        [AuthenticationFilter]
        public async Task<ApiResult> UpdateOrder(OrderUpdateDto input)
        {
            var entity = await repository.GetAsync(input.Id);
            if (entity == null)
                throw new ApplicationServiceException("");
            //entity.CreateOrUpdate();
            repository.Update(entity);
            await unitofWork.CommitAsync();
            return ApiResult.Ok();
        }

        [AuthenticationFilter]
        public async Task<ApiResult> DeleteOrder(OrderDeleteDto input)
        {
            var entity = await repository.GetAsync(input.Id);
            if (entity == null)
                throw new ApplicationServiceException("");
            repository.Delete(entity);
            await unitofWork.CommitAsync();
            return ApiResult.Ok();
        }
        async Task<List<OrderGoodsSnapshot>> GetGoodsListByIds(IEnumerable<Guid> input)
        {
            return (await goodsQueryService.GetGoodsListByIds(new GetGoodsListByIdsDto(input))).GetData<List<OrderGoodsSnapshot>>();
        }
        async Task<bool> DeductionGoodsStock(CreateOrderDeductionGoodsStockDto input)
        {
            var data = input.CopyTo<CreateOrderDeductionGoodsStockDto, DeductionStockDto>();
            data.ActorId = input.GoodsId.ToString();
            return (await goodsActorService.DeductionGoodsStock(data)).GetData<bool>();
        }
        async Task<bool> UnDeductionGoodsStock(CreateOrderDeductionGoodsStockDto input)
        {
            var data = input.CopyTo<CreateOrderDeductionGoodsStockDto, DeductionStockDto>();
            data.ActorId = input.GoodsId.ToString();
            return (await goodsActorService.UnDeductionGoodsStock(data)).GetData<bool>();
        }
    }
}
