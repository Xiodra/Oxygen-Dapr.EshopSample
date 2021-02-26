using System;
using DomainBase;

namespace Domain.Entities
{
    /// <summary>
    /// ����ʵ��
    /// </summary>
    public class MallSetting : Entity, IAggregateRoot
    {
        /// <summary>
        /// �ļ�������
        /// </summary>
        public string DeliverName { get; set; }
        /// <summary>
        /// �ļ��˵�ַ
        /// </summary>
        public string DeliverAddress { get; set; }
        public void CreateOrUpdate(string deliverName, string deliverAddress)
        {
            this.DeliverName = deliverName;
            this.DeliverAddress = deliverAddress;
        }
    }
}
