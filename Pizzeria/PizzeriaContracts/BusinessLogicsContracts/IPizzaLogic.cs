using System.Collections.Generic;
using PizzeriaContracts.BindingModels;
using PizzeriaContracts.ViewModels;

namespace PizzeriaContracts.BusinessLogicsContracts
{
    public interface IPizzaLogic
    {
        List<PizzaViewModel> Read(PizzaBindingModel model);

        void CreateOrUpdate(PizzaBindingModel model);

        void Delete(PizzaBindingModel model);
    }
}
