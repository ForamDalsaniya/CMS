using CabManagementSystem.Models;
using CabManagementSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace CabManagementSystem.Controllers
{

    public class HomeController : Controller
    {
        private string _address = "_address";
        private string _userName = "_user";
        private readonly ICabRepository _cabRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        public HomeController(ICabRepository cabRepository, IHostingEnvironment hostingEnvironment)
        {
            this._cabRepository = cabRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> CabIndex()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CabDriverHome()
        {
            return View();
        }
        private string ProcessUploadFile(CabCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CabDriverHome(CabCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(model);
                Cab cab = new Cab
                {
                    CabModel = model.CabModel,
                    numberPlate = model.numberPlate,
                    AC = model.AC,
                    PhotoPath = uniqueFileName,
                    DriverId = model.DrivarId,
                    City = model.City,
                    isRegister = false,
                    registerDate = null,
                    registerAddress = null,
                    registerBy = null,
                };
                _cabRepository.Add(cab);
                return RedirectToAction("CabIndex");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CabBook()
        {
            return View();
        }

        public ICabRepository Get_cabRepository()
        {
            return _cabRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CabBook([FromForm] string? cityName, [FromForm] string? cabAddress, [FromForm] string? registerBy)
        {
            string city = cityName ?? String.Empty;
            string address = cabAddress ?? String.Empty;
            string registerby = registerBy ?? String.Empty;
            HttpContext.Session.SetString(_address, address);
            HttpContext.Session.SetString(_userName, registerby);
            ViewBag.city = city;
            ViewBag.address = address;
            ViewBag.userId = registerby;
            var model = _cabRepository.GetAllCab();
            return View("ListCab", model);
        }
        public async Task<IActionResult> ListCab()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Book(int? id)
        {
            
            if (id != null)
            {
                
                var model = _cabRepository.GetCab(id.Value);
                model.isRegister = true;
                model.registerDate = DateTime.Now;
                model.registerBy = HttpContext.Session.GetString(_userName);
                model.registerAddress = HttpContext.Session.GetString(_address);
                _cabRepository.Update(model);
            }
            return View("Index","Home");
        }
        public IActionResult RegisteredCabList(string? id)
        {
            IEnumerable<Cab> model = _cabRepository.GetAllCab();
            ////IEnumerable<Cab> cabModels = (IEnumerable<Cab>)cab;
            //List<Cab> cabs = new List<Cab>();
            //foreach(var model in cabModels)
            //{
            //    if(model.DriverId == id)
            //    {
            //       cabs.Add(new Cab() { AC=model.AC, CabModel=model.CabModel, CarId=model.CarId, City=model.City, DriverId=model.DriverId, isRegister=model.isRegister });
            //    }
            //}
            //Console.WriteLine(cabs);
            ViewBag.Id = id;
            return View(model);
        }
        [HttpGet]
        public IActionResult Confirm(int? id)
        {
            if(id != null)
            { 
                var model = _cabRepository.GetCab(id.Value);
                model.isRegister = false;
                model.registerAddress = null;
                model.registerDate = null;
                model.registerBy = null;
                _cabRepository.Update(model);
            return View("CabIndex","home");
            }
            return View("CabDriverHome");
        }
    }
}
