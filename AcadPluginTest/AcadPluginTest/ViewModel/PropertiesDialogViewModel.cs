using GalaSoft.MvvmLight;

namespace AcadPluginTest.ViewModel
{
    public class PropertiesDialogViewModel : ViewModelBase
    {
        private string _text;

        public string Text
        {
            get { return _text; }
            set { Set(() => Text, ref _text, value); }
        }
    }
}
