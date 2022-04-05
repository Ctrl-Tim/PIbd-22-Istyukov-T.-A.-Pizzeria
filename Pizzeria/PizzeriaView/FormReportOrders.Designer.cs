
namespace PizzeriaView
{
    partial class FormReportOrders
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
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.ButtonMake = new System.Windows.Forms.Button();
            this.ButtonToPdf = new System.Windows.Forms.Button();
            this.labelFrom = new System.Windows.Forms.Label();
            this.labelTo = new System.Windows.Forms.Label();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panel = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Location = new System.Drawing.Point(36, 6);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(200, 23);
            this.dateTimePickerFrom.TabIndex = 0;
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Location = new System.Drawing.Point(282, 6);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(200, 23);
            this.dateTimePickerTo.TabIndex = 1;
            // 
            // ButtonMake
            // 
            this.ButtonMake.Location = new System.Drawing.Point(526, 6);
            this.ButtonMake.Name = "ButtonMake";
            this.ButtonMake.Size = new System.Drawing.Size(114, 23);
            this.ButtonMake.TabIndex = 2;
            this.ButtonMake.Text = "Сформировать";
            this.ButtonMake.UseVisualStyleBackColor = true;
            this.ButtonMake.Click += new System.EventHandler(this.ButtonMake_Click);
            // 
            // ButtonToPdf
            // 
            this.ButtonToPdf.Location = new System.Drawing.Point(657, 6);
            this.ButtonToPdf.Name = "ButtonToPdf";
            this.ButtonToPdf.Size = new System.Drawing.Size(110, 23);
            this.ButtonToPdf.TabIndex = 3;
            this.ButtonToPdf.Text = "В Pdf";
            this.ButtonToPdf.UseVisualStyleBackColor = true;
            this.ButtonToPdf.Click += new System.EventHandler(this.ButtonToPdf_Click);
            // 
            // labelFrom
            // 
            this.labelFrom.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelFrom.Location = new System.Drawing.Point(4, 4);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(26, 37);
            this.labelFrom.TabIndex = 4;
            this.labelFrom.Text = "С";
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTo.Location = new System.Drawing.Point(242, 4);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(34, 25);
            this.labelTo.TabIndex = 5;
            this.labelTo.Text = "по";
            // 
            // panelMenu
            // 
            this.panelMenu.Controls.Add(this.ButtonToPdf);
            this.panelMenu.Controls.Add(this.ButtonMake);
            this.panelMenu.Controls.Add(this.dateTimePickerTo);
            this.panelMenu.Controls.Add(this.labelTo);
            this.panelMenu.Controls.Add(this.labelFrom);
            this.panelMenu.Controls.Add(this.dateTimePickerFrom);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(776, 35);
            this.panelMenu.TabIndex = 5;
            // 
            // panel
            // 
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.Location = new System.Drawing.Point(0, 35);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(776, 487);
            this.panel.TabIndex = 6;
            // 
            // FormReportOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 525);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.panelMenu);
            this.Name = "FormReportOrders";
            this.Text = "Заказы";
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Button ButtonMake;
        private System.Windows.Forms.Button ButtonToPdf;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panel;
    }
}