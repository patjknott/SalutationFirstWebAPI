using System;
using Xunit;
using SalutationsFirstWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SalutationsFirstWebAPI.Controllers
{
    public class SalutationControllerTests
    {
        private SalutationController thiscontroller; 

        
        private ISalutationItemRepository repo { get; set; }
        //TODO: IOC
        public SalutationControllerTests() {
            repo = new SalutationItemRepository(); 
            thiscontroller = new SalutationController(repo);  
        }
        [Fact]
        public void checkstrue() {
            var result = thiscontroller.test();
            Assert.False(result);
        }

        [Theory]
        [InlineData("Hi", "hello")]
        [InlineData("Bye", "goodbye")]
        [InlineData("other", "another")]
        public void checkGetHello(string word, string matchingSynonym) {
            var synonymFromApi = thiscontroller.GetHello(word);
            Assert.True(doWordsMatch(synonymFromApi, matchingSynonym)); 
            Assert.NotNull(synonymFromApi);
        }
        bool doWordsMatch(string synonymToTest, string matchingSynonym) {
            return (synonymToTest.ToLower() == matchingSynonym.ToLower()) ? true : false;
        }


    }
}
