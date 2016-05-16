using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using AcadPluginTest.Enums;
using AcadPluginTest.Model.Implementations;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace AcadPluginTest.Helpers
{
    public static class Extensions
    {
        public static ObjectType AsObjectType(this string description)
        {
            var enumType = typeof (ObjectType);

            foreach (var field in enumType.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(
                    field,
                    typeof (DescriptionAttribute)) as DescriptionAttribute;

                if (attribute == null) 
                    continue;

                if (attribute.Description == description)
                {
                    return (ObjectType) field.GetValue(null);
                }
            }

            return ObjectType.Unknown;
        }

        public static T GetTypedObject<T>(this Transaction tr, ObjectId id, OpenMode openMode) where T : class
        {
            return tr.GetObject(id, openMode) as T;
        }

        public static IsoCoordinate ToIsoCoordinates(this Point3d point)
        {
            return new IsoCoordinate(point.X, point.Y, point.Z);
        }

        public static string ToCoordinateString(this IsoCoordinate coordinate)
        {
            return string.Format("({0}, {1}, {2})", coordinate.XCoordinate.ToString("F3"),
                coordinate.YCoordinate.ToString("F3"),
                coordinate.ZCoordinate.ToString("F3"));
        }

        public static Color ToSystemColor(this Autodesk.AutoCAD.Colors.Color color)
        {
            return Color.FromRgb(color.Red, color.Green, color.Blue);
        }
    }
}
