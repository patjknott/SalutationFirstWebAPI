using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace SalutationsFirstWebAPI.Models
{
    public class SalutationItemRepository : ISalutationItemRepository
    {
        private static ConcurrentDictionary<string, SalutationItem> _salutations =
            new ConcurrentDictionary<string, SalutationItem>();
        public SalutationItemRepository()
        {
            Add(new SalutationItem { word = "Hi", synonym = "Bonjour" });
            Add(new SalutationItem { word = "Bye", synonym = "Au Revoir" });
        }
        public IEnumerable<SalutationItem> GetAll() {
            return _salutations.Values;
        }
        //TODO Add Logic to Workflow:
        public void Add(SalutationItem item){

            _salutations[item.word] = item;
        }
        public SalutationItem Find(string word) {
            SalutationItem item;
            _salutations.TryGetValue(word, out item);
            return item;
        }
        public SalutationItem Remove(string word) {
            SalutationItem item;
            _salutations.TryRemove(word, out item);
            return item;
        }
        public void Update(SalutationItem item)
        {
            _salutations[item.word] = item;
        }

    }
}
