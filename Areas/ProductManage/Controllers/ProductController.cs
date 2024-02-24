using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Controller_View.Service;
using Microsoft.AspNetCore.Mvc;

namespace Controller_View.Controllers
{
    [Area("ProductManage")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ProductService productService, ILogger<ProductController> logger) {
            _productService = productService;
            _logger = logger;
        }

        [Route("/cac-san-pham/{id?}")]
        public IActionResult Index()
        {
            // Khi ProductController khai báo Area thì thư mục mặc định chứa file .cshtml: /Areas/AreaName/Views/ControllerName/Action.cshtml
            var products = _productService.OrderBy(p => p.Name).ToList();
            return View(products); // Areas/ProductMangae/Views/Product/Index.cshtml
        }
    }
}