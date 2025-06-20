﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Todo.Domain.Entities.Identity
{
    public class Appuser : IdentityUser
    {
        public string DisplayName { get; set; }

        public Adress Adress { get; set; }

        public ICollection<TodoItem> TodoItems { get; set; }

    }
}
