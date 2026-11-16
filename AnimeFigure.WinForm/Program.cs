//using System;
//using System.Windows.Forms;
//using AnimeFigures.Presenter;
//using AnimeFigures.WinForms;

//namespace AnimeFigures.WinForms
//{
//    internal static class Program
//    {
//        /// <summary>
//        /// Главная точка входа для приложения.
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);

//            // 1. Создаем View
//            var mainForm = new MainForm();

//            // 2. Создаем Presenter и внедряем в него View
//            var presenter = CompositionRoot.CreateMainPresenter(mainForm);

//            Application.Run(mainForm);

//            presenter.Dispose();
//            CompositionRoot.Shutdown();
//        }
//    }
//}