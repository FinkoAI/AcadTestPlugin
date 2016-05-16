using System.Collections.ObjectModel;
using System.Windows;
using AcadPluginTest.Helpers;
using AcadPluginTest.ViewModel.Entities.Implementations;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.ApplicationServices;
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
            set { Set(() => SelectedItem, ref _selectedItem, value); }
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
    }
}
