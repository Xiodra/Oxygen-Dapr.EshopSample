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
