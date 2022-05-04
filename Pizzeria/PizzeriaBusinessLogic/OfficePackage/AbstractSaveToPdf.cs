using PizzeriaBusinessLogic.OfficePackage.HelperEnums;
using PizzeriaBusinessLogic.OfficePackage.HelperModels;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdf
    {
        public void CreateDoc(PdfInfo info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph { Text = info.Title, Style = "NormalTitle" });
            CreateParagraph(new PdfParagraph { Text = $"с {info.DateFrom.ToShortDateString()} по {info.DateTo.ToShortDateString()}", Style = "Normal" });

            CreateTable(new List<string> { "3cm", "6cm", "3cm", "2cm", "3cm" });

            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Дата заказа", "Пицца", "Количество", "Сумма", "Статус" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });

            foreach (var order in info.Orders)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { order.DateCreate.ToShortDateString(), order.PizzaName, order.Count.ToString(), order.Sum.ToString(), order.Status.ToString() },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }
            decimal sum = info.Orders.Sum(rec => rec.Sum);
            CreateParagraph(new PdfParagraph
            {
                Text = $"Итого: {sum}",
                Style = "NormalTitle",
            });

            SavePdf(info);
        }

        public void CreateDocOrdersByDate(PdfInfo info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });
            CreateParagraph(new PdfParagraph
            {
                Text = $"Заказы по датам",
                Style = "Normal"
            });
            CreateTable(new List<string> { "3cm", "3cm", "3cm" });
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Дата заказов", "Количество заказов", "Сумма" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            foreach (var order in info.OrdersByDate)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { order.DateCreate.ToShortDateString(), order.Count.ToString(), order.Sum.ToString() },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }
            decimal sum = info.OrdersByDate.Sum(rec => rec.Sum);
            CreateParagraph(new PdfParagraph
            {
                Text = $"Итого: {sum}",
                Style = "NormalTitle"
            });
            SavePdf(info);
        }

        /// <summary> 
        /// Создание doc-файла
        /// </summary> 
        /// <param name="info"></param>
        protected abstract void CreatePdf(PdfInfo info); 
 
        /// <summary>
        /// Создание параграфа с текстом
        /// </summary>
        /// <param name="title"></param>
        /// <param name="style"></param>
        protected abstract void CreateParagraph(PdfParagraph paragraph); 
 
        /// <summary>
        /// Создание таблицы 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="style"></param>
        protected abstract void CreateTable(List<string> columns); 
 
        /// <summary>
        /// Создание и заполнение строки
        /// </summary>
        /// <param name="rowParameters"></param>
        protected abstract void CreateRow(PdfRowParameters rowParameters); 
 
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SavePdf(PdfInfo info); 
    }
}
