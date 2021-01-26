using IApplicationService.GoodsService.Dtos.Input;
using Oxygen.Client.ServerSymbol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.GoodsCategoryService
{
    [RemoteService("goodsservice", "categoryusecase","��Ʒ�������")]
    public interface GoodsCategoryUseCaseService
    {
        [RemoteFunc(funcDescription: "������Ʒ����")]
        Task<ApiResult> CreateCategory(CategoryCreateDto input);
        [RemoteFunc(funcDescription: "������Ʒ����")]
        Task<ApiResult> UpdateCategory(CategoryUpdateDto input);
        [RemoteFunc(funcDescription: "ɾ����Ʒ����")]
        Task<ApiResult> DeleteCategory(CategoryDeleteDto input);
    }
}
