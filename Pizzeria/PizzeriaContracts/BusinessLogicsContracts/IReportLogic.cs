using PizzeriaContracts.BindingModels;
using PizzeriaContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        /// </summary>
        /// Получение списка ингредиентов с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        List<ReportPizzaIngredientViewModel> GetPizzaIngredient();

        /// </summary>
        /// Получение списка складов с указанием, в каких ингредиентов и их количества
        /// </summary>
        /// <returns></returns>
        List<ReportStorageIngredientViewModel> GetStorageIngredient();

        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<ReportOrdersViewModel> GetOrders(ReportBindingModel model);

        /// <summary>
        /// Получение списка заказов за весь период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<ReportOrdersByDateViewModel> GetOrdersByDate();

        /// <summary>
        /// Сохранение ингредиентов в файл-Word
        /// </summary>
        /// <param name="model"></param>
        void SaveIngredientsToWordFile(ReportBindingModel model);

        /// <summary>
        /// Сохранение складов в файл-Word
        /// </summary>
        /// <param name="model"></param>
        void SaveStoragesToWordFile(ReportBindingModel model);

        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        void SavePizzaIngredientToExcelFile(ReportBindingModel model);

        /// <summary>
        /// Сохранение ингредиентов с указаеним складов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        void SaveStorageIngredientToExcelFile(ReportBindingModel model);

        /// <summary>
        /// Сохранение заказов в файл-Pdf 
        /// </summary>
        /// <param name="model"></param>
        void SaveOrdersToPdfFile(ReportBindingModel model);

        /// <summary>
        /// Сохранение заказов в файл-Pdf 
        /// </summary>
        /// <param name="model"></param>
        void SaveOrdersByDateToPdfFile(ReportBindingModel model);
    }
}
