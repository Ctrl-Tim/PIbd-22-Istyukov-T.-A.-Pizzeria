using System;
using System.Collections.Generic;

namespace PizzeriaFileImplement.Models
{
    public class Storage
    {
        public int Id { get; set; }

        public string StorageName { get; set; }

        public string StorageManager { get; set; }

        public DateTime DateCreate { get; set; }

        public Dictionary<int, int> StorageIngredients { get; set; }
    }
}
