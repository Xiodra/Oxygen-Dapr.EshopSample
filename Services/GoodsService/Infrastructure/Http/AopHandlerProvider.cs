using DomainBase;
using IApplicationService;
using InfrastructureBase;
using InfrastructureBase.Http;
using InfrastructureBase.Object;
using Oxygen.Common.Implements;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Http
{
    public class AopHandlerProvider
    {
        public static void ContextHandler(OxygenHttpContextWapper oxygenHttpContext)
        {
            HttpContextExt.SetCurrent(oxygenHttpContext);//ע��http�����ĸ�����ҵ�������Ķ���
        }
        public static async Task BeforeSendHandler(object param, OxygenHttpContextWapper oxygenHttpContext)
        {
            await new GoodsAuthenticationHandler().AuthenticationCheck(HttpContextExt.Current.RoutePath);//��ȨУ��
            //����ǰ�����������У��
            if (param != null)
                CustomModelValidator.Valid(param);
            oxygenHttpContext.Headers.Add("AuthIgnore", "true");
            await Task.CompletedTask;
        }
        public static async Task AfterMethodInvkeHandler(object result)
        {
            await Task.CompletedTask;
        }

        public static async Task<object> ExceptionHandler(Exception exception)
        {
            //�쳣����
            if (exception is ApplicationServiceException || exception is DomainException || exception is InfrastructureException)
            {
                return await Task.FromResult(ApiResult.Err(exception.Message));
            }
            else
            {
                Console.WriteLine("ϵͳ�쳣��" + exception.Message);
                return await Task.FromResult(ApiResult.Err());
            }
        }
    }
}
