using Oxygen.Client.ServerSymbol;
using IApplicationService.TradeService.Dtos.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.TradeService
{
    [RemoteService("tradeservice", "logisticsusecase", "��������")]
    public interface ILogisticsUseCaseService
    {
        [RemoteFunc(funcDescription: "����")]
        Task<ApiResult> Deliver(LogisticsDeliverDto input);
		
        [RemoteFunc(funcDescription: "ȷ���ջ�")]
        Task<ApiResult> Receive(LogisticsReceiveDto input);
    }
}
