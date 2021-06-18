using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestProject.WebService.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
        public int TicketId { get; set; }
    }
}
