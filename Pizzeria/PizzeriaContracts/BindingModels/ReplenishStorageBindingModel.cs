namespace PizzeriaContracts.BindingModels
{
    public class ReplenishStorageBindingModel
    {
        public int StorageId { get; set; }

        public int IngredientId { get; set; }

        public int Count { get; set; }
    }
}
