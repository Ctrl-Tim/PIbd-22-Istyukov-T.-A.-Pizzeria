using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PizzeriaContracts.BusinessLogicsContracts;
using PizzeriaContracts.BindingModels;

namespace PizzeriaView
{
    public partial class FormClients : Form
    {
        private readonly IClientLogic _logic;

        public FormClients(IClientLogic logic)
        {
            InitializeComponent();
            this._logic = logic;
        }

        private void LoadData()
        {
            var list = _logic.Read(null);
            if (list != null)
            {
                dataGridViewClients.DataSource = list;
                dataGridViewClients.Columns[0].Visible = false;
                dataGridViewClients.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void FormClients_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewClients.SelectedRows.Count == 1)
            {

                if (MessageBox.Show("Удалить клиента", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridViewClients.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        _logic.Delete(new ClientBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
    }
}
