using System.Collections.Generic;
using PizzeriaContracts.BindingModels;
using PizzeriaContracts.ViewModels;

namespace PizzeriaContracts.StoragesContracts
{
    public interface IPizzaStorage
    {
        List<PizzaViewModel> GetFullList();

        List<PizzaViewModel> GetFilteredList(PizzaBindingModel model);

        PizzaViewModel GetElement(PizzaBindingModel model);

        void Insert(PizzaBindingModel model);

        void Update(PizzaBindingModel model);

        void Delete(PizzaBindingModel model);
    }
}
