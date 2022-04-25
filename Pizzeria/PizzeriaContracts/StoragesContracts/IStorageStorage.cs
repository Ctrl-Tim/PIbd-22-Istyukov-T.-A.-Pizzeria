using System.Collections.Generic;
using PizzeriaContracts.BindingModels;
using PizzeriaContracts.ViewModels;


namespace PizzeriaContracts.StoragesContracts
{
    public interface IStorageStorage
    {
        List<StorageViewModel> GetFullList();

        List<StorageViewModel> GetFilteredList(StorageBindingModel model);

        StorageViewModel GetElement(StorageBindingModel model);

        void Insert(StorageBindingModel model);

        void Update(StorageBindingModel model);

        void Delete(StorageBindingModel model);

        bool CheckIngredientsCount(int count, Dictionary<int, (string, int)> ingredients);
    }
}
