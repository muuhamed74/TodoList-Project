using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Todo.Domain.Entities.Identity;

namespace Todo.Domain.Services
{
    public interface ITokenService
    {
        
            Task<string> CreateTokenAsync(Appuser user , UserManager<Appuser> userManager);
        
    }
}

