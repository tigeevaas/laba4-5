using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AirportTicketSystem
{
    public partial class AddFareForm : Form
    {
        public AddFareForm()
        {
            InitializeComponent();
            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            cmbDestination.Items.Clear();
            foreach (Destination destination in Enum.GetValues(typeof(Destination)))
            {
                cmbDestination.Items.Add(Airport.DestinationToString(destination));
            }
            cmbDestination.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Валидация цены
            if (!Helpers.IsValidPrice(txtPrice.Text, out double price))
            {
                MessageBox.Show("Введите корректную цену (1-100000)", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPrice.Focus();
                return;
            }

            Destination destination = Airport.StringToDestination(cmbDestination.SelectedItem.ToString());

            // Проверяем, существует ли уже тариф для этого направления
            var existingFare = Airport.Instance.Fares.FirstOrDefault(f => f.Destination == destination);
            if (existingFare != null)
            {
                if (MessageBox.Show($"Тариф для {Airport.DestinationToString(destination)} уже существует. Обновить?",
                    "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            Fare newFare = new Fare(destination, price);
            Airport.Instance.AddFare(newFare);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}