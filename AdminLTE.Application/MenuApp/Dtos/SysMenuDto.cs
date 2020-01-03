using AdminLTE.Domain.Entities;
using System.Collections.Generic;

namespace AdminLTE.AppService.MenuApp.Dtos
{
    public class SysMenuDto:Menu
    {
        public List<SysMenuDto> Children { get; set; }
    }
}
