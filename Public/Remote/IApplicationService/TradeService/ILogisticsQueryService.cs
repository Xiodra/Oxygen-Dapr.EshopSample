using Oxygen.Client.ServerSymbol;
using IApplicationService.Base.AppQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.TradeService
{
    [RemoteService("tradeservice", "logisticsquery", "��������")]
    public interface ILogisticsQueryService
    {
        [RemoteFunc(funcDescription: "��ȡ������Ϣ")]
        Task<ApiResult> GetLogisticsList(PageQueryInputBase input);
    }
}
