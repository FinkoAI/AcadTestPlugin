using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using AcadPluginTest.Enums;
using AcadPluginTest.Helpers;
using AcadPluginTest.ViewModel.Entities.Implementations;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace AcadPluginTest.ViewModel
{
    public class PropertiesDialogViewModel : ViewModelBase
    {
        private readonly Document _acadDocument;

        private IAcadObject _selectedItem;

        public PropertiesDialogViewModel(Document doc)
        {
            _acadDocument = doc;
            RefreshCommandHandler();
        }

        public IAcadObject SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set(() => SelectedItem, ref _selectedItem, value);
                SelectObjectsOnDrawing();
            }
        }

        public RelayCommand RefreshCommand
        {
            get { return new RelayCommand(RefreshCommandHandler); }
        }

        public RelayCommand<RoutedPropertyChangedEventArgs<object>> SelectionItemChangedCommand
        {
            get { return new RelayCommand<RoutedPropertyChangedEventArgs<object>>(SelectionItemChangedCommandHandler); }
        }

        public ObservableCollection<AcadLayerVm> Layers { get; set; }


        private void SelectionItemChangedCommandHandler(RoutedPropertyChangedEventArgs<object> eventArgs)
        {
            SelectedItem = (IAcadObject) eventArgs.NewValue;
        }

        private void RefreshCommandHandler()
        {
            if (Layers == null)
                Layers = new ObservableCollection<AcadLayerVm>();
            
            SelectedItem = null;
            Layers.Clear();
            AcadHelper.GetLayerVms(_acadDocument).ForEach(Layers.Add); 
        }

        private void SelectObjectsOnDrawing()
        {
            if (SelectedItem == null)
            {
                AcadHelper.DeselectAllDrawingObjects(_acadDocument);
            }
            else if (SelectedItem.AcadObjectType != ObjectType.Layer)
            {
                AcadHelper.SelectDrawingObjects(new[] {SelectedItem.Id}, _acadDocument);
            }
            else
            {
                var layer = (AcadLayerVm) SelectedItem;
                AcadHelper.SelectDrawingObjects(layer.Objects.Select(x => x.Id), _acadDocument);
            }
        }
    }
}
