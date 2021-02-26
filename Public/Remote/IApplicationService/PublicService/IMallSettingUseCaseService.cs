using Oxygen.Client.ServerSymbol;
using IApplicationService.PublicService.Dtos.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.PublicService
{
    [RemoteService("publicservice", "mallsettingusecase", "��������")]
    public interface IMallSettingUseCaseService
    {
        [RemoteFunc(funcDescription: "����������̳�����")]
        Task<ApiResult> CreateOrUpdateMallSetting(CreateOrUpdateMallSettingDto input);
    }
}
