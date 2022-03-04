using System;
using System.Collections.Generic;

namespace PizzeriaContracts.BindingModels
{
    public class StorageBindingModel
    {
        public int? Id { get; set; }

        public string StorageName { get; set; }

        public string StorageManager { get; set; }

        public DateTime DateCreate { get; set; }

        public Dictionary<int, (string, int)> StorageIngredients { get; set; }
    }
}
