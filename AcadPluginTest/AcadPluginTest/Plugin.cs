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
            //очень странный костыль но без этого не работает EventToCommand
            typeof (System.Windows.Interactivity.Behavior).ToString();
            typeof (GalaSoft.MvvmLight.Command.EventToCommand).ToString();
        }

        public void Terminate()
        {
            //throw new NotImplementedException();
        }
    }
}
