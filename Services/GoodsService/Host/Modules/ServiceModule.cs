using ApplicationService;
using Domain;
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using System.Linq;
using System.Runtime.Loader;
using InfrastructureBase;

namespace Host.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Common.GetProjectAssembliesArray())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            //�¼���������Ҫ����ע����Ϊ��ӿ���ͬ
            Common.RegisterAllEventHandlerInAutofac(Common.GetProjectAssembliesArray(), builder);
        }
    }
}
