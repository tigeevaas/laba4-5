namespace AirportTicketSystem
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvFares;
        private System.Windows.Forms.DataGridView dgvTickets;
        private System.Windows.Forms.Button btnAddFare;
        private System.Windows.Forms.Button btnPurchaseTicket;
        private System.Windows.Forms.Button btnCalculatePassenger;
        private System.Windows.Forms.TextBox txtPassportNumber;
        private System.Windows.Forms.Label lblPassport;
        private System.Windows.Forms.Label lblTotalSales;
        private System.Windows.Forms.Label lblTotalTickets;
        private System.Windows.Forms.Button btnSaveToFile;
        private System.Windows.Forms.Button btnLoadFromFile;
        private System.Windows.Forms.Button btnSortByPrice;
        private System.Windows.Forms.Button btnSortByDestination;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnViewStatistics;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestination;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewButtonColumn colDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPassengerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPassport;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTicketDestination;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTicketPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPurchaseDate;

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
            this.dgvFares = new System.Windows.Forms.DataGridView();
            this.colDestination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dgvTickets = new System.Windows.Forms.DataGridView();
            this.colPassengerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPassport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTicketDestination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTicketPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPurchaseDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddFare = new System.Windows.Forms.Button();
            this.btnPurchaseTicket = new System.Windows.Forms.Button();
            this.btnCalculatePassenger = new System.Windows.Forms.Button();
            this.txtPassportNumber = new System.Windows.Forms.TextBox();
            this.lblPassport = new System.Windows.Forms.Label();
            this.lblTotalSales = new System.Windows.Forms.Label();
            this.lblTotalTickets = new System.Windows.Forms.Label();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.btnLoadFromFile = new System.Windows.Forms.Button();
            this.btnSortByPrice = new System.Windows.Forms.Button();
            this.btnSortByDestination = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnViewStatistics = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTickets)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvFares
            // 
            this.dgvFares.AllowUserToAddRows = false;
            this.dgvFares.AllowUserToDeleteRows = false;
            this.dgvFares.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFares.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDestination,
            this.colPrice,
            this.colDelete});
            this.dgvFares.Location = new System.Drawing.Point(20, 60);
            this.dgvFares.Name = "dgvFares";
            this.dgvFares.ReadOnly = true;
            this.dgvFares.RowHeadersVisible = false;
            this.dgvFares.Size = new System.Drawing.Size(450, 400);
            this.dgvFares.TabIndex = 0;
            this.dgvFares.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFares_CellClick);
            // 
            // colDestination
            // 
            this.colDestination.HeaderText = "Направление";
            this.colDestination.Name = "colDestination";
            this.colDestination.ReadOnly = true;
            this.colDestination.Width = 150;
            // 
            // colPrice
            // 
            this.colPrice.HeaderText = "Цена ($)";
            this.colPrice.Name = "colPrice";
            this.colPrice.ReadOnly = true;
            // 
            // colDelete
            // 
            this.colDelete.HeaderText = "";
            this.colDelete.Name = "colDelete";
            this.colDelete.ReadOnly = true;
            this.colDelete.Text = "Удалить";
            this.colDelete.UseColumnTextForButtonValue = true;
            this.colDelete.Width = 70;
            // 
            // dgvTickets
            // 
            this.dgvTickets.AllowUserToAddRows = false;
            this.dgvTickets.AllowUserToDeleteRows = false;
            this.dgvTickets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTickets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPassengerName,
            this.colPassport,
            this.colTicketDestination,
            this.colTicketPrice,
            this.colPurchaseDate});
            this.dgvTickets.Location = new System.Drawing.Point(500, 60);
            this.dgvTickets.Name = "dgvTickets";
            this.dgvTickets.ReadOnly = true;
            this.dgvTickets.RowHeadersVisible = false;
            this.dgvTickets.Size = new System.Drawing.Size(600, 400);
            this.dgvTickets.TabIndex = 1;
            // 
            // colPassengerName
            // 
            this.colPassengerName.HeaderText = "Пассажир";
            this.colPassengerName.Name = "colPassengerName";
            this.colPassengerName.ReadOnly = true;
            this.colPassengerName.Width = 120;
            // 
            // colPassport
            // 
            this.colPassport.HeaderText = "Паспорт";
            this.colPassport.Name = "colPassport";
            this.colPassport.ReadOnly = true;
            this.colPassport.Width = 80;
            // 
            // colTicketDestination
            // 
            this.colTicketDestination.HeaderText = "Направление";
            this.colTicketDestination.Name = "colTicketDestination";
            this.colTicketDestination.ReadOnly = true;
            this.colTicketDestination.Width = 120;
            // 
            // colTicketPrice
            // 
            this.colTicketPrice.HeaderText = "Цена ($)";
            this.colTicketPrice.Name = "colTicketPrice";
            this.colTicketPrice.ReadOnly = true;
            // 
            // colPurchaseDate
            // 
            this.colPurchaseDate.HeaderText = "Дата покупки";
            this.colPurchaseDate.Name = "colPurchaseDate";
            this.colPurchaseDate.ReadOnly = true;
            this.colPurchaseDate.Width = 120;
            // 
            // btnAddFare
            // 
            this.btnAddFare.Location = new System.Drawing.Point(20, 30);
            this.btnAddFare.Name = "btnAddFare";
            this.btnAddFare.Size = new System.Drawing.Size(120, 40);
            this.btnAddFare.TabIndex = 2;
            this.btnAddFare.Text = "Добавить тариф";
            this.btnAddFare.UseVisualStyleBackColor = true;
            this.btnAddFare.Click += new System.EventHandler(this.btnAddFare_Click);
            // 
            // btnPurchaseTicket
            // 
            this.btnPurchaseTicket.Location = new System.Drawing.Point(150, 30);
            this.btnPurchaseTicket.Name = "btnPurchaseTicket";
            this.btnPurchaseTicket.Size = new System.Drawing.Size(120, 40);
            this.btnPurchaseTicket.TabIndex = 3;
            this.btnPurchaseTicket.Text = "Купить билет";
            this.btnPurchaseTicket.UseVisualStyleBackColor = true;
            this.btnPurchaseTicket.Click += new System.EventHandler(this.btnPurchaseTicket_Click);
            // 
            // btnCalculatePassenger
            // 
            this.btnCalculatePassenger.Location = new System.Drawing.Point(20, 30);
            this.btnCalculatePassenger.Name = "btnCalculatePassenger";
            this.btnCalculatePassenger.Size = new System.Drawing.Size(150, 40);
            this.btnCalculatePassenger.TabIndex = 4;
            this.btnCalculatePassenger.Text = "Сумма пассажира";
            this.btnCalculatePassenger.UseVisualStyleBackColor = true;
            this.btnCalculatePassenger.Click += new System.EventHandler(this.btnCalculatePassenger_Click);
            // 
            // txtPassportNumber
            // 
            this.txtPassportNumber.Location = new System.Drawing.Point(180, 37);
            this.txtPassportNumber.Name = "txtPassportNumber";
            this.txtPassportNumber.Size = new System.Drawing.Size(120, 20);
            this.txtPassportNumber.TabIndex = 5;
            // 
            // lblPassport
            // 
            this.lblPassport.AutoSize = true;
            this.lblPassport.Location = new System.Drawing.Point(177, 20);
            this.lblPassport.Name = "lblPassport";
            this.lblPassport.Size = new System.Drawing.Size(123, 13);
            this.lblPassport.TabIndex = 6;
            this.lblPassport.Text = "Номер паспорта (6-12):";
            // 
            // lblTotalSales
            // 
            this.lblTotalSales.AutoSize = true;
            this.lblTotalSales.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTotalSales.Location = new System.Drawing.Point(20, 470);
            this.lblTotalSales.Name = "lblTotalSales";
            this.lblTotalSales.Size = new System.Drawing.Size(136, 15);
            this.lblTotalSales.TabIndex = 7;
            this.lblTotalSales.Text = "Общая сумма продаж:";
            // 
            // lblTotalTickets
            // 
            this.lblTotalTickets.AutoSize = true;
            this.lblTotalTickets.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTotalTickets.Location = new System.Drawing.Point(20, 490);
            this.lblTotalTickets.Name = "lblTotalTickets";
            this.lblTotalTickets.Size = new System.Drawing.Size(132, 15);
            this.lblTotalTickets.TabIndex = 8;
            this.lblTotalTickets.Text = "Всего продано билетов:";
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.Location = new System.Drawing.Point(280, 30);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(100, 40);
            this.btnSaveToFile.TabIndex = 9;
            this.btnSaveToFile.Text = "Сохранить";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            // 
            // btnLoadFromFile
            // 
            this.btnLoadFromFile.Location = new System.Drawing.Point(390, 30);
            this.btnLoadFromFile.Name = "btnLoadFromFile";
            this.btnLoadFromFile.Size = new System.Drawing.Size(100, 40);
            this.btnLoadFromFile.TabIndex = 10;
            this.btnLoadFromFile.Text = "Загрузить";
            this.btnLoadFromFile.UseVisualStyleBackColor = true;
            this.btnLoadFromFile.Click += new System.EventHandler(this.btnLoadFromFile_Click);
            // 
            // btnSortByPrice
            // 
            this.btnSortByPrice.Location = new System.Drawing.Point(20, 80);
            this.btnSortByPrice.Name = "btnSortByPrice";
            this.btnSortByPrice.Size = new System.Drawing.Size(120, 40);
            this.btnSortByPrice.TabIndex = 12;
            this.btnSortByPrice.Text = "Сорт. по цене";
            this.btnSortByPrice.UseVisualStyleBackColor = true;
            this.btnSortByPrice.Click += new System.EventHandler(this.btnSortByPrice_Click);
            // 
            // btnSortByDestination
            // 
            this.btnSortByDestination.Location = new System.Drawing.Point(150, 80);
            this.btnSortByDestination.Name = "btnSortByDestination";
            this.btnSortByDestination.Size = new System.Drawing.Size(120, 40);
            this.btnSortByDestination.TabIndex = 13;
            this.btnSortByDestination.Text = "Сорт. по напр.";
            this.btnSortByDestination.UseVisualStyleBackColor = true;
            this.btnSortByDestination.Click += new System.EventHandler(this.btnSortByDestination_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnViewStatistics);
            this.groupBox1.Controls.Add(this.btnAddFare);
            this.groupBox1.Controls.Add(this.btnPurchaseTicket);
            this.groupBox1.Controls.Add(this.btnSaveToFile);
            this.groupBox1.Controls.Add(this.btnLoadFromFile);
            this.groupBox1.Controls.Add(this.btnSortByPrice);
            this.groupBox1.Controls.Add(this.btnSortByDestination);
            this.groupBox1.Location = new System.Drawing.Point(20, 520);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 140);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Управление данными";
            // 
            // btnViewStatistics
            // 
            this.btnViewStatistics.Location = new System.Drawing.Point(280, 80);
            this.btnViewStatistics.Name = "btnViewStatistics";
            this.btnViewStatistics.Size = new System.Drawing.Size(100, 40);
            this.btnViewStatistics.TabIndex = 14;
            this.btnViewStatistics.Text = "Статистика";
            this.btnViewStatistics.UseVisualStyleBackColor = true;
            this.btnViewStatistics.Click += new System.EventHandler(this.btnViewStatistics_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCalculatePassenger);
            this.groupBox2.Controls.Add(this.txtPassportNumber);
            this.groupBox2.Controls.Add(this.lblPassport);
            this.groupBox2.Location = new System.Drawing.Point(540, 520);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 90);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Расчеты";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(20, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 17;
            this.label1.Text = "Тарифы";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(497, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 15);
            this.label2.TabIndex = 18;
            this.label2.Text = "Проданные билеты";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 680);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTotalTickets);
            this.Controls.Add(this.lblTotalSales);
            this.Controls.Add(this.dgvTickets);
            this.Controls.Add(this.dgvFares);
            this.Name = "MainForm";
            this.Text = "Аэропорт - Система продажи билетов";
            ((System.ComponentModel.ISupportInitialize)(this.dgvFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTickets)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


    }
}