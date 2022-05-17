using PizzeriaContracts.BindingModels;
using PizzeriaContracts.BusinessLogicsContracts;
using PizzeriaContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StorageController : Controller
    {
        private readonly IStorageLogic storageLogic;
        private readonly IIngredientLogic ingredientLogic;
        public StorageController(IStorageLogic storageLogic, IIngredientLogic ingredientLogic)
        {
            this.storageLogic = storageLogic;
            this.ingredientLogic = ingredientLogic;
        }
        [HttpGet]
        public List<StorageViewModel> GetStorageList() => storageLogic.Read(null)?.ToList();
        [HttpGet]
        public StorageViewModel GetStorage(int storageId) => storageLogic.Read(new StorageBindingModel { Id = storageId })?[0];
        [HttpGet]
        public List<IngredientViewModel> GetIngredientsList() => ingredientLogic.Read(null)?.ToList();
        [HttpPost]
        public void CreateUpdateWarehouse(StorageBindingModel model) => storageLogic.CreateOrUpdate(model);
        [HttpPost]
        public void DeleteWarehouse(StorageBindingModel model) => storageLogic.Delete(model);
        [HttpPost]
        public void AddIngrdientStorage(ReplenishStorageBindingModel model) => storageLogic.Replenishment(new ReplenishStorageBindingModel { StorageId = model.StorageId }, model.IngredientId, model.Count);
    }
}
