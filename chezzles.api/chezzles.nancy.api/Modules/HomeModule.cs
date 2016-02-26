using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chezzles.nancy.api.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => "Wellcome to Chezzles!";
        }
    }
}