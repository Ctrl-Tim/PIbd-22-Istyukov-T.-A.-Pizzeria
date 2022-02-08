namespace PizzeriaContracts.BindingModels
{
    /// <summary>
    /// Игредиент, требуемый для изготовления пиццы
    /// </summary>   
    public class IngredientBindingModel
    {
        public int? Id { get; set; }

        public string IngredientName { get; set; }
    }
}
