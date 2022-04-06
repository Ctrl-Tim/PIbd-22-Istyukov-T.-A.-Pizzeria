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
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using var context = new PizzeriaDatabase();
            return context.Orders.
                Include(rec => rec.Pizza)
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    PizzaId = rec.PizzaId,
                    PizzaName = rec.Pizza.PizzaName,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status.ToString(),
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement

                }).ToList();
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new PizzeriaDatabase();
            return context.Orders.Include(rec => rec.Pizza).Where(rec => rec.PizzaId == model.PizzaId ||
            (rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo)).Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                PizzaId = rec.PizzaId,
                PizzaName = rec.Pizza.PizzaName,
                Count = rec.Count,
                Sum = rec.Sum,
                Status = rec.Status.ToString(),
                DateCreate = rec.DateCreate,
                DateImplement = rec.DateImplement
            }).ToList();
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new PizzeriaDatabase();
            var order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            return order != null ? CreateModel(order, context) : null;
        }
        public void Insert(OrderBindingModel model)
        {
            using var context = new PizzeriaDatabase();
            context.Orders.Add(CreateModel(model, new Order()));
            context.SaveChanges();
        }
        public void Update(OrderBindingModel model)
        {
            using var context = new PizzeriaDatabase();
            var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(OrderBindingModel model)
        {
            using var context = new PizzeriaDatabase();
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Orders.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.PizzaId = model.PizzaId;
            order.Sum = model.Sum;
            order.Count = model.Count;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;

            return order;
        }

        public OrderViewModel CreateModel(Order order, PizzeriaDatabase context)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                PizzaId = order.PizzaId,
                PizzaName = context.Pizzas.FirstOrDefault(rec => rec.Id == order.PizzaId)?.PizzaName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status.ToString(),
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}
