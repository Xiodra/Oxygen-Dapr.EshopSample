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
    [RemoteService("tradeservice", "orderusecase", "��������")]
    public interface IOrderUseCaseService
    {
        [RemoteFunc(funcDescription: "��������")]
        Task<ApiResult> CreateOrder(OrderCreateDto input);
    }
}
