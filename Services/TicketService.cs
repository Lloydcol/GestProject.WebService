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
    public class TicketService
    {
        private readonly GestProjectContext _context;

        public TicketService(GestProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<TicketDto> GetAllByColumn(int columnId)
        {
            return _context.Tickets.Where(t => t.Column.Id == columnId)
                .Select(t => new TicketDto
                {
                    Id = t.Id,
                    CreatedAt = t.CreatedAt,
                    Title = t.Title,
                    Description = t.Description,
                    Color = t.Color,
                    State = t.State,
                    Priority = t.Priority,
                    ColumnId = t.ColumnId,
                    Comments = t.Comments.Select(c => new CommentDto
                    {
                        Id = c.Id,
                        CreatedAt = c.CreatedAt,
                        Text = c.Text,
                        TicketId = c.TicketId
                    })
                });
        }

        public TicketDto GetById(int id)
        {
            Ticket t = _context.Tickets.Find(id);
            if (t is null) return null;
            return new TicketDto
            {
                Id = t.Id,
                CreatedAt = t.CreatedAt,
                Title = t.Title,
                Description = t.Description,
                Color = t.Color,
                State = t.State,
                Priority = t.Priority,
                ColumnId = t.ColumnId,
                Comments = t.Comments.Select(c => new CommentDto
                {
                    Id = c.Id,
                    CreatedAt = c.CreatedAt,
                    Text = c.Text,
                    TicketId = c.TicketId
                })
            };
        }

        public void Add(TicketAddFormDto form)
        {
            _context.Add(new Ticket
            {
                Id = form.Id,
                Title = form.Title,
                CreatedAt = DateTime.UtcNow,
                ColumnId = form.ColumnId
            });
            _context.SaveChanges();
        }

        public void Update(TicketUpdateFormDto form)
        {
            Ticket t = _context.Tickets.Find(form.Id);

            t.Id = form.Id;
            t.Title = form.Title;
            t.Description = form.Description;
            t.State = form.State;
            t.Color = form.Color;
            t.Priority = form.Priority;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Ticket t = _context.Tickets.Find(id);
            _context.Remove(t);
            _context.SaveChanges();
        }
    }
}
