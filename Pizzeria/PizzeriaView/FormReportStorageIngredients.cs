using PizzeriaContracts.BindingModels;
using PizzeriaContracts.BusinessLogicsContracts;
using System;
using System.Windows.Forms;

namespace PizzeriaView
{
    public partial class FormReportStorageIngredients : Form
    {
        private readonly IReportLogic logic;

        public FormReportStorageIngredients(IReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormReportStorageIngredients_Load(object sender, EventArgs e)
        {
            try
            {
                var dict = logic.GetStorageIngredient();
                if (dict != null)
                {
                    dataGridViewWarehouses.Rows.Clear();
                    foreach (var elem in dict)
                    {
                        dataGridViewWarehouses.Rows.Add(new object[] { elem.StorageName, "", "" });
                        foreach (var listElem in elem.Ingredients)
                        {
                            dataGridViewWarehouses.Rows.Add(new object[] { "", listElem.Item1, listElem.Item2 });
                        }
                        dataGridViewWarehouses.Rows.Add(new object[] { "Итого", "", elem.TotalCount });
                        dataGridViewWarehouses.Rows.Add(Array.Empty<object>());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSaveExcel_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    logic.SaveStorageIngredientToExcelFile(new ReportBindingModel
                    {
                        FileName = dialog.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
