using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalutationsFirstWebAPI.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SalutationsFirstWebAPI.Controllers
{
    [Route("api/[controller]")] //controller is salutation. or controllername-"controller" = controllername route.
    public class SalutationController : Controller
    {
        public SalutationController(ISalutationItemRepository salutationItems) { //constructor injection (Startup.cs)
            SalutationItems = salutationItems;
        }
        private readonly ISalutationItemRepository SalutationItems; //use fields, don't need to modify this.

        [HttpGet]
        public IEnumerable<SalutationItem> GetAll() {
            return SalutationItems.GetAll();
        }
        //this test will return hello or goodbye based upon its input word string.  
        [HttpGet("hiorbye/{word}", Name = "GetHello")]
        public string GetHello(string word) { //IActionResult
            string greeting = (word.ToUpper() == "HI") ? "hello" : (word.ToUpper() == "BYE") ? "goodbye" : "not hi or bye";
            return greeting;
            //return new ObjectResult(greeting);
        }

        //This method is my first test to ensure the test will talk to this controller.  
        [HttpGet("true/true")]
        public bool test() {
            return true;
        }
        [HttpGet("{word}", Name = "GetSalutation")]
        public IActionResult GetByWord(string word) {
            var item = SalutationItems.Find(word);
            if (item == null) {  //guard clause
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpPost]
        public IActionResult Create([FromBody] SalutationItem item) {
            if (item == null) {
                return BadRequest();
            }
            SalutationItems.Add(item);
            return CreatedAtRoute("GetSalutation", new { id = item.word }, item);
        }
        [HttpPut("{word}")]
        public IActionResult Update(string word, [FromBody] SalutationItem item) {
            if (item == null || item.word != word) {
                return BadRequest();
            }

            var salutation = SalutationItems.Find(word);
            if (salutation == null) {
                return NotFound();
            }
            SalutationItems.Update(item);
            return new NoContentResult();
        }
        [HttpPatch("{word}")]
        public IActionResult Update([FromBody] SalutationItem item, string word) {
            if (item == null) {
                return BadRequest();
            }
            var salutation = SalutationItems.Find(word);
            if(salutation == null)
            {
                return NotFound();
            }
            SalutationItems.Update(item);
            return new NoContentResult();
        }
        [HttpDelete("{word}")]
        public IActionResult Delete(string word) {
            var salutation = SalutationItems.Find(word);
            if (salutation == null) {
                return NotFound();
            }
            SalutationItems.Remove(word);
            return new NoContentResult();
        }
    }
}










/*

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
*/