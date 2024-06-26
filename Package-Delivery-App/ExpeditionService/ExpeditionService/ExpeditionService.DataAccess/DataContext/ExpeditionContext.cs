﻿using ExpeditionService.DataAccess.Models;
using MongoFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionService.DataAccess.DataContext
{
    public class ExpeditionContext : MongoDbContext
    {
        public ExpeditionContext(IMongoDbConnection connection) :base(connection)
        {

        }

        public MongoDbSet<Person> Persons { get; set; }
        public MongoDbSet<Package> Packages { get; set; }
        public MongoDbSet<Location> Locations { get; set; } 
        public MongoDbSet<Shipment> Shipments { get; set; } 



    }
}
