using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Laundry.Model;


namespace Laundry.DAL
{
   public  class LaundryContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<LaundryMan> LaundryMans { get; set; }

        public DbSet<Clothing> Clothings { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<PaymentDetail> PaymentDetails { get; set; }

        public DbSet<Codes> Code { get; set; }

        public DbSet<CompanyDetail> CompanyDetails { get; set; }

        public DbSet<Remittance> Remittances { get; set; }

        public DbSet<AdminDetail> AdminDetails { get; set; }
    }
}
