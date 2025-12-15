using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace AirportTicketSystem
{
    // Enum для направлений
    public enum Destination
    {
        MEXICO,
        LONDON,
        TOKYO,
        MOSCOW,
        PARIS,
        BERLIN,
        ROME,
        NEWYORK
    }

    // Класс тарифа
    public class Fare
    {
        public Destination Destination { get; set; }
        public double Price { get; set; }

        public Fare() { }

        public Fare(Destination dest, double price)
        {
            Destination = dest;
            Price = price;
        }

        public string GetDestinationString()
        {
            return Airport.DestinationToString(Destination);
        }

        public override string ToString()
        {
            return $"{GetDestinationString()} - ${Price:F2}";
        }
    }

    // Класс пассажира
    public class Passenger
    {
        public string PassportNumber { get; set; }
        public string Name { get; set; }

        public Passenger() { }

        public Passenger(string passportNumber, string name)
        {
            PassportNumber = passportNumber;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name} ({PassportNumber})";
        }
    }

    // Класс билета
    public class Ticket
    {
        public Passenger Passenger { get; set; }
        public Fare Fare { get; set; }
        public DateTime PurchaseDate { get; set; }

        public Ticket() { }

        public Ticket(Passenger passenger, Fare fare)
        {
            Passenger = passenger;
            Fare = fare;
            PurchaseDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Passenger.Name} -> {Fare.GetDestinationString()} (${Fare.Price:F2})";
        }
    }

    // Singleton класс аэропорта
    public sealed class Airport
    {
        private static Airport _instance;
        private static readonly object _lock = new object();

        public List<Fare> Fares { get; set; }
        public List<Ticket> SoldTickets { get; private set; }

        // Приватный конструктор
        private Airport()
        {
            Fares = new List<Fare>();
            SoldTickets = new List<Ticket>();
            InitializeDefaultFares();
        }

        // Singleton инстанс
        public static Airport Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Airport();
                    }
                    return _instance;
                }
            }
        }

        // Инициализация стандартных тарифов
        private void InitializeDefaultFares()
        {
            Fares.Add(new Fare(Destination.MEXICO, 500.0));
            Fares.Add(new Fare(Destination.LONDON, 400.0));
            Fares.Add(new Fare(Destination.TOKYO, 700.0));
            Fares.Add(new Fare(Destination.MOSCOW, 300.0));
            Fares.Add(new Fare(Destination.PARIS, 350.0));
        }

        // Добавить тариф
        public void AddFare(Fare fare)
        {
            // Удаляем старый тариф для этого направления
            var existing = Fares.FirstOrDefault(f => f.Destination == fare.Destination);
            if (existing != null)
            {
                Fares.Remove(existing);
            }
            Fares.Add(fare);
        }

        // Удалить тариф
        public bool RemoveFare(Destination destination)
        {
            var fare = Fares.FirstOrDefault(f => f.Destination == destination);
            if (fare != null)
            {
                Fares.Remove(fare);
                return true;
            }
            return false;
        }

        // Купить билет
        public bool PurchaseTicket(Passenger passenger, Destination dest)
        {
            Fare fare = Fares.Find(f => f.Destination == dest);
            if (fare != null)
            {
                SoldTickets.Add(new Ticket(passenger, fare));
                return true;
            }
            return false;
        }

        // Посчитать сумму покупок пассажира
        public double CalculatePassengerTotal(string passportNumber)
        {
            return SoldTickets
                .Where(t => t.Passenger.PassportNumber == passportNumber)
                .Sum(t => t.Fare.Price);
        }

        // Посчитать общую сумму продаж
        public double CalculateTotalSales()
        {
            return SoldTickets.Sum(t => t.Fare.Price);
        }

        // Получить все билеты пассажира
        public List<Ticket> GetPassengerTickets(string passportNumber)
        {
            return SoldTickets
                .Where(t => t.Passenger.PassportNumber == passportNumber)
                .ToList();
        }

        // Сортировка тарифов по цене
        public List<Fare> GetFaresSortedByPrice()
        {
            return Fares.OrderBy(f => f.Price).ToList();
        }

        // Сортировка тарифов по направлению
        public List<Fare> GetFaresSortedByDestination()
        {
            return Fares.OrderBy(f => f.GetDestinationString()).ToList();
        }

        // Экспорт тарифов в файл
        public bool ExportFaresToFile(string filePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<Fare>));
                using (var writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, Fares);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Импорт тарифов из файла
        public bool ImportFaresFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return false;

                var serializer = new XmlSerializer(typeof(List<Fare>));
                using (var reader = new StreamReader(filePath))
                {
                    var importedFares = (List<Fare>)serializer.Deserialize(reader);
                    Fares.Clear();
                    Fares.AddRange(importedFares);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Получить направление по строке
        public static Destination StringToDestination(string dest)
        {
            switch (dest)
            {
                case "Мексика": return Destination.MEXICO;
                case "Лондон": return Destination.LONDON;
                case "Токио": return Destination.TOKYO;
                case "Москва": return Destination.MOSCOW;
                case "Париж": return Destination.PARIS;
                case "Берлин": return Destination.BERLIN;
                case "Рим": return Destination.ROME;
                case "Нью-Йорк": return Destination.NEWYORK;
                default: return Destination.MEXICO;
            }
        }

        // Получить строку по направлению
        public static string DestinationToString(Destination dest)
        {
            switch (dest)
            {
                case Destination.MEXICO: return "Мексика";
                case Destination.LONDON: return "Лондон";
                case Destination.TOKYO: return "Токио";
                case Destination.MOSCOW: return "Москва";
                case Destination.PARIS: return "Париж";
                case Destination.BERLIN: return "Берлин";
                case Destination.ROME: return "Рим";
                case Destination.NEWYORK: return "Нью-Йорк";
                default: return "Неизвестный";
            }
        }
    }

    // Вспомогательные методы
    public static class Helpers
    {
        // Валидация номера паспорта
        public static bool IsValidPassportNumber(string passport)
        {
            if (string.IsNullOrWhiteSpace(passport))
                return false;

            if (passport.Length < 6 || passport.Length > 12)
                return false;

            foreach (char c in passport)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        // Валидация имени
        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            return name.All(c => char.IsLetter(c) || c == ' ' || c == '-');
        }

        // Валидация цены
        public static bool IsValidPrice(string priceStr, out double price)
        {
            price = 0;
            return double.TryParse(priceStr, out price) && price >= 1.0 && price <= 100000.0;
        }
    }
}