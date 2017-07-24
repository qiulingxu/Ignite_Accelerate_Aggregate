﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.Ignite.Core;
using Apache.Ignite.Core.Cache;
using Apache.Ignite.Core.Cache.Configuration;
using Apache.Ignite.Core.Transactions;
using System.Configuration;
using Apache.Ignite.Core.Cluster;

namespace CacheService
{
    class Program
    {
        static void Main(string[] args)
        {
            //string conn_string = ConfigurationManager.ConnectionStrings["CacheService.Properties.Settings.testConnectionString"].Name;
            Ignition.ClientMode = true;
            using (IIgnite ignite = Ignition.StartFromApplicationConfiguration())
            {

                ICluster cluster = ignite.GetCluster();
                ICollection<IClusterNode> t = cluster.ForRemotes().GetNodes();
                List< IClusterNode> t1=t.ToList<IClusterNode>();
                Console.WriteLine("{0}", t1.ToString());
                PreLoad preload = new PreLoad();
                Query query = new Query();
                var cache = ignite.GetOrCreateCache<int, Customer>("Cache");
                preload.Process(cache);
                query.Process(cache);

                //var cache = ignite.GetOrCreateCache<int, string>("myCache");
                // Store keys in cache (values will end up on different cache nodes).
                /*for (int i = 0; i < 10; i++)
                    cache.Put(i, i.ToString());

                for (int i = 0; i < 10; i++)
                    Console.WriteLine("Got [key={0}, val={1}]", i, cache.Get(i));*/
            }
        }
    }
}
