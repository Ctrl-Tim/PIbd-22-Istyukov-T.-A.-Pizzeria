namespace PizzeriaContracts.BindingModels
{
    /// <summary>
    /// Данные от клиента, для создания заказа
    /// </summary>   
    class CreateOrderBindingModel
    {
        public int ProductId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}
