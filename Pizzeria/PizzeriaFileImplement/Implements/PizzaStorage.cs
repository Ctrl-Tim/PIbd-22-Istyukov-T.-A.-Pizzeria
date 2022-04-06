using PizzeriaContracts.BindingModels;
using PizzeriaContracts.StoragesContracts;
using PizzeriaContracts.ViewModels;
using PizzeriaFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaFileImplement.Implements
{
    public class PizzaStorage : IPizzaStorage
    {
        private readonly FileDataListSingleton source;

        public PizzaStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<PizzaViewModel> GetFullList()
        { 
            return source.Pizzas
                .Select(CreateModel)
                .ToList(); 
        }

        public List<PizzaViewModel> GetFilteredList(PizzaBindingModel model)
        {
            if (model == null)
            { 
                return null;
            }

            return source.Pizzas
                .Where(rec => rec.PizzaName.Contains(model.PizzaName))
                .Select(CreateModel)
                .ToList();
        }

        public PizzaViewModel GetElement(PizzaBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            var product = source.Pizzas
                .FirstOrDefault(rec => rec.PizzaName == model.PizzaName || rec.Id == model.Id);

            return product != null ? CreateModel(product) : null;
        }

        public void Insert(PizzaBindingModel model)
        {
            int maxId = source.Pizzas.Count > 0 ? source.Ingredients.Max(rec => rec.Id) : 0;
            var element = new Pizza { Id = maxId + 1, PizzaIngredients = new Dictionary<int, int>() };
            source.Pizzas.Add(CreateModel(model, element));
        }

        public void Update(PizzaBindingModel model)
        {
            var element = source.Pizzas.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            CreateModel(model, element);
        }

        public void Delete(PizzaBindingModel model)
        {
            Pizza element = source.Pizzas.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Pizzas.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private static Pizza CreateModel(PizzaBindingModel model, Pizza pizza)
        {
            pizza.PizzaName = model.PizzaName;
            pizza.Price = model.Price;

            // удаляем убранные
            foreach (var key in pizza.PizzaIngredients.Keys.ToList())
            {
                if (!model.PizzaIngredients.ContainsKey(key))
                {
                    pizza.PizzaIngredients.Remove(key);
                }
            } 
            // обновляем существуюущие и добавляем новые
            foreach (var ingredient in model.PizzaIngredients)
            {
                if (pizza.PizzaIngredients.ContainsKey(ingredient.Key)) 
                { 
                    pizza.PizzaIngredients[ingredient.Key] = model.PizzaIngredients[ingredient.Key].Item2;
                }
                else
                {
                    pizza.PizzaIngredients.Add(ingredient.Key, model.PizzaIngredients[ingredient.Key].Item2);
                }
            } 

            return pizza;
        }

        private PizzaViewModel CreateModel(Pizza pizza)
        {
            return new PizzaViewModel
            {
                Id = pizza.Id,
                PizzaName = pizza.PizzaName,
                Price = pizza.Price,
                PizzaIngredients = pizza.PizzaIngredients
                    .ToDictionary(recPC => recPC.Key, recPC =>
                        (source.Ingredients.FirstOrDefault(recC => recC.Id == recPC.Key)?.IngredientName, recPC.Value))
            };
        }
    }
}
