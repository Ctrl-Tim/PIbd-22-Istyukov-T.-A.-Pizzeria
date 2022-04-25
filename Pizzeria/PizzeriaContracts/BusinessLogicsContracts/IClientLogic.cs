using System.Collections.Generic;
using PizzeriaContracts.BindingModels;
using PizzeriaContracts.ViewModels;

namespace PizzeriaContracts.BusinessLogicsContracts
{
    public interface IClientLogic
    {
        List<ClientViewModel> Read(ClientBindingModel model);
        void CreateOrUpdate(ClientBindingModel model);
        void Delete(ClientBindingModel model);
    }
}
