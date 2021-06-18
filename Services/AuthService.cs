using GestProject.EF;
using GestProject.EF.Entities;
using GestProject.WebService.Dto;
using GestProject.WebService.Dto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GestProject.WebService.Services
{
    public class AuthService
    {
        private readonly GestProjectContext _context;

        public AuthService(GestProjectContext context)
        {
            _context = context;
        }
        public void Register(RegisterFormDto dto)
        {
            Guid salt = Guid.NewGuid();
            _context.Add(new User
            {
                Email = dto.Email,
                Role = "ADMIN",
                Salt = salt,
                Passwd = HashPassword(dto.PlainPassword, salt)
            });
            _context.SaveChanges();
        }

        public PayloadDto Login(LoginFormDto form)
        {
            User u = _context.Users.FirstOrDefault(u => u.Email == form.Email);
            if (u is null) return null;

            byte[] hash = HashPassword(form.PlainPassword, u.Salt);

            if (Encoding.UTF8.GetString(u.Passwd) == Encoding.UTF8.GetString(hash))
            {
                return new PayloadDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    Role = u.Role
                };
            }
            return null;
        }

        private byte[] HashPassword(string plainPassword, Guid salt)
        {
            HashAlgorithm algo = new SHA512CryptoServiceProvider();
            return algo.ComputeHash(Encoding.UTF8.GetBytes(plainPassword + salt.ToString()));
        }
    }
}
