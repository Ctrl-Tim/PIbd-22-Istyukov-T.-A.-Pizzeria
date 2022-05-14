﻿using System;
using System.Collections.Generic;

namespace PizzeriaListImplement.Models
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