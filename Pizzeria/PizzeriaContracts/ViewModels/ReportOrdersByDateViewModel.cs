using System;

namespace PizzeriaContracts.ViewModels
{
    public class ReportOrdersByDateViewModel
    {
        public DateTime DateCreate { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
