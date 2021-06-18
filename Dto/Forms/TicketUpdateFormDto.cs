using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestProject.WebService.Dto.Forms
{
    public class TicketUpdateFormDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
        public string Color { get; set; }
        public int Priority { get; set; }
    }
}
