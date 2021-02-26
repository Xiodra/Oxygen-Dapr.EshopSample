using Oxygen.Client.ServerSymbol;
using IApplicationService.Base.AppQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.PublicService
{
    [RemoteService("publicservice", "eventhandleerrorinfoquery", "��������")]
    public interface IEventHandleErrorInfoQueryService
    {
        [RemoteFunc(funcDescription: "��ȡ�б�")]
        Task<ApiResult> GetEventHandleErrorInfoList(PageQueryInputBase input);
    }
}
