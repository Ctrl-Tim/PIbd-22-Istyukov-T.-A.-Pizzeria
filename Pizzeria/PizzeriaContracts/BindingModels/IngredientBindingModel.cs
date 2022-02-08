namespace PizzeriaContracts.BindingModels
{
    /// <summary>
    /// Игредиент, требуемый для изготовления изделия
    /// </summary>   
    class IngredientBindingModel
    {
        public int? Id { get; set; }

        public string IngredientName { get; set; }
    }
}
