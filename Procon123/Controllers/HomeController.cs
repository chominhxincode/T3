using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Procon123.Models;

namespace Procon123.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Tạo một đối tượng ChessboardModel
            var model = new ChessboardModel(rows: 8, columns: 8); // Ví dụ: Bàn cờ 8x8

            // Trả về view "Index" và truyền model tới view
            return View(model);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Move(int currentRow, int currentColumn)
        {
            var chessboard = new ChessboardModel(8, 8); // Tạo một bàn cờ mới với kích thước 8x8
            chessboard.UpdateWorkerPosition(currentRow, currentColumn); // Cập nhật vị trí của Worker

            return Json(new { success = true }); // Trả về một JSON response
        }

    }
}