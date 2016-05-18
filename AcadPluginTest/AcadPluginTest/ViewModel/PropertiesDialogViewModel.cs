using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using AcadPluginTest.Enums;
using AcadPluginTest.Helpers;
using AcadPluginTest.Model.Interfaces;
using AcadPluginTest.ViewModel.Entities.Implementations;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace AcadPluginTest.ViewModel
{
    public class PropertiesDialogViewModel : ViewModelBase
    {
        #region Constructors

        public PropertiesDialogViewModel(IPropertiesDialogModel model)
        {
            _model = model;
            RefreshCommandHandler();
            //_acadDocument.CommandEnded += ViewChangedHandler;
        }

        #endregion

        #region Private fields

        private readonly IPropertiesDialogModel _model;
        private IAcadObject _selectedItem;

        #endregion

        #region Properties
        
        public IAcadObject SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set(() => SelectedItem, ref _selectedItem, value);
                SelectObjectsOnDrawing();
            }
        }

        public ObservableCollection<ILayerObject> Layers { get; set; }

        #endregion

        #region Commands

        public RelayCommand RefreshCommand
        {
            get { return new RelayCommand(RefreshCommandHandler); }
        }

        public RelayCommand SaveCommand
        {
            get { return new RelayCommand(SaveCommandHandler, CanSave); }
        }

        public RelayCommand<RoutedPropertyChangedEventArgs<object>> SelectionItemChangedCommand
        {
            get { return new RelayCommand<RoutedPropertyChangedEventArgs<object>>(SelectionItemChangedCommandHandler); }
        }

        #endregion

        #region Command handlers

        private void SelectionItemChangedCommandHandler(RoutedPropertyChangedEventArgs<object> eventArgs)
        {
            SelectedItem = (IAcadObject) eventArgs.NewValue;
        }

        private void RefreshCommandHandler()
        {
            if (Layers == null)
                Layers = new ObservableCollection<ILayerObject>();

            SelectedItem = null;
            Layers.Clear();
            var layers = _model.GetLayersData().ToList();
            layers.ForEach(Layers.Add);
        }

        private bool CanSave()
        {
            return true; //InitializeDateTime >= LastModifyDateTime;
        }

        private void SaveCommandHandler()
        {
            _model.SaveChanges(Layers);
        }

        #endregion

        #region Private methods

        private void SelectObjectsOnDrawing()
        {
            if (SelectedItem == null)
            {
                AcadHelper.DeselectAllDrawingObjects(_model.Document);
            }
            else if (SelectedItem.AcadObjectType != ObjectType.Layer)
            {
                AcadHelper.SelectDrawingObjects(new[] { SelectedItem.Id }, _model.Document);
            }
            else
            {
                var layer = (AcadLayerVm) SelectedItem;
                AcadHelper.SelectDrawingObjects(layer.Objects.Select(x => x.Id), _model.Document);
            }
        }

        #endregion
    }
}
