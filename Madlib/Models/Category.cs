using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Madlib.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Example { get; set; }
        public virtual ICollection<MadlibBlank> MadlibBlanks { get; set; }
        public virtual ICollection<SinglePlayerGameFilledBlank> SinglePlayerGameFilledBlanks { get; set; }

        public Category()
        {
            MadlibBlanks = new HashSet<MadlibBlank>();
            SinglePlayerGameFilledBlanks = new HashSet<SinglePlayerGameFilledBlank>();
        }
    }
}
