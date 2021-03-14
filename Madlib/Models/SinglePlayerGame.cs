using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Madlib.Models
{
    public class SinglePlayerGame
    {
        [Key]
        public int Id { get; set; }
        public int ActiveMadlibBlankIndex { get; set; }
        public string CompletedStory { get; set; }
        public int MadlibId { get; set; }
        public virtual Madlib Madlib { get; set; }
        public virtual ICollection<SinglePlayerGameFilledBlank> Answers { get; set; }

        public SinglePlayerGame()
        {
            Answers = new HashSet<SinglePlayerGameFilledBlank>();
        }
    }
}
