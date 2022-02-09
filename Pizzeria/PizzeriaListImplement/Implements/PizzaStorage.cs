using PizzeriaContracts.BindingModels;
using PizzeriaContracts.StoragesContracts;
using PizzeriaContracts.ViewModels;
using PizzeriaListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaListImplement.Implements
{
    public class PizzaStorage : IPizzaStorage
    {
        private readonly DataListSingleton source;

        public PizzaStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<PizzaViewModel> GetFullList()
        {
            var result = new List<PizzaViewModel>();
            foreach (var ingredient in source.Pizzas)
            {
                result.Add(CreateModel(ingredient));
            }
            return result;
        }

        public List<PizzaViewModel> GetFilteredList(PizzaBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            var result = new List<PizzaViewModel>();
            foreach (var pizza in source.Pizzas)
            {
                if (pizza.PizzaName.Contains(model.PizzaName))
                {
                    result.Add(CreateModel(pizza));
                }
            }
            return result;
        }

        public PizzaViewModel GetElement(PizzaBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            foreach (var pizza in source.Pizzas)
            {
                if (pizza.Id == model.Id || pizza.PizzaName == model.PizzaName)
                {
                    return CreateModel(pizza);
                }
            }

            return null;
        }

        public void Insert(PizzaBindingModel model)
        {
            var tempPizza = new Pizza { Id = 1, PizzaIngredients = new Dictionary<int, int>() };
            foreach (var pizza in source.Pizzas)
            {
                if (pizza.Id >= tempPizza.Id)
                {
                    tempPizza.Id = pizza.Id + 1;
                }
            }

            source.Pizzas.Add(CreateModel(model, tempPizza));
        }

        public void Update(PizzaBindingModel model)
        {
            Pizza tempPizza = null;
            foreach (var pizza in source.Pizzas)
            {
                if (pizza.Id == model.Id)
                {
                    tempPizza = pizza;
                }
            }

            if (tempPizza == null)
            {
                throw new Exception("Элемент не найден");
            }

            CreateModel(model, tempPizza);
        }

        public void Delete(PizzaBindingModel model)
        {
            for (int i = 0; i < source.Pizzas.Count; ++i)
            {
                if (source.Pizzas[i].Id == model.Id)
                {
                    source.Pizzas.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private static Pizza CreateModel(PizzaBindingModel model, Pizza pizza)
        {
            pizza.PizzaName = model.PizzaName;
            pizza.Price = model.Price;

            // удаляем убранные
            foreach(var key in pizza.PizzaIngredients.Keys.ToList())
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
            // требуется дополнительно получить список ингредиентов для пиццы с названиями и их количество
            var pizzaIngredients = new Dictionary<int, (string, int)>();
            foreach (var pz in pizza.PizzaIngredients)
            {
                string ingredientName = string.Empty;
                foreach (var ingredient in source.Ingredients)
                {
                    if (pz.Key == ingredient.Id)
                    {
                        ingredientName = ingredient.IngredientName;
                        break;
                    }
                }
            pizzaIngredients.Add(pz.Key, (ingredientName, pz.Value));
            }
            return new PizzaViewModel
            {
                Id = pizza.Id,
                PizzaName = pizza.PizzaName,
                Price = pizza.Price,
                PizzaIngredients = pizzaIngredients
            };
        }
    }
}
