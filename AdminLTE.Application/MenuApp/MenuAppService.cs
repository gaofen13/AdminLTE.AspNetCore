using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminLTE.AppService.MenuApp.Dtos;
using AdminLTE.Domain.IRepositories;
using AdminLTE.Domain.Entities;
using AutoMapper;

namespace AdminLTE.AppService.MenuApp
{
    public class MenuAppService : IMenuAppService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public MenuAppService(IMenuRepository menuRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _menuRepository = menuRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public List<MenuDto> GetAllList()
        {
            var menus = _menuRepository.GetAllList().OrderBy(it => it.SerialNumber);
            //使用AutoMapper进行实体转换
            return Mapper.Map<List<MenuDto>>(menus);
        }

        public List<MenuDto> GetMenusByParent(Guid parentId, int startPage, int pageSize, out int rowCount)
        {
            var menus = _menuRepository.LoadPageList(startPage, pageSize, out rowCount, it => it.ParentId == parentId, it => it.SerialNumber);
            return Mapper.Map<List<MenuDto>>(menus);
        }

        public bool InsertOrUpdate(MenuDto dto)
        {
            var menu = _menuRepository.InsertOrUpdate(Mapper.Map<Menu>(dto));
            return menu != null;
        }

        public void DeleteBatch(List<Guid> ids)
        {
            _menuRepository.Delete(it => ids.Contains(it.Id));
        }

        public void Delete(Guid id)
        {
            _menuRepository.Delete(id);
        }

        public MenuDto Get(Guid id)
        {
            return Mapper.Map<MenuDto>(_menuRepository.Get(id));
        }
        /// <summary>
        /// 根据用户获取功能菜单
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public List<MenuDto> GetMenusByUser(Guid userId)
        {
            List<MenuDto> result = new List<MenuDto>();
            var allMenus = _menuRepository.GetAllList(it=>it.Type == 0).OrderBy(it => it.SerialNumber);
            var currentUser = _userRepository.Get(userId);
            if ("超级管理员".Equals(currentUser.Name)) //超级管理员
                return Mapper.Map<List<MenuDto>>(allMenus);
            var user = _userRepository.GetWithRoles(userId);
            if (user == null)
                return result;
            var userRoles = user.UserRoles;
            List<Guid> menuIds = new List<Guid>();
            foreach (var role in userRoles)
            {
                menuIds = menuIds.Union(_roleRepository.GetAllMenuListByRole(role.RoleId)).ToList();
            }
            allMenus = allMenus.Where(it => menuIds.Contains(it.Id)).OrderBy(it => it.SerialNumber);
            return Mapper.Map<List<MenuDto>>(allMenus);
        }
        public List<SysMenuDto> GetSysMenusByParent(Guid parentId)
        {
            List<SysMenuDto> result = new List<SysMenuDto>();
            var menus = _menuRepository.GetAllList(it => it.ParentId == parentId).OrderBy(it => it.SerialNumber);
            foreach (var menu in menus)
            {
                SysMenuDto sysMenu = new SysMenuDto
                {
                    Id = menu.Id,
                    ParentId = menu.ParentId,
                    SerialNumber = menu.SerialNumber,
                    Text = menu.Text,
                    Code = menu.Code,
                    Url = menu.Url,
                    Type = menu.Type,
                    Icon = menu.Icon,
                    Remarks = menu.Remarks,
                    Children = GetSysMenusByParent(menu.Id)
                };
                result.Add(sysMenu);
            }
            return result;
        }
        /// <summary>
        /// 根据用户获取功能菜单
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public List<SysMenuDto> GetSysMenusByUser(Guid userId)
        {
            List<SysMenuDto> result = new List<SysMenuDto>();
            var allMenus = GetSysMenusByParent(new Guid());
            var currentUser = _userRepository.Get(userId);
            if ("超级管理员".Equals(currentUser.Name)) //超级管理员
                return allMenus;
            var user = _userRepository.GetWithRoles(userId);
            if (user == null)
                return result;
            var userRoles = user.UserRoles;
            List<Guid> menuIds = new List<Guid>();
            foreach (var role in userRoles)
            {
                menuIds = menuIds.Union(_roleRepository.GetAllMenuListByRole(role.RoleId)).ToList();
            }
            RemoveNoPermission(allMenus, menuIds);
            return allMenus;

            void RemoveNoPermission(List<SysMenuDto> menus, List<Guid> guids)
            {
                for (int i = 0; i < menus.Count; i++)
                {
                    var theMenu = menus[i];
                    if (theMenu.Type==0 && !guids.Contains(theMenu.Id))
                    {
                        menus.RemoveAt(i);
                        i -= 1;
                    }
                    else if (theMenu.Children?.Count > 0)
                    {
                        RemoveNoPermission(theMenu.Children, guids);
                        if (theMenu.Children.Count == 0 && theMenu.Type==1)
                        {
                            menus.RemoveAt(i);
                            i -= 1;
                        }
                    }
                }
            }
        }
    }
}
