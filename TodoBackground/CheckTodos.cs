using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using Windows.UI.Popups;

namespace TodoBackground
{
    public sealed class CheckTodos : IBackgroundTask
    {
        private BackgroundTaskDeferral _deferral;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();
            //
            // TODO: Insert code to start one or more asynchronous methods using the
            //       await keyword, for example:
            //
            // await ExampleMethodAsync();
            //

            taskInstance.Progress = 50;

            //ApplicationData.Current.LocalSettings.

            new MessageDialog("Fertig").ShowAsync().AsTask();
            
            _deferral.Complete();
        }
    }
}
