using GestProject.EF;
using GestProject.EF.Entities;
using GestProject.WebService.Dto;
using GestProject.WebService.Dto.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolBox.AutoMapper;

namespace GestProject.WebService.Services
{
    public class ProjectService
    {
        private readonly GestProjectContext _context;

        public ProjectService(GestProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<ProjectDto> GetAllProject()
        {
            return _context.Projects
                .Include(p => p.Columns)
                .ThenInclude(c => c.Tickets)
                .ToList()// pour sortir de la requête
                .Select(p => {
                    ProjectDto result = p.MapTo<ProjectDto>();
                    result.Columns = p.Columns.Select(c =>
                    {
                        ColumnDto rc = c.MapTo<ColumnDto>();
                        rc.Tickets = c.Tickets.Select(t => t.MapTo<TicketDto>());
                        return rc;
                    });
                    return result;
                });
        }

        public IEnumerable<ProjectDto> GetAllProjectByUser(int userId)
        {
            return _context.WorkOn.Include(w => w.Project)
                .Where(w => w.UserId == userId)
                .Select(w => w.Project.MapTo<ProjectDto>());
        }

        public ProjectDto FindById(int id)
        {
            Project p = _context.Projects
                .Include(p => p.Columns)
                .FirstOrDefault(p => p.Id == id);

            if (p is null) return null;

            return new ProjectDto
            {
                Id = p.Id,
                Title = p.Title,
                CreatedAt = p.CreatedAt,
                Description = p.Description,
                Columns = p.Columns.Select(c => new ColumnDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    AccessibleBy = c.AccessibleBy,
                    ProjectId = c.ProjectId,
                    Position = c.Position
                })
            };
        }

        public void AddProject(ProjectAddFormDto form, int userId)
        {
            Project entity = _context.Add(new Project
            {
                Title = form.Title,
                CreatedAt = DateTime.UtcNow,
                Description = form.Description
            }).Entity;
            _context.SaveChanges();
            _context.WorkOn.Add(new WorkOn
            {
                ProjectId = entity.Id,
                UserId = userId,
                UserRole = "ADMIN"
            });
            _context.SaveChanges();
        }


        public void UpdateProject(ProjectUpdateFormDto form)
        {
            Project p = _context.Projects.Find(form.Id);
            p.Title = form.Title;
            p.Description = form.Description;
            _context.SaveChanges();
        }

        public void DeleteProject(int id)
        {
            Project p = _context.Projects.Find(id);
            _context.Remove(p);
            _context.SaveChanges();
        }
    }
}
