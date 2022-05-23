using System.Collections.Generic;
using PizzeriaContracts.Attributes;

namespace PizzeriaContracts.ViewModels
{
    /// <summary>
    /// Пицца, изготавливаемая в пиццерии
    /// </summary> 
    public class PizzaViewModel
    {
        [Column(title: "Номер", width: 100)]
        public int Id { get; set; }

        [Column(title: "Название пиццы", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string PizzaName { get; set; }

        [Column(title: "Цена", gridViewAutoSize: GridViewAutoSize.Fill)]
        public decimal Price { get; set; }

        public Dictionary<int, (string, int)> PizzaIngredients { get; set; }
    }
}
