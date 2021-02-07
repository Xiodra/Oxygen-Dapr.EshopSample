using Domain.Dtos;
using Domain.Enums;
using Domain.ValueObject;
using DomainBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [NotMapped]
        public IEnumerable<OrderItem> OrderItems { get; set; }
        //����״̬
        public OrderState OrderState { get; set; }
        //�µ���
        public Guid UserId { get; set; }
        //�µ�ʱ��
        public DateTime CreateTime { get; set; }
        
        public OrderConsigneeInfo ConsigneeInfo { get; set; }
        //��������
        public void CreateOrder(Guid userId, string consigneeName, string consigneeAddress, string consigneeTel, IEnumerable<OrderItem> orderItems)
        {
            if (string.IsNullOrEmpty(consigneeName) || string.IsNullOrEmpty(consigneeAddress) || string.IsNullOrEmpty(consigneeTel))
                throw new DomainException("�ռ�����Ϣȱʧ,�벹ȫ�ռ�����Ϣ�ٽ����µ�����!");
            ConsigneeInfo = new OrderConsigneeInfo()
            {
                Name = consigneeName,
                Address = consigneeAddress,
                Tel = consigneeTel
            };

            if (!orderItems.Any())
                throw new DomainException("������ϸ����Ϊ��!");
            OrderNo = CreateOrderNo();
            TotalPrice = orderItems.Sum(x => x.TotalPrice);
            OrderState = OrderState.Create;
            UserId = userId;
            OrderItems = orderItems;
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
        /// <summary>
        /// ȡ����ǰ����
        /// </summary>
        public void CancelOrder()
        {
            if (OrderState == OrderState.Create)
                OrderState = OrderState.Cancel;
            else
                throw new DomainException("����ȡ���ɹ�!");
        }
    }
}
