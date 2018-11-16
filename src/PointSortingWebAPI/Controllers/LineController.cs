using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PointSortingWebAPI.Controllers
{
    public class LineController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage PostLine([FromBody]int[,] date)
        {
            try
            {
                List<Point> ListPoint = DatePointGet.Create(date);
                ListPoint.Sort();

                HttpConfiguration config = DatePointGet.Config();
                return Request.CreateResponse(HttpStatusCode.Created, ListPoint, config);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
