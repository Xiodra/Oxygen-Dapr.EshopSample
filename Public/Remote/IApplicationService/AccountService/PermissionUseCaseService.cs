using IApplicationService.AccountService.Dtos.Input;
using Oxygen.Client.ServerSymbol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.PermissionService
{
    [RemoteService("accountservice", "permissionusecase", "Ȩ�޷���")]
    public interface PermissionUseCaseService
    {
        [RemoteFunc(funcDescription:"��������Ȩ��")]
        Task<ApiResult> SavePermissions(List<CreatePermissionDto> input);
    }
}
