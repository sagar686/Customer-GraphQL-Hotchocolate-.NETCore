using System.Linq;
using CustomerGraphQLApi.Models;

namespace CustomerGraphQLApi.Data
{
    /// <summary>
    /// Used for Code First Migrations to change the database schema instead of dropping and re-creating the database
    /// </summary>
    public static class DbInitializer
    {
        public static void Initialize(CustomerContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Customers.
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            var Customer = new Customer[]
            {
                new Customer{Email="Alexander.Carson@graphqldemo.com",Name="Alexander Carson",Code=1001,Status=Status.Active,IsBlocked = false},
                new Customer{Email="Alonso.Meredith@graphqldemo.com",Name="Alonso Meredith",Code=1002,Status=Status.Active,IsBlocked = false},
                new Customer{Email="Anand.Arturo@graphqldemo.com",Name="Anand Arturo",Code=1003,Status=Status.Active,IsBlocked = false},
                new Customer{Email="Barzdukas.Gytis@graphqldemo.com",Name="Barzdukas Gytis",Code=1004,Status=Status.Active,IsBlocked = false},
                new Customer{Email="Li.Yan@graphqldemo.com",Name="Li Yan",Code=1005,Status=Status.Active,IsBlocked = false},

                new Customer{Email="Justice.Peggy@graphqldemo.com",Name="Justice Peggy",Code=1006,Status=Status.Active,IsBlocked = true}, //Blocked
                new Customer{Email="Norman.Laura@graphqldemo.com",Name="Norman Laura",Code=1007,Status=Status.Inactive,IsBlocked = false},//Inactive
                new Customer{Email="Olivetto.Nino@graphqldemo.com",Name="Olivetto Nino",Status=Status.Active,IsBlocked = false}//Code is null
            };

            foreach (Customer s in Customer)
            {
                context.Customers.Add(s);
            }

            context.SaveChanges();
        }

    }
}
