using IApplicationService.Base.AppQuery;
using IApplicationService.GoodsService.Dtos.Input;
using Oxygen.Client.ServerSymbol;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.GoodsService
{
    [RemoteService("goodsservice", "goodsquery","��Ʒ����")]
    public interface IGoodsQueryService
    {
        [RemoteFunc(funcDescription: "��ȡ��Ʒ�б�")]
        Task<ApiResult> GetGoodsList(PageQueryInputBase input);
        [RemoteFunc(funcDescription: "��ȡ��Ʒ�б�")]
        Task<ApiResult> GetGoodsListByIds(GetGoodsListByIdsDto input);

        [RemoteFunc(funcDescription: "������Ʒ�б�")]
        Task<ApiResult> GetGoodslistByGoodsName(GetGoodslistByGoodsNameDto input);
    }
}
