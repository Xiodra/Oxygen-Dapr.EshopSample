using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.PublicService.Dtos.Input
{
    public class CreateOrUpdateMallSettingDto
    {
        /// <summary>
        /// ������
        /// </summary>
        [Required(ErrorMessage = "������������")]
        public string ShopName { get; set; }
        /// <summary>
        /// ����һ�仰����
        /// </summary>
        [Required(ErrorMessage = "����������һ�仰����")]
        public string ShopDescription { get; set; }
        /// <summary>
        /// ����ͼ��
        /// </summary>
        [Required(ErrorMessage = "���ϴ�����ͼ��")]
        public string ShopIconUrl { get; set; }
        /// <summary>
        /// ͨ�ù���
        /// </summary>
        [Required(ErrorMessage = "������ͨ�ù���")]
        public string Notice { get; set; }
        /// <summary>
        /// �ļ�������
        /// </summary>
        [Required(ErrorMessage = "������ļ�������")]
        public string DeliverName { get; set; }
        /// <summary>
        /// �ļ��˵�ַ
        /// </summary>
        [Required(ErrorMessage = "������ļ��˵�ַ")]
        public string DeliverAddress { get; set; }
    }
}
