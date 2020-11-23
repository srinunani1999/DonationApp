using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Models;
using OrganizationService.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrganizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        // GET: api/<OrganizationController>
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(OrganizationController));
        private readonly IRepository<Organization> _repository;
        public OrganizationController(IRepository<Organization> repository)
        {

            _repository = repository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Organization>> GetOrganization()
        {
            try
            {
                _log4net.Info("Http Get request Initiated");

                var orgs = _repository.Get();
                if (orgs != null)
                {
                    _log4net.Info("successfully got details");
                    return Ok(orgs);


                }

            }
            catch (Exception e)
            {
                _log4net.Error("No result " + e.Message);
                return new NoContentResult();


            }
            return BadRequest();

        }

        [HttpGet("{id}")]
        public ActionResult<Organization> GetOrganization(int id)
        {
            try
            {
                _log4net.Info("Http get request initiated with "+id);
                var organization = _repository.GetById(id);

                if (organization == null)
                {
                    _log4net.Info("Organization with that Requested Id not Found");

                    return NotFound();
                }
                _log4net.Info("Found Matching Organization");


                return organization;
            }
            catch (Exception e)
            {

                _log4net.Error("No content Obtained " + e.Message);
                return NotFound();
            }



        }
        [HttpPost]
        public ActionResult<Organization> PostOrganization([FromBody] Organization organization)
        {
            try
            {
                _log4net.Info("HttpPost Request Initiated for Id "+organization.Id);

                if (ModelState.IsValid)
                {
                    _log4net.Info("Model state is  valid for Id "+organization.Id);


                    var addorganization = _repository.Add(organization);


                    return CreatedAtAction("GetOrganization", new { id = organization.Id }, organization);

                }


            }
            catch (Exception e)
            {

                _log4net.Error("Model state is not valid for id "+organization.Id + e.Message);
                return NotFound();
            }
            return BadRequest();

        }
        [HttpPut]
        public ActionResult<Organization> PutOrganization([FromBody] Organization organization)
        {
            try
            {
                _log4net.Info("HttpPost Request Initiated for Id " + organization.Id);

                if (ModelState.IsValid)
                {
                    _log4net.Info("Model state is  valid for Id " + organization.Id);


                    var updateorganization = _repository.Update(organization);

                    if (updateorganization != null)
                    {
                        return Ok(updateorganization);
                    }


                    return BadRequest();

                }


            }
            catch (Exception e)
            {

                _log4net.Error("Model state is not valid for id " + organization.Id + e.Message);
                return NotFound();
            }
            return BadRequest();

        }

    }
}
