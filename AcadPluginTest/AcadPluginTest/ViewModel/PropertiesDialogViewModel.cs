using System.Collections.ObjectModel;
using System.Windows;
using AcadPluginTest.ViewModel.Entities.Implementations;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace AcadPluginTest.ViewModel
{
    public class PropertiesDialogViewModel : ViewModelBase
    {
        private IAcadObject _selectedItem;

        public IAcadObject SelectedItem
        {
            get { return _selectedItem; }
            set { Set(() => SelectedItem, ref _selectedItem, value); }
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
    }
}
