using System;
using System.Windows;
using System.Windows.Controls;
using AcadPluginTest.Enums;
using AcadPluginTest.ViewModel.Entities.Interfaces;

namespace AcadPluginTest.Content.TemplateSelectors
{
    public class AcadObjectTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Выбирает Template для отображения редактируемых полей объектов чертежа
        /// </summary>
        /// <param name="item"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate dataTemplate;
            var element = container as FrameworkElement;

            if (element == null)
                return null;

            var acadItem = item as IAcadObject;

            if (acadItem == null)
                return (DataTemplate) element.FindResource("EmptyControlTemplate");

            switch (acadItem.AcadObjectType)
            {
                case ObjectType.Point:
                    dataTemplate = element.FindResource("PointControlTemplate") as DataTemplate;
                    break;
                case ObjectType.Layer:
                    dataTemplate = element.FindResource("LayerPropertyGridTemplate") as DataTemplate;
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
