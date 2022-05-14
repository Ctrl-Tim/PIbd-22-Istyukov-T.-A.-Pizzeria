using PizzeriaContracts.BindingModels;
using PizzeriaContracts.StoragesContracts;
using PizzeriaContracts.ViewModels;
using PizzeriaDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaDatabaseImplement.Implements
{
    public class StorageStorage : IStorageStorage
    {
        public List<StorageViewModel> GetFullList()
        {
            using (var context = new PizzeriaDatabase())
            {
                return context.Storages.Include(rec => rec.StorageIngredients).ThenInclude(rec => rec.Ingredient)
                    .ToList()
                    .Select(CreateModel)
                    .ToList();
            }
        }
        public List<StorageViewModel> GetFilteredList(StorageBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new PizzeriaDatabase())
            {
                return context.Storages
                    .Include(rec => rec.StorageIngredients)
                    .ThenInclude(rec => rec.Ingredient)
                    .Where(rec => rec.StorageName.Contains(model.StorageName)).ToList().Select(CreateModel).ToList();
            }
        }
        public StorageViewModel GetElement(StorageBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new PizzeriaDatabase())
            {
                var storage = context.Storages.Include(rec => rec.StorageIngredients).ThenInclude(rec => rec.Ingredient).FirstOrDefault(rec => rec.StorageName == model.StorageName || rec.Id == model.Id);
                return storage != null ? CreateModel(storage) : null;
            }
        }

        public void Insert(StorageBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Storage storage = new Storage()
                        {
                            StorageName = model.StorageName,
                            StorageManager = model.StorageManager,
                            DateCreate = model.DateCreate
                        };
                        context.Storages.Add(storage);
                        context.SaveChanges();
                        CreateModel(model, storage, context);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Update(StorageBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Storages.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        CreateModel(model, element, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(StorageBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                Storage element = context.Storages.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Storages.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private static Storage CreateModel(StorageBindingModel model, Storage storage, PizzeriaDatabase context)
        {
            storage.StorageName = model.StorageName;
            storage.StorageManager = model.StorageManager;
            storage.DateCreate = model.DateCreate;
            if (model.Id.HasValue)
            {
                var storageIngredients = context.StorageIngredients.Where(rec => rec.StorageId == model.Id.Value).ToList();
                context.StorageIngredients.RemoveRange(storageIngredients
                    .Where(rec => !model.StorageIngredients.ContainsKey(rec.IngredientId)).ToList());
                context.SaveChanges();
                foreach (var updateIngredient in storageIngredients)
                {
                    updateIngredient.Count = model.StorageIngredients[updateIngredient.IngredientId].Item2;
                    model.StorageIngredients.Remove(updateIngredient.IngredientId);
                }
                context.SaveChanges();
            }
            foreach (var pc in model.StorageIngredients)
            {
                context.StorageIngredients.Add(new StorageIngredient
                {
                    StorageId = storage.Id,
                    IngredientId = pc.Key,
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
            return storage;
        }

        private static StorageViewModel CreateModel(Storage storage)
        {
            return new StorageViewModel
            {
                Id = storage.Id,
                StorageName = storage.StorageName,
                StorageManager = storage.StorageManager,
                DateCreate = storage.DateCreate,
                StorageIngredients = storage.StorageIngredients.ToDictionary(recPC => recPC.IngredientId, recPC => (recPC.Ingredient?.IngredientName, recPC.Count))
            };
        }

        public bool CheckIngredientsCount(int count, Dictionary<int, (string, int)> ingredients)
        {
            using (var context = new PizzeriaDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var ingredient in ingredients)
                        {
                            int requiredCount = ingredient.Value.Item2 * count;
                            foreach (var storage in context.Storages.Include(rec => rec.StorageIngredients))
                            {
                                int? availableCount = storage.StorageIngredients.FirstOrDefault(rec => rec.IngredientId == ingredient.Key)?.Count;
                                if (availableCount == null) { continue; }
                                requiredCount -= (int)availableCount;
                                storage.StorageIngredients.FirstOrDefault(rec => rec.IngredientId == ingredient.Key).Count = (requiredCount < 0) ? (int)availableCount - ((int)availableCount + requiredCount) : 0;
                            }
                            if (requiredCount > 0)
                            {
                                throw new Exception("На складах недостаточно компонентов");
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return true;
        }
    }
}
