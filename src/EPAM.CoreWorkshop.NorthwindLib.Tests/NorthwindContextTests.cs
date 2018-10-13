using EPAM.CoreWorkshop.NorthwindLib.Model;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EPAM.CoreWorkshop.NorthwindLib.Tests
{
    [TestClass]
    public class NorthwindContextTests
    {
        [TestMethod]
        public void GetDataFromSqlServer()
        {
            var optionsBuilder = new DbContextOptionsBuilder<NorthwindContext>();
            optionsBuilder.UseSqlServer("Server=(local);Database=Northwind;Integrated Security=True;");

            var context = new NorthwindContext(optionsBuilder.Options);

            context.Products.CountAsync().Result.Should().Be(77);
            context.Categories.CountAsync().Result.Should().Be(8);
        }

        [TestMethod]
        public void GreateMySQLDb()
        {
            var optionsBuilder = new DbContextOptionsBuilder<NorthwindContext>();
            optionsBuilder.UseMySql("Server=epam-corewsvm26.northeurope.cloudapp.azure.com;Database=Northwind;User=user;Password=123;Compress=true",
                opt =>
                {
                    opt.CommandTimeout(200);
                });


            var context = new NorthwindContext(optionsBuilder.Options);

            context.Database.EnsureCreated();
        }
    }
}
