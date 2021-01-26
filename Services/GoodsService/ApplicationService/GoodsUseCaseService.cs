using Domain.Repository;
using IApplicationService;
using IApplicationService.GoodsService.Dtos.Input;
using Infrastructure.EfDataAccess;
using Oxygen.Client.ServerProxyFactory.Interface;
using System.Threading.Tasks;
using Domain;
using InfrastructureBase.AuthBase;
using Domain.Specification;
using InfrastructureBase;

namespace ApplicationService
{
    public class GoodsUseCaseService : IApplicationService.GoodsService.GoodsUseCaseService
    {
        private readonly IGoodsRepository repository;
        private readonly IGoodsCategoryRepository goodsCategoryRepository;
        private readonly IUnitofWork unitofWork;
        private readonly IEventBus eventBus;
        private readonly IStateManager stateManager;
        public GoodsUseCaseService(IGoodsRepository repository, IGoodsCategoryRepository goodsCategoryRepository, IEventBus eventBus, IStateManager stateManager, IUnitofWork unitofWork)
        {
            this.repository = repository;
            this.goodsCategoryRepository = goodsCategoryRepository;
            this.unitofWork = unitofWork;
            this.eventBus = eventBus;
            this.stateManager = stateManager;
        }
        [AuthenticationFilter]
        public async Task<ApiResult> CreateGoods(GoodsCreateDto input)
        {
            var goods = new Goods();
            goods.CreateOrUpdateGoods(input.GoodsName, input.GoodsImage, input.Price, input.CategoryId);
            goods.UpOrDownShelf(false);
            goods.ChangeStock(input.Stock);
            repository.Add(goods);
            if (await new GoodsValidityCheckSpecification(repository, goodsCategoryRepository).IsSatisfiedBy(goods))
                await unitofWork.CommitAsync();
            return ApiResult.Ok("��Ʒ�����ɹ�");
        }
        [AuthenticationFilter]
        public async Task<ApiResult> UpdateGoods(GoodsUpdateDto input)
        {
            var goods = await repository.GetAsync(input.Id);
            if (goods == null)
                throw new ApplicationServiceException("û�в�ѯ������Ʒ!");
            goods.CreateOrUpdateGoods(input.GoodsName, input.GoodsImage, input.Price, input.CategoryId);
            goods.ChangeStock(input.Stock);
            repository.Update(goods);
            if (await new GoodsValidityCheckSpecification(repository, goodsCategoryRepository).IsSatisfiedBy(goods))
                await unitofWork.CommitAsync();
            return ApiResult.Ok("��Ʒ���³ɹ�");
        }
        [AuthenticationFilter]
        public async Task<ApiResult> UpOrDownShelfGoods(UpOrDownShelfGoodsDto input)
        {
            var goods = await repository.GetAsync(input.Id);
            if (goods == null)
                throw new ApplicationServiceException("û�в�ѯ������Ʒ!");
            goods.UpOrDownShelf(input.ShelfState);
            repository.Update(goods);
            await unitofWork.CommitAsync();
            return ApiResult.Ok("��Ʒ�ϼ�/�¼ܳɹ�");
        }
        [AuthenticationFilter]
        public async Task<ApiResult> DeleteGoods(GoodsDeleteDto input)
        {
            var entity = await repository.GetAsync(input.Id);
            if (entity == null)
                throw new ApplicationServiceException("û�в�ѯ������Ʒ!");
            repository.Delete(entity);
            await unitofWork.CommitAsync();
            return ApiResult.Ok("��Ʒɾ���ɹ�");
        }
    }
}
