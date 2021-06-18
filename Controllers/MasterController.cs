using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GestProject.WebService.Controllers
{
    public abstract class MasterController : ControllerBase
    {
        public int UserId 
        { 
            get {
                return int.Parse(User.FindFirst(ClaimTypes.PrimarySid).Value);
            } 
        }
    }
}
