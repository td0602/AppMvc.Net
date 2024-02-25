using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Controller_View.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controller_View.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("/database-mangae/[action]")]
    public class DbManageController : Controller
    {
        private readonly AppDbContext _dbContext;

        public DbManageController(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteDb() {
            return View();
        }

        [TempData]
        public string StatusMessage {set; get;}

        [HttpPost]
        public async Task<IActionResult> DeleteDbAsync() {
            var success = await _dbContext.Database.EnsureDeletedAsync();
            StatusMessage = success ? "Xóa Database thành công" : "Không xóa được Database";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Migrate() {
            // Cập nhật các Migration đang ở trạng thái pending
            await _dbContext.Database.MigrateAsync();
            StatusMessage = "Cập nhật Database thành công";
            return RedirectToAction(nameof(Index));
        }
    }
}