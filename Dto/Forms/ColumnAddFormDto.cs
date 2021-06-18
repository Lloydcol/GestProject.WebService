using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestProject.WebService.Dto.Forms
{
    public class ColumnAddFormDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProjectId { get; set; }
    }
}
