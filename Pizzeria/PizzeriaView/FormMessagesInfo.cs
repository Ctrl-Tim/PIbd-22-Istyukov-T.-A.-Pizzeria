using PizzeriaContracts.BusinessLogicsContracts;
using System;
using System.Windows.Forms;

namespace PizzeriaView
{
    public partial class FormMessagesInfo : Form
    {
        private readonly IMessageInfoLogic _messageInfoLogic;

        public FormMessagesInfo(IMessageInfoLogic messageInfoLogic)
        {
            InitializeComponent();
            _messageInfoLogic = messageInfoLogic;
        }

        private void FormMessagesInfo_Load(object sender, EventArgs e)
        {
            try
            {
                Program.ConfigGrid(_messageInfoLogic.Read(null), dataGridView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
