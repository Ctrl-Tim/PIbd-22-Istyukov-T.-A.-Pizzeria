﻿using PizzeriaContracts.BindingModels;
using PizzeriaContracts.BusinessLogicsContracts;
using System;
using System.Windows.Forms;
using Unity;

namespace PizzeriaView
{
    public partial class FormMain : Form
    {
        private readonly IOrderLogic _orderLogic;

        public FormMain(IOrderLogic orderLogic)
        {
            InitializeComponent();
            _orderLogic = orderLogic;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = _orderLogic.Read(null);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ингредиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormIngredients>();
            form.ShowDialog();
        }

        private void пиццыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormPizzas>();
            form.ShowDialog();
        }

        private void складыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormStorages>();
            form.ShowDialog();
        }

        private void пополнениеСкладаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormStorageReplenishment>();
            form.ShowDialog();
        }

        private void ButtonCreateOrder_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormCreateOrder>();
            form.ShowDialog();
            LoadData();
        }

        private void ButtonTakeOrderInWork_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    _orderLogic.TakeOrderInWork(new ChangeStatusBindingModel { OrderId = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonOrderReady_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    _orderLogic.FinishOrder(new ChangeStatusBindingModel { OrderId = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonIssuedOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    _orderLogic.DeliveryOrder(new ChangeStatusBindingModel { OrderId = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
