using System;
using System.Web.Mvc;

namespace TravelAlbum.Controllers
{
    public class CultureController : Controller
    {

        public RedirectToRouteResult RedirectLanguage(string language, string query)
        {
            var urlParts = query.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (urlParts.Length == 2 && !(query.Contains("/bg/") || query.Contains("/en/")))
            {
                return this.RedirectToRoute(new { lang = language, controller = urlParts[0], action = urlParts[1] });
            }
            else if (urlParts.Length == 3 && (query.Contains("/bg/") || query.Contains("/en/")))
            {
                return this.RedirectToRoute(new { lang = language, controller = urlParts[1], action = urlParts[2] });
            }
            else if (urlParts.Length == 3 && !(query.Contains("/bg/") || query.Contains("/en/")))
            {
                return this.RedirectToRoute(new { lang = language, controller = urlParts[0], action = urlParts[1], id = urlParts[2] });
            }
            else if (urlParts.Length == 4 && (query.Contains("/bg/") || query.Contains("/en/")))
            {
                return this.RedirectToRoute(new { lang = language, controller = urlParts[1], action = urlParts[2], id = urlParts[3] });
            }
            else
            {
                return this.RedirectToRoute(new { lang = language, controller = "Home", action = "Index" });
            }
        }
    }
}