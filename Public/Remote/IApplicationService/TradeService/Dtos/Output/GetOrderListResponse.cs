using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.TradeService.Dtos.Output
{
    public class GetOrderListResponse
    {
        public Guid OrderId { get; set; }
        //������
        public string OrderNo { get; set; }
        //����״̬
        public int OrderState { get; set; }
        //�����ܼ�
        public decimal TotalPrice { get; set; }
        //������ϸ
        public IEnumerable<GetOrderListItemResponse> OrderItems { get; set; }
        //�µ���
        public string UserName { get; set; }
        //�µ�ʱ��
        public DateTime CreateTime { get; set; }
    }
    public class GetOrderListItemResponse
    {
        //��Ʒ��
        public string GoodsName { get; set; }
        //��Ʒ�۸�
        public decimal Price { get; set; }
        //��Ʒ����
        public int Count { get; set; }
    }
}
