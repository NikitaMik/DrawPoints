using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using Newtonsoft.Json.Serialization;

namespace PointSortingWebAPI.Controllers
{

    public class PointController : ApiController
    {
        //HttpResponseMessage

        [HttpPost]
        public HttpResponseMessage Post([FromBody]int[,] date)
        {
            int rows = date.GetUpperBound(0) + 1;
            int length = date.Length / rows;

            List<Point> ListPoint = new List<Point>();

            for(int i = 0; i < length; i++)
            {
                Point point = new Point(date[0,i], date[1, i]);
                ListPoint.Add(point);
            }


            ListPoint.Sort();

            HttpConfiguration config = new HttpConfiguration();
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            return Request.CreateResponse(HttpStatusCode.Created, ListPoint, config); ;
        }

        [HttpGet]
        public string Get()
        {
            //double angle = 98;
            //double x1 = 2;
            //double y1 = 4;

            //double dx = 2;
            //double dy = 0.46;

            //90+90+90
            double angle = 270;
            double x1 = 5.51;
            double y1 = 4.48;

            double dx = 2;
            double dy = 4;

            double x2 = x1 + (dx - x1) * Math.Cos(angle * Math.PI / 180) - (dy - y1) * Math.Sin(angle * Math.PI / 180); //Получили первую координату
            double y2 = y1 + (dx - x1) * Math.Sin(angle * Math.PI / 180) + (dy - y1) * Math.Cos(angle * Math.PI / 180); //Получили вторую координату




            return DateTime.Now.ToString();
        }

        //// POST api/fruits
        //[HttpPost]
        //public HttpResponseMessage Post([FromBody]int[,] points)
        //{

        //    int rows = points.GetUpperBound(0) + 1;
        //    int length = points.Length / rows;


        //    double[] rad = new double[length];



        //    //int x = points[0,0];
        //    //int y = points[1,0];



        //    for (int i = 0; i < length; i++)
        //    {
        //        rad[i] = Radius(0, 0, points[0, i], points[1, i]);
        //    }

        //    //Сортировка
        //    double temp;
        //    int sx, sy;
        //    for (int i = 0; i < length ; i++)
        //    {
        //        for (int j = 0; j < length -1; j++)
        //        {
        //            if (rad[i] < rad[j])
        //            {
        //                temp = rad[i];
        //                sx = points[0, i];
        //                sy = points[1, i];

        //                rad[i] = rad[j];
        //                points[0, i] = points[0, j];
        //                points[1, i] = points[1, j];

        //                rad[j] = temp;
        //                points[0, j] = sx;
        //                points[1, j] = sy;
        //            }
        //        }
        //    }
        //    return Request.CreateResponse(HttpStatusCode.Created, points); ;
        //}


    }
}
