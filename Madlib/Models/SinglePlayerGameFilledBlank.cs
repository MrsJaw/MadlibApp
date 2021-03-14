using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Madlib.Models
{
    public class SinglePlayerGameFilledBlank
    {
        [Key]
        public int Id { get; set; }
        public int MadlibBlankIndex { get; set; }
        //todo: add validation on answer
        public string Answer { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get;set;}
        public int SinglePlayerGameId { get; set; }
        public virtual SinglePlayerGame SinglePlayerGame { get;set;}
    }
}
