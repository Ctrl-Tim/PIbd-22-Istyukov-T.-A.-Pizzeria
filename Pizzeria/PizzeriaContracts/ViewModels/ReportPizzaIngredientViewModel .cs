﻿using System;
using System.Collections.Generic;

namespace PizzeriaContracts.ViewModels
{
    public class ReportPizzaIngredientViewModel
    {
        public string PizzaName { get; set; }

        public int TotalCount { get; set; }

        public List<Tuple<string, int>> Ingredients { get; set; }
    }
}