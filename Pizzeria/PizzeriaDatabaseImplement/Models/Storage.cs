﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaDatabaseImplement.Models
{
    public class Storage
    {
        public int Id { get; set; }

        [Required]
        public string StorageName { get; set; }

        [Required]
        public string StorageManager { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        [ForeignKey("StorageId")]
        public virtual List<StorageIngredient> StorageIngredients { get; set; }
    }
}