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

            var ps = new PaletteSet("Редактирование графических примитивов")
            {
                    
                Size = new Size(450, 600),
                DockEnabled = (DockSides) ((int) DockSides.Left + (int) DockSides.Right),
            };

            var uc = new PaletteControl()
            {
                DataContext = new PropertiesDialogViewModel(model)
            };
            ps.AddVisual("PaletteControl", uc);


            ps.KeepFocus = true;
            ps.Visible = true;
        }
    }
}
