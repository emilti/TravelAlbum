using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace TravelAlbum.HttpModules
{
    public class CultureModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += this.Context_BeginRequest;
        }

        public void Dispose()
        {
        }

        private void Context_BeginRequest(object sender, EventArgs e)
        {
            var urlParts = HttpContext.Current.Request.Url.AbsoluteUri.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            string absoluteUri = HttpContext.Current.Request.Url.AbsoluteUri;
            if (urlParts.Count() > 2 && absoluteUri.Contains("/en"))
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            }
        }
    }
}