using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.LimitedTimeActivitieService.Dtos.Input
{
    public class LimitedTimeActivitieCreateDto
    {
        /// <summary>
        /// ���
        /// </summary>
        [Required(ErrorMessage = "���������д")]
        [MaxLength(40, ErrorMessage = "����Ʊ�����40������")]
        public string ActivitieName { get; set; }
        /// <summary>
        /// ������Ʒ
        /// </summary>
        public Guid GoodsId { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        [Required(ErrorMessage = "�����۸������д")]
        [Range(0, double.MaxValue, ErrorMessage = "�����۸�������0")]
        public decimal ActivitiePrice { get; set; }
        /// <summary>
        /// ���ʼʱ��
        /// </summary>
        [Required(ErrorMessage = "���ʼʱ�������д")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// �����ʱ��
        /// </summary>
        [Required(ErrorMessage = "�����ʱ�������д")]
        public DateTime EndTime { get; set; }
    }
}
