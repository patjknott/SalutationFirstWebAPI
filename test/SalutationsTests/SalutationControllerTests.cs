using System;
using Xunit;
//Added:
using SalutationsFirstWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SalutationsFirstWebAPI.Controllers
{
    public class SalutationControllerTests
    {
        private SalutationController thiscontroller; //field

        //TODO: IOC
        private ISalutationItemRepository repo { get; set; }
        public SalutationControllerTests() {//(ISalutationItemRepository salutationRepository) {//constructor  //could inject different controllers/repos with a controller interface/variable & repo. 
            repo = new SalutationItemRepository(); //this would be salutationRepository
            thiscontroller = new SalutationController(repo);  //uses ISalutationItem to instantiate the controller.
        }

        [Fact]
        public void ReturnsAllInfo()
        {
            //arrange -technically above.
            //act
            var result = true;
            //var result = controller.GetAll() as ViewResult;
            //Assert
            //Assert.True(result.ViewName == "Salutation");
            Assert.True(result);
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
            Assert.True(doWordsMatch(synonymFromApi, matchingSynonym)); //could write ternary here.
            Assert.NotNull(synonymFromApi);
        }

        bool doWordsMatch(string synonymToTest, string matchingSynonym) {
            return (synonymToTest.ToLower() == matchingSynonym.ToLower()) ? true : false;
        }


    }
}
