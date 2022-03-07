using System.Collections.Generic;
using PizzeriaContracts.BindingModels;
using PizzeriaContracts.ViewModels;

namespace PizzeriaContracts.BusinessLogicsContracts
{
    public interface IStorageLogic
    {
        List<StorageViewModel> Read(StorageBindingModel model);

        void CreateOrUpdate(StorageBindingModel model);

        void Delete(StorageBindingModel model);
    }
}
