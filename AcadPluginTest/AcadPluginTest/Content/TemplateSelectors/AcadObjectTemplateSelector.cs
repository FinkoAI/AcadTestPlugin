using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AcadPluginTest.Enums;
using AcadPluginTest.ViewModel.Entities.Implementations;
using AcadPluginTest.ViewModel.Entities.Interfaces;

namespace AcadPluginTest.Content.TemplateSelectors
{
    public class AcadObjectTemplateSelector : DataTemplateSelector
    {

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate dataTemplate;
            var element = container as FrameworkElement;

            var acadItem = item as IAcadObject;

            if (acadItem == null)
                return (DataTemplate) element.FindResource("EmptyControlTemplate");

            switch (acadItem.AcadObjectType)
            {
                case ObjectType.Point:
                    dataTemplate = element.FindResource("PointControlTemplate") as DataTemplate;
                    break;
                case ObjectType.Layer:
                    dataTemplate = element.FindResource("LayerControlTemplate") as DataTemplate;
                    break;
                case ObjectType.Line:
                    dataTemplate = element.FindResource("LineControlTemplate") as DataTemplate;
                    break;
                case ObjectType.Circle:
                    dataTemplate = element.FindResource("CircleControlTemplate") as DataTemplate;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return dataTemplate;
        }
    }
}
