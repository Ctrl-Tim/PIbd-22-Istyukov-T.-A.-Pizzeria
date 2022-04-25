using PizzeriaContracts.BindingModels;
using PizzeriaContracts.StoragesContracts;
using PizzeriaContracts.ViewModels;
using PizzeriaFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaFileImplement.Implements
{
    public class StorageStorage : IStorageStorage
    {
        private readonly FileDataListSingleton source;

        public StorageStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<StorageViewModel> GetFullList()
        {
            return source.Storages
                .Select(CreateModel)
                .ToList();
        }

        public List<StorageViewModel> GetFilteredList(StorageBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Storages
                .Where(rec => rec.StorageName.Contains(model.StorageName))
                .Select(CreateModel)
                .ToList();
        }

        public StorageViewModel GetElement(StorageBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var storage = source.Storages
                .FirstOrDefault(rec => rec.Id == model.Id || rec.StorageName == model.StorageName);
            return storage != null ? CreateModel(storage) : null;
        }

        public void Insert(StorageBindingModel model)
        {
            int maxId = source.Storages.Count > 0 ? source.Ingredients.Max(rec => rec.Id) : 0;
            var element = new Storage { Id = maxId + 1, StorageIngredients = new Dictionary<int, int>() };
            source.Storages.Add(CreateModel(model, element));
        }

        public void Update(StorageBindingModel model)
        {
            var element = source.Storages.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            CreateModel(model, element);
        }

        public void Delete(StorageBindingModel model)
        {
            Storage element = source.Storages.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Storages.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private Storage CreateModel(StorageBindingModel model, Storage storage)
        {
            storage.StorageName = model.StorageName;
            storage.StorageManager = model.StorageManager;
            storage.DateCreate = model.DateCreate;

            // удаляем убранные
            foreach (var key in storage.StorageIngredients.Keys.ToList())
            {
                if (!model.StorageIngredients.ContainsKey(key))
                {
                    storage.StorageIngredients.Remove(key);
                }
            }

            // обновляем существуюущие и добавляем новые
            foreach (var ingredient in model.StorageIngredients)
            {
                if (storage.StorageIngredients.ContainsKey(ingredient.Key))
                {
                    storage.StorageIngredients[ingredient.Key] = model.StorageIngredients[ingredient.Key].Item2;
                }
                else
                {
                    storage.StorageIngredients.Add(ingredient.Key, model.StorageIngredients[ingredient.Key].Item2);
                }
            }
            return storage;
        }

        private StorageViewModel CreateModel(Storage storage)
        {
            return new StorageViewModel
            {
                Id = storage.Id,
                StorageName = storage.StorageName,
                StorageManager = storage.StorageManager,
                DateCreate = storage.DateCreate,
                StorageIngredients = storage.StorageIngredients
                    .ToDictionary(recPC => recPC.Key, recPC =>
                        (source.Ingredients.FirstOrDefault(recC => recC.Id == recPC.Key)?.IngredientName, recPC.Value))
            };
        }

        public bool CheckIngredientsCount(int count, Dictionary<int, (string, int)> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                //суммарное количество необходимого ингредиента
                int ingredientCount = source.Storages
                    .Where(rec => rec.StorageIngredients.ContainsKey(ingredient.Key))
                    .Sum(rec => rec.StorageIngredients[ingredient.Key]);
                if (ingredientCount < (ingredient.Value.Item2 * count))
                {
                    return false;
                }
            }
            foreach (var ingredient in ingredients)
            {
                //забираем ингредиенты со складов
                int requiredCount = ingredient.Value.Item2 * count;
                while (requiredCount > 0)
                {
                    var storage = source.Storages
                        .FirstOrDefault(rec => rec.StorageIngredients.ContainsKey(ingredient.Key) && rec.StorageIngredients[ingredient.Key] > 0);
                    int availableCount = storage.StorageIngredients[ingredient.Key];
                    requiredCount -= availableCount;
                    if (availableCount > requiredCount + availableCount)
                    {
                        storage.StorageIngredients[ingredient.Key] = availableCount - (requiredCount + availableCount);
                    }
                    else
                    {
                        storage.StorageIngredients[ingredient.Key] = 0;
                    }
                }
            }
            return true;
        }
    }
}
