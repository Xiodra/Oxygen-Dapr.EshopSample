using Autofac;
using InfrastructureBase;
using InfrastructureBase.AuthBase;
using InfrastructureBase.Http;
using InfrastructureBase.Object;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Http
{
    public class LimitedTimeActivitieAuthenticationHandler : AuthenticationManager
    {
        public static new void RegisterAllFilter()
        {
            AuthenticationManager.RegisterAllFilter();
        }
        public override async Task AuthenticationCheck(string routePath)
        {
            if (AuthenticationMethods.Any(x => x.Equals(routePath)))
            {
                var accountInfo = await HttpContextExt.Current.RequestService.Resolve<IServiceProxyFactory>().CreateProxy<IApplicationService.AccountService.QueryService>().GetAccountInfo();
                if (accountInfo.Code != 0)
                    throw new InfrastructureException(accountInfo.Message);
                HttpContextExt.SetUser(accountInfo.GetData<CurrentUser>());
                if (!HttpContextExt.Current.User.IgnorePermission && !HttpContextExt.Current.GetAuthIgnore() && !HttpContextExt.Current.User.Permissions.Contains(routePath))
                    throw new InfrastructureException("��ǰ��¼�û�ȱ��ʹ�øýӿڵı�ҪȨ��,������!");
            }
        }
    }
}
