using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PointSortingWebAPI.Controllers
{
    public class SpiralController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage PostSpiral([FromBody]int[,] date)
        {
            try
            {
                List<Point> ListPoint = DatePointGet.Create(date);
                ListPoint.Sort();

                int len = ListPoint.Count;
                int centr = (len % 2 == 1) ? (int)len / 2 : (int)len / 2 - 1;

                foreach (Point item in ListPoint)
                {
                    item.RadiusList(ListPoint[centr]);
                }
                ListPoint.Sort();

                List<SpiralPoint> ListSpiral = DatePointGet.SpiralListSend(ListPoint);

                HttpConfiguration config = DatePointGet.Config();
                return Request.CreateResponse(HttpStatusCode.Created, ListSpiral, config);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}
