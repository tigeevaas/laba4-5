namespace AirportTicketSystem
{
    partial class PurchaseTicketForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbDestination;
        private System.Windows.Forms.TextBox txtPassport;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnPurchase;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.Label lblPassport;
        private System.Windows.Forms.Label lblName;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbDestination = new System.Windows.Forms.ComboBox();
            this.txtPassport = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnPurchase = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDestination = new System.Windows.Forms.Label();
            this.lblPassport = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbDestination
            // 
            this.cmbDestination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDestination.FormattingEnabled = true;
            this.cmbDestination.Location = new System.Drawing.Point(140, 92);
            this.cmbDestination.Name = "cmbDestination";
            this.cmbDestination.Size = new System.Drawing.Size(160, 21);
            this.cmbDestination.TabIndex = 2;
            // 
            // txtPassport
            // 
            this.txtPassport.Location = new System.Drawing.Point(140, 57);
            this.txtPassport.Name = "txtPassport";
            this.txtPassport.Size = new System.Drawing.Size(160, 20);
            this.txtPassport.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(140, 22);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(160, 20);
            this.txtName.TabIndex = 0;
            // 
            // btnPurchase
            // 
            this.btnPurchase.Location = new System.Drawing.Point(125, 150);
            this.btnPurchase.Name = "btnPurchase";
            this.btnPurchase.Size = new System.Drawing.Size(90, 30);
            this.btnPurchase.TabIndex = 3;
            this.btnPurchase.Text = "Купить билет";
            this.btnPurchase.UseVisualStyleBackColor = true;
            this.btnPurchase.Click += new System.EventHandler(this.btnPurchase_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(220, 150);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.Location = new System.Drawing.Point(20, 95);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(80, 13);
            this.lblDestination.TabIndex = 5;
            this.lblDestination.Text = "Направление:";
            // 
            // lblPassport
            // 
            this.lblPassport.AutoSize = true;
            this.lblPassport.Location = new System.Drawing.Point(20, 60);
            this.lblPassport.Name = "lblPassport";
            this.lblPassport.Size = new System.Drawing.Size(105, 13);
            this.lblPassport.TabIndex = 6;
            this.lblPassport.Text = "Номер паспорта:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(20, 25);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(99, 13);
            this.lblName.TabIndex = 7;
            this.lblName.Text = "Имя пассажира:";
            // 
            // PurchaseTicketForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 210);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblPassport);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPurchase);
            this.Controls.Add(this.cmbDestination);
            this.Controls.Add(this.txtPassport);
            this.Controls.Add(this.txtName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PurchaseTicketForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Покупка билета";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}