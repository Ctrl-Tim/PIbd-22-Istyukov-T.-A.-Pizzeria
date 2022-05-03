using System;
using System.Collections.Generic;

namespace PizzeriaContracts.ViewModels
{
    public class ReportStorageIngredientViewModel
    {
        public string StorageName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Ingredients { get; set; }
    }
}
