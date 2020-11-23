using DonarService.Data;
using DonarService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DonarService.Repositories
{
    public class DonarRepository : IRepository<Donar>
    {
        DonarDbContext _context = null;
        public DonarRepository(DonarDbContext donarDbContext)
        {
            _context = donarDbContext;
        }
        public async Task<IEnumerable<Organization>> http()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44353");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/int"));

            var response = await client.GetAsync("/api/organization");
            //var orgs = new List<Organization>();
            var jsoncontent = "";
            if (response.IsSuccessStatusCode)
            {
                 jsoncontent = await response.Content.ReadAsStringAsync();




            }

            var orgs = JsonConvert.DeserializeObject<List<Organization>>(jsoncontent);
            

            return orgs;
            
        }
        public int Add(Donar donar)
        {
            var organizationsList = http().Result.ToList();
            foreach (var item in organizationsList)
            {
                if (donar.organization_Id==item.Id)
                {
                    _context.Add(donar);
                    _context.SaveChanges();
                    break;
                }
            }




            return -1;
        }
        public Donar Add2(Donar donar)
        {
                    _context.Add(donar);
                    _context.SaveChanges();


            return donar;
        }

        public IEnumerable<Donar> Get()
        {
            return _context.DonarDetails.ToList();
        }

        public Donar GetById(int id)
        {
            var donar = _context.DonarDetails.FirstOrDefault(c=>c.DonorId==id);
            return donar;
        }
    }
}
