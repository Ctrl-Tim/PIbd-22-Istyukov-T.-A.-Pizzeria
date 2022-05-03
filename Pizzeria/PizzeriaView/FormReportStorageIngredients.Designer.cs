namespace PizzeriaView
{
    partial class FormReportStorageIngredients
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewWarehouses = new System.Windows.Forms.DataGridView();
            this.buttonSaveExcel = new System.Windows.Forms.Button();
            this.ColumnWarehouse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnIngredient = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWarehouses)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewWarehouses
            // 
            this.dataGridViewWarehouses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewWarehouses.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewWarehouses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWarehouses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnWarehouse,
            this.ColumnIngredient,
            this.ColumnCount});
            this.dataGridViewWarehouses.Location = new System.Drawing.Point(10, 10);
            this.dataGridViewWarehouses.Name = "dataGridViewWarehouses";
            this.dataGridViewWarehouses.ReadOnly = true;
            this.dataGridViewWarehouses.RowHeadersWidth = 51;
            this.dataGridViewWarehouses.RowTemplate.Height = 25;
            this.dataGridViewWarehouses.Size = new System.Drawing.Size(539, 226);
            this.dataGridViewWarehouses.TabIndex = 3;
            // 
            // buttonSaveExcel
            // 
            this.buttonSaveExcel.Location = new System.Drawing.Point(210, 256);
            this.buttonSaveExcel.Name = "buttonSaveExcel";
            this.buttonSaveExcel.Size = new System.Drawing.Size(136, 32);
            this.buttonSaveExcel.TabIndex = 2;
            this.buttonSaveExcel.Text = "Сохранить в Excel";
            this.buttonSaveExcel.UseVisualStyleBackColor = true;
            this.buttonSaveExcel.Click += new System.EventHandler(this.buttonSaveExcel_Click);
            // 
            // ColumnWarehouse
            // 
            this.ColumnWarehouse.HeaderText = "Склад";
            this.ColumnWarehouse.MinimumWidth = 6;
            this.ColumnWarehouse.Name = "ColumnWarehouse";
            this.ColumnWarehouse.ReadOnly = true;
            // 
            // ColumnIngredient
            // 
            this.ColumnIngredient.HeaderText = "Ингредиент";
            this.ColumnIngredient.MinimumWidth = 6;
            this.ColumnIngredient.Name = "ColumnIngredient";
            this.ColumnIngredient.ReadOnly = true;
            // 
            // ColumnCount
            // 
            this.ColumnCount.HeaderText = "Количество";
            this.ColumnCount.MinimumWidth = 6;
            this.ColumnCount.Name = "ColumnCount";
            this.ColumnCount.ReadOnly = true;
            // 
            // FormReportStorageIngredients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 306);
            this.Controls.Add(this.dataGridViewWarehouses);
            this.Controls.Add(this.buttonSaveExcel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormReportStorageIngredients";
            this.Text = "Ингредиенты на складах";
            this.Load += new System.EventHandler(this.FormReportStorageIngredients_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWarehouses)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewWarehouses;
        private System.Windows.Forms.Button buttonSaveExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWarehouse;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIngredient;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCount;
    }
}