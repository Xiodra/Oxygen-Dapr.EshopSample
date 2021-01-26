using IApplicationService.GoodsService.Dtos.Input;
using Oxygen.Client.ServerSymbol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.GoodsService
{
    [RemoteService("goodsservice", "goodsusecase", "��Ʒ����")]
    public interface GoodsUseCaseService
    {
        [RemoteFunc(funcDescription: "������Ʒ")]
        Task<ApiResult> CreateGoods(GoodsCreateDto input);

        [RemoteFunc(funcDescription: "������Ʒ������Ϣ")]
        Task<ApiResult> UpdateGoods(GoodsUpdateDto input);

        [RemoteFunc(funcDescription: "ɾ����Ʒ")]
        Task<ApiResult> DeleteGoods(GoodsDeleteDto input);

        [RemoteFunc(funcDescription: "���¼���Ʒ")]
        Task<ApiResult> UpOrDownShelfGoods(UpOrDownShelfGoodsDto input);
    }

}
