﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.Hosting.Wcf;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Basta
{
    class Program
    {
        // TODO: move address setting to app.config

        private static readonly Uri BaseUri = new Uri("http://0.0.0.0:3000/");

        static void Main(string[] args)
        {
            using (CreateAndOpenWebServiceHost())
            {
                using (Storage.Data)
                {
                    Console.WriteLine("Service is now running on: {0}", BaseUri);
                    Console.ReadLine();
                }
            }
        }

        private static WebServiceHost CreateAndOpenWebServiceHost()
        {
            var host = new WebServiceHost(
                new NancyWcfGenericService(new DefaultNancyBootstrapper()),
                BaseUri);

            host.AddServiceEndpoint(typeof(NancyWcfGenericService), new WebHttpBinding(), "");
            host.Open();

            return host;
        }
    }
}
