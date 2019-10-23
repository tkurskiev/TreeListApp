using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace TreeListApp.Exceptions
{
    public static class ExceptionExtensions
    {
        public static void ShowMessage(this Exception exception)
        {
            XtraMessageBox.Show(exception.Message, "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
