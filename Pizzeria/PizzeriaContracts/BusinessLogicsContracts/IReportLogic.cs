using PizzeriaContracts.BindingModels;
using PizzeriaContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        /// </summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        List<ReportPizzaIngredientViewModel> GetPizzaIngredient(); 

        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<ReportOrdersViewModel> GetOrders(ReportBindingModel model); 

        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        void SaveIngredientsToWordFile(ReportBindingModel model); 

        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        void SavePizzaIngredientToExcelFile(ReportBindingModel model); 

        /// <summary>
        /// Сохранение заказов в файл-Pdf 
        /// </summary>
        /// <param name="model"></param>
        void SaveOrdersToPdfFile(ReportBindingModel model); 
    }
}
