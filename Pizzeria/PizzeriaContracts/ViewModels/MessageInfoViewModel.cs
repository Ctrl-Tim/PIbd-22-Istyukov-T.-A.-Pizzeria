using System;
using PizzeriaContracts.Attributes;

namespace PizzeriaContracts.ViewModels
{
    /// <summary>
    /// Сообщения, приходящие на почту
    /// </summary> 
    public class MessageInfoViewModel
    {
        [Column(title: "Номер", width: 100)]
        public string MessageId { get; set; }

        [Column(title: "Отправитель", width: 150)]
        public string SenderName { get; set; }

        [Column(title: "Дата письма", width: 50)]
        public DateTime DateDelivery { get; set; }

        [Column(title: "Заголовок", width: 150)]
        public string Subject { get; set; }

        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Body { get; set; }
    }
}
