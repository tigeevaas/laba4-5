using System;
using System.Drawing;
using System.Windows.Forms;

namespace AirportTicketSystem
{
    public partial class PurchaseTicketForm : Form
    {
        public PurchaseTicketForm()
        {
            InitializeComponent();
            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            cmbDestination.Items.Clear();
            if (Airport.Instance.Fares.Count > 0)
            {
                foreach (var fare in Airport.Instance.Fares)
                {
                    cmbDestination.Items.Add(Airport.DestinationToString(fare.Destination));
                }
                cmbDestination.SelectedIndex = 0;
            }
            else
            {
                cmbDestination.Items.Add("Нет доступных направлений");
                cmbDestination.Enabled = false;
                btnPurchase.Enabled = false;
            }
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            // Валидация имени
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите имя пассажира", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return;
            }

            if (!Helpers.IsValidName(txtName.Text))
            {
                MessageBox.Show("Некорректное имя (только буквы, пробелы и дефисы)", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return;
            }

            // Валидация паспорта
            if (string.IsNullOrWhiteSpace(txtPassport.Text))
            {
                MessageBox.Show("Введите номер паспорта", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassport.Focus();
                return;
            }

            if (!Helpers.IsValidPassportNumber(txtPassport.Text))
            {
                MessageBox.Show("Некорректный номер паспорта (6-12 цифр)", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassport.Focus();
                return;
            }

            // Валидация направления
            if (cmbDestination.SelectedItem == null || !cmbDestination.Enabled)
            {
                MessageBox.Show("Выберите направление или добавьте тарифы", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Создание пассажира и покупка билета
                Passenger passenger = new Passenger(txtPassport.Text.Trim(), txtName.Text.Trim());
                Destination dest = Airport.StringToDestination(cmbDestination.SelectedItem.ToString());

                if (Airport.Instance.PurchaseTicket(passenger, dest))
                {
                    // Показываем квитанцию
                    double price = Airport.Instance.Fares.Find(f => f.Destination == dest).Price;
                    SecondForm receiptForm = new SecondForm(passenger, dest, price);
                    receiptForm.ShowDialog();

                    MessageBox.Show("Билет успешно куплен!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Ошибка при покупке билета", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}