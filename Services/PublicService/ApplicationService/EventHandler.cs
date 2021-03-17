using Domain.Entities;
using Domain.Repository;
using IApplicationService.AppEvent;
using IApplicationService.PublicService.Dtos.Event;
using Infrastructure.EfDataAccess;
using InfrastructureBase.Object;
using Microsoft.Extensions.Logging;
using Oxygen.Client.ServerProxyFactory.Interface;
using Oxygen.Client.ServerSymbol.Events;
using System;
using System.Threading.Tasks;

namespace ApplicationService
{
    public class EventHandler : IEventHandler
    {
        private readonly IUnitofWork unitofWork;
        private readonly IEventBus eventBus;
        private readonly IStateManager stateManager;
        private readonly ILogger<EventHandler> logger;
        private readonly IEventHandleErrorInfoRepository infoRepository;
        private readonly IMallSettingRepository mallsettingRepository;
        public EventHandler(IEventBus eventBus, IStateManager stateManager, IUnitofWork unitofWork, ILogger<EventHandler> logger, IEventHandleErrorInfoRepository infoRepository, IMallSettingRepository mallsettingRepository)
        {
            this.logger = logger;
            this.unitofWork = unitofWork;
            this.eventBus = eventBus;
            this.stateManager = stateManager;
            this.infoRepository = infoRepository;
            this.mallsettingRepository = mallsettingRepository;
        }
        [EventHandlerFunc(EventTopicDictionary.Common.EventHandleErrCatch)]
        public async Task<DefaultEventHandlerResponse> EventHandleErrCatch(EventHandleRequest<EventHandlerErrDto> input)
        {
            try
            {
                var entity = input.GetData().CopyTo<EventHandlerErrDto, EventHandleErrorInfo>();
                infoRepository.Add(entity);
                await unitofWork.CommitAsync();
            }
            catch (Exception e)
            {
                logger.LogError($"�¼��������쳣����־û�ʧ��,�쳣��Ϣ:{e.Message}");
            }
            return DefaultEventHandlerResponse.Default();
        }
        [EventHandlerFunc(EventTopicDictionary.Account.InitTestUserSuccess)]
        public async Task<DefaultEventHandlerResponse> EventHandleSetDefMallSetting(EventHandleRequest<string> input)
        {
            var entity = new MallSetting();
            entity.CreateOrUpdate("��Ʒ�㷻", "����ר��/38�����ʹ�", "http://static.galileo.xiaojukeji.com/static/tms/seller_avatar_256px.jpg", "��Ʒ�㷻��������ϵ��ط�Դ���й�ǧ��ŷ������ں��ִ��������գ���������⿴�ʦ�������������з������ش���Ȼ��0��ӵ�����Ʒ�������������������չ�����Ϊ���������Ʒ�ơ���2008����˻��2013��԰����ָ�����������̡�", "���϶�", "�����к�����̫ƽ·13����Ʒ�㷻");
            mallsettingRepository.Add(entity);
            await unitofWork.CommitAsync();
            return DefaultEventHandlerResponse.Default();
        }
    }
}
