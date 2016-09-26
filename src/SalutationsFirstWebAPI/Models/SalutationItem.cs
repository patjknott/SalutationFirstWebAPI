using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SalutationsFirstWebAPI.Models
{
    public class SalutationItem
    {
        [Key]
        [Required]
        [StringLength(20)]
        public string word { get; set; }
        [StringLength(20)]
        public string synonym { get; set; }
        //options:
        //public List<string> synonyms { get; set; }
        //public List<synonym> synonyms { get; set; }
        //create a second model named synonyms and then make a list of those.
    }
}
