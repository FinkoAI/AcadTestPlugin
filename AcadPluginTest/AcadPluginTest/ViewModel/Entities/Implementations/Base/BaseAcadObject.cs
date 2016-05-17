using AcadPluginTest.Enums;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.DatabaseServices;
using GalaSoft.MvvmLight;

namespace AcadPluginTest.ViewModel.Entities.Implementations.Base
{
    public class BaseAcadObject : ViewModelBase, IAcadObject
    {
        #region Fields

        private ObjectId _id;
        private string _name;
        private bool _isModified;

        #endregion

        #region Properties

        /// <summary>
        /// Id объекта в чертеже
        /// </summary>
        public ObjectId Id
        {
            get { return _id; }
            set { Set(() => Id, ref _id, value); }
        }

        /// <summary>
        /// Название объекта
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { Set(() => Name, ref _name, value); }
        }

        /// <summary>
        /// Идентификатор наличия изменений
        /// </summary>
        public virtual bool IsModified
        {
            get { return _isModified; }
            set { Set(() => IsModified, ref _isModified, value); }
        }

        public ObjectType AcadObjectType { get; set; }

        #endregion
    }
}
