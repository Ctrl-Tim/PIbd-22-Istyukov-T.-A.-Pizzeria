using System.ComponentModel;

namespace PizzeriaContracts.ViewModels
{
    /// <summary>
    /// Игредиент, требуемый для изготовления изделия
    /// </summary> 
    class IngredientViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название ингредиента")]
        public string IngredientName { get; set; }
    }
}
