using System;
using Nancy.Bootstrapper;
using System.Diagnostics;

namespace chezzles.nancy.api.Config
{
    public class ApplicationStartup : IApplicationStartup
    {
        public void Initialize(IPipelines pipelines)
        {
            pipelines.OnError.AddItemToEndOfPipeline((ctx, ex) => {
                Debug.WriteLine($"Error on request {ex.Message}");
                return null;
            });
        }
    }
}