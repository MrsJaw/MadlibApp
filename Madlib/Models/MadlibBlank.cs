using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Madlib.Models
{
    public class MadlibBlank
    {
        [Key]
        public int Id { get; set; }
        public int Index { get; set; }
        public int MadlibId { get; set; }
        public virtual Madlib Madlib {get;set;}
        public int CategoryId { get; set; }
        public virtual Category Category { get;set;}
    }
}
