using Oxygen.Client.ServerSymbol;
using IApplicationService.Base.AppQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.LimitedTimeActivitieService
{
    [RemoteService("goodsservice", "activitiquery", "��ʱ�����")]
    public interface ILimitedTimeActivitieQueryService
    {
        [RemoteFunc(funcDescription: "��ȡ��ʱ��б�")]
        Task<ApiResult> GetLimitedTimeActivitieList(PageQueryInputBase input);
    }
}
