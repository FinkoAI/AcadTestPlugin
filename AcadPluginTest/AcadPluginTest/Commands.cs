using AcadPluginTest.View;
using AcadPluginTest.ViewModel;
using Autodesk.AutoCAD.Runtime;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AcadPluginTest
{
    public class Commands : IExtensionApplication
    {
        public void Initialize()
        {
            //
        }

        public void Terminate()
        {
            //throw new NotImplementedException();
        }

        [CommandMethod("SHOW_DIALOG")]
        public void ShowSettingsDialog()
        {
            var vm = new PropertiesDialogViewModel
            {
                Text = "Hello World"
            };

            var dialogWindow = new PropertiesDialog()
            {
                DataContext = vm
            };

            Application.ShowModalWindow(dialogWindow);
        }
    }
}
