using DomainBase;
using System;

namespace Domain
{
    /// <summary>
    /// ����ʵ��
    /// </summary>
    public class LimitedTimeActivitie : Entity, IAggregateRoot
    {
        /// <summary>
        /// ���
        /// </summary>
        public string ActivitieName { get; set; }
        /// <summary>
        /// ������Ʒ
        /// </summary>
        public Guid GoodsId { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public decimal ActivitiePrice { get; set; }
        /// <summary>
        /// ���ʼʱ��
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// �����ʱ��
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// ��Ƿ��ϼ�
        /// </summary>
        public bool ShelfState { get; set; }

        public void CreateOrUpdate(string activitieName, Guid goodsId, decimal activitiePrice, DateTime startTime, DateTime endTime)
        {
            if (!string.IsNullOrEmpty(activitieName))
                ActivitieName = activitieName;
            GoodsId = goodsId;
            if (activitiePrice <= 0)
                throw new DomainException("��۲���С��0");
            ActivitiePrice = activitiePrice;
            StartTime = startTime;
            if (endTime < DateTime.Now)
                throw new DomainException("�����ʱ�䲻��С�ڵ�ǰʱ��");
            if (endTime > DateTime.Now.AddMonths(1))
                throw new DomainException("�����ʱ�䲻�ܳ�����ǰʱ��һ����");
            EndTime = endTime;
            if (startTime >= endTime)
                throw new DomainException("�����ʱ�������ڿ�ʼʱ��");
        }

        public void UpOrDownShelf(bool shelfState)
        {
            ShelfState = shelfState;
        }
    }
}
