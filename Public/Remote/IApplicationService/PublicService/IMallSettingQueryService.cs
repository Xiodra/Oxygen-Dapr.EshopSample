using Oxygen.Client.ServerSymbol;
using IApplicationService.Base.AppQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.PublicService
{
    [RemoteService("publicservice", "mallsettingquery", "��������")]
    public interface IMallSettingQueryService
    {
        [RemoteFunc(funcDescription: "��ȡ�̳�����")]
        Task<ApiResult> GetMallSetting();
    }
}
