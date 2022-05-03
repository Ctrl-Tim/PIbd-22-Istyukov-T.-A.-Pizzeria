using PizzeriaContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<PizzaViewModel> Pizzas { get; set; }

        public List<StorageViewModel> Storages { get; set; }
    }
}
