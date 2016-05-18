using System.Drawing;
using AcadPluginTest.Model.Implementations;
using AcadPluginTest.View;
using AcadPluginTest.ViewModel;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using AutocadApplication = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AcadPluginTest
{
    public class PluginCommands
    {
        private static PaletteSet _ps = null;

        [CommandMethod("SHOW_DIALOG")]
        public void ShowSettingsDialog()
        {
            var document = AutocadApplication.DocumentManager.MdiActiveDocument;
            var model = new PropertiesDialogModel(document);

            var dialogWindow = new PropertiesDialog()
            {
                DataContext = new PropertiesDialogViewModel(model)
            };

            AutocadApplication.ShowModalWindow(dialogWindow);
        }

        [CommandMethod("SHOW_PALETTE")]
        public void ShowPallete()
        {
            var document = AutocadApplication.DocumentManager.MdiActiveDocument;
            var model = new PropertiesDialogModel(document);

            _ps = new PaletteSet("Редактирование графических примитивов")
            {
                    
                Size = new Size(450, 600),
                DockEnabled = (DockSides) ((int) DockSides.Left + (int) DockSides.Right),
            };

            var uc = new PaletteControl()
            {
                DataContext = new PropertiesDialogViewModel(model)
            };
            _ps.AddVisual("PaletteControl", uc);
            

            _ps.KeepFocus = true;
            _ps.Visible = true;
        }
    }
}
