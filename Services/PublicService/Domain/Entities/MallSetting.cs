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
        /// ������
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// ����һ�仰����
        /// </summary>
        public string ShopDescription { get; set; }
        /// <summary>
        /// ����ͼ��
        /// </summary>
        public string ShopIconUrl { get; set; }
        /// <summary>
        /// ͨ�ù���
        /// </summary>
        public string Notice { get; set; }
        /// <summary>
        /// �ļ�������
        /// </summary>
        public string DeliverName { get; set; }
        /// <summary>
        /// �ļ��˵�ַ
        /// </summary>
        public string DeliverAddress { get; set; }
        public void CreateOrUpdate(string shopName, string shopDescription, string shopIconUrl, string notice, string deliverName, string deliverAddress)
        {
            this.ShopName = shopName;
            this.ShopDescription = shopDescription;
            this.ShopIconUrl = shopIconUrl;
            this.Notice = notice;
            this.DeliverName = deliverName;
            this.DeliverAddress = deliverAddress;
        }
    }
}
