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
            using (var context = new PizzeriaDatabase())
            {
                return context.Orders
                    .Select(rec => new OrderViewModel
                    {
                        Id = rec.Id,
                        PizzaName = rec.Pizza.PizzaName,
                        PizzaId = rec.PizzaId,
                        Count = rec.Count,
                        Sum = rec.Sum,
                        Status = rec.Status.ToString(),
                        DateCreate = rec.DateCreate,
                        DateImplement = rec.DateImplement
                    })
                    .ToList();
            }
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PizzeriaDatabase())
            {
                return context.Orders
                    .Where(rec => rec.PizzaId == model.PizzaId)
                    .Select(rec => new OrderViewModel
                    {
                        Id = rec.Id,
                        PizzaName = rec.Pizza.PizzaName,
                        PizzaId = rec.PizzaId,
                        Count = rec.Count,
                        Sum = rec.Sum,
                        Status = rec.Status.ToString(),
                        DateCreate = rec.DateCreate,
                        DateImplement = rec.DateImplement
                    })
                    .ToList();
            }
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PizzeriaDatabase())
            {
                var order = context.Orders
                    .FirstOrDefault(rec => rec.Id == model.Id);

                return order != null ?
                    new OrderViewModel
                    {
                        Id = order.Id,
                        PizzaName = context.Pizzas.FirstOrDefault(rec => rec.Id == order.PizzaId)?.PizzaName,
                        PizzaId = order.PizzaId,
                        Count = order.Count,
                        Sum = order.Sum,
                        Status = order.Status.ToString(),
                        DateCreate = order.DateCreate,
                        DateImplement = order.DateImplement
                    } :
                    null;
            }
        }
        public void Insert(OrderBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                context.Orders.Add(CreateModel(model, new Order()));
                context.SaveChanges();
            }
        }
        public void Update(OrderBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                var order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);

                if (order == null)
                {
                    throw new Exception("Заказ не найден");
                }

                CreateModel(model, order);
                context.SaveChanges();
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                var order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);

                if (order == null)
                {
                    throw new Exception("Заказ не найден");
                }

                context.Orders.Remove(order);
                context.SaveChanges();
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
    }
}
