using System;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using TreeListApp.Exceptions;

namespace TreeListApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");

            //AppDomain.CurrentDomain.UnhandledException += (sender, args) => (args.ExceptionObject as Exception).ShowMessage();
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                if (!(args.ExceptionObject is Exception exception))
                    return;

                var title = exception.InnerException?.GetType().FullName ?? "Ошибка.";
                var message = exception.Message;

                if (exception.InnerException is DbException dbException)
                {
                    message += $"{Environment.NewLine}{dbException.Message}";
                    title = dbException.InnerException?.GetType().FullName ?? typeof(DbException).Name;
                }

                XtraMessageBox.Show(message, title, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            };
            Application.ApplicationExit += (sender, args) => { ConnectionProvider.ReleaseConnection(); };
            Application.Run(new TreeListForm());
        }
    }
}
