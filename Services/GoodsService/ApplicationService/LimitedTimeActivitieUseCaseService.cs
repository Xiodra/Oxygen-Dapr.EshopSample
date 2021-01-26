using Domain.Repository;
using Domain.Specification;
using IApplicationService;
using IApplicationService.GoodsService.Dtos.Input;
using IApplicationService.LimitedTimeActivitieService.Dtos.Input;
using Infrastructure.EfDataAccess;
using Infrastructure.PersistenceObject;
using InfrastructureBase;
using InfrastructureBase.AuthBase;
using Oxygen.Client.ServerProxyFactory.Interface;
using System.Threading.Tasks;

namespace ApplicationService
{
    public class LimitedTimeActivitieUseCaseService : IApplicationService.LimitedTimeActivitieService.LimitedTimeActivitieUseCaseService
    {
        private readonly ILimitedTimeActivitieRepository repository;
        private readonly IGoodsRepository goodsRepository;
        private readonly IUnitofWork unitofWork;
        private readonly IEventBus eventBus;
        private readonly IStateManager stateManager;
        public LimitedTimeActivitieUseCaseService(ILimitedTimeActivitieRepository repository, IGoodsRepository goodsRepository, IEventBus eventBus, IStateManager stateManager, IUnitofWork unitofWork)
        {
            this.repository = repository;
            this.goodsRepository = goodsRepository;
            this.unitofWork = unitofWork;
            this.eventBus = eventBus;
            this.stateManager = stateManager;
        }
		
        [AuthenticationFilter]
        public async Task<ApiResult> CreateLimitedTimeActivitie(LimitedTimeActivitieCreateDto input)
        {
            var entity = new LimitedTimeActivitie();
            entity.CreateOrUpdate(input.ActivitieName, input.GoodsId, input.ActivitiePrice, input.StartTime, input.EndTime);
            repository.Add(entity);
            if (await new LimitedTimeActivitieValidityCheckSpecification(goodsRepository).IsSatisfiedBy(entity))
                await unitofWork.CommitAsync();
            return ApiResult.Ok("������ɹ�");
        }
		
        [AuthenticationFilter]
        public async Task<ApiResult> UpdateLimitedTimeActivitie(LimitedTimeActivitieUpdateDto input)
        {
            var entity = await repository.GetAsync(input.Id);
            if (entity == null)
                throw new ApplicationServiceException("û���ҵ��û");
            entity.CreateOrUpdate(input.ActivitieName, input.GoodsId, input.ActivitiePrice, input.StartTime, input.EndTime);
            repository.Update(entity);
            if (await new LimitedTimeActivitieValidityCheckSpecification(goodsRepository).IsSatisfiedBy(entity))
                await unitofWork.CommitAsync();
            return ApiResult.Ok("����³ɹ�");
        }
		
        [AuthenticationFilter]
        public async Task<ApiResult> DeleteLimitedTimeActivitie(LimitedTimeActivitieDeleteDto input)
        {
            var entity = await repository.GetAsync(input.Id);
            if (entity == null)
                throw new ApplicationServiceException("û���ҵ��û");
            repository.Delete(entity);
            await unitofWork.CommitAsync();
            return ApiResult.Ok("�ɾ���ɹ�");
        }

        [AuthenticationFilter]
        public async Task<ApiResult> UpOrDownShelfActivitie(UpOrDownShelfActivitieDto input)
        {
            var entity = await repository.GetAsync(input.Id);
            if (entity == null)
                throw new ApplicationServiceException("û���ҵ��û");
            entity.UpOrDownShelf(input.ShelfState);
            repository.Update(entity);
            await unitofWork.CommitAsync();
            return ApiResult.Ok("��ϼ�/�¼ܳɹ�");
        }
    }
}
