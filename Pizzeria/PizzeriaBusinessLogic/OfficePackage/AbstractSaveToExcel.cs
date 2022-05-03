using PizzeriaBusinessLogic.OfficePackage.HelperEnums;
using PizzeriaBusinessLogic.OfficePackage.HelperModels;

namespace PizzeriaBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToExcel
    {
        /// <summary>
        /// Создание отчета
        /// </summary>
        /// <param name="info"></param>
        public void CreateReport(ExcelInfo info)
        {
            CreateExcel(info); 

                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = 1,
                    Text = info.Title,
                    StyleInfo = ExcelStyleInfoType.Title
                }); 
 
                 MergeCells(new ExcelMergeParameters
                 {
                     CellFromName = "A1",
                     CellToName = "C1"
                 });

                uint rowIndex = 2;
                foreach (var pizza in info.PizzaIngredients)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "A",
                        RowIndex = rowIndex,
                        Text = pizza.PizzaName,
                        StyleInfo = ExcelStyleInfoType.Text
                    });
                    rowIndex++;

                    foreach (var ingredient in pizza.Ingredients)
                    {
                        InsertCellInWorksheet(new ExcelCellParameters
                        {
                            ColumnName = "B",
                            RowIndex = rowIndex,
                            Text = ingredient.Item1,
                            StyleInfo = ExcelStyleInfoType.TextWithBroder
                        });

                        InsertCellInWorksheet(new ExcelCellParameters
                        {
                            ColumnName = "C",
                            RowIndex = rowIndex,
                            Text = ingredient.Item2.ToString(),
                            StyleInfo = ExcelStyleInfoType.TextWithBroder
                        });

                        rowIndex++;
                    }

                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "C",
                        RowIndex = rowIndex,
                        Text = pizza.TotalCount.ToString(),
                        StyleInfo = ExcelStyleInfoType.Text
                    });
                    rowIndex++;
                }

                SaveExcel(info);
        }

        public void CreateReportStorage(ExcelInfo info)
        {
            CreateExcel(info);
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });
            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });
            uint rowIndex = 2;
            foreach (var comp in info.StorageIngredients)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = comp.StorageName,
                    StyleInfo = ExcelStyleInfoType.Text
                });
                rowIndex++;
                foreach (var product in comp.Ingredients)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "B",
                        RowIndex = rowIndex,
                        Text = product.Item1,
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "C",
                        RowIndex = rowIndex,
                        Text = product.Item2.ToString(),
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });
                    rowIndex++;
                }
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = "Итого",
                    StyleInfo = ExcelStyleInfoType.Text
                });
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "C",
                    RowIndex = rowIndex,
                    Text = comp.TotalCount.ToString(),
                    StyleInfo = ExcelStyleInfoType.Text
                });
                rowIndex++;
            }
            SaveExcel(info);
        }

        /// <summary>
        /// Создание excel-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreateExcel(ExcelInfo info); 
 
        /// <summary>
        /// Добавляем новую ячейку в лист
        /// </summary>
        /// <param name="cellParameters"></param>
        protected abstract void InsertCellInWorksheet(ExcelCellParameters excelParams); 
 
        /// <summary>
        /// Объединение ячеек
        /// </summary>
        /// <param name="mergeParameters"></param>
        protected abstract void MergeCells(ExcelMergeParameters excelParams); 
 
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SaveExcel(ExcelInfo info);  
    }
}
