using System.Collections.Generic;
using PizzeriaContracts.BindingModels;
using PizzeriaContracts.ViewModels;

namespace PizzeriaContracts.BusinessLogicsContracts
{
    public interface IIngredientLogic
    {
        List<IngredientViewModel> Read(IngredientBindingModel model);

        void CreateOrUpdate(IngredientBindingModel model);

        void Delete(IngredientBindingModel model);
    }
}
