using AnimeFigures.Logic;
using AnimeFigures.Model;

namespace AnimeFigures.Console
{
    class Program
    {
        static readonly FigureLogic _logic = new FigureLogic();

        static void Main(string[] args)
        {
            System.Console.WriteLine("=== МАГАЗИН АНИМЕ-ФИГУРОК ===\n");

            bool exit = false;
            while (!exit)
            {
                ShowMenu();
                string choice = System.Console.ReadLine();

                switch (choice)
                {
                    case "1": CreateFigure(); break;
                    case "2": ReadFigure(); break;
                    case "3": UpdateFigure(); break;
                    case "4": DeleteFigure(); break;
                    case "5": ShowAll(); break;
                    case "6": GroupByManufacturer(); break;
                    case "7": FilterByScale(); break;
                    case "8": exit = true; break;
                    default:
                        System.Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            System.Console.WriteLine("\n=== МЕНЮ ===");
            System.Console.WriteLine("1. Добавить фигурку");
            System.Console.WriteLine("2. Показать фигурку по ID");
            System.Console.WriteLine("3. Обновить фигурку");
            System.Console.WriteLine("4. Удалить фигурку");
            System.Console.WriteLine("5. Показать все фигурки");
            System.Console.WriteLine("6. Группировка по производителю");
            System.Console.WriteLine("7. Фильтрация по масштабу");
            System.Console.WriteLine("8. Выход");
            System.Console.Write("Выбор: ");
        }

        static void CreateFigure()
        {
            System.Console.Write("Название: ");
            string name = System.Console.ReadLine();
            System.Console.Write("Персонаж: ");
            string character = System.Console.ReadLine();
            System.Console.Write("Аниме: ");
            string anime = System.Console.ReadLine();
            System.Console.Write("Производитель: ");
            string manufacturer = System.Console.ReadLine();
            System.Console.Write("Цена: ");
            decimal price = decimal.Parse(System.Console.ReadLine());
            System.Console.Write("Масштаб: ");
            string scale = System.Console.ReadLine();
            System.Console.Write("Высота (см): ");
            double height = double.Parse(System.Console.ReadLine());
            System.Console.Write("Количество на складе: ");
            int inStock = int.Parse(System.Console.ReadLine());

            var figure = _logic.CreateFigure(name, character, anime, manufacturer,
                                             price, scale, height, DateTime.Now, inStock);
            System.Console.WriteLine($"Фигурка добавлена с ID: {figure.Id}");
        }

        static void ReadFigure()
        {
            System.Console.Write("Введите ID: ");
            int id = int.Parse(System.Console.ReadLine());
            var figure = _logic.ReadFigure(id);
            System.Console.WriteLine(figure != null ? figure.ToString() : "Не найдена");
        }

        static void UpdateFigure()
        {
            System.Console.Write("ID для обновления: ");
            int id = int.Parse(System.Console.ReadLine());
            System.Console.Write("Новое название: ");
            string name = System.Console.ReadLine();
            System.Console.Write("Новый персонаж: ");
            string character = System.Console.ReadLine();
            System.Console.Write("Новое аниме: ");
            string anime = System.Console.ReadLine();
            System.Console.Write("Новый производитель: ");
            string manufacturer = System.Console.ReadLine();
            System.Console.Write("Новая цена: ");
            decimal price = decimal.Parse(System.Console.ReadLine());
            System.Console.Write("Новый масштаб: ");
            string scale = System.Console.ReadLine();
            System.Console.Write("Новая высота: ");
            double height = double.Parse(System.Console.ReadLine());
            System.Console.Write("Новое количество: ");
            int inStock = int.Parse(System.Console.ReadLine());

            if (_logic.UpdateFigure(id, name, character, anime, manufacturer,
                                    price, scale, height, inStock))
                System.Console.WriteLine("Обновлено успешно");
            else
                System.Console.WriteLine("Фигурка не найдена");
        }

        static void DeleteFigure()
        {
            System.Console.Write("ID для удаления: ");
            int id = int.Parse(System.Console.ReadLine());
            if (_logic.DeleteFigure(id))
                System.Console.WriteLine("Удалено успешно");
            else
                System.Console.WriteLine("Фигурка не найдена");
        }

        static void ShowAll()
        {
            var figures = _logic.GetAllFigures();
            foreach (var figure in figures)
                System.Console.WriteLine(figure);
        }

        static void GroupByManufacturer()
        {
            var groups = _logic.GroupByManufacturer();
            System.Console.WriteLine("\n📊 Группировка по производителям:");
            foreach (var group in groups)
                System.Console.WriteLine($"  {group.Key}: {group.Value} шт.");
        }

        static void FilterByScale()
        {
            System.Console.Write("Введите масштаб: ");
            string scale = System.Console.ReadLine();
            var filtered = _logic.FilterByScale(scale);
            System.Console.WriteLine($"\n Фигурки масштаба '{scale}':");
            foreach (var figure in filtered)
                System.Console.WriteLine($"  {figure}");
        }
    }
}