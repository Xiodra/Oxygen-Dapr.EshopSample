using IApplicationService.Base.AppQuery;
using Oxygen.Client.ServerSymbol;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.RoleService
{
    [RemoteService("accountservice", "rolequery", "��ɫ����")]
    public interface RoleQueryService
    {
        [RemoteFunc(funcDescription: "��ȡ��ɫ�б�")]
        Task<ApiResult> GetRoleList(PageQueryInputBase input);

        [RemoteFunc(funcDescription: "��ȡ���н�ɫ")]
        Task<ApiResult> GetAllRoles();
    }
}
