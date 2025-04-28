using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MVCEmpty.Models;

namespace MVCEmpty.Controllers
{
    public class HomeController : Controller
    {
        private static List<Student> stds = [
                    new Student("Sakina", "Aliyeva"),
                    new Student("Emiliya", "Zeynalzada"),
                    new Student("Gulyana", "Khalilova"),
                    new Student("Gunel", "Shikarova"),
                ];
        public IActionResult Index()
        {
            ViewData["Students"] = stds;
            //ViewBag.Test = "Index";
            //TempData["Test"] = "Tempdata";
            //return RedirectToAction("Sum");
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(string name, string surname)
        {
            stds.Add(new Student(name, surname));
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            var std = stds.FirstOrDefault(x => x.Id == id);
            if (std == null) return NotFound();
            return Content(std.Id + " " + std.Name + " " + std.Surname);
        }
        //public ViewResult Index()
        //{
        //    #region ContentResult and JsonResult
        //    return new ContentResult
        //    {
        //        Content = "<p>ashdasdghas</p>",
        //        ContentType = "video/mp4"
        //        //StatusCode = 500
        //    };
        //    return new JsonResult(new
        //    {
        //        Name = "asdasd",
        //        Surname = "asdasd"
        //    });
        //    #endregion
        //    var data = new ViewDataDictionary(ViewData)
        //    {
        //        new KeyValuePair<string, object>("Name", "Value"),
        //        new KeyValuePair<string, object>("Name2", "Value2")
        //    };

        //    ViewData["Student"] = new Student
        //    {
        //        Name = "Sakina",
        //        Surname = "Aliyeva"
        //    };
        //    var view = new ViewResult
        //    {
        //        ViewName = "/Views/Index.cshtml",
        //        ViewData = ViewData
        //    };
        //    return view;
        //}
        public IActionResult Sum(int a, int b)
        {
            //ViewBag.Test = "Piti";
            return View();
            //return a + b;
        }
    }
}
