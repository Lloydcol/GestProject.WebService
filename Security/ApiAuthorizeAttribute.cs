using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestProject.WebService.Security
{
    public class ApiAuthorizeAttribute: Attribute, IAuthorizationFilter
    {
        private string[] _roles;

        public ApiAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool flag = false;
            foreach (string role in _roles)
            {
                if (context.HttpContext.User.IsInRole(role))
                {
                    flag = true;
                }
            }
            if (!flag)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
