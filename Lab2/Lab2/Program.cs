using System;
using System.Collections.Generic;

namespace HotelBookingSystem
{
 
    public struct Apartment
    {
        public int Number { get; set; }
        public bool IsAvailable { get; set; }
           
        public string Rooms { get; set; }

        public Apartment(int number, bool isAvailable, string rooms)
        {
            Number = number;
            IsAvailable = isAvailable;
            Rooms = rooms;
        }

        public Apartment With(bool isAvailable)
        {
            return new Apartment(Number, isAvailable, Rooms);
        }

        public override string ToString()
        {
            return $"{Rooms} апартамент номер {Number}: Свободен = {IsAvailable}";
        }
    }

    public class BookingSystem
    {
        private List<Apartment> apartments;

        public BookingSystem(List<Apartment> apartments)
        {
            this.apartments = apartments;
        }

        public void ShowAvailableApartments()
        {
            foreach (var apartment in apartments)
            {
                if (apartment.IsAvailable)
                {
                    Console.WriteLine(apartment);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Apartment> apartments = new List<Apartment>
            {
                new Apartment(1, true, "Двуместный"),
                new Apartment(2, false, "Одноместный"),
                new Apartment(3, true, "Однометсный"),
                new Apartment(4, true, "Трёхместный")
           };
          

            BookingSystem bookingSystem = new BookingSystem(apartments);

            Console.WriteLine("Свободные апартаменты: ");
            bookingSystem.ShowAvailableApartments();
        }
    }
}
