using System.Collections.Generic;
using PizzeriaContracts.BindingModels;
using PizzeriaContracts.ViewModels;

namespace PizzeriaContracts.BusinessLogicsContracts
{
    public interface IOrderLogic
    {
        List<OrderViewModel> Read(OrderBindingModel model);

        void CreateOrder(CreateOrderBindingModel model);

        void TakeOrderInWork(ChangeStatusBindingModel model);

        void FinishOrder(ChangeStatusBindingModel model);

        void DeliveryOrder(ChangeStatusBindingModel model);
    }
}
