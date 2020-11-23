using OrganizationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrganizationService.Data;

namespace OrganizationService.Repositories
{
    public class OrganizationRepository : IRepository<Organization>
    {
        OrganizationDBContext _context = null;
        public OrganizationRepository(OrganizationDBContext context)
        {
            this._context = context;
        }
        public int Add(Organization org)
        {
            _context.Organization.Add(org);
            return _context.SaveChanges();

        }


        public Organization Add2(Organization org)
        {
            _context.Organization.Add(org);
            _context.SaveChanges();
            return org;



        }
        public IEnumerable<Organization> Get()
        {
            var organizations = _context.Organization.ToList();

            return organizations;

        }

        public Organization GetById(int id)
        {
            var org = _context.Organization.FirstOrDefault(c=>c.Id==id);
            return org;
        }

        public Organization Update(Organization organization)
        {
            _context.Organization.Update(organization);
            _context.SaveChanges();
            return organization;
        }
    }
}
