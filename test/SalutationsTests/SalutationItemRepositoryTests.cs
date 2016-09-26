using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SalutationsFirstWebAPI.Models
{
    public class SalutationItemRepositoryTests
    {
        private ISalutationItemRepository repo { get; set; }
        private SalutationItem checkthis { get; set; }
        public SalutationItemRepositoryTests() 
        {
            checkthis = new SalutationItem { word = "Hi", synonym = "Bonjour" };
            repo = new SalutationItemRepository(); 
        }
        [Fact]
        public void TestIfDBHasData()
        { 
            Assert.True(CheckifinDB(checkthis));
        }
        [Fact]
        public void TestAdd() {
            repo.Add(checkthis);
            Assert.True(CheckifinDB(checkthis));  
        }

        [Theory]
        [InlineData("Hi")]
        [InlineData("Bye")]
        [InlineData("Yo")]
        [InlineData("What")]
        public void TestDelete(string word) {
            checkthis.word = word;
            Assert.True(CheckifinDB(checkthis)); 
            repo.Remove(word);
            Assert.False(CheckifinDB(checkthis)); 
        }
        [Theory]
        [InlineData("Hi", "Marhaba")] 
        [InlineData("Hi", "Chao")]
        [InlineData("Bye", "Hasta La Vista")]
        public void TestUpdate(string Word, string Synonym) { 
            checkthis.word = Word;
            checkthis.synonym = Synonym;
            repo.Update(checkthis);
            Assert.True(checkthis.synonym == repo.GetAll().FirstOrDefault(x => x.word == Word).synonym, repo.Find(checkthis.word).synonym);
        }

        public bool CheckifinDB(SalutationItem item)
        {
            return (repo.GetAll().Any(x => x.word == item.word));
        }


    }
}