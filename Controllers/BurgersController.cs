using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using burgershack_c.Models;
using burgershack_c.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace burgershack_c.Controllers
{
    [Route("api/[controller]")]
    public class BurgersController : Controller
    {
        private readonly BurgerRepository db;
        public BurgersController(BurgerRepository burgerRepo)
        {
            db = burgerRepo;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Burger> Get()
        {
            return db.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Burger Get(int id)
        {
            return db.GetById(id);
        }

        // POST api/values
        [HttpPost]
        public Burger Post([FromBody]Burger burger)
        {
            return db.Add(burger);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Burger Put(int id, [FromBody]Burger burger)
        {
            if (ModelState.IsValid)
            {
                return db.GetOneByIdAndUpdate(id, burger);
            }
            return null;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return db.FindByIdAndRemove(id);
        }
    }
}
