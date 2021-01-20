using IApplicationService.Base.AppQuery;
using Oxygen.Client.ServerSymbol;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.PermissionService
{
    [RemoteService("accountservice", "permissionquery", "Ȩ�޷���")]
    public interface PermissionQueryService
    {
        [RemoteFunc(funcDescription: "��ȡ��ʼ��Ȩ�޽ӿ�")]
        Task<ApiResult> GetInitPermissionApilist();

        [RemoteFunc(funcDescription: "��ȡȨ���б�")]
        Task<ApiResult> GetPermissionList(PageQueryInputBase input);

        [RemoteFunc(funcDescription: "��ȡ����Ȩ��")]
        Task<ApiResult> GetAllPermissions();

        [RemoteFunc(funcDescription: "��ȡ�û�·��")]
        Task<ApiResult> GetUserRouter();
    }
}
