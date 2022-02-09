using PizzeriaContracts.BusinessLogicsContracts;
using PizzeriaContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PizzeriaView
{
    public partial class FormPizzaIngredient : Form
    {
        public int Id { get { return Convert.ToInt32(comboBoxIngredient.SelectedValue); } set { comboBoxIngredient.SelectedValue = value; } }

        public string IngredientName { get { return comboBoxIngredient.Text; } }

        public int Count { get { return Convert.ToInt32(textBoxCount.Text); } set { textBoxCount.Text = value.ToString(); } }

        public FormPizzaIngredient(IIngredientLogic logic)
        {
            InitializeComponent();

            List<IngredientViewModel> list = logic.Read(null);
            if (list != null)
            {
                comboBoxIngredient.DisplayMember = "IngredientName";
                comboBoxIngredient.ValueMember = "Id";
                comboBoxIngredient.DataSource = list;
                comboBoxIngredient.SelectedItem = null;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxIngredient.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
