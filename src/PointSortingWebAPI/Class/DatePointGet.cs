using System;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Web.Http;

namespace PointSortingWebAPI
{
    public static class DatePointGet
    {
        public static List<Point> Create(int[,] date)
        {
            int rows = date.GetUpperBound(0) + 1;
            int length = date.Length / rows;

            List<Point> ListPoint = new List<Point>();

            for (int i = 0; i < length; i++)
            {
                Point point = new Point(date[0, i], date[1, i]);
                ListPoint.Add(point);
            }

            return ListPoint;
        }

        //Настройка сериализаций
        public static HttpConfiguration Config ()
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            return config;
        }

        public static List<SpiralPoint> SpiralListSend(List<Point> ListPoint)
        {
            List<SpiralPoint> ListSpiral = new List<SpiralPoint>();

            for (int i = 1; i < ListPoint.Count; i++)
            {
                ListSpiral.Add(new SpiralPoint(ListPoint[i-1], ListPoint[i]));
            }

            return ListSpiral;
        }

    }
}