using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using claimmicroservice.Models;
using claimmicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace claimmicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class claimController : ControllerBase
    {  
        readonly log4net.ILog _log4net;
        Imemberclaimrepo db;
        public claimController(Imemberclaimrepo _db)
        {
            db = _db;
            _log4net = log4net.LogManager.GetLogger(typeof(claimController));
        }

        /// <summary>
        /// GET: api/<claimController>
        /// </summary>
        /// <returns>List<memberclaim></returns>

        [HttpGet]
        public IActionResult Get()
        {
            _log4net.Info("claimController get called");
            try
            {
               // memberclaimrepo ob = new memberclaimrepo();
                return Ok(db.give());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// POST api/<claimController>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>string</returns>

        [HttpPost]
        public IActionResult Post([FromBody] memberclaim obj)
        {
            _log4net.Info("claimController postmethod called");
            if (ModelState.IsValid)
            {
                try
                {
                   // memberclaimrepo ob = new memberclaimrepo();
                    db.create(obj);
                    return Ok("SUCCESSFULLY ADDED");
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return NotFound();

        }
        /// <summary>
        /// PUT api/<claimController>/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] memberclaim obj)
        {
            _log4net.Info("claimController put called");
            try
            {
                memberclaim obb = new memberclaim();
                obb = db.GetClaimStatus(id, obj);
                if (obb != null)
                    return Ok(obb);
                else
                    return BadRequest(obb);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}
