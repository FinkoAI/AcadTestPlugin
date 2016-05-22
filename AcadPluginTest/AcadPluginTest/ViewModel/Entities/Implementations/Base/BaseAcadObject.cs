using System.Linq;
using AcadPluginTest.Enums;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.DatabaseServices;
using FluentValidation;
using FluentValidation.Results;
using GalaSoft.MvvmLight;

namespace AcadPluginTest.ViewModel.Entities.Implementations.Base
{
    /// <summary>
    /// Базовый класс для всех объектов чертежа
    /// </summary>
    public class BaseAcadObject : ViewModelBase, IAcadObject
    {
        #region Fields

        private ObjectId _id;
        private string _name;
        private bool _isModified;
        private bool _isSelected;
        protected IValidator Validator;

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
        public virtual string Name
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

        public bool IsSelected
        {
            get { return _isSelected; }
            set { Set(() => IsSelected, ref _isSelected, value); }
        }

        public ObjectType AcadObjectType { get; set; }

        public ValidationResult ValidationResult
        {
            get
            {
                if (Validator == null)
                    return null;

                return Validator.Validate(this);
            }
        }

        public bool IsValid
        {
            get
            {
                if (ValidationResult == null)
                    return true;

                return ValidationResult.IsValid;
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (ValidationResult == null)
                    return string.Empty;

                var errors = ValidationResult.Errors;

                if (errors == null)
                    return string.Empty;

                var firstError = errors.FirstOrDefault(x => x.PropertyName == columnName);


                RaisePropertyChanged(()=>IsValid);
                return firstError == null ? string.Empty : firstError.ErrorMessage;
            }
        }

        public string Error { get; private set; }

        #endregion

        #region Public methods

        public void SetModified()
        {
            IsModified = true;
        }

        #endregion
    }
}
