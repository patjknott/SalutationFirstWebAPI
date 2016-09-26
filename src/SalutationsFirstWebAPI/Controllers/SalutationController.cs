using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalutationsFirstWebAPI.Models;

namespace SalutationsFirstWebAPI.Controllers
{
    [Route("api/[controller]")] 
    public class SalutationController : Controller
    {
        public SalutationController(ISalutationItemRepository salutationItems) {
            SalutationItems = salutationItems;
        }
        private readonly ISalutationItemRepository SalutationItems; 
        [HttpGet]
        public IEnumerable<SalutationItem> GetAll() {
            return SalutationItems.GetAll();
        }
        [HttpGet("hiorbye/{word}", Name = "GetHello")]
        public string GetHello(string word) { 
            string greeting = (word.ToUpper() == "HI") ? "hello" : (word.ToUpper() == "BYE") ? "goodbye" : "not hi or bye";
            return greeting;
        }
        [HttpGet("true/true")]
        public bool test() {
            return true;
        }
        [HttpGet("{word}", Name = "GetSalutation")]
        public IActionResult GetByWord(string word) {
            var item = SalutationItems.Find(word);
            if (item == null) {  
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