## Controller
- Là một lớp kế từ thừa lớp Controller : Microsoft.AspNetCore.Mvc.Controller
- Action trong controller là một phương public (không được static)
- Action trả về bất kỳ kiểu dữ liệu nào, thường là IActionResult
- Các dịch vụ inject vào controller qua hàm tạo
## View
-Là file .cshtml
- View cho Action lưu tại: /View/ControllerName/ActionName.cshtml
Thêm thư mục lưu trữ View:
'''
// {0} -> ten Action
// {1} -> ten Controller
// {2} -> ten Area
options.ViewLocationFormats.Add("/MyView/{1}/{0}" + RazorViewEngine.ViewExtension);

options.AreaViewLocationFormats.Add("/MyArea/{1}/View/{1}/{0}.cshtml");
'''
## Truyền dữ liệu sang View
- Model
- ViewData
- ViewBag
- TempData

## Areas
- Để thiết lập Controller thuộc về một vùng nào đó, 1 controller có thể thuộc về một Area
- Là tên dùng để routing
- Là cấu trúc thư mục chứa M.V.C
- Thiết lập Area cho controller bằng '''[Area("Area Name")]'''
- Tạo cấu trúc thư mục
'''
dotnet aspnet-codegenerator area AreaName
'''
## Route
- endpoints.MapControllerRoute
- endpoints.MapAreaControllerRoute
- [AcceptVerbs("POST", "GET")]
- [Route("pattern")]
- [HttpGet] [HttpPost] 
## Url Generation
### UrlHelper : Action, ActionLink, RouteUrl, Link
```
Url.Action("PlanetInfo", "Planet", new {id = 1}, Context.Request.Scheme)

Url.RouteUrl("default", new {controller= "First", action="HelloView", id = 1, username =  "XuanThuLab"})
```
### HtmlTagHelper: ```<a> <button> <form>```
Sử dụng thuộc tính:
```
asp-area="Area"
asp-action="Action"
asp-controller="Product"
asp-route...="123"
asp-route="default"
```

## Connect DB
- Thêm các thư viện cần thiết
- Cấu hình kết nối tới database trong CSDL
- Tạo file AppDbContext.cs
- Đăng ký dịch vụ cho DbContext