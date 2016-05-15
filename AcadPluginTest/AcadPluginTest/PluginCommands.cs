using System.Collections.Generic;
using System.Collections.ObjectModel;
using AcadPluginTest.Helpers;
using AcadPluginTest.View;
using AcadPluginTest.ViewModel;
using AcadPluginTest.ViewModel.Entities.Implementations;
using Autodesk.AutoCAD.Runtime;
using AutocadApplication = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AcadPluginTest
{
    public class PluginCommands
    {
        [CommandMethod("SHOW_DIALOG")]
        public void ShowSettingsDialog()
        {
            var document = AutocadApplication.DocumentManager.MdiActiveDocument;
            var database = document.Database;

            var acadLayers = AcadHelper.GetLayers(database);
            var layerVmList = new List<AcadLayerVm>();

            foreach (var layerRecord in acadLayers)
            {
                var layerObjectIds = AcadHelper.GetLayerObjectIds(layerRecord.Name, document);
                var layerObjects = AcadHelper.GetAllObjectsByIdList(layerObjectIds, database);

                var layerVm = new AcadLayerVm(layerRecord, layerObjects);

                layerVmList.Add(layerVm);
            }

            var vm = new PropertiesDialogViewModel() {Layers = new ObservableCollection<AcadLayerVm>(layerVmList)};

            var dialogWindow = new PropertiesDialog()
            {
                DataContext = vm
            };

            AutocadApplication.ShowModalWindow(dialogWindow);
        }
    }
}
