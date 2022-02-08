﻿using System.Collections.Generic;
using System.ComponentModel;

namespace PizzeriaContracts.ViewModels
{
    /// <summary>
    /// Пицца, изготавливаемая в пиццерии
    /// </summary> 
    class PizzaViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название пиццы")]
        public string PizzaName { get; set; }

        [DisplayName("Цена")]
        public decimal Price { get; set; }

        public Dictionary<int, (string, int)> PizzaIngredients { get; set; }
    }
}