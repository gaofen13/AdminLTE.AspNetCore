using AdminLTE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminLTE.AppService.MenuApp.Dtos
{
    public class SysMenuDto:Menu
    {
        public List<SysMenuDto> Children { get; set; }
    }
}
