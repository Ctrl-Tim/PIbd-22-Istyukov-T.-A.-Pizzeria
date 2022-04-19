﻿using System.Collections.Generic;
using PizzeriaContracts.BindingModels;
using PizzeriaContracts.ViewModels;

namespace PizzeriaContracts.StoragesContracts
{
    public interface IClientStorage
    {
		List<ClientViewModel> GetFullList();
		List<ClientViewModel> GetFilteredList(ClientBindingModel model);
		ClientViewModel GetElement(ClientBindingModel model);
		void Insert(ClientBindingModel model);
		void Update(ClientBindingModel model);
		void Delete(ClientBindingModel model);
	}
}