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
    public class PizzaStorage : IPizzaStorage
    {
        public List<PizzaViewModel> GetFullList()
        {
            using var context = new PizzeriaDatabase();
            return context.Pizzas
                .Include(rec => rec.PizzaIngredients)
                .ThenInclude(rec => rec.Ingredient)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<PizzaViewModel> GetFilteredList(PizzaBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new PizzeriaDatabase();

            return context.Pizzas
                .Include(rec => rec.PizzaIngredients)
                .ThenInclude(rec => rec.Ingredient)
                .Where(rec => rec.PizzaName.Contains(model.PizzaName))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public PizzaViewModel GetElement(PizzaBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new PizzeriaDatabase();
            var pizza = context.Pizzas
                .Include(rec => rec.PizzaIngredients)
                .ThenInclude(rec => rec.Ingredient)
                .FirstOrDefault(rec => rec.PizzaName == model.PizzaName || rec.Id == model.Id);

            return pizza != null ? CreateModel(pizza) : null;
        }

        public void Insert(PizzaBindingModel model)
        {
            using var context = new PizzeriaDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Pizza pizza = new Pizza()
                {
                    PizzaName = model.PizzaName,
                    Price = model.Price
                };
                context.Pizzas.Add(pizza);
                context.SaveChanges();
                CreateModel(model, pizza, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(PizzaBindingModel model)
        {
            using var context = new PizzeriaDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Pizzas
                    .FirstOrDefault(rec => rec.Id == model.Id);

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

        public void Delete(PizzaBindingModel model)
        {
            using var context = new PizzeriaDatabase();
            Pizza element = context.Pizzas
                .FirstOrDefault(rec => rec.Id == model.Id);

            if (element != null)
            {
                context.Pizzas.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private static Pizza CreateModel(PizzaBindingModel model, Pizza pizza, PizzeriaDatabase context)
        {
            pizza.PizzaName = model.PizzaName;
            pizza.Price = model.Price;

            if (model.Id.HasValue)
            {
                var pizzaIngredients = context.PizzaIngredients
                    .Where(rec => rec.PizzaId == model.Id.Value)
                    .ToList();
                // удалили те, которых нет в модели
                context.PizzaIngredients.RemoveRange(pizzaIngredients
                    .Where(rec => !model.PizzaIngredients
                    .ContainsKey(rec.IngredientId))
                    .ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateIngredient in pizzaIngredients)
                {
                    updateIngredient.Count = model.PizzaIngredients[updateIngredient.IngredientId].Item2;
                    model.PizzaIngredients.Remove(updateIngredient.IngredientId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.PizzaIngredients)
            { 
                context.PizzaIngredients.Add(new PizzaIngredient
                {
                    PizzaId = pizza.Id,
                    IngredientId = pc.Key,
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }

            return pizza;
        }

        private static PizzaViewModel CreateModel(Pizza pizza)
        {
            return new PizzaViewModel
            {
                Id = pizza.Id,
                PizzaName = pizza.PizzaName,
                Price = pizza.Price,
                PizzaIngredients = pizza.PizzaIngredients
                    .ToDictionary(recPC => recPC.IngredientId, recPC => (recPC.Ingredient?.IngredientName, recPC.Count))
            };
        }
    }
}
