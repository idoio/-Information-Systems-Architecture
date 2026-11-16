using System;
using System.Reflection;
using System.Windows.Forms;
using AnimeFigures.Shared;

namespace AnimeFigures.Presenter
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var formAssembly = Assembly.Load("AnimeFigure.WinForm");
            var formType = formAssembly.GetType("AnimeFigures.WinForms.MainForm");

            var mainForm = (IMainView)Activator.CreateInstance(formType);

            var presenter = CompositionRoot.CreateMainPresenter(mainForm);

            Application.Run((Form)mainForm);

            presenter.Dispose();
            CompositionRoot.Shutdown();
        }
    }
}