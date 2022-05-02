using PizzeriaContracts.BindingModels;
using PizzeriaContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaContracts.StoragesContracts
{
    public interface IMessageInfoStorage
    {
        List<MessageInfoViewModel> GetFullList();

        List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model);

        void Insert(MessageInfoBindingModel model);
    }
}
