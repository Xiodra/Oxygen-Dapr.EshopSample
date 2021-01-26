﻿using Domain;
using Domain.Repository;
using IApplicationService;
using IApplicationService.%placeholder%Service.Dtos.Output;
using IApplicationService.%placeholder%Service.Dtos.Input;
using Infrastructure.EfDataAccess;
using InfrastructureBase.AuthBase;
using InfrastructureBase.Object;
using Oxygen.Client.ServerProxyFactory.Interface;
using System.Threading.Tasks;
using IApplicationService.Base.AppQuery;
using System.Linq;
using InfrastructureBase.Data;

namespace ApplicationService
{
    public class %placeholder%QueryService : IApplicationService.%placeholder%Service.%placeholder%QueryService
    {
        private readonly EfDbContext dbContext;
        private readonly IStateManager stateManager;
        public %placeholder%QueryService(EfDbContext dbContext, IStateManager stateManager)
        {
            this.dbContext = dbContext;
            this.stateManager = stateManager;
        }
		
        [AuthenticationFilter]
        public async Task<ApiResult> Get%placeholder%List(PageQueryInputBase input)
        {
            var query = from %placeholder% in dbContext.%placeholder% select new Get%placeholder%ListResponse() { };
            var (Data, Total) = await QueryServiceHelper.PageQuery(query, input.Page, input.Limit);
            return ApiResult.Ok(new PageQueryResonseBase<Get%placeholder%ListResponse>(Data, Total));
        }
		
    }
}
