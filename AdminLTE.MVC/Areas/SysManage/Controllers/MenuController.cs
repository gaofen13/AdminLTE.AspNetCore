using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdminLTE.AppService.MenuApp.Dtos;
using AdminLTE.AppService.MenuApp;
using AdminLTE.MVC.Models;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminLTE.MVC.Controllers
{
    [Area("SysManage")]
    /// <summary>
    /// 功能管理控制器
    /// </summary>
    public class MenuController : BaseController
    {
        private readonly IMenuAppService _menuAppService;
        public MenuController(IMenuAppService menuAppService, AppService.UserApp.IUserAppService userAppService)
        {
            _menuAppService = menuAppService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取功能树JSON数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetMenuTreeData()
        {
            var menus = _menuAppService.GetAllList();
            List<TreeModel> treeModels = new List<TreeModel>();
            foreach (var menu in menus)
            {
                treeModels.Add(new TreeModel() { Id = menu.Id.ToString(), Text = menu.Name, Parent = menu.ParentId == Guid.Empty ? "#" : menu.ParentId.ToString() });
            }
            return Json(treeModels);
        }
        /// <summary>
        /// 获取子级功能列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetMenusByParent(Guid parentId, int startPage, int pageSize)
        {
            var result = _menuAppService.GetMenusByParent(parentId, startPage, pageSize, out int rowCount);
            return Json(new
            {
                rowCount = rowCount,
                pageCount = Math.Ceiling(Convert.ToDecimal(rowCount) / pageSize),
                rows = result,
            });
        }
        /// <summary>
        /// 获取子级功能列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetSysMenus()
        {
            var userId = HttpContext.Session.GetString("CurrentUserId");
            var result = _menuAppService.GetSysMenusByUser(Guid.Parse(userId));
            return Json(new
            {
                result="Success",
                data=result
            });
        }
        /// <summary>
        /// 新增或编辑功能
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public IActionResult Edit(MenuDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Result = "Failed",
                    Message = GetModelStateError()
                });
            }
            if (_menuAppService.InsertOrUpdate(dto))
            {
                return Json(new { Result = "Success" });
            }
            return Json(new { Result = "Failed" });
        }

        public IActionResult DeleteMuti(string ids)
        {
            try
            {
                string[] idArray = ids.Split(',');
                List<Guid> delIds = new List<Guid>();
                foreach (string id in idArray)
                {
                    delIds.Add(Guid.Parse(id));
                }
                _menuAppService.DeleteBatch(delIds);
                return Json(new
                {
                    Result = "Success"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Result = "Failed",
                    Message = ex.Message
                });
            }
        }
        public IActionResult Delete(Guid id)
        {
            try
            {
                _menuAppService.Delete(id);
                return Json(new
                {
                    Result = "Success"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Result = "Failed",
                    Message = ex.Message
                });
            }
        }
        public ActionResult Get(Guid id)
        {
            var dto = _menuAppService.Get(id);
            return Json(dto);
        }

       
    }
}
