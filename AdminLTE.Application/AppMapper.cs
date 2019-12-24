using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using AdminLTE.AppService.DepartmentApp.Dtos;
using AdminLTE.AppService.MenuApp.Dtos;
using AdminLTE.AppService.RoleApp.Dtos;
using AdminLTE.AppService.UserApp.Dtos;
using AdminLTE.Domain.Entities;

namespace AdminLTE.AppService
{
    /// <summary>
    /// Entity与Dto映射
    /// </summary>
    public class AppMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Menu, MenuDto>();
                cfg.CreateMap<MenuDto, Menu>();
                cfg.CreateMap<Department, DepartmentDto>();
                cfg.CreateMap<DepartmentDto, Department>();
                cfg.CreateMap<RoleDto, Role>();
                cfg.CreateMap<Role, RoleDto>();
                cfg.CreateMap<RoleMenuDto, RoleMenu>();
                cfg.CreateMap<RoleMenu, RoleMenuDto>();
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserRoleDto, UserRole>();
                cfg.CreateMap<UserRole, UserRoleDto>();
            });
        }
    }
}
