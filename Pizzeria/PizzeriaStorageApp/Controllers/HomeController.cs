using PizzeriaContracts.BindingModels;
using PizzeriaContracts.ViewModels;
using PizzeriaStorageApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaStorageApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            if (Program.Autorized == false)
            {
                return Redirect("~/Home/Enter");
            }
            return
            View(APIClient.GetRequest<List<StorageViewModel>>($"api/Storage/GetStorageList"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                if (configuration["Password"] != password)
                {
                    throw new Exception("Incorrect password");
                }
                Program.Autorized = true;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Enter password");
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (Program.Autorized == false)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }

        [HttpPost]
        public void Create(string storageName, string storageManager)
        {
            if (String.IsNullOrEmpty(storageName) || String.IsNullOrEmpty(storageManager))
            {
                return;
            }
            APIClient.PostRequest("api/Storage/CreateUpdateStorage", new StorageBindingModel
            {
                StorageName = storageName,
                StorageManager = storageManager,
                DateCreate = DateTime.Now,
                StorageIngredients = new Dictionary<int, (string, int)>()
            });
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult Adding()
        {
            if (Program.Autorized == false)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Storages = APIClient.GetRequest<List<StorageViewModel>>("api/Storage/GetStorageList");
            ViewBag.Ingredients = APIClient.GetRequest<List<IngredientViewModel>>("api/Storage/GetIngredientsList");
            return View();
        }

        [HttpPost]
        public void Adding(int storage, int ingredient, int count)
        {
            APIClient.PostRequest("api/Storage/ReplenishmentStorage", new ReplenishStorageBindingModel
            {
                StorageId = storage,
                IngredientId = ingredient,
                Count = count
            });
            Response.Redirect("Adding");
        }

        [HttpGet]
        public IActionResult Delete()
        {
            if (Program.Autorized == false)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Storages = APIClient.GetRequest<List<StorageViewModel>>("api/Storage/GetStorageList");
            return View();
        }

        [HttpPost]
        public void Delete(int storage)
        {
            APIClient.PostRequest("api/Storage/DeleteStorage", new StorageBindingModel
            {
                Id = storage
            });
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult Editing(int storageId)
        {
            if (Program.Autorized == false)
            {
                return Redirect("~/Home/Enter");
            }
            StorageViewModel storage = APIClient.GetRequest<StorageViewModel>($"api/Storage/GetStorage?storageId={storageId}");
            ViewBag.StorageName = storage.StorageName;
            ViewBag.StorageManager = storage.StorageManager;
            ViewBag.IngredientsStorage = storage.StorageIngredients;
            return View();
        }

        [HttpPost]
        public void Editing(int storageId, string storageName, string storageManager)
        {
            if (String.IsNullOrEmpty(storageName) || String.IsNullOrEmpty(storageManager))
            {
                return;
            }
            StorageViewModel storage = APIClient.GetRequest<StorageViewModel>($"api/Storage/GetStorage?storageId={storageId}");
            APIClient.PostRequest("api/Storage/CreateUpdateStorage", new StorageBindingModel
            {
                Id = storageId,
                StorageName = storageName,
                StorageManager = storageManager,
                StorageIngredients = storage.StorageIngredients,
                DateCreate = DateTime.Now
            });
            Response.Redirect("Index");
        }
    }
}
