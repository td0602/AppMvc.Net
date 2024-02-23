
using Controller_View.Service;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);
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

app.UseRouting();
app.UseAuthentication(); // xac dinh danh tinh
app.UseAuthorization(); // xac thuc quyen truy cap

//URL: /{controller}/{action}/{id?}
//Ex: Abc/Xys --> He thong se tim Controller co ten Abc va khoi tao --> Goi den phuong thuc Xyz co trong controller do
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages(); // tao ra nhung diem endpoint toi nhung trang Razor
app.Run();
