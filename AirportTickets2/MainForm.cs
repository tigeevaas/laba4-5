using System;
using System.Windows.Forms;

namespace AirportTicketSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            RefreshData();
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

        private void btnAddFare_Click(object sender, EventArgs e)
        {
            AddFareForm addFareForm = new AddFareForm();
            if (addFareForm.ShowDialog() == DialogResult.OK)
            {
                RefreshData();
            }
        }

        private void btnPurchaseTicket_Click(object sender, EventArgs e)
        {
            PurchaseTicketForm purchaseForm = new PurchaseTicketForm();
            if (purchaseForm.ShowDialog() == DialogResult.OK)
            {
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
                    MessageBox.Show("Тарифы успешно сохранены!", "Успех",
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
                    RefreshData();
                    MessageBox.Show("Тарифы успешно загружены!", "Успех",
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

                if (MessageBox.Show($"Удалить тариф в {destinationStr}?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (Airport.Instance.RemoveFare(destination))
                    {
                        RefreshData();
                    }
                }
            }
        }
    }
}