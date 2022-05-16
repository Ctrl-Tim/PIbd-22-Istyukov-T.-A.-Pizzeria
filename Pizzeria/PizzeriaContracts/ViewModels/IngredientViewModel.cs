using PizzeriaContracts.Attributes;

namespace PizzeriaContracts.ViewModels
{
    /// <summary>
    /// Игредиент, требуемый для изготовления пиццы
    /// </summary> 
    public class IngredientViewModel
    {
        [Column(title: "Номер", width: 100)]
        public int Id { get; set; }

        [Column(title: "Название ингредиента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string IngredientName { get; set; }
    }
}
