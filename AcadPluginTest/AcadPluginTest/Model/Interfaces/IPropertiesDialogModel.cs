using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.ApplicationServices;

namespace AcadPluginTest.Model.Interfaces
{
    public interface IPropertiesDialogModel
    {
        Document Document { get; }
        void SaveChanges(IEnumerable<ILayerObject> layersList);
        bool IsDataValid(IEnumerable<ILayerObject> layersList);
        IEnumerable<ILayerObject> GetLayersData();
    }
}
