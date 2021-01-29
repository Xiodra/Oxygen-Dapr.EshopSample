using Oxygen.Client.ServerSymbol;
using IApplicationService.Base.AppQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.TradeService
{
    [RemoteService("tradeservice", "orderquery", "��������")]
    public interface IOrderQueryService
    {
        [RemoteFunc(funcDescription: "��ȡ�����б�")]
        Task<ApiResult> GetOrderList(PageQueryInputBase input);
    }
}
