﻿using AdminLTE.AppService.MenuApp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.AppService.RoleApp.Dtos
{
    public class RoleMenuDto
    {
        public Guid RoleId { get; set; }
        public RoleDto Role { get; set; }

        public Guid MenuId { get; set; }
        public MenuDto Menu { get; set; }
    }
}
