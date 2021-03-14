using Madlib.Data;
using Madlib.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madlib.Controllers
{
    public class SinglePlayerGameController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SinglePlayerGameController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Madlib.Models.Madlib> madlibs = _db.Madlib;
            return View(madlibs);
        }

        //Start - GET
        public IActionResult Start(int? id)
        {
            Madlib.Models.Madlib obj = _db.Madlib.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                return View(obj);
            }
        }

        //POST - START  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Start(Madlib.Models.Madlib obj)
        {
            if (obj == null)
            {
                return NotFound();
            }
            SinglePlayerGame NewGame = new SinglePlayerGame()
            {
                MadlibId = obj.Id,
                ActiveMadlibBlankIndex = 0
            };
            if (ModelState.IsValid)
            {
                _db.SinglePlayerGame.Add(NewGame);
                _db.SaveChanges();
                SinglePlayerGameFilledBlank nextBlank = AddNextBlankToAnswers(NewGame);
                return RedirectToAction("Play", nextBlank);
            }
            else
            {
                return View(obj);
            }
        }

        //GET - PLAY
        [HttpGet]
        public IActionResult Play(SinglePlayerGameFilledBlank nextBlank)
        {
            if (nextBlank == null)
            {
                return NotFound();
            }            
            nextBlank.Category = _db.Category.Find(nextBlank.CategoryId);
            nextBlank.SinglePlayerGame = _db.SinglePlayerGame.Find(nextBlank.SinglePlayerGameId);
            return View(nextBlank);
        }

        //POST - PLAY
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PlayPost(SinglePlayerGameFilledBlank obj)
        {
            if (ModelState.IsValid)
            {
                //Save Answer
                _db.SinglePlayerGameFilledBlank.Update(obj);
                _db.SaveChanges();

                //Update Game
                SinglePlayerGame game = _db.SinglePlayerGame.Where(g => g.Id == obj.SinglePlayerGameId).FirstOrDefault();
                game.ActiveMadlibBlankIndex++;
                
                //If it's the last one, show the results
                if(game.ActiveMadlibBlankIndex > _db.MadlibBlank.Where( b=> b.MadlibId == game.MadlibId).Select(b => b.Index).Max())
                {
                    return RedirectToAction("DisplayResults", game);
                }

                //Otherwise move to the next one
                SinglePlayerGameFilledBlank nextBlank = AddNextBlankToAnswers(game);
                _db.SinglePlayerGame.Update(obj.SinglePlayerGame);
                _db.SaveChanges();

                return RedirectToAction("Play", nextBlank);
            }
            else
            {
                return View(obj);
            }
        }

        private SinglePlayerGameFilledBlank AddNextBlankToAnswers(SinglePlayerGame game)
        {
            MadlibBlank currentBlank = _db.MadlibBlank.Where(b => b.MadlibId == game.MadlibId && b.Index == game.ActiveMadlibBlankIndex).FirstOrDefault();
            SinglePlayerGameFilledBlank nextBlank = new SinglePlayerGameFilledBlank()
            {
                MadlibBlankIndex = currentBlank.Index,
                CategoryId = currentBlank.CategoryId,
                SinglePlayerGameId = game.Id
            };
            _db.SinglePlayerGameFilledBlank.Add(nextBlank);
            _db.SaveChanges();
            return nextBlank;
        }

        //GET - DISPLAYRESULTS
        public IActionResult DisplayResults(SinglePlayerGame game)
        {
            if (game == null)
            {
                return NotFound();
            }
            Madlib.Models.Madlib madLib = _db.Madlib.Find(game.MadlibId);
            string[] Answers = _db.SinglePlayerGameFilledBlank.Where(b => b.SinglePlayerGameId == game.Id).OrderBy(b => b.MadlibBlankIndex).Select(b => b.Answer).ToArray();
            game.Madlib = madLib;
            game.CompletedStory = string.Format(madLib.Text, Answers);
            return View(game);
        }
    }
}
