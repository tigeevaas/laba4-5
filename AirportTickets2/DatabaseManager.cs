using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AirportTicketSystem
{
    public static class DatabaseManager
    {
        private static string _connectionString;

        static DatabaseManager()
        {
            // Настройте строку подключения под вашу БД
            // Вам нужно будет создать базу данных 'airport_db' в MySQL
            string server = "localhost";
            string database = "airport_db";
            string username = "root";
            string password = "tigeevaas-1135"; // укажите ваш пароль
            
            _connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};charset=utf8mb4;";


        }

        // Создание таблиц, если они не существуют
        public static void InitializeDatabase()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    // Таблица тарифов
                    string createFaresTable = @"
                        CREATE TABLE IF NOT EXISTS Fares (
                            Destination VARCHAR(20) PRIMARY KEY,
                            Price DECIMAL(10, 2) NOT NULL
                        )";

                    // Таблица пассажиров
                    string createPassengersTable = @"
                        CREATE TABLE IF NOT EXISTS Passengers (
                            PassportNumber VARCHAR(12) PRIMARY KEY,
                            Name VARCHAR(100) NOT NULL
                        )";

                    // Таблица билетов
                    string createTicketsTable = @"
                        CREATE TABLE IF NOT EXISTS Tickets (
                            Id INT AUTO_INCREMENT PRIMARY KEY,
                            PassengerPassport VARCHAR(12),
                            Destination VARCHAR(20),
                            PurchaseDate DATETIME NOT NULL,
                            Price DECIMAL(10, 2) NOT NULL,
                            FOREIGN KEY (PassengerPassport) REFERENCES Passengers(PassportNumber) ON DELETE CASCADE,
                            FOREIGN KEY (Destination) REFERENCES Fares(Destination) ON DELETE CASCADE
                        )";

                    using (var command = new MySqlCommand(createFaresTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (var command = new MySqlCommand(createPassengersTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (var command = new MySqlCommand(createTicketsTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации БД: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Сохранение тарифов в БД
        public static bool SaveFaresToDatabase(List<Fare> fares)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    // Очищаем таблицу
                    using (var clearCommand = new MySqlCommand("DELETE FROM Fares", connection))
                    {
                        clearCommand.ExecuteNonQuery();
                    }
                    
                    // Добавляем новые тарифы
                    foreach (var fare in fares)
                    {
                        string insertQuery = @"
                            INSERT INTO Fares (Destination, Price) 
                            VALUES (@Destination, @Price) 
                            ON DUPLICATE KEY UPDATE Price = @Price";
                        
                        using (var command = new MySqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@Destination", fare.Destination.ToString());
                            command.Parameters.AddWithValue("@Price", fare.Price);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения тарифов в БД: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Загрузка тарифов из БД
        public static List<Fare> LoadFaresFromDatabase()
        {
            var fares = new List<Fare>();
            
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    string query = "SELECT Destination, Price FROM Fares";
                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Enum.TryParse<Destination>(reader["Destination"].ToString(), out Destination dest))
                            {
                                double price = Convert.ToDouble(reader["Price"]);
                                fares.Add(new Fare(dest, price));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки тарифов из БД: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return fares;
        }

        // Сохранение пассажира в БД
        public static bool SavePassengerToDatabase(Passenger passenger)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    string query = @"
                        INSERT INTO Passengers (PassportNumber, Name) 
                        VALUES (@Passport, @Name) 
                        ON DUPLICATE KEY UPDATE Name = @Name";
                    
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Passport", passenger.PassportNumber);
                        command.Parameters.AddWithValue("@Name", passenger.Name);
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения пассажира в БД: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Сохранение билета в БД
        public static bool SaveTicketToDatabase(Ticket ticket)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    // Сначала сохраняем пассажира
                    SavePassengerToDatabase(ticket.Passenger);
                    
                    // Затем сохраняем билет
                    string query = @"
                        INSERT INTO Tickets (PassengerPassport, Destination, PurchaseDate, Price) 
                        VALUES (@Passport, @Destination, @PurchaseDate, @Price)";
                    
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Passport", ticket.Passenger.PassportNumber);
                        command.Parameters.AddWithValue("@Destination", ticket.Fare.Destination.ToString());
                        command.Parameters.AddWithValue("@PurchaseDate", ticket.PurchaseDate);
                        command.Parameters.AddWithValue("@Price", ticket.Fare.Price);
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения билета в БД: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Загрузка всех билетов из БД
        public static List<Ticket> LoadTicketsFromDatabase()
        {
            var tickets = new List<Ticket>();
            
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    string query = @"
                        SELECT t.*, p.Name 
                        FROM Tickets t 
                        JOIN Passengers p ON t.PassengerPassport = p.PassportNumber 
                        JOIN Fares f ON t.Destination = f.Destination
                        ORDER BY t.PurchaseDate DESC";
                    
                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var passenger = new Passenger(
                                reader["PassengerPassport"].ToString(),
                                reader["Name"].ToString()
                            );
                            
                            if (Enum.TryParse<Destination>(reader["Destination"].ToString(), out Destination dest))
                            {
                                double price = Convert.ToDouble(reader["Price"]);
                                var fare = new Fare(dest, price);
                                var ticket = new Ticket(passenger, fare)
                                {
                                    PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"])
                                };
                                
                                tickets.Add(ticket);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки билетов из БД: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return tickets;
        }

        // Экспорт данных из БД в XML файл
        public static bool ExportDatabaseToFile(string filePath)
        {
            try
            {
                var allData = new DatabaseExportData
                {
                    Fares = LoadFaresFromDatabase(),
                    Tickets = LoadTicketsFromDatabase()
                };
                
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(DatabaseExportData));
                using (var writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, allData);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка экспорта данных: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Импорт данных из файла в БД
        public static bool ImportDatabaseFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return false;
                
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(DatabaseExportData));
                DatabaseExportData importedData;
                
                using (var reader = new StreamReader(filePath))
                {
                    importedData = (DatabaseExportData)serializer.Deserialize(reader);
                }
                
                // Сохраняем тарифы
                SaveFaresToDatabase(importedData.Fares);
                
                // Сохраняем билеты (пассажиры сохранятся автоматически)
                foreach (var ticket in importedData.Tickets)
                {
                    SaveTicketToDatabase(ticket);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка импорта данных: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Получение статистики из БД
        public static (double TotalSales, int TicketCount, int PassengerCount) GetStatistics()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    // Общая сумма продаж
                    string totalQuery = "SELECT SUM(Price) as Total FROM Tickets";
                    double totalSales = 0;
                    using (var command = new MySqlCommand(totalQuery, connection))
                    {
                        var result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                            totalSales = Convert.ToDouble(result);
                    }
                    
                    // Количество билетов
                    string countQuery = "SELECT COUNT(*) FROM Tickets";
                    int ticketCount = 0;
                    using (var command = new MySqlCommand(countQuery, connection))
                    {
                        ticketCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                    
                    // Количество пассажиров
                    string passengerQuery = "SELECT COUNT(DISTINCT PassengerPassport) FROM Tickets";
                    int passengerCount = 0;
                    using (var command = new MySqlCommand(passengerQuery, connection))
                    {
                        passengerCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                    
                    return (totalSales, ticketCount, passengerCount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения статистики: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (0, 0, 0);
            }
        }
    }

    // Класс для экспорта/импорта всех данных
    [Serializable]
    public class DatabaseExportData
    {
        public List<Fare> Fares { get; set; }
        public List<Ticket> Tickets { get; set; }
        
        public DatabaseExportData()
        {
            Fares = new List<Fare>();
            Tickets = new List<Ticket>();
        }
    }
}