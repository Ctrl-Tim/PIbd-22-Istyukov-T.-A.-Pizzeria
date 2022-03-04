using PizzeriaContracts.BindingModels;
using PizzeriaContracts.StoragesContracts;
using PizzeriaContracts.ViewModels;
using PizzeriaListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaListImplement.Implements
{
    public class StorageStorage : IStorageStorage
    {
        private readonly DataListSingleton source;

        public StorageStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<StorageViewModel> GetFullList()
        {
            List<StorageViewModel> result = new List<StorageViewModel>();

            foreach (var storage in source.Storages)
            {
                result.Add(CreateModel(storage));
            }

            return result;
        }

        public List<StorageViewModel> GetFilteredList(StorageBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            List<StorageViewModel> result = new List<StorageViewModel>();

            foreach (var storage in source.Storages)
            {
                if (storage.StorageName.Contains(model.StorageName))
                {
                    result.Add(CreateModel(storage));
                }
            }

            return result;
        }

        public StorageViewModel GetElement(StorageBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            foreach (var storage in source.Storages)
            {
                if (storage.Id == model.Id || storage.StorageName == model.StorageName)
                {
                    return CreateModel(storage);
                }
            }

            return null;
        }

        public void Insert(StorageBindingModel model)
        {
            Storage tempStorage = new Storage
            {
                Id = 1,
                StorageIngredients = new Dictionary<int, int>(),
                DateCreate = DateTime.Now
            };

            foreach (var storage in source.Storages)
            {
                if (storage.Id >= tempStorage.Id)
                {
                    tempStorage.Id = storage.Id + 1;
                }
            }

            source.Storages.Add(CreateModel(model, tempStorage));
        }

        public void Update(StorageBindingModel model)
        {
            Storage tempStorage = null;

            foreach (var storage in source.Storages)
            {
                if (storage.Id == model.Id)
                {
                    tempStorage = storage;
                }
            }

            if (tempStorage == null)
            {
                throw new Exception("Элемент не найден");
            }

            CreateModel(model, tempStorage);
        }

        public void Delete(StorageBindingModel model)
        {
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == model.Id)
                {
                    source.Storages.RemoveAt(i);

                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private Storage CreateModel(StorageBindingModel model, Storage storage)
        {
            storage.StorageName = model.StorageName;
            storage.StorageManager = model.StorageManager;

            foreach (var key in storage.StorageIngredients.Keys.ToList())
            {
                if (!model.StorageIngredients.ContainsKey(key))
                {
                    storage.StorageIngredients.Remove(key);
                }
            }

            foreach (var materials in model.StorageIngredients)
            {
                if (storage.StorageIngredients.ContainsKey(materials.Key))
                {
                    storage.StorageIngredients[materials.Key] = model.StorageIngredients[materials.Key].Item2;
                }
                else
                {
                    storage.StorageIngredients.Add(materials.Key, model.StorageIngredients[materials.Key].Item2);
                }
            }

            return storage;
        }

        private StorageViewModel CreateModel(Storage storage)
        {

            Dictionary<int, (string, int)> storageMaterials = new Dictionary<int, (string, int)>();

            foreach (var storageMaterial in storage.StorageIngredients)
            {
                string materialName = string.Empty;

                foreach (var material in source.Ingredients)
                {
                    if (storageMaterial.Key == material.Id)
                    {
                        materialName = material.IngredientName;

                        break;
                    }
                }

                storageMaterials.Add(storageMaterial.Key, (materialName, storageMaterial.Value));
            }

            return new StorageViewModel
            {
                Id = storage.Id,
                StorageName = storage.StorageName,
                StorageManager = storage.StorageManager,
                DateCreate = storage.DateCreate,
                StorageIngredients = storageMaterials
            };
        }
    }
}
