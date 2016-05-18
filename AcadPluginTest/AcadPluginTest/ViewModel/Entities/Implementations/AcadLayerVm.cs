﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using AcadPluginTest.ViewModel.Entities.Implementations.Base;
using AcadPluginTest.ViewModel.Entities.Interfaces;

namespace AcadPluginTest.ViewModel.Entities.Implementations
{
    public class AcadLayerVm : BaseAcadObject, ILayerObject
    {
        #region Fields

        private Color _color;
        private bool _isHidden;
        private ObservableCollection<IAcadGeometryObject> _objects;

        #endregion

        #region Properties

        public Color Color
        {
            get { return _color; }
            set
            {
                if (!_color.Equals(value))
                    IsModified = true;

                Set(() => Color, ref _color, value);
            }
        }

        public bool IsHidden
        {
            get { return _isHidden; }
            set
            {
                if (_isHidden != value)
                    IsModified = true;

                Set(() => IsHidden, ref _isHidden, value);
            }
        }

        public ObservableCollection<IAcadGeometryObject> Objects
        {
            get { return _objects; }
            set { Set(() => Objects, ref _objects, value); }
        }

        #endregion

    }
}
