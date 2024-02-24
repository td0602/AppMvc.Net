using Controller_View.Service;
using Microsoft.AspNetCore.Mvc;

namespace Controller_View.Controllers;

public class FirstController : Controller {
    //Inject cac dich vu
    private readonly ILogger<FirstController> _logger;
    private readonly string _rootPath;
    private readonly ProductService _productService;
    [ActivatorUtilitiesConstructor]
    public FirstController(ILogger<FirstController> logger, IHostEnvironment env, ProductService productService) {
        _logger = logger;
        _rootPath = env.ContentRootPath;
        _productService = productService;
    }
    // view trong controller
    public string Index() {
        //this.HttpContext
        // this.Request
        // this.Response
        // this.RouteData

        // this.User
        // this.ModelState
        // this.ViewBag
        // this.ViewData
        // this.Url
        // this.TempData

        //viet ra dong Log:
        _logger.LogInformation("Index Acction");    
        return "Toi la Index cua First";
    }

    /* 
    Action trong controller, có thể trả về bất kỳ kiểu dữ liệu j
    la phuong thuc dc khai bao public, khong duoc khai bao static

    Action trong controller thuong tra ve cac doi tuong trien khai giao dien IActionResult:
            Kiểu trả về                 | Phương thức
        ------------------------------------------------
        ContentResult               | Content()
        EmptyResult                 | new EmptyResult()
        FileResult                  | File()
        ForbidResult                | Forbid()
        JsonResult                  | Json()
        LocalRedirectResult         | LocalRedirect()
        RedirectResult              | Redirect()
        RedirectToActionResult      | RedirectToAction()
        RedirectToPageResult        | RedirectToRoute()
        RedirectToRouteResult       | RedirectToPage()
        PartialViewResult           | PartialView()
        ViewComponentResult         | ViewComponent()
        StatusCodeResult            | StatusCode()
        ViewResult                  | View()
    */
    public void Nothing() {
        _logger.LogInformation("Nothing action");
        Response.Headers.Add("hi", "Xin chao cac ban");
    }

    public object AnyThing() => DateTime.Now;

    public IActionResult Readme() {
        var content = @"
        Xin chao cac ban, 
        cac ban dang hoc ve ASP.NET MVC
        
        
        XUANTHULAB.NET
        ";

        return Content(content, "text/plain");
    }

    public IActionResult Car() {
        string filePath = Path.Combine(_rootPath, "Files", "car.jpg");
        // doc file
        var bytes = System.IO.File.ReadAllBytes(filePath);
        //tao FileResults
        return File(bytes, "image/jpg");
    }

    public IActionResult IphonePrice() {
        return Json(
            new {
                productName = "Iphone X",
                price = 1000
            }
        );
    }

    public IActionResult Privacy() {
        // di chuyen toi action Privacy cua controller Home
        var url = Url.Action("Privacy", "Home");
        _logger.LogInformation("Chuyen huong den" + url);
        return LocalRedirect(url);
    }
    public IActionResult Google() {
        var url = "https://www.google.com/";
        _logger.LogInformation("Chuyen huong den" + url);
        return Redirect(url);
    }

    public IActionResult HelloView(string username) {
        if(string.IsNullOrEmpty(username)) {
            username = "khach";
        }
        /*
        View() --> su dung Razor Engine, doc va thi hanh cshtml (template) --> ket qua duoc luu trong ViewResult
        TH1: View(template) - template la duong dan tuyet doi toi .cshtml
        TH2: View(template, model)
        TH3: View("namefilecshtml", model) --> tu dong tim file cshtml theo Views/{nameCoontroller}/file.cshtml
        TH4: View() / View((object)model); --> tu dong tim file cshtml theo  Views/{nameCoontroller}/{nameMethod}.cshtml
        --> TH4 hay su dung nhat
        */
        return View("xinchao3", username);
    }

    /*
    cau truc model truyen du lieu giua cac thanh phan cua ung dung
    */
    [AcceptVerbs("POST", "GET")]
    public IActionResult ViewProduct(int? id) {
        var product = _productService.Where(p => p.Id == id).FirstOrDefault();
        if(product == null) {
            /*
            TempData:
            Truyen data tu trang nay sang trang khac
            Su dung Session cua he thong de lu data va trang khac co the doc duoc data nay, neu doc o trang khac thi lan doc dau tien data
            day duoc xoa luon.
            */
            TempData["StatusMessage"] = "San Pham ban yeu cau khong co";
            return Redirect(Url.Action("Index", "Home"));
        }

        // /Views/First/ViewProduct.cshtml
        // /MyViews/First/ViewProduct.cshtml
        //TH1:  truyen model sang view
        // return View(product); 
        //TH2: truyen data sang view bang cach su dung ViewData() --> key: value
        // this.ViewData["product"] = product;
        // ViewData["Title"] = product.Name;
        // return View("ViewProduct2");
        /* 
        TH3: truyen data sang view sd ViewBag
        Tuong tu ViewData, nhung ViewBag la doi tuong dynamic --> co the thiet lap thuoc tinh o thoi diem thuc thi bat ky thuoc
        tinh nao do chung ta ghi ra.
        Trong .cshtml cung co doi tuong ViewBag
        */
        ViewBag.product = product;
        return View("ViewProduct3");
    }
}