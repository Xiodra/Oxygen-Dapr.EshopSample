using Oxygen.Client.ServerSymbol;
using IApplicationService.Base.AppQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IApplicationService.TradeService.Dtos.Input;

namespace IApplicationService.TradeService
{
    [RemoteService("tradeservice", "tradelogquery", "���׷���")]
    public interface ITradeLogQueryService
    {
        [RemoteFunc(funcDescription: "��ȡ���׼�¼")]
        Task<ApiResult> GetTradeLogListByOrderId(GetTradeLogListByOrderIdDto input);
    }
}
