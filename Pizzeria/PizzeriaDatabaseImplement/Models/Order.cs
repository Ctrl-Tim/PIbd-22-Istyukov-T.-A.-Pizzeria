﻿using System;
using PizzeriaContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int? ImplementerId { get; set; }

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

        public virtual Client Client { get; set; }

        public virtual Implementer Implementer { get; set; }
    }
}