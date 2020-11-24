using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using OrganizationService.Data;
using OrganizationService.Models;
using OrganizationService.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace OrganizationServiceTest
{
    public class Tests
    {
        List<Organization> organizations = new List<Organization>();
        IQueryable<Organization> orgdata;
        Mock<DbSet<Organization>> mockSet;
        Mock<OrganizationDBContext> orgcontextmock;
        [SetUp]
        public void Setup()
        {
            organizations = new List<Organization>()
            {
                new Organization(){Id=1,OrganizationName="tnt",TotalDonations="10000"},
                new Organization(){Id=2,OrganizationName="ynt",TotalDonations="20000"}
             

            };
            orgdata = organizations.AsQueryable();
            mockSet = new Mock<DbSet<Organization>>();

            mockSet.As<IQueryable<Organization>>().Setup(m => m.Provider).Returns(orgdata.Provider);
            mockSet.As<IQueryable<Organization>>().Setup(m => m.Expression).Returns(orgdata.Expression);
            mockSet.As<IQueryable<Organization>>().Setup(m => m.ElementType).Returns(orgdata.ElementType);
            mockSet.As<IQueryable<Organization>>().Setup(m => m.GetEnumerator()).Returns(orgdata.GetEnumerator());
            var p = new DbContextOptions<OrganizationDBContext>();
            orgcontextmock = new Mock<OrganizationDBContext>(p);
            orgcontextmock.Setup(x => x.Organization).Returns(mockSet.Object);

        }

        [Test]
        public void GetAllTest()
        {
            var OrganizationsRepo = new OrganizationRepository(orgcontextmock.Object);
            var orglist = OrganizationsRepo.Get();
            Assert.AreEqual(2, orglist.Count());




        }
        [Test]
        public void GetByIdTest()
        {
            var orgrepo = new OrganizationRepository(orgcontextmock.Object);
            var org = orgrepo.GetById(2);

            Assert.AreEqual(2, org.Id);

        }
        [Test]
        public void PostOrganizationTest()
        {
            var orgrepo = new OrganizationRepository(orgcontextmock.Object);
            var org = orgrepo.Add2(new Organization() { Id = 9, OrganizationName = "tnt", TotalDonations = "10000" });

            Assert.AreEqual(9, org.Id);

        }
        [Test]
        public void GetAllFailTest()
        {
            var OrganizationsRepo = new OrganizationRepository(orgcontextmock.Object);
            var orglist = OrganizationsRepo.Get();
            Assert.AreNotEqual(3, orglist.Count());




        }
        
        [Test]
        public void GetByIdFailTest()
        {
            var orgrepo = new OrganizationRepository(orgcontextmock.Object);
            var org = orgrepo.GetById(2);

            Assert.AreNotEqual(3, org.Id);

        }

        [Test]
        public void PostOrganizationFailTest()
        {
            var orgrepo = new OrganizationRepository(orgcontextmock.Object);
            var org = orgrepo.Add2(new Organization() { Id = 9, OrganizationName = "tnt", TotalDonations = "10000" });

            Assert.AreNotEqual(10, org.Id);

        }
    }
}
