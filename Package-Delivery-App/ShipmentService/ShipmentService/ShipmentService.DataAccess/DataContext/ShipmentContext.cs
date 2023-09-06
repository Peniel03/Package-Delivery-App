using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.DataAccess.DataContext
{
    /// <summary>
    /// the shipment database context
    /// </summary>
    public class ShipmentContext:DbContext
    {
        /// <summary>
        /// Initializes an instance of <see cref="ShipmentContext"/>
        /// </summary>
        /// <param name="options"></param>
        public ShipmentContext(DbContextOptions<ShipmentContext> options): base(options)
        {
            
        }
        /// <summary>
        /// Function to add configuration for tables
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShipmentContext).Assembly);
            base.OnModelCreating(modelBuilder); 
        }
    }
}
