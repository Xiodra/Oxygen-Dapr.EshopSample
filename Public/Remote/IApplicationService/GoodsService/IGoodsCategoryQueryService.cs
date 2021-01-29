using IApplicationService.Base.AppQuery;
using Oxygen.Client.ServerSymbol;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.GoodsCategoryService
{
    [RemoteService("goodsservice", "categoryquery","��Ʒ�������")]
    public interface IGoodsCategoryQueryService
    {

        [RemoteFunc(funcDescription: "��ȡ��Ʒ�����б�")]
        Task<ApiResult> GetCategoryList(PageQueryInputBase input);
        [RemoteFunc(funcDescription: "��ȡȫ����Ʒ����")]
        Task<ApiResult> GetAllCategoryList();
    }
}
