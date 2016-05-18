using System;
using System.Collections.Generic;
using AcadPluginTest.Helpers;
using AcadPluginTest.Model.Interfaces;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.ApplicationServices;

namespace AcadPluginTest.Model.Implementations
{
    public class PropertiesDialogModel : IPropertiesDialogModel
    {
        private readonly Document _document;

        public Document Document
        {
            get { return _document; }
        }

        public PropertiesDialogModel(Document document)
        {
            _document = document;
        }

        

        public void SaveChanges(IEnumerable<ILayerObject> layersList)
        {
            
        }

        public bool IsDataValid(IEnumerable<ILayerObject> layersList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ILayerObject> GetLayersData()
        {
            return AcadHelper.GetLayerVms(_document);
        }
    }
}
