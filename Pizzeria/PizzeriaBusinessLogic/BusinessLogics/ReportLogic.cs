using PizzeriaBusinessLogic.OfficePackage;
using PizzeriaBusinessLogic.OfficePackage.HelperModels;
using PizzeriaContracts.BindingModels;
using PizzeriaContracts.BusinessLogicsContracts;
using PizzeriaContracts.StoragesContracts;
using PizzeriaContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IIngredientStorage _ingredientStorage;

        private readonly IPizzaStorage _pizzaStorage;

        private readonly IOrderStorage _orderStorage;

        private readonly AbstractSaveToExcel _saveToExcel;

        private readonly AbstractSaveToWord _saveToWord;

        private readonly AbstractSaveToPdf _saveToPdf;

        public ReportLogic(IPizzaStorage pizzaStorage, IIngredientStorage ingredientStorage, IOrderStorage orderStorage,
                            AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord, AbstractSaveToPdf saveToPdf)
        {
            _pizzaStorage = pizzaStorage;
            _ingredientStorage = ingredientStorage;
            _orderStorage = orderStorage;

            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        } 
 
        /// <summary>
        /// Получение списка компонент с указанием цены
        /// </summary> 
        /// <returns></returns>
        public List<ReportPizzaIngredientViewModel> GetPizzaIngredient()
        {
            var ingredients = _ingredientStorage.GetFullList(); 
 
            var pizzas = _pizzaStorage.GetFullList();

            var list = new List<ReportPizzaIngredientViewModel>(); 
 
            foreach(var pizza in pizzas)
            {
                var record = new ReportPizzaIngredientViewModel
                {
                    PizzaName = pizza.PizzaName,
                    Ingredients = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var ingredient in ingredients)
                {
                    if(pizza.PizzaIngredients.ContainsKey(ingredient.Id))
                    {
                        record.Ingredients.Add(new Tuple<string, int>(ingredient.IngredientName, pizza.PizzaIngredients[ingredient.Id].Item2));
                        record.TotalCount += pizza.PizzaIngredients[ingredient.Id].Item2;
                    }
                }

                list.Add(record);
            } 
 
            return list;
        } 
 
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel { DateFrom = model.DateFrom, DateTo = model.DateTo })
                .Select(x => new ReportOrdersViewModel
                {
                    DateCreate = x.DateCreate,
                    PizzaName = x.PizzaName,
                    Count = x.Count,
                    Sum = x.Sum,
                    Status = x.Status.ToString()
                })
                .ToList();
        } 
 
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveIngredientsToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список пицц",
                Pizzas = _pizzaStorage.GetFullList()
            });
        } 
 
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SavePizzaIngredientToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список ингредиентов",
                PizzaIngredients = GetPizzaIngredient()
            });
        } 
 
        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }   
    }
}
