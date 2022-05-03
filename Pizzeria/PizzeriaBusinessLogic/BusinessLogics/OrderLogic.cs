using PizzeriaContracts.BusinessLogicsContracts;
using PizzeriaContracts.BindingModels;
using PizzeriaContracts.Enums;
using PizzeriaContracts.ViewModels;
using PizzeriaContracts.StoragesContracts;
using System;
using System.Collections.Generic;

namespace PizzeriaBusinessLogic.BusinessLogics
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderStorage _orderStorage;
        private readonly IStorageStorage _storageStorage;
        private readonly IPizzaStorage _pizzaStorage;

        public OrderLogic(IOrderStorage orderStorage, IStorageStorage storageStorage, IPizzaStorage pizzaStorage)
        {
            _orderStorage = orderStorage;
            _storageStorage = storageStorage;
            _pizzaStorage = pizzaStorage;
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return _orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderViewModel> { _orderStorage.GetElement(model) };
            }

            return _orderStorage.GetFilteredList(model);
        }

        public void CreateOrder(CreateOrderBindingModel model)
        {
            _orderStorage.Insert(new OrderBindingModel
            {
                PizzaId = model.PizzaId,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Принят
            });
        }

        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel{ Id = model.OrderId });

            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }

            if (order.Status != OrderStatus.Принят.ToString())
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }

            var pizza = _pizzaStorage.GetElement(new PizzaBindingModel { Id = order.PizzaId });
            if (!_storageStorage.CheckIngredientsCount(order.Count, pizza.PizzaIngredients))
            {
                throw new Exception("Недостаточно ингредиентов на складе!");
            }

            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                PizzaId = order.PizzaId,
                PizzaName = order.PizzaName,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = DateTime.Now,
                Status = OrderStatus.Выполняется
            });
        }
        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel{ Id = model.OrderId });

            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }

            if (order.Status != OrderStatus.Выполняется.ToString())
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }

            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                PizzaId = order.PizzaId,
                PizzaName = order.PizzaName,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Готов
            });
        }
        public void DeliveryOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel{ Id = model.OrderId });
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Готов.ToString())
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                PizzaId = order.PizzaId,
                PizzaName = order.PizzaName,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Выдан
            });
        }
    }
}
