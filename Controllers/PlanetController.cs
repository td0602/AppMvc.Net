using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Controller_View.Service;
using Controller_View.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controller_View.Controllers
{
    [Route("he-mat-troi/[action]")]
    public class PlanetController : Controller
    {
        private readonly PlanetService _planetService;
        private readonly ILogger<PlanetController> _logger;

        public PlanetController(PlanetService planetService, ILogger<PlanetController> logger) {
            _planetService = planetService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        // moi khi truy cap ve action (Mercury, ...) thi trong route bao gio cung co tham so action
        //thiet lap cho Name duoc binding tu tham so cua route ten la action
        [BindProperty(SupportsGet = true, Name = "action")] // --> neu truy cap vao action Marcury thi Name = Macury
        public string Name {set; get;}

        public IActionResult Mercury() {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        public IActionResult Comet() {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        public IActionResult Earth() {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        public IActionResult Mars() {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        [Route("sao/[controller]/[action]", Order = 2, Name = "neptune2")] // sao/Planet/Neptune
        [Route("[controller]-[action].html", Order = 1, Name = "neptune1")] // Planet-Neptune.html
        public IActionResult Neptune() {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        public IActionResult Saturn() {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        public IActionResult Uranus() {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

        // Atribute Httpget/Post ... giong voi Route là thiết lập địa chỉ truy cập nhưng nó sẽ khống chế chỉ cho phép truy cập bằng
        // phương thức Get/Post ...
        [HttpGet("/saomoc.html")]
        public IActionResult Jupiter() {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        public IActionResult Venus() {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

        //Tong quat cho 8 ham tren
        // Nhung action co Atribute Route se khong con bi anh huong boi route khai bao trong MapController
        // Route giup ta dinh nghia rieng url nao do cho action 
        [Route("hanhtinh/{id:int}")] // Ex: hanhtinh/1
        public IActionResult PlanetInfo(int id) {
            var planet = _planetService.Where(p => p.Id == id).FirstOrDefault();
            return View("Detail", planet);
        }
    }
}