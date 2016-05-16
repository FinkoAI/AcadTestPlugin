using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;

namespace AcadPluginTest
{
    public class Plugin : IExtensionApplication
    {
        public void Initialize()
        {
            ForceLibraies();
        }

        public void Terminate()
        {
            //throw new NotImplementedException();
        }

        //очень странный костыль, но без этого не работают подключённые библиотеки
        private void ForceLibraies()
        {
            typeof (System.Windows.Interactivity.Behavior).ToString();
            typeof (GalaSoft.MvvmLight.Command.EventToCommand).ToString();
            typeof (Xceed.Wpf.Toolkit.DecimalUpDown).ToString();
        }
    }
}
