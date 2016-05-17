using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AcadPluginTest.Enums;
using AcadPluginTest.Helpers;
using AcadPluginTest.ViewModel.Entities.Implementations;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.ApplicationServices;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace AcadPluginTest.ViewModel
{
    public class PropertiesDialogViewModel : ViewModelBase
    {
        #region Constructors

        public PropertiesDialogViewModel(Document doc)
        {
            _acadDocument = doc;
            RefreshCommandHandler();
            _acadDocument.CommandEnded += ViewChangedHandler;
        }

        #endregion

        #region Private fields

        private readonly Document _acadDocument;
        private IAcadObject _selectedItem;
        private DateTime _initializeDateTime;
        private DateTime _lastModifyDateTime;

        #endregion

        #region Properties

        public DateTime InitializeDateTime
        {
            get { return _initializeDateTime; }
            set { Set(() => InitializeDateTime, ref _initializeDateTime, value); }
        }

        public DateTime LastModifyDateTime
        {
            get { return _lastModifyDateTime; }
            set { Set(() => LastModifyDateTime, ref _lastModifyDateTime, value); }
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

        public ObservableCollection<AcadLayerVm> Layers { get; set; }

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
                Layers = new ObservableCollection<AcadLayerVm>();

            SelectedItem = null;
            Layers.Clear();
            AcadHelper.GetLayerVms(_acadDocument).ForEach(Layers.Add);

            var date = DateTime.Now;

            InitializeDateTime = date;
            LastModifyDateTime = date;
        }

        private bool CanSave()
        {
            return InitializeDateTime >= LastModifyDateTime;
        }

        private void SaveCommandHandler()
        {
            //TODO: реализовать сохранение
        }

        #endregion

        #region Private methods

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

        private void ViewChangedHandler(object sender, CommandEventArgs e)
        {
            if (AcadHelper.ChangingCommands.Contains(e.GlobalCommandName))
            {
                LastModifyDateTime = DateTime.Now;
            }

            SaveCommand.RaiseCanExecuteChanged();
        }

        #endregion
    }
}
