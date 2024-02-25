
using System.Security.Permissions;
using Controller_View.ExtendMethods;
using Controller_View.Models;
using Controller_View.Service;
using Controller_View.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing.Constraints;

var builder = WebApplication.CreateBuilder(args);
// đăng ký dịch vụ cho AppDbContext
builder.Services.AddDbContext<AppDbContext>();
// Add services to the container.
builder.Services.AddControllersWithViews(); // dang ky dich vu mo hinh MVC
builder.Services.AddRazorPages(); // dang ky cac dich vu lien quan den trang Razor
// cau hinh them cau truc thu muc cho Razor Engine
builder.Services.Configure<RazorViewEngineOptions>(options => {
    // mac dinh dang tim cac file template Razor theo: /Views/{ContrllerName}/ActionName.cstml
    // cau hinh them:/MyViews/{1}ControllerName/{0}ActionName.cshtml, chi so {2} neu co la ten cua Area

    options.ViewLocationFormats.Add("MyViews/{1}/{0}" + RazorViewEngine.ViewExtension); // RazorViewEngine.ViewExtension de phong truong hop thay doi ten file mo rong .cshtml
});
/*
    Dang ky dich vu ProductService cho he thong:
--> Tai Controller/View nao muon su dung chi viec Inject vao 
Cach 2 va 4: Khi lay ra doi tuong ProductService co the tao ra doi tuong ProductService hoac doi tuong tu cac lop trien khai ke thua
ProductService 
*/
// builder.Services.AddSingleton<ProductService>(); //Cach 1
// builder.Services.AddSingleton<ProductService, ProductService>(); //Cach 2
// builder.Services.AddSingleton(typeof(ProductService)); //Cach 3
builder.Services.AddSingleton(typeof(ProductService), typeof(ProductService)); //Cach 
builder.Services.AddSingleton<PlanetService>();

var app = builder.Build();

//Inject
// IWebHostEnvironment environment = app.Environment;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
// Code Error: 400 - 599
app.AddStatusCodePage(); // Tuy bien Response loi: 400-599

app.UseRouting(); // EndpointRoutingMiddleware
app.UseAuthentication(); // xac dinh danh tinh
app.UseAuthorization(); // xac thuc quyen truy cap

//URL: /{controller}/{action}/{id?}
// Ex: Abc/Xys --> He thong se tim Controller co ten Abc va khoi tao --> Goi den phuong thuc Xyz co trong controller do
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

// Tao cac diem endpoint
app.MapGet("/sayhi", async (context) => {
    await context.Response.WriteAsync($"Hello ASP.NET MVC {DateTime.Now}");
}); 

/*
Tao cac diem endpoint anh xa URL vao cac controller
    1. app.MapControllers 
    --> Cau hinh de tao cac endpoint toi cac controller, sau do cac diem endpoint phai dinh nghia truc tiep trong controller thong
    qua Atributte.
    2. app.MapAreaControllerRoute
    --> Tao cac diem endpoint den cac controller nam trong Area (muc rieng)
    3. app.MapControllerRoute
    4. app.MapDefaultControllerRoute
*/
//rang buoc cac tham so cua Route phai thoa man dk nao do, Ex:
app.MapControllerRoute(
    name: "first",
    pattern: "{url:regex(^((xemsanpham)|(viewproduct))$)}/{id:range(2,4)}",
    defaults: new {
        controller = "First",
        action = "ViewProduct"
    }
    // constraints: new {
    //     // url = new StringRouteConstraint("xemsanpham")
    //     url = "xemsanpham",
    //     id = new RangeRouteConstraint(2,4)
    // }
);

// MapAreaControllerRoute de anh xa Url toi cac Controller có khai báo Area
// MapAreaControllerRoute phai nam trc MapController default
app.MapAreaControllerRoute(
    name: "product",
    pattern: "/{controller}/{action=Index}/{id?}",
    areaName: "ProductManage"
);

// URL: /start-here
app.MapControllerRoute(
    name: "default", // routename
    pattern: "/{controller=Home}/{action=Index}/{id?}"
    // chua cac tham so mac dinh cua route, la doi tuong kieu vo danh
    // defaults: new {
    //     controller = "First",
    //     action = "ViewProduct",
    //     id = 3
    // }
);
//Tao cac diem endpoint de co the truy cap den trang Razor page neu co
app.MapRazorPages();
app.Run();
