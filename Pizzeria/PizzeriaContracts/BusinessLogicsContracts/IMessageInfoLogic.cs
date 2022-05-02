using PizzeriaContracts.BindingModels;
using PizzeriaContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaContracts.BusinessLogicsContracts
{
    public interface IMessageInfoLogic
    {
        List<MessageInfoViewModel> Read(MessageInfoBindingModel model);

        void CreateOrUpdate(MessageInfoBindingModel model);
    }
}
