﻿using System;
using System.ComponentModel;
using AcadPluginTest.Enums;
using AcadPluginTest.ViewModel.Entities.Implementations;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AcadColor = Autodesk.AutoCAD.Colors.Color;
using MediaColor = System.Windows.Media.Color;

namespace AcadPluginTest.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Приводит строку к перечислению типов объекта на чертеже
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Получает объект переданного типа
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tr"></param>
        /// <param name="id"></param>
        /// <param name="openMode"></param>
        /// <returns></returns>
        public static T GetTypedObject<T>(this Transaction tr, ObjectId id, OpenMode openMode) where T : class
        {
            return tr.GetObject(id, openMode) as T;
        }

        /// <summary>
        /// Получает объект типа IsoCoordinate из Autocad Point3d
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static IsoCoordinate ToIsoCoordinates(this Point3d point)
        {
            return new IsoCoordinate(point.X, point.Y, point.Z);
        }

        /// <summary>
        /// Получает объект типа Point3d из IIsoCoordinate
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public static Point3d ToPoint3D(this IIsoCoordinate coordinate)
        {
            return new Point3d(coordinate.XCoordinate, coordinate.YCoordinate, coordinate.ZCoordinate);
        }

        /// <summary>
        /// Получает строку координат
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public static string ToCoordinateString(this IsoCoordinate coordinate)
        {
            return string.Format("({0}, {1}, {2})", coordinate.XCoordinate.ToString("F3"),
                coordinate.YCoordinate.ToString("F3"),
                coordinate.ZCoordinate.ToString("F3"));
        }

        /// <summary>
        /// Получает объект типа System.Windows.Media.Color из Autodesk.AutoCAD.Colors.Color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static MediaColor ToSystemColor(this AcadColor color)
        {
            return MediaColor.FromRgb(color.Red, color.Green, color.Blue);
        }

        /// <summary>
        /// Получает объект типа Autodesk.AutoCAD.Colors.Color из System.Windows.Media.Color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static AcadColor ToAcadColor(this MediaColor color)
        {
            return AcadColor.FromColor(color);
        }
    }
}