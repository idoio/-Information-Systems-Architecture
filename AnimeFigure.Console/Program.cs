using AnimeFigures.DataAccessLayer;
using AnimeFigures.Logic;
using AnimeFigures.Model;

namespace AnimeFigures.ConsoleApp
{
    /// <summary>
    /// Консольное приложение для управления магазином аниме-фигурок
    /// </summary>
    class Program
    {
        private static readonly AppDbContext _context = new AppDbContext();
        private static readonly IRepository<Figure> _repository = new EfRepository<Figure>(_context);
        //private static readonly IRepository<Figure> _repository = new DapperFigureRepository();
        private static readonly FigureLogic _logic = new FigureLogic(_repository);
 

        static void Main(string[] args)
        {
            _context.Database.EnsureCreated();
            Console.WriteLine("МАГАЗИН АНИМЕ-ФИГУРОК\n");
            _logic.AddDefaultFigures();
            

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                ShowMenu();
                string choice = Console.ReadLine();

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
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }

        /// <summary>
        /// Ожидает нажатия Enter, чтобы пользователь успел прочитать результат
        /// </summary>
        static void WaitForUser()
        {
            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
        }


        /// <summary>
        /// Отображает главное меню приложения
        /// </summary>
        static void ShowMenu()
        {
            Console.WriteLine("\nМЕНЮ");
            Console.WriteLine("1. Добавить фигурку");
            Console.WriteLine("2. Показать фигурку по ID");
            Console.WriteLine("3. Обновить фигурку");
            Console.WriteLine("4. Удалить фигурку");
            Console.WriteLine("5. Показать все фигурки");
            Console.WriteLine("6. Группировка по производителю");
            Console.WriteLine("7. Фильтрация по масштабу");
            Console.WriteLine("8. Выход");
            Console.Write("Выбор: ");
        }

        //ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ВВОДА

        /// <summary>
        /// Запрашивает у пользователя целое число. Повторяет ввод, пока не будет корректно.
        /// </summary>
        /// <param name="prompt">Текст приглашения</param>
        /// <param name="minValue">Минимально допустимое значение</param>
        /// <returns>Введённое число</returns>
        static int ReadInt(string prompt, int minValue = int.MinValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= minValue)
                    return value;
                Console.WriteLine($"Ошибка: введите целое число >= {minValue}.");
            }
        }

        /// <summary>
        /// Запрашивает у пользователя decimal. Повторяет ввод, пока не будет корректно.
        /// </summary>
        /// <param name="prompt">Текст приглашения</param>
        /// <param name="minValue">Минимально допустимое значение</param>
        /// <returns>Введённое число</returns>
        static decimal ReadDecimal(string prompt, decimal minValue = 0)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal value) && value >= minValue)
                    return value;
                Console.WriteLine($"Ошибка: введите число >= {minValue}.");
            }
        }

        /// <summary>
        /// Запрашивает у пользователя double. Повторяет ввод, пока не будет корректно.
        /// </summary>
        /// <param name="prompt">Текст приглашения</param>
        /// <param name="minValue">Минимально допустимое значение</param>
        /// <returns>Введённое число</returns>
        static double ReadDouble(string prompt, double minValue = 0)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double value) && value >= minValue)
                    return value;
                Console.WriteLine($"Ошибка: введите число >= {minValue}.");
            }
        }

        /// <summary>
        /// Запрашивает непустую строку.
        /// </summary>
        /// <param name="prompt">Текст приглашения</param>
        /// <returns>Введённая строка без пробелов по краям</returns>
        static string ReadNonEmptyString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string value = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(value))
                    return value.Trim();
                Console.WriteLine("Ошибка: строка не может быть пустой.");
            }
        }

        /// <summary>
        /// Запрашивает масштаб с проверкой на допустимые значения.
        /// Допустимо: "1/4", "1/6", "1/7", "1/8", "1/10", "Nendoroid", "Figuarts", "Figma", "Scale".
        /// Разделитель ':' нормализуется в '/'.
        /// </summary>
        /// <param name="prompt">Текст приглашения</param>
        /// <returns>Введённый масштаб в нормализованном виде</returns>
        static string ReadScale(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (FigureLogic.TryValidateScale(input, out string errorMessage))
                {
                    // Возвращаем нормализованное значение (с '/' вместо ':')
                    return input.Trim().Replace(':', '/');
                }

                Console.WriteLine($"Ошибка: {errorMessage}");
                Console.WriteLine("Попробуйте ввести снова.\n");
            }
        }

        /// <summary>
        /// Запрашивает дату в формате ДД.ММ.ГГГГ.
        /// </summary>
        /// <param name="prompt">Текст приглашения</param>
        /// <returns>Введённая дата</returns>
        static DateTime ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + " (ДД.ММ.ГГГГ): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime value))
                    return value;
                Console.WriteLine("Ошибка: неверный формат даты. Пример: 15.05.2023.");
            }
        }

        //CRUD 

        /// <summary>
        /// Запрашивает данные у пользователя и создаёт новую фигурку
        /// </summary>
        static void CreateFigure()
        {
            Console.Clear();
            string name = ReadNonEmptyString("Название: ");
            string character = ReadNonEmptyString("Персонаж: ");
            string anime = ReadNonEmptyString("Аниме: ");
            string manufacturer = ReadNonEmptyString("Производитель: ");
            decimal price = ReadDecimal("Цена: ", 0);
            string scale = ReadScale("Масштаб: ");
            double height = ReadDouble("Высота (см): ", 0);
            DateTime releaseDate = ReadDate("Дата выхода");
            int inStock = ReadInt("Количество на складе: ", 0);

            var figure = _logic.CreateFigure(name, character, anime, manufacturer,
                                             price, scale, height, releaseDate, inStock);
            Console.WriteLine($"Фигурка добавлена с ID: {figure.Id}");
            WaitForUser();
        }

        /// <summary>
        /// Запрашивает ID и выводит фигурку
        /// </summary>
        static void ReadFigure()
        {
            Console.Clear();
            int id = ReadInt("Введите ID: ", 1);
            var figure = _logic.ReadFigure(id);
            Console.WriteLine(figure != null ? figure.ToString() : "Не найдена");
            WaitForUser();
        }

        /// <summary>
        /// Запрашивает ID и новые данные, обновляет фигурку
        /// </summary>
        static void UpdateFigure()
        {
            Console.Clear();
            var figures = _logic.GetAllFigures();
            if (figures.Count == 0)
            {
                Console.WriteLine("Список фигурок пуст. Нечего обновлять.");
                return;
            }

            Console.WriteLine("\nСУЩЕСТВУЮЩИЕ ФИГУРКИ");
            foreach (var f in figures)
                Console.WriteLine(f);

            int id = ReadInt("\nID для обновления (0 — отмена): ", 0);
            if (id == 0)
            {
                Console.WriteLine("Отменено.");
                WaitForUser();
                return;
            }

            var existing = _logic.ReadFigure(id);
            if (existing == null)
            {
                Console.WriteLine("Фигурка не найдена.");
                WaitForUser();
                return;
            }

            string name = ReadNonEmptyString("Новое название: ");
            string character = ReadNonEmptyString("Новый персонаж: ");
            string anime = ReadNonEmptyString("Новое аниме: ");
            string manufacturer = ReadNonEmptyString("Новый производитель: ");
            decimal price = ReadDecimal("Новая цена: ", 0);
            string scale = ReadScale("Новый масштаб: ");
            double height = ReadDouble("Новая высота: ", 0);
            DateTime releaseDate = ReadDate("Новая дата выхода");
            int inStock = ReadInt("Новое количество: ", 0);

            if (_logic.UpdateFigure(id, name, character, anime, manufacturer,
                                    price, scale, height, releaseDate, inStock))
                Console.WriteLine("Обновлено успешно");
            else
                Console.WriteLine("Фигурка не найдена");
            WaitForUser();
        }

        /// <summary>
        /// Запрашивает ID и удаляет фигурку
        /// </summary>
        static void DeleteFigure()
        {
            Console.Clear();
            var figures = _logic.GetAllFigures();
            if (figures.Count == 0)
            {
                Console.WriteLine("Список фигурок пуст. Нечего удалять.");
                WaitForUser();
                return;
            }

            Console.WriteLine("\nСУЩЕСТВУЮЩИЕ ФИГУРКИ");
            foreach (var f in figures)
                Console.WriteLine(f);

            int id = ReadInt("\nID для удаления (0 — отмена): ", 0);
            if (id == 0)
            {
                Console.WriteLine("Отменено.");
                WaitForUser();
                return;
            }

            if (_logic.DeleteFigure(id))
                Console.WriteLine("Удалено успешно");
            else
                Console.WriteLine("Фигурка не найдена");
            WaitForUser();
        }

        /// <summary>
        /// Выводит все фигурки
        /// </summary>
        static void ShowAll()
        {
            Console.Clear();
            var figures = _logic.GetAllFigures();
            if (figures.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }
            foreach (var figure in figures)
                Console.WriteLine(figure);
            WaitForUser();
        }

        //БИЗНЕС-ФУНКЦИИ

        /// <summary>
        /// Выводит группировку фигурок по производителям
        /// </summary>
        static void GroupByManufacturer()
        {
            Console.Clear();
            
            Console.WriteLine("\nГруппировка по производителям:");
            var groups = _logic.GroupByManufacturer();
            if (groups.Count == 0)
            {
                Console.WriteLine("Нет данных для группировки.");
                WaitForUser();
                return;
            }

            foreach (var group in groups)
            {
                Console.WriteLine($"{group.Key} ({group.Value.Count} шт.):");
                foreach (var figure in group.Value)
                    Console.WriteLine($"   {figure}");
                Console.WriteLine();
            }

            WaitForUser();
        }

        /// <summary>
        /// Запрашивает масштаб и выводит отфильтрованные фигурки
        /// </summary>
        static void FilterByScale()
        {
            Console.Clear();
            string scale = ReadScale("Введите масштаб: ");
            var filtered = _logic.FilterByScale(scale);
            Console.WriteLine($"\nФигурки масштаба '{scale}':");
            if (filtered.Count == 0)
            {
                Console.WriteLine("  Ничего не найдено.");
                return;
            }
            foreach (var figure in filtered)
                Console.WriteLine($"  {figure}");
            WaitForUser();
        }
    }
}