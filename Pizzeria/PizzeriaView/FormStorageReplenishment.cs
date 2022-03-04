using PizzeriaContracts.BindingModels;
using PizzeriaBusinessLogic.BusinessLogics;
using PizzeriaContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace PizzeriaView
{
    public partial class FormStorageReplenishment : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int MaterialId
        {
            get
            {
                return Convert.ToInt32(comboBoxIngredient.SelectedValue);
            }
            set
            {
                comboBoxIngredient.SelectedValue = value;
            }
        }

        public int Storage
        {
            get
            {
                return Convert.ToInt32(comboBoxIngredient.SelectedValue);
            }
            set
            {
                comboBoxIngredient.SelectedValue = value;
            }
        }

        public int Count
        {
            get
            {
                return Convert.ToInt32(textBoxCount.Text);
            }
            set
            {
                textBoxCount.Text = value.ToString();
            }
        }

        private readonly StorageLogic _storageLogic;

        public FormStorageReplenishment(IngredientLogic ingredientLogic, StorageLogic storageLogic)
        {
            InitializeComponent();

            _storageLogic = storageLogic;

            List<IngredientViewModel> listIngredients = ingredientLogic.Read(null);

            if (listIngredients != null)
            {
                comboBoxIngredient.DisplayMember = "IngredientName";
                comboBoxIngredient.ValueMember = "Id";
                comboBoxIngredient.DataSource = listIngredients;
                comboBoxIngredient.SelectedItem = null;
            }

            List<StorageViewModel> listStorages = storageLogic.Read(null);

            if (listStorages != null)
            {
                comboBoxName.DisplayMember = "StorageName";
                comboBoxName.ValueMember = "Id";
                comboBoxName.DataSource = listStorages;
                comboBoxName.SelectedItem = null;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxName.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxIngredient.SelectedValue == null)
            {
                MessageBox.Show("Выберите ингредиент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Неизвестное количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _storageLogic.Replenishment(new ReplenishStorageBindingModel
            {
                StorageId = Convert.ToInt32(comboBoxName.SelectedValue),
                IngredientId = Convert.ToInt32(comboBoxIngredient.SelectedValue),
                Count = Convert.ToInt32(textBoxCount.Text)
            });

            DialogResult = DialogResult.OK;

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
