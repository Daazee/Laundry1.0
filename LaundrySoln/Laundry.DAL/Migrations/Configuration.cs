namespace Laundry.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Laundry.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<Laundry.DAL.LaundryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Laundry.DAL.LaundryContext";
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Laundry.DAL.LaundryContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

           /* LaundryMan LaundryManObj = new LaundryMan();
            LaundryManObj.LaundryManId = 1;
            LaundryManObj.Surname = "Administrator";
            LaundryManObj.Othername = "Administrator";
            LaundryManObj.Sex = "Male";
            LaundryManObj.PhoneNumber = "07053900429";
            LaundryManObj.Address = "TBA";
            LaundryManObj.Username = "admin";
            LaundryManObj.Password = "admin";
            LaundryManObj.Reg_Status = "AD";
            LaundryManObj.Flag = "A";
            LaundryManObj.Keydate = DateTime.Now;
            context.LaundryMans.AddOrUpdate(c => c.Username, LaundryManObj);


            CompanyDetail CompanyDetailObj = new CompanyDetail();
            CompanyDetailObj.Company_Code = "Test";
            CompanyDetailObj.Company_Name = "Test Laundry";
            CompanyDetailObj.Company_ShortName = "Test Laundry";
            CompanyDetailObj.Company_Address = "Lagos";
            CompanyDetailObj.Company_Phone1 = "07053900429";
            CompanyDetailObj.Company_Phone2 = "07053900429";
            CompanyDetailObj.Company_Email = "test@yahoo.com";
            CompanyDetailObj.Company_Username = "admin";
            CompanyDetailObj.Company_Password = "admin";
            CompanyDetailObj.Company_Flag = "A";
            CompanyDetailObj.Company_KeyDate = DateTime.Now;
            context.CompanyDetails.AddOrUpdate(c => c.Company_Username, CompanyDetailObj);*/





        }
    }
}
