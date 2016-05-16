using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using AcadPluginTest.Helpers;
using AcadPluginTest.View;
using AcadPluginTest.ViewModel;
using AcadPluginTest.ViewModel.Entities.Implementations;
using Autodesk.AutoCAD.ApplicationServices;
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
            
            var dialogWindow = new PropertiesDialog()
            {
                DataContext = InitializePluginDialogViewModel(document)
            };

            AutocadApplication.ShowModalWindow(dialogWindow);
        }

        [CommandMethod("SHOW_PALETTE")]
        public void ShowPallete()
        {
            var document = AutocadApplication.DocumentManager.MdiActiveDocument;

            if (_ps == null)
            {
                _ps = new PaletteSet("Plugin Palette")
                {
                    
                    Size = new Size(400, 600),
                    DockEnabled = (DockSides) ((int) DockSides.Left + (int) DockSides.Right),
                };

                var uc = new PaletteControl()
                {
                    DataContext = InitializePluginDialogViewModel(document)
                };
                _ps.AddVisual("AddVisual", uc);
            }

            _ps.KeepFocus = true;
            _ps.Visible = true;
        }

        private PropertiesDialogViewModel InitializePluginDialogViewModel(Document document)
        {
            var layerVmList = AcadHelper.GetLayerVms(document);
            var vm = new PropertiesDialogViewModel() { Layers = new ObservableCollection<AcadLayerVm>(layerVmList) };

            return vm;
        }
    }
}
