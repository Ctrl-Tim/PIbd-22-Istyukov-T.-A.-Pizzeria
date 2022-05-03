using PizzeriaBusinessLogic.OfficePackage.HelperEnums;
using PizzeriaBusinessLogic.OfficePackage.HelperModels;
using System.Collections.Generic;

namespace PizzeriaBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWord
    {
        public void CreateDoc(WordInfo info)
        {
            CreateWord(info);

            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });

            foreach (var pizza in info.Pizzas)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> {(pizza.PizzaName, new WordTextProperties{Bold = true, Size = "24", }),
                        (" Цена: " + pizza.Price.ToString(), new WordTextProperties {Bold = false, Size = "24"})},
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
            }

            SaveWord(info);
        }

        public void CreateDocStorage(WordInfo info)
        {
            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24" }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            CreateTable(new List<string>() { "Название", "ФИО ответственного", "Дата создания" });
            foreach (var storage in info.Storages)
            {
                AddRowTable(new List<string>() {
                    storage.StorageName,
                    storage.StorageManager,
                    storage.DateCreate.ToShortDateString()
                });
            }
            SaveWord(info);
        }

        /// <summary>
        /// Создание doc-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreateWord(WordInfo info); 
 
        /// <summary>
        /// Создание абзаца с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns> 

        protected abstract void CreateParagraph(WordParagraph paragraph);

        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SaveWord(WordInfo info);

        /// <summary>
        /// Сохранение таблицы
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreateTable(List<string> tableHeaderInfo);

        /// <summary>
        /// Создание новой строки в таблице
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns> 
        protected abstract void AddRowTable(List<string> tableRowInfo);
    }
}
