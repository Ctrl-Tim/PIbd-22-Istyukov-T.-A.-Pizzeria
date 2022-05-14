using PizzeriaContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportPizzaIngredientViewModel> PizzaIngredients { get; set; }

        public List<ReportStorageIngredientViewModel> StorageIngredients { get; set; }
    }
}
