using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IApplicationService.LimitedTimeActivitieService.Dtos.Input
{
    public class LimitedTimeActivitieDeleteDto
    {
        [Required(ErrorMessage = "��ѡ��һ���")]
        public Guid Id { get; set; }
    }
}
