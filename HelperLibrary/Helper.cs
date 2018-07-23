using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace HelperLibrary
{
    public class Helper
    {
        public static void ShowMessage(string msg)
        {
            var dialog = new MessageDialog(msg);
            dialog.ShowAsync().AsTask();
        }
    }
}
