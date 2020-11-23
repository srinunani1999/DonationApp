using DonarService.Data;
using DonarService.Models;
using DonarService.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DonarTest
{
    public class Tests
    {
        List<DonarService.Models.Donar> Donars = new List<DonarService.Models.Donar>();
        IQueryable<DonarService.Models.Donar> donardata;
        Mock<DbSet<DonarService.Models.Donar>> mockset;
        Mock<DbSet<Organization>> orgmockset;

        Mock<DonarDbContext> donarcontextmock;
      
        [SetUp]
        public void Setup()
        {
            Donars = new List<DonarService.Models.Donar>()
            {
                new DonarService.Models.Donar()
                {
                    DonorId=1,Amount=200,DateOfDonation=DateTime.Parse("10-10-2020"),DonorName="srinu",organization_Id=2

                },
                 new DonarService.Models.Donar()
                {
                    DonorId=2,Amount=300,DateOfDonation=DateTime.Parse("11-10-2020"),DonorName="nani",organization_Id=1

                },
                     new DonarService.Models.Donar()
                {
                    DonorId=2,Amount=300,DateOfDonation=DateTime.Parse("11-10-2020"),DonorName="nani",organization_Id=2

                },
            };

            donardata = Donars.AsQueryable();
            mockset = new Mock<DbSet<DonarService.Models.Donar>>();


            mockset.SetupAllProperties();
            mockset.As<IQueryable<DonarService.Models.Donar>>().Setup(c => c.Provider).Returns(donardata.Provider);
            mockset.As<IQueryable<DonarService.Models.Donar>>().Setup(c => c.Expression).Returns(donardata.Expression);
            mockset.As<IQueryable<DonarService.Models.Donar>>().Setup(c => c.ElementType).Returns(donardata.ElementType);
            mockset.As<IQueryable<DonarService.Models.Donar>>().Setup(c => c.GetEnumerator()).Returns(donardata.GetEnumerator());

            var contextoptions = new DbContextOptions<DonarDbContext>();

            donarcontextmock = new Mock<DonarDbContext>(contextoptions);
            donarcontextmock.Setup(c => c.DonarDetails).Returns(mockset.Object);
        }

        [Test]
        public void GetAllDonarsTest()
        {
            var donarRepository = new DonarRepository(donarcontextmock.Object);
            var donars = donarRepository.Get();
            Assert.AreEqual(3, donars.Count());
        }

        [Test]
        public void getDonarByIdTest()
        {
            var donarRepository = new DonarRepository(donarcontextmock.Object);

            var donartest = donarRepository.GetById(1);
            Assert.IsNotNull(donartest);
        }



        [Test]
        public void PostDonarTest()
        {
            var donarRepository = new DonarRepository(donarcontextmock.Object);



            var donar = new DonarService.Models.Donar()
            {
                DonorId = 3,
                Amount = 200,
                DateOfDonation = DateTime.Parse("10-10-2020"),
                DonorName = "kodati",
                organization_Id = 1


            };
            var donartest = donarRepository.Add2(donar);
            Assert.AreEqual(3, donartest.DonorId);
        }

    }
}