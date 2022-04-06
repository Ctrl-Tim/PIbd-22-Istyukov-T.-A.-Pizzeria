using PizzeriaContracts.BindingModels;
using PizzeriaContracts.BusinessLogicsContracts;
using PizzeriaContracts.StoragesContracts;
using PizzeriaContracts.ViewModels;
using System;
using System.Collections.Generic;

namespace PizzeriaBusinessLogic.BusinessLogics
{
    public class StorageLogic : IStorageLogic
    {
        private readonly IStorageStorage _storageStorage;

        private readonly IIngredientStorage _ingredientStorage;

        public StorageLogic(IStorageStorage storage, IIngredientStorage ingredientStorage)
        {
            _storageStorage = storage;
            _ingredientStorage = ingredientStorage;
        }

        public List<StorageViewModel> Read(StorageBindingModel model)
        {
            if (model == null)
            {
                return _storageStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<StorageViewModel>
                {
                    _storageStorage.GetElement(model)
                };
            }

            return _storageStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(StorageBindingModel model)
        {
            var element = _storageStorage.GetElement(new StorageBindingModel
            {
                StorageName = model.StorageName
            });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть склад с таким названием");
            }

            if (model.Id.HasValue)
            {
                _storageStorage.Update(model);
            }
            else
            {
                _storageStorage.Insert(model);
            }
        }

        public void Delete(StorageBindingModel model)
        {
            var element = _storageStorage.GetElement(new StorageBindingModel
            {
                Id = model.Id
            });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _storageStorage.Delete(model);
        }

        public void Replenishment(ReplenishStorageBindingModel model, int ingredientId, int Count)
        {
            var storage = _storageStorage.GetElement(new StorageBindingModel
            {
                Id = model.StorageId
            });

            var ingredient = _ingredientStorage.GetElement(new IngredientBindingModel
            {
                Id = model.IngredientId
            });

            if (storage == null)
            {
                throw new Exception("Не найден склад");
            }

            if (ingredient == null)
            {
                throw new Exception("Не найден материал");
            }

            if (storage.StorageIngredients.ContainsKey(model.IngredientId))
            {
                storage.StorageIngredients[model.IngredientId] = (ingredient.IngredientName, storage.StorageIngredients[model.IngredientId].Item2 + Count);
            }
            else
            {
                storage.StorageIngredients.Add(ingredient.Id, (ingredient.IngredientName, model.Count));
            }

            _storageStorage.Update(new StorageBindingModel
            {
                Id = storage.Id,
                StorageName = storage.StorageName,
                StorageManager = storage.StorageManager,
                DateCreate = storage.DateCreate,
                StorageIngredients = storage.StorageIngredients
            });
        }
    }
}
