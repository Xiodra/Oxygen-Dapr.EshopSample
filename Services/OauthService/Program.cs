using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infrastructure.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OauthService.Modules;
using Oxygen.IocModule;
using Oxygen.Mesh.Dapr;
using Oxygen.ProxyGenerator.Implements;
using Oxygen.Server.Kestrel.Implements;
using System.IO;
using System.Threading.Tasks;

namespace OauthService
{
    public class Program
    {
        private static IConfiguration _configuration { get; set; }
        static async Task Main(string[] args)
        {
            await CreateDefaultHost(args).Build().RunAsync();
        }

        static IHostBuilder CreateDefaultHost(string[] args) => new HostBuilder()
                .ConfigureWebHostDefaults(webhostbuilder =>
                {
                    //ע���Ϊoxygen����ڵ�
                    webhostbuilder.StartOxygenServer<OxygenStartup>((config) =>
                    {
                        config.Port = 80;
                        config.PubSubCompentName = "pubsub";
                        config.StateStoreCompentName = "statestore";
                        config.TracingHeaders = "Authentication,AuthIgnore";
                    });
                })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json");
                    _configuration = config.Build();
                })
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    //ע��oxygen����
                    builder.RegisterOxygenModule();
                    //ע��ҵ������
                    builder.RegisterModule(new ServiceModule());
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddHttpClient();
                    //ע��ȫ��������
                    LocalMethodAopProvider.RegisterPipelineHandler(AopHandlerProvider.ContextHandler, AopHandlerProvider.BeforeSendHandler, AopHandlerProvider.AfterMethodInvkeHandler, AopHandlerProvider.ExceptionHandler);
                    services.AddLogging(configure =>
                    {
                        configure.AddConfiguration(_configuration.GetSection("Logging"));
                        configure.AddConsole();
                    });
                    services.AddAutofac();
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
