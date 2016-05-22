using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using AcadPluginTest.Enums;
using AcadPluginTest.Helpers;
using AcadPluginTest.Model.Interfaces;
using AcadPluginTest.ViewModel.Entities.Implementations;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.DatabaseServices;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace AcadPluginTest.ViewModel
{
    /// <summary>
    /// ViewModel основного окна/панели
    /// </summary>
    public class PropertiesDialogViewModel : ViewModelBase
    {
        #region Constructors

        public PropertiesDialogViewModel(IPropertiesDialogModel model)
        {
            _model = model;
            RefreshCommandHandler();
        }

        #endregion

        #region Private fields

        private readonly IPropertiesDialogModel _model;
        private IAcadObject _selectedItem;
        private string _saveLockDescription;

        #endregion

        #region Properties

        /// <summary>
        /// Выбранный в дереве элемент
        /// </summary>
        public IAcadObject SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set(() => SelectedItem, ref _selectedItem, value);
                SelectObjectsOnDrawing();
            }
        }

        /// <summary>
        /// Список слоёв с вложенными объектами
        /// </summary>
        public ObservableCollection<ILayerObject> Layers { get; set; }

        /// <summary>
        /// Сообщение для описания причин блокировки кнопки сохранить
        /// </summary>
        public string SaveLockDescription
        {
            get { return _saveLockDescription; }
            set { Set(() => SaveLockDescription, ref _saveLockDescription, value); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Обработка кнопки "Обновить"
        /// </summary>
        public RelayCommand RefreshCommand
        {
            get { return new RelayCommand(RefreshCommandHandler); }
        }

        /// <summary>
        /// Обработка кнопки "Сохранить"
        /// </summary>>
        public RelayCommand SaveCommand
        {
            get { return new RelayCommand(SaveCommandHandler, CanSave); }
        }

        /// <summary>
        /// Обработка события смены выбранного элемента
        /// </summary>
        public RelayCommand<RoutedPropertyChangedEventArgs<object>> SelectionItemChangedCommand
        {
            get { return new RelayCommand<RoutedPropertyChangedEventArgs<object>>(SelectionItemChangedCommandHandler); }
        }

        #endregion

        #region Command handlers
        /// <summary>
        /// Устанавливает значение SelectedItem
        /// </summary>
        /// <param name="eventArgs"></param>
        private void SelectionItemChangedCommandHandler(RoutedPropertyChangedEventArgs<object> eventArgs)
        {
            SelectedItem = (IAcadObject) eventArgs.NewValue;
        }

        /// <summary>
        /// Обновляет данные в панели
        /// </summary>
        private void RefreshCommandHandler()
        {
            var id = SelectedItem != null ? SelectedItem.Id : ObjectId.Null;

            if (Layers == null)
                Layers = new ObservableCollection<ILayerObject>();

            SelectedItem = null;
            Layers.Clear();
            var layers = _model.GetLayersData().ToList();
            layers.ForEach(Layers.Add);

            if (id != ObjectId.Null)
            {
                var flatTree = AcadHelper.GetFlatElementsTree(Layers.ToList());
                var selected = flatTree.FirstOrDefault(x => x.Id == id);
                if (selected != null)
                {
                    selected.IsSelected = true;
                    SelectedItem = selected;
                }
            }
        }

        /// <summary>
        /// Проверяет возможность сохранения изменений
        /// </summary>
        /// <returns></returns>
        private bool CanSave()
        {
            var flatTree = AcadHelper.GetFlatElementsTree(Layers.ToList());

            if (flatTree.Any(x => !x.IsValid))
            {
                SaveLockDescription = "Некоторые элементы заполнены с неверно. Пожалуйста, исправьте перед сохранением";
                return false;
            }


            if (!_model.IsDataValid(Layers))
            {
                SaveLockDescription = "Данные не актуальны. Обновите панель инструментов.";
                return false;
            }

            if (Layers.Any(layer => layer.IsModified || layer.Objects.Any(acadObject => acadObject.IsModified)))
            {
                SaveLockDescription = null;
                return true;
            }

            SaveLockDescription = "Отсутствуют не сохранённые изменения";
            return false;
        }

        /// <summary>
        /// Сохраняет изменения
        /// </summary>
        private void SaveCommandHandler()
        {
            if (!_model.TrySaveChanges(Layers.ToList()))
                MessageBox.Show("Во время сохранения произошла непредвиденная ошибка.", "Ошибка!",
                    MessageBoxButton.OKCancel, MessageBoxImage.Error);

            SelectObjectsOnDrawing();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Выбор объекта на чертеже
        /// </summary>
        private void SelectObjectsOnDrawing()
        {
            if (SelectedItem == null)
            {
                AcadHelper.DeselectAllDrawingObjects(_model.Document);
            }
            else if (SelectedItem.AcadObjectType != ObjectType.Layer)
            {
                AcadHelper.SelectDrawingObjects(new[] {SelectedItem.Id}, _model.Document);
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
