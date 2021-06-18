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
    public class ColumnService
    {
        private readonly GestProjectContext _context;

        public ColumnService(GestProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<ColumnDto> GetAllByProject(int projectId)
        {
            return _context.Columns.Where(c => c.Project.Id == projectId)
                .Select(c => new ColumnDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    AccessibleBy = c.AccessibleBy,
                    ProjectId = c.ProjectId,
                    Position = c.Position,
                    Tickets = c.Tickets.Select(t => new TicketDto
                    {
                        Id = t.Id,
                        CreatedAt = t.CreatedAt,
                        Title = t.Title,
                        Description = t.Description,
                        State = t.State,
                        Color = t.Color,
                        ColumnId = t.ColumnId,
                        Priority = t.Priority
                    })
                });
        }

        public ColumnDto GetById(int id)  //!\\
        {
            Column c = _context.Columns.Find(id);
            if (c is null) return null;
            return new ColumnDto
            {
                Id = c.Id,
                Title = c.Title,
                AccessibleBy = c.AccessibleBy,
                ProjectId = c.ProjectId,
                Position = c.Position,
                Tickets = c.Tickets.Select(t => new TicketDto
                {
                    Id = t.Id,
                    CreatedAt = t.CreatedAt,
                    Title = t.Title,
                    Description = t.Description,
                    State = t.State,
                    Color = t.Color,
                    ColumnId = t.ColumnId,
                    Priority = t.Priority
                })
            };
        }

        public void Add(ColumnAddFormDto form)
        {
            _context.Add(new Column
            {
                Id = form.Id,
                Title = form.Title,
                ProjectId = form.ProjectId
            });
            _context.SaveChanges();
        }

        public void Update(ColumnUpdateFormDto form)
        {
            Column c = _context.Columns.Find(form.Id);

            c.Title = form.Title;
            c.AccessibleBy = form.AccessibleBy;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Column c = _context.Columns.Find(id);
            _context.Remove(c);
            _context.SaveChanges();
        }
    }
}
