using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AirportTicketSystem
{
    public partial class SecondForm : Form
    {
        public SecondForm()
        {
            InitializeComponent();
            InitializeStatisticsView();
            this.Text = "Статистика аэропорта";
            this.BackColor = Color.LightBlue;
        }

        public SecondForm(string passportNumber) : this()
        {
            InitializePassengerTicketsView(passportNumber);
            this.Text = $"Билеты пассажира {passportNumber}";
            this.BackColor = Color.LightGreen;
        }

        public SecondForm(Passenger passenger, Destination destination, double price) : this()
        {
            InitializeReceiptView(passenger, destination, price);
            this.Text = "Квитанция о покупке";
            this.BackColor = Color.LightYellow;
        }

        private void InitializeStatisticsView()
        {
            lblTitle.Text = "Статистика аэропорта";

            double totalSales = Airport.Instance.CalculateTotalSales();
            int totalTickets = Airport.Instance.SoldTickets.Count;
            int totalPassengers = Airport.Instance.SoldTickets
                .Select(t => t.Passenger.PassportNumber)
                .Distinct()
                .Count();

            dataGridView.Rows.Clear();
            dataGridView.Rows.Add("Общая сумма продаж", $"${totalSales:F2}");
            dataGridView.Rows.Add("Всего продано билетов", totalTickets.ToString());
            dataGridView.Rows.Add("Уникальных пассажиров", totalPassengers.ToString());

            // Показываем топ-5 направлений
            var topDestinations = Airport.Instance.SoldTickets
                .GroupBy(t => t.Fare.Destination)
                .Select(g => new
                {
                    Destination = g.Key,
                    Count = g.Count(),
                    Total = g.Sum(t => t.Fare.Price)
                })
                .OrderByDescending(x => x.Count)
                .Take(5);

            dataGridView.Rows.Add("", "");
            dataGridView.Rows.Add("Топ направлений:", "");

            foreach (var dest in topDestinations)
            {
                dataGridView.Rows.Add(
                    Airport.DestinationToString(dest.Destination),
                    $"{dest.Count} билетов (${dest.Total:F2})"
                );
            }
        }

        private void InitializePassengerTicketsView(string passportNumber)
        {
            lblTitle.Text = $"Билеты пассажира {passportNumber}";

            var tickets = Airport.Instance.GetPassengerTickets(passportNumber);
            double total = Airport.Instance.CalculatePassengerTotal(passportNumber);

            dataGridView.Rows.Clear();
            dataGridView.Rows.Add("Номер паспорта", passportNumber);
            dataGridView.Rows.Add("Общая стоимость", $"${total:F2}");
            dataGridView.Rows.Add("Количество билетов", tickets.Count.ToString());
            dataGridView.Rows.Add("", "");
            dataGridView.Rows.Add("История покупок:", "");

            foreach (var ticket in tickets.OrderByDescending(t => t.PurchaseDate))
            {
                dataGridView.Rows.Add(
                    Airport.DestinationToString(ticket.Fare.Destination),
                    $"{ticket.PurchaseDate:dd.MM.yyyy HH:mm} (${ticket.Fare.Price:F2})"
                );
            }
        }

        private void InitializeReceiptView(Passenger passenger, Destination destination, double price)
        {
            lblTitle.Text = "Квитанция о покупке билета";

            dataGridView.Rows.Clear();
            dataGridView.Rows.Add("Пассажир", passenger.Name);
            dataGridView.Rows.Add("Номер паспорта", passenger.PassportNumber);
            dataGridView.Rows.Add("Направление", Airport.DestinationToString(destination));
            dataGridView.Rows.Add("Цена", $"${price:F2}");
            dataGridView.Rows.Add("Дата покупки", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
            dataGridView.Rows.Add("Номер квитанции", Guid.NewGuid().ToString().Substring(0, 8).ToUpper());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                Title = "Сохранить квитанцию",
                FileName = $"receipt_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveDialog.FileName))
                {
                    writer.WriteLine(lblTitle.Text);
                    writer.WriteLine(new string('=', 40));

                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        if (dataGridView.Rows[i].Cells[0].Value != null)
                        {
                            writer.WriteLine($"{dataGridView.Rows[i].Cells[0].Value}: {dataGridView.Rows[i].Cells[1].Value}");
                        }
                    }
                }

                MessageBox.Show("Квитанция сохранена!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}