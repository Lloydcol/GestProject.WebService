using GestProject.EF;
using GestProject.EF.Entities;
using GestProject.WebService.Dto;
using GestProject.WebService.Dto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestProject.WebService.Services
{
    public class CommentService
    {
        private readonly GestProjectContext _context;

        public CommentService(GestProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<CommentDto> GetAllByTicket(int ticketId)
        {
            return _context.Comments.Where(c => c.Ticket.Id == ticketId)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    CreatedAt = c.CreatedAt,
                    Text = c.Text,
                    TicketId = c.TicketId,
                });
        }

        public CommentDto GetById(int id)
        {
            Comment c = _context.Comments.Find(id);
            if (c is null) return null;
            return new CommentDto
            {
                Id = c.Id,
                CreatedAt = c.CreatedAt,
                Text = c.Text,
                TicketId = c.TicketId
            };
        }

        public void Add(CommentAddFormDto form)
        {
            _context.Add(new Comment
            {
                Id = form.Id,
                Text = form.Text,
                CreatedAt = DateTime.UtcNow,
                TicketId = form.ColumnId
            });
            _context.SaveChanges();
        }

        public void Update(CommentUpdateFormDto form)
        {
            Comment c = _context.Comments.Find(form.Id);

            c.Id = form.Id;
            c.Text = form.Text;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Comment c = _context.Comments.Find(id);
            _context.Remove(c);
            _context.SaveChanges();
        }


        // -----------------------------------------------

        public CommentUpdateFormDto GetByIdOnlyName(int id)
        {
            Comment c = _context.Comments.Find(id);
            if (c is null) return null;
            return new CommentUpdateFormDto
            {
                Id = c.Id,
                Text = c.Text,
            };
        }
    }
}
