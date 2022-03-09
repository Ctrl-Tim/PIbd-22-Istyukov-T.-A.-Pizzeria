﻿using System;
using PizzeriaContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int PizzaId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public decimal Sum { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }

        public virtual Pizza Pizza { get; set; }
    }
}