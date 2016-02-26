using Nancy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace chezzles.nancy.api.Modules
{
    public class PuzzlesModule : NancyModule
    {
        public PuzzlesModule() : base ("/api")
        {
            Get["/puzzles"] = _ => "puzzles";

            Get["/puzzles/{id:int}", runAsync: true] = async (_, token) =>
            {
                return $"Puzzle with ID: {_.id}";
            };

            Delete["/puzzles/{id:int}", runAsync: true] = async (_, token) =>
            {
                // $"Puzzle with ID {_.id} deleted";
                return HttpStatusCode.OK;
            };
        }
    }
}