using System;
using System.Collections.Generic;

namespace Pamiatka_Replit
{
    public interface IMovie
    {
        public void SetYear(int year);
        public IMemento Save();
        public void Restore(IMemento memento);
    }

    class BackToTheFuture : IMovie
    {
        private int Year;

        public BackToTheFuture(int year)
        {
            this.Year = year;
            // początkowa wartość
            Console.WriteLine("Początkowy rok: " + year);
        }

        public void SetYear(int year)
        {
            this.Year = year;
            // ustawia pole na właściwą wartość
            Console.WriteLine("Rok zmieniony na: " + year);
            // print
        }

        public IMemento Save()
        {
            Console.WriteLine("Zapisano pamiątkę z roku: " + Year);

            return new Memento(this.Year);
        }

        public void Restore(IMemento memento)
        {
            // przywraca wartość pola
            // print o przywróceniu

        }
    }

    public interface IMemento
    {
        public int GetYear();
    }

    class Memento : IMemento
    {
        private int Year;

        // konstruktor
        public Memento(int year)
        {
            Year = year;
        }

        public int GetYear()
        {
            return Year;
        }
    }

     class Caretaker
    {
        private List<IMemento> Mementos = new List<IMemento>();

        private IMovie movie;

        public Caretaker(IMovie movie)
        {
            this.movie = movie;
        }

        public void Save()
        {
            Mementos.Add(movie.Save());
            

            // print o zapisie
        }

        public void Undo()
        {
            if (Mementos.Count == 0)
            {
                Console.WriteLine("Nie można cofnąć - brak zapisanych danych");
                return;
            }

            var memento = Mementos[Mementos.Count - 1];

            Mementos.RemoveAt(Mementos.Count - 1);
            movie.Restore(memento);

            Console.WriteLine("Przywrócony rok: " + memento.GetYear());

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BackToTheFuture favoriteMovie = new BackToTheFuture(1985);
            Caretaker caretaker = new Caretaker(favoriteMovie);

            caretaker.Undo(); // test ;)

            Console.WriteLine();

            Console.WriteLine("Część I:");
            favoriteMovie.SetYear(1955);
            caretaker.Save();
            favoriteMovie.SetYear(1985);

            Console.WriteLine();

            Console.WriteLine("Część II:");
            favoriteMovie.SetYear(2015);
            favoriteMovie.SetYear(1985);
            caretaker.Undo();
            favoriteMovie.SetYear(1985);
            caretaker.Save();

            Console.WriteLine();

            Console.WriteLine("Część III:");
            favoriteMovie.SetYear(1885);
            caretaker.Undo();

        }
    }
}