using System;
using System.Drawing;
using System.Windows.Forms;

namespace AirportTicketSystem
{
    public partial class MainForm : Form
    {
        // Дополнительные элементы управления для работы с БД
        private Button btnExportDB;
        private Button btnImportDB;
        private Button btnRefreshFromDB;
        private Button btnViewStatisticsDB;
        private Button btnClearDB;
        private GroupBox groupBox3;
        private Label label3;

        public MainForm()
        {
            InitializeComponent();
            InitializeDatabaseControls(); // Инициализируем элементы управления БД
            RefreshData();

            // Инициализируем БД при запуске
            InitializeDatabase();
        }

        // Инициализация дополнительных элементов управления для БД
        private void InitializeDatabaseControls()
        {
            // Группа для кнопок управления БД
            groupBox3 = new GroupBox
            {
                Text = "Управление базой данных",
                Location = new Point(880, 520),
                Size = new Size(220, 140),
                Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold)
            };

            // Кнопка экспорта БД в файл
            btnExportDB = new Button
            {
                Text = "Экспорт БД",
                Location = new Point(10, 20),
                Size = new Size(95, 30),
                Font = new Font("Microsoft Sans Serif", 8.25F)
            };
            btnExportDB.Click += BtnExportDB_Click;

            // Кнопка импорта БД из файла
            btnImportDB = new Button
            {
                Text = "Импорт БД",
                Location = new Point(110, 20),
                Size = new Size(95, 30),
                Font = new Font("Microsoft Sans Serif", 8.25F)
            };
            btnImportDB.Click += BtnImportDB_Click;

            // Кнопка обновления из БД
            btnRefreshFromDB = new Button
            {
                Text = "Обновить из БД",
                Location = new Point(10, 55),
                Size = new Size(95, 30),
                Font = new Font("Microsoft Sans Serif", 8.25F)
            };
            btnRefreshFromDB.Click += BtnRefreshFromDB_Click;

            // Кнопка статистики БД
            btnViewStatisticsDB = new Button
            {
                Text = "Статистика БД",
                Location = new Point(110, 55),
                Size = new Size(95, 30),
                Font = new Font("Microsoft Sans Serif", 8.25F)
            };
            btnViewStatisticsDB.Click += BtnViewStatisticsDB_Click;

            // Кнопка очистки БД
            btnClearDB = new Button
            {
                Text = "Очистить БД",
                Location = new Point(10, 90),
                Size = new Size(95, 30),
                Font = new Font("Microsoft Sans Serif", 8.25F),
                BackColor = Color.LightCoral
            };
            btnClearDB.Click += BtnClearDB_Click;

            // Метка состояния БД
            label3 = new Label
            {
                Text = "БД: Подключено",
                Location = new Point(120, 95),
                Size = new Size(85, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.LightGreen,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Добавляем элементы в группу
            groupBox3.Controls.Add(btnExportDB);
            groupBox3.Controls.Add(btnImportDB);
            groupBox3.Controls.Add(btnRefreshFromDB);
            groupBox3.Controls.Add(btnViewStatisticsDB);
            groupBox3.Controls.Add(btnClearDB);
            groupBox3.Controls.Add(label3);

            // Добавляем группу на форму
            this.Controls.Add(groupBox3);
        }

        private void InitializeDatabase()
        {
            try
            {
                DatabaseManager.InitializeDatabase();
                label3.Text = "БД: Подключено";
                label3.BackColor = Color.LightGreen;
            }
            catch (Exception ex)
            {
                label3.Text = "БД: Ошибка";
                label3.BackColor = Color.LightCoral;
                MessageBox.Show($"Ошибка подключения к БД: {ex.Message}\n" +
                              "Проверьте настройки подключения в DatabaseManager.cs",
                              "Ошибка БД",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
            }
        }

        private void RefreshData()
        {
            // Обновляем DataGridView тарифов
            dgvFares.Rows.Clear();
            foreach (var fare in Airport.Instance.Fares)
            {
                dgvFares.Rows.Add(
                    Airport.DestinationToString(fare.Destination),
                    fare.Price
                );
            }

            // Обновляем DataGridView билетов
            dgvTickets.Rows.Clear();
            foreach (var ticket in Airport.Instance.SoldTickets)
            {
                dgvTickets.Rows.Add(
                    ticket.Passenger.Name,
                    ticket.Passenger.PassportNumber,
                    Airport.DestinationToString(ticket.Fare.Destination),
                    ticket.Fare.Price,
                    ticket.PurchaseDate.ToString("dd.MM.yyyy HH:mm")
                );
            }

            // Обновляем статистику
            lblTotalSales.Text = $"Общая сумма продаж: ${Airport.Instance.CalculateTotalSales():F2}";
            lblTotalTickets.Text = $"Всего продано билетов: {Airport.Instance.SoldTickets.Count}";
        }

        // Методы для работы с БД
        private void BtnExportDB_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "XML файлы (*.xml)|*.xml|Все файлы (*.*)|*.*";
                saveDialog.Title = "Экспорт всей базы данных";
                saveDialog.FileName = $"airport_db_export_{DateTime.Now:yyyyMMdd_HHmmss}.xml";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    if (DatabaseManager.ExportDatabaseToFile(saveDialog.FileName))
                    {
                        MessageBox.Show("База данных успешно экспортирована!\n" +
                                      $"Файл: {saveDialog.FileName}",
                                      "Экспорт завершен",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при экспорте базы данных",
                                      "Ошибка",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnImportDB_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "XML файлы (*.xml)|*.xml|Все файлы (*.*)|*.*";
                openDialog.Title = "Импорт базы данных";
                openDialog.CheckFileExists = true;

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    if (MessageBox.Show("ВНИМАНИЕ! Все существующие данные в БД будут перезаписаны.\n" +
                                      "Продолжить?",
                                      "Подтверждение импорта",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (DatabaseManager.ImportDatabaseFromFile(openDialog.FileName))
                        {
                            // Обновляем данные в памяти из БД
                            Airport.Instance.Fares = DatabaseManager.LoadFaresFromDatabase();
                            Airport.Instance.SoldTickets = DatabaseManager.LoadTicketsFromDatabase();

                            RefreshData();

                            MessageBox.Show("База данных успешно импортирована из файла!\n" +
                                          $"Файл: {openDialog.FileName}\n" +
                                          $"Тарифов: {Airport.Instance.Fares.Count}\n" +
                                          $"Билетов: {Airport.Instance.SoldTickets.Count}",
                                          "Импорт завершен",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при импорте базы данных",
                                          "Ошибка",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void BtnRefreshFromDB_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Загрузить все данные из базы данных?\n" +
                              "Несохраненные изменения будут потеряны.",
                              "Обновление из БД",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // Загружаем данные из БД
                    var faresFromDb = DatabaseManager.LoadFaresFromDatabase();
                    var ticketsFromDb = DatabaseManager.LoadTicketsFromDatabase();

                    Airport.Instance.Fares = faresFromDb;
                    Airport.Instance.SoldTickets = ticketsFromDb;

                    RefreshData();

                    MessageBox.Show($"Данные успешно обновлены из базы данных!\n" +
                                  $"Тарифов: {faresFromDb.Count}\n" +
                                  $"Билетов: {ticketsFromDb.Count}",
                                  "Обновление завершено",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке данных из БД: {ex.Message}",
                                  "Ошибка",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                }
            }
        }

        private void BtnViewStatisticsDB_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаем статистику из БД
                var (totalSales, ticketCount, passengerCount) = DatabaseManager.GetStatistics();

                // Получаем топ направлений
                var topDestinations = GetTopDestinationsFromDB();

                // Создаем форму для отображения статистики
                var statsForm = new Form
                {
                    Text = "Статистика из базы данных",
                    Size = new Size(500, 400),
                    StartPosition = FormStartPosition.CenterParent,
                    BackColor = Color.AliceBlue
                };

                var textBox = new TextBox
                {
                    Multiline = true,
                    ReadOnly = true,
                    ScrollBars = ScrollBars.Vertical,
                    Dock = DockStyle.Fill,
                    Font = new Font("Consolas", 10),
                    BackColor = Color.WhiteSmoke
                };

                textBox.Text = $"СТАТИСТИКА ИЗ БАЗЫ ДАННЫХ\n";
                textBox.Text += $"==============================\n\n";
                textBox.Text += $"Общая сумма продаж:   ${totalSales:F2}\n";
                textBox.Text += $"Количество билетов:   {ticketCount}\n";
                textBox.Text += $"Уникальных пассажиров: {passengerCount}\n";
                textBox.Text += $"\n";
                textBox.Text += $"ТОП 5 НАПРАВЛЕНИЙ\n";
                textBox.Text += $"=================\n";

                foreach (var dest in topDestinations)
                {
                    textBox.Text += $"{dest.Destination,-15} {dest.Count,3} билетов   ${dest.Total,10:F2}\n";
                }

                textBox.Text += $"\n";
                textBox.Text += $"ДАННЫЕ В ПАМЯТИ\n";
                textBox.Text += $"================\n";
                textBox.Text += $"Тарифов в памяти:     {Airport.Instance.Fares.Count}\n";
                textBox.Text += $"Билетов в памяти:     {Airport.Instance.SoldTickets.Count}\n";

                statsForm.Controls.Add(textBox);
                statsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения статистики из БД: {ex.Message}",
                              "Ошибка",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void BtnClearDB_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ВНИМАНИЕ! Вы действительно хотите очистить базу данных?\n" +
                              "Все данные будут удалены без возможности восстановления.",
                              "Подтверждение очистки БД",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (ClearDatabase())
                {
                    // Обновляем данные в памяти
                    Airport.Instance.Fares.Clear();
                    Airport.Instance.SoldTickets.Clear();

                    RefreshData();

                    MessageBox.Show("База данных успешно очищена!",
                                  "Очистка завершена",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                }
            }
        }

        private bool ClearDatabase()
        {
            try
            {
                using (var connection = new MySql.Data.MySqlClient.MySqlConnection(
                    "Server=localhost;Database=airport_db;Uid=root;Pwd=your_password;"))
                {
                    connection.Open();

                    // Удаляем все данные из таблиц (в правильном порядке из-за внешних ключей)
                    using (var command1 = new MySql.Data.MySqlClient.MySqlCommand(
                        "DELETE FROM Tickets", connection))
                    {
                        command1.ExecuteNonQuery();
                    }

                    using (var command2 = new MySql.Data.MySqlClient.MySqlCommand(
                        "DELETE FROM Passengers", connection))
                    {
                        command2.ExecuteNonQuery();
                    }

                    using (var command3 = new MySql.Data.MySqlClient.MySqlCommand(
                        "DELETE FROM Fares", connection))
                    {
                        command3.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка очистки БД: {ex.Message}",
                              "Ошибка",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                return false;
            }
        }

        private System.Collections.Generic.List<(string Destination, int Count, double Total)> GetTopDestinationsFromDB()
        {
            var result = new System.Collections.Generic.List<(string, int, double)>();

            try
            {
                using (var connection = new MySql.Data.MySqlClient.MySqlConnection(
                    "Server=localhost;Database=airport_db;Uid=root;Pwd=your_password;"))
                {
                    connection.Open();

                    string query = @"
                        SELECT f.Destination, COUNT(t.Id) as TicketCount, SUM(t.Price) as TotalPrice
                        FROM Tickets t
                        JOIN Fares f ON t.Destination = f.Destination
                        GROUP BY f.Destination
                        ORDER BY TicketCount DESC
                        LIMIT 5";

                    using (var command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string dest = reader["Destination"].ToString();
                            int count = Convert.ToInt32(reader["TicketCount"]);
                            double total = Convert.ToDouble(reader["TotalPrice"]);

                            // Преобразуем enum в читаемое название
                            if (Enum.TryParse<Destination>(dest, out Destination destEnum))
                            {
                                result.Add((Airport.DestinationToString(destEnum), count, total));
                            }
                        }
                    }
                }
            }
            catch
            {
                // В случае ошибки возвращаем пустой список
            }

            return result;
        }

        // Сохранение данных в БД перед закрытием приложения
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (MessageBox.Show("Сохранить изменения в базе данных перед выходом?",
                              "Сохранение",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Сохраняем тарифы
                DatabaseManager.SaveFaresToDatabase(Airport.Instance.Fares);

                // Сохраняем билеты
                foreach (var ticket in Airport.Instance.SoldTickets)
                {
                    DatabaseManager.SaveTicketToDatabase(ticket);
                }

                MessageBox.Show("Данные успешно сохранены в базу данных!",
                              "Сохранено",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
            }

            base.OnFormClosing(e);
        }

        // Оригинальные методы формы (оставляем без изменений)
        private void btnAddFare_Click(object sender, EventArgs e)
        {
            AddFareForm addFareForm = new AddFareForm();
            if (addFareForm.ShowDialog() == DialogResult.OK)
            {
                // После добавления тарифа сохраняем его в БД
                DatabaseManager.SaveFaresToDatabase(Airport.Instance.Fares);
                RefreshData();
            }
        }

        private void btnPurchaseTicket_Click(object sender, EventArgs e)
        {
            PurchaseTicketForm purchaseForm = new PurchaseTicketForm();
            if (purchaseForm.ShowDialog() == DialogResult.OK)
            {
                // Билет автоматически сохраняется в БД через Airport.Instance.PurchaseTicket()
                RefreshData();
            }
        }

        private void btnCalculatePassenger_Click(object sender, EventArgs e)
        {
            string passportNumber = txtPassportNumber.Text.Trim();

            if (!Helpers.IsValidPassportNumber(passportNumber))
            {
                MessageBox.Show("Введите корректный номер паспорта (6-12 цифр)", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassportNumber.Focus();
                return;
            }

            double total = Airport.Instance.CalculatePassengerTotal(passportNumber);
            var tickets = Airport.Instance.GetPassengerTickets(passportNumber);

            SecondForm resultForm = new SecondForm(passportNumber);
            resultForm.ShowDialog();
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "XML файлы (*.xml)|*.xml|Все файлы (*.*)|*.*",
                Title = "Сохранить тарифы",
                FileName = "fares.xml"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                if (Airport.Instance.ExportFaresToFile(saveDialog.FileName))
                {
                    MessageBox.Show("Тарифы успешно сохранены в файл!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при сохранении файла", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "XML файлы (*.xml)|*.xml|Все файлы (*.*)|*.*",
                Title = "Загрузить тарифы"
            };

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                if (Airport.Instance.ImportFaresFromFile(openDialog.FileName))
                {
                    // Сохраняем загруженные тарифы в БД
                    DatabaseManager.SaveFaresToDatabase(Airport.Instance.Fares);
                    RefreshData();
                    MessageBox.Show("Тарифы успешно загружены из файла и сохранены в БД!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при загрузке файла", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSortByPrice_Click(object sender, EventArgs e)
        {
            dgvFares.Rows.Clear();
            foreach (var fare in Airport.Instance.GetFaresSortedByPrice())
            {
                dgvFares.Rows.Add(
                    Airport.DestinationToString(fare.Destination),
                    fare.Price
                );
            }
        }

        private void btnSortByDestination_Click(object sender, EventArgs e)
        {
            dgvFares.Rows.Clear();
            foreach (var fare in Airport.Instance.GetFaresSortedByDestination())
            {
                dgvFares.Rows.Add(
                    Airport.DestinationToString(fare.Destination),
                    fare.Price
                );
            }
        }

        private void btnViewStatistics_Click(object sender, EventArgs e)
        {
            SecondForm statsForm = new SecondForm();
            statsForm.ShowDialog();
        }

        private void dgvFares_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvFares.Columns["colDelete"].Index)
            {
                string destinationStr = dgvFares.Rows[e.RowIndex].Cells["colDestination"].Value.ToString();
                Destination destination = Airport.StringToDestination(destinationStr);

                if (MessageBox.Show($"Удалить тариф в {destinationStr}?\n" +
                                  "Тариф также будет удален из базы данных.",
                                  "Подтверждение удаления",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (Airport.Instance.RemoveFare(destination))
                    {
                        // Обновляем тарифы в БД
                        DatabaseManager.SaveFaresToDatabase(Airport.Instance.Fares);
                        RefreshData();
                    }
                }
            }
        }

        // Дополнительный метод для автоматического сохранения в БД при изменениях
        public void SaveDataToDatabase()
        {
            try
            {
                DatabaseManager.SaveFaresToDatabase(Airport.Instance.Fares);
                foreach (var ticket in Airport.Instance.SoldTickets)
                {
                    DatabaseManager.SaveTicketToDatabase(ticket);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка автоматического сохранения в БД: {ex.Message}",
                              "Ошибка",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
            }
        }
    }
}