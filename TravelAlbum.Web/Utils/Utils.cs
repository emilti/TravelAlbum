using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelAlbum.Web.Utils
{
    public class Utils : IUtils
    {
        public int GetCurrentLanguage(Controller controller)
        {
            string query = controller.Request.Url.PathAndQuery;
            if (!(query.Contains("/en")))
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
    }
}