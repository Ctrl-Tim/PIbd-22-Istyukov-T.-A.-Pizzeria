namespace PizzeriaContracts.BindingModels
{
    /// <summary>
    /// Данные от клиента, для создания заказа
    /// </summary>   
    public class CreateOrderBindingModel
    {
        public int ClientId { get; set; }

        public int PizzaId { get; set; }

        public string ClientFIO { get; set; }

        public string PizzaName { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}
