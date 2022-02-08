﻿using System;
using System.ComponentModel;

namespace PizzeriaContracts.ViewModels
{
    /// <summary>
    /// Заказ
    /// </summary> 
    class OrderViewModel
    {
        public int Id { get; set; }

        public int PizzaId { get; set; }

        [DisplayName("Пицца")]
        public string PizzaName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }

        [DisplayName("Сумма")]
        public decimal Sum { get; set; }

        [DisplayName("Статус")]
        public string Status { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}