using GestProject.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestProject.WebService.Dto
{
    public class ColumnDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AccessibleBy { get; set; }
        public int ProjectId { get; set; }
        public int Position { get; set; }
        public IEnumerable<TicketDto> Tickets { get; set; }
    }
}
