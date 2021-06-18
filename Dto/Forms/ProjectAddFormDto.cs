using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestProject.WebService.Dto.Forms
{
    public class ProjectAddFormDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
    }
}
