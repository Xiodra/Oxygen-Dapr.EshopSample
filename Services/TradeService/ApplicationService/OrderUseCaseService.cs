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
                var order = await createOrderService.CreateOrder(HttpContextExt.Current.User.Id, HttpContextExt.Current.User.UserName, HttpContextExt.Current.User.Address, HttpContextExt.Current.User.Tel, input.Items.CopyTo<OrderCreateDto.OrderCreateItemDto, OrderItem>().ToList());//ͨ���������񴴽�����
                repository.Add(order);
                if (await new CheckOrderCanCreateSpecification(repository).IsSatisfiedBy(order))
                    await unitofWork.CommitAsync();
                await eventBus.SendEvent(EventTopicDictionary.Order.CreateOrderSucc, new OperateOrderSuccessEvent(order, HttpContextExt.Current.User.UserName));//���Ͷ��������ɹ��¼�
            },
            //ʧ�ܻع�
            createOrderService.UnCreateOrder);
        }
        [AuthenticationFilter(false)]
        public async Task<ApiResult> OrderPay(OrderPayDto input)
        {
            var order = await repository.GetAsync(input.OrderId);
            if (order == null)
                throw new ApplicationServiceException("û���ҵ��ö���!");
            order.PayOrder(HttpContextExt.Current.User.Id);
            repository.Update(order);
            await unitofWork.CommitAsync();
            await eventBus.SendEvent(EventTopicDictionary.Order.PayOrderSucc, new OperateOrderSuccessEvent(order, HttpContextExt.Current.User.UserName));//���Ͷ���֧���ɹ��¼�
            return ApiResult.Ok();
        }

        #region ˽��Զ�̷����װ������
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
        #endregion
    }
}
