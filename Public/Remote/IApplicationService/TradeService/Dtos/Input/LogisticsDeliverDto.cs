using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.TradeService.Dtos.Input
{
    public class LogisticsDeliverDto
    {
        [Required(ErrorMessage = "��ѡ�񶩵�")]
        public Guid OrderId { get; set; }
        [Required(ErrorMessage = "��ѡ����������")]
        [Range(0, 5, ErrorMessage = "��������ѡ�����")]
        public int LogisticsType { get; set; }
        [Required(ErrorMessage = "������������ִ")]
        public string LogisticsNo { get; set; }
        public DateTime? DeliveTime { get; set; }
    }
}
