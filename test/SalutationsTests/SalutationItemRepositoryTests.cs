using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//added:
using Xunit;

namespace SalutationsFirstWebAPI.Models
{
    public class SalutationItemRepositoryTests
    {
        private ISalutationItemRepository repo { get; set; }
        private SalutationItem checkthis { get; set; }
        public SalutationItemRepositoryTests() //(ISalutationItemRepository salutationRepository, ISalutationItem SalutationItem) 
            //discuss could provide a default in case of failed injection. Yet then how would we know? 
        {
            checkthis = new SalutationItem { word = "Hi", synonym = "Bonjour" };
            //checkthis = new SalutationItem { word = "Yo", synonym = "" }; //testable item.
            repo = new SalutationItemRepository();  //this method could accept a salutationRepository and assign here. IOC
        }
        /*  //A test class may only define ONE public constructor.
        public SalutationItemRepositoryTests(SalutationItem injectedItem, ISalutationItemRepository repository) { //from where do we inject with no startup?
            checkthis = injectedItem; //discuss isalutationItem didn't work as a design pattern. Should we inject an item like this?
            repo = repository; //question injecting abstract classes vs. interfaces.
        }
        */
        [Fact]
        //[Theory]
        //[MemberData("testdata")]
        public void TestIfDBHasData()
        { //(SalutationItem checkthis) {
            
            //Assert.NotEmpty(repo.GetAll());
            Assert.True(CheckifinDB(checkthis));
        }
        public static IEnumerable<object> testdata //this method was an attempt to add salutation items directly to TestIfDBHasData.  I left it in to show work and attempts.
        { //Covariance for value types was not supported.
            get
            {
                List<SalutationItem> tmp = new List<SalutationItem>();
                tmp.Add(new SalutationItem { word = "Hi", synonym = "Bonjour" });
                tmp.Add(new SalutationItem { word = "Bye", synonym = "Au Revoir" });
                return tmp;
            }
        }
        [Fact]
        public void TestAdd() {
            repo.Add(checkthis);
            Assert.True(CheckifinDB(checkthis));  //repeatedly adds the entry, no logic prevents this from happening.
        }

        [Theory]
        [InlineData("Hi")]
        [InlineData("Bye")]
        [InlineData("Yo")]
        [InlineData("What")]
        public void TestDelete(string word) {
            checkthis.word = word;
            Assert.True(CheckifinDB(checkthis)); //checks to see if the word is in the database; if not, then test fails here.
            repo.Remove(word);
            Assert.False(CheckifinDB(checkthis)); //only checks hi because checkthis is only hi; if checkthis could change with each iteration, this would be a better test method.
        }
        [Theory]
        [InlineData("Hi", "Marhaba")] //test works first time, but doesn't work second time.  
        [InlineData("Hi", "Chao")]
        [InlineData("Bye", "Hasta La Vista")]
        public void TestUpdate(string Word, string Synonym) { //attempt to make this test stand alone.
            /*using () {
            }*/ //can't use using changeditem because its not disposable.  
                //SalutationItem changeditem = new SalutationItem { word = Word, synonym = Synonym }; //this causes test to fail because can't reassign a variable twice.  Could make it global.
                /*rather than recreating variable, reuse first variable.  
                if you reuse, it may have side effects, so rewrite tests above to ensure test solidarity.  */
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