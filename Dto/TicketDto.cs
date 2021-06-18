using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestProject.WebService.Dto
{
    public class TicketDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
        public string Color { get; set; }
        public int ColumnId { get; set; }
        public int Priority { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
    }
}
