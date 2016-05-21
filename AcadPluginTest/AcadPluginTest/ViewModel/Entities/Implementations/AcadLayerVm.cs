using System.Collections.ObjectModel;
using System.Windows.Media;
using AcadPluginTest.ViewModel.Entities.Implementations.Base;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using AcadPluginTest.ViewModel.Validation;

namespace AcadPluginTest.ViewModel.Entities.Implementations
{
    /// <summary>
    /// ViewModel для объекта типа слой
    /// </summary>
    public class AcadLayerVm : BaseAcadObject, ILayerObject
    {
        #region Fields

        private string _name;
        private Color _color;
        private bool _isHidden;
        private ObservableCollection<IAcadGeometryObject> _objects;

        #endregion

        #region Constructors

        public AcadLayerVm()
        {
            _validator = new AcadLayerVmValidator();
        }

        #endregion

        #region Properties
        /// <summary>
        /// Название слоя
        /// </summary>
        public override string Name {
            get { return _name; }
            set
            {
                if (_name != value)
                    SetModified();

                Set(() => Name, ref _name, value);
            }
        }

        /// <summary>
        /// Цвет слоя
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set
            {
                if (!_color.Equals(value))
                    SetModified();

                Set(() => Color, ref _color, value);
            }
        }

        /// <summary>
        /// Отметка о состоянии видимости
        /// </summary>
        public bool IsHidden
        {
            get { return _isHidden; }
            set
            {
                if (_isHidden != value)
                    SetModified();

                Set(() => IsHidden, ref _isHidden, value);
            }
        }

        /// <summary>
        /// Показывает является ли слой нулевым (влияет на возможность редактирования имени)
        /// </summary>
        public bool IsNotZeroLayer { get; set; }


        public ObservableCollection<IAcadGeometryObject> Objects
        {
            get { return _objects; }
            set { Set(() => Objects, ref _objects, value); }
        }

        #endregion

    }
}
