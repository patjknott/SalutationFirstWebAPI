using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalutationsFirstWebAPI.Models
{
    public interface ISalutationItemRepository
    {
        void Add(SalutationItem item);
        IEnumerable<SalutationItem> GetAll();
        SalutationItem Find(string word);
        SalutationItem Remove(string word);
        void Update(SalutationItem item);

    }
}
