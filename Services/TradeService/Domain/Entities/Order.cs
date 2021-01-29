using Domain.Enums;
using DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    /// <summary>
    /// ����ʵ��
    /// </summary>
    public class Order : Entity, IAggregateRoot
    {
        //������
        public string OrderNo { get; set; }
        //�����ܼ�
        public decimal TotalPrice { get; set; }
        //��������
        public List<OrderItem> OrderItems { get; set; }
        //����״̬
        public OrderState OrderState { get; set; }
        //�µ�ʱ��
        public DateTime CreateTime { get; set; }

        //��������
        public void CreateOrder(List<OrderItem> orderItems)
        {
            if (!orderItems.Any())
                throw new DomainException("�������鲻��Ϊ��!");
            OrderNo = CreateOrderNo();
            TotalPrice = orderItems.Sum(x => x.TotalPrice);
            OrderState = OrderState.Create;
            CreateTime = DateTime.Now;
        }
        /// <summary>
        /// ���ɶ�����
        /// </summary>
        /// <returns></returns>
        string CreateOrderNo()
        {
            return $"{DateTime.Now.ToString("yyyyMMddHHmmss")}{new Random(Guid.NewGuid().GetHashCode()).Next(1000, 9999)}";
        }
        //�������״̬
        public void ChangeOrderState(OrderState orderState)
        {

        }
    }
}
