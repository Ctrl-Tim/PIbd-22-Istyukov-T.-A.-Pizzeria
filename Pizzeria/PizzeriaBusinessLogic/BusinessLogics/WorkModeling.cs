﻿using PizzeriaContracts.BindingModels;
using PizzeriaContracts.BusinessLogicsContracts;
using PizzeriaContracts.Enums;
using PizzeriaContracts.ViewModels;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace PizzeriaBusinessLogic.BusinessLogics
{
   public class WorkModeling : IWorkProcess
   {
        private IOrderLogic _orderLogic;

        private readonly Random rnd;

        public WorkModeling()
        {
            rnd = new Random(1000);
        }

        /// <summary>
        /// Запуск работ
        /// </summary>
        public void DoWork(IImplementerLogic implementerLogic, IOrderLogic orderLogic)
        {
            _orderLogic = orderLogic;
            var implementers = implementerLogic.Read(null); 

            ConcurrentBag<OrderViewModel> orders = new(_orderLogic.Read(new OrderBindingModel { SearchStatus = OrderStatus.Принят })); 
 
            foreach (var implementer in implementers)
            {
                Task.Run(async () => await WorkerWorkAsync(implementer, orders));
            }
        }

        /// <summary> 
        /// Иммитация работы исполнителя
        /// </summary>
        /// <param name="implementer"></param>
        /// <param name="orders"></param>
        private async Task WorkerWorkAsync(ImplementerViewModel implementer, ConcurrentBag<OrderViewModel> orders)
        {
            // ищем заказы, которые уже в работе (вдруг исполнителя прервали)
            var runOrders = await Task.Run(() => _orderLogic.Read(new OrderBindingModel
            {
                ImplementerId = implementer.Id,
                Status = OrderStatus.Выполняется
            }));
            foreach (var order in runOrders)
            {
                // делаем работу заново
                Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count); 
                _orderLogic.FinishOrder(new ChangeStatusBindingModel { OrderId = order.Id, ImplementerId = order.ImplementerId });

                // отдыхаем
                Thread.Sleep(implementer.PauseTime);
            }

            // ищем заказы, которым не хватает материалов
            var ordersMaterialsRequired = await Task.Run(() => _orderLogic.Read(new OrderBindingModel
            {
                SearchStatus = OrderStatus.ТребуютсяМатериалы
            }));
            foreach (var order in ordersMaterialsRequired)
            {
                try
                {
                    _orderLogic.TakeOrderInWork(new ChangeStatusBindingModel
                    {
                        OrderId = order.Id,
                        ImplementerId = implementer.Id
                    });
                    if (_orderLogic.Read(new OrderBindingModel { Id = order.Id })?[0].Status == "Требуются_материалы")
                    {
                        continue;
                    }
                    Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count);
                    _orderLogic.FinishOrder(new ChangeStatusBindingModel
                    {
                        OrderId = order.Id
                    });
                    Thread.Sleep(implementer.PauseTime);
                }
                catch (Exception) { }
            }

            await Task.Run(() =>
            {
                while (!orders.IsEmpty)
                {
                    if (orders.TryTake(out OrderViewModel order))
                    {
                        // пытаемся назначить заказ на исполнителя
                        _orderLogic.TakeOrderInWork(new ChangeStatusBindingModel { OrderId = order.Id, ImplementerId = implementer.Id }); 

                        // делаем работу
                        Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count); 
                        _orderLogic.FinishOrder(new ChangeStatusBindingModel { OrderId = order.Id, ImplementerId = implementer.Id });

                        // отдыхаем
                        Thread.Sleep(implementer.PauseTime);
                    }
                }
            });
        }  
    }
}