using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NFLCore.Data;
using NFLCore.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NFLCore.Controllers
{
    [Route("api/[controller]")]
    public class NflController : Controller
    {
        private readonly NflContext _context;

        public NflController(NflContext context)
        {
            _context = context;

            if (_context.Players.Count() == 0 && _context.Teams.Count() == 0 && _context.Teams.Count() <= 8)
            {
                _context.Players.Add(new Player { Id = 1, FirstName = "Tom", LastName = "Brady"});
                _context.Players.Add(new Player { Id = 2, FirstName = "Larry", LastName = "FitzGerald"});
                _context.Players.Add(new Player { Id = 3, FirstName = "Cam", LastName = "Newton"});
                _context.Players.Add(new Player { Id = 4, FirstName = "Eli", LastName = "Manning"});
                _context.Players.Add(new Player { Id = 5, FirstName = "Odell", LastName = "Beckham"});
                _context.Players.Add(new Player { Id = 6, FirstName = "Richard", LastName = "Sherman" });
                _context.Players.Add(new Player { Id = 7, FirstName = "Aaron", LastName = "Donald"});
                _context.Teams.Add(new Team { Id = 1, Name = "New England Patriots", Location = "Foxborough, MA"});
                _context.Teams.Add(new Team { Id = 2, Name = "Arizona Cardinals", Location = "Glendale, AZ" });
                _context.Teams.Add(new Team { Id = 3, Name = "Carolina Panthers", Location = "Charlotte, NC" });
                _context.Teams.Add(new Team { Id = 4, Name = "New York Giants", Location = "East Rutherford, NJ" });
                _context.Teams.Add(new Team { Id = 5, Name = "Cleveland Browns", Location = "Cleveland, OH" });
                _context.Teams.Add(new Team { Id = 6, Name = "San Francisco 49ers", Location = "Santa Clara, CA" });
                _context.Teams.Add(new Team { Id = 7, Name = "Los Angeles Rams", Location = "Los Angeles, CA" });
                _context.SaveChanges();
            }
        }

        private List<Player> RetrieveAllPlayers()
        {
            var playerRecord = from c in _context.Players
                select c;

            return playerRecord.ToList();
        }

        private List<Player> RetrieveAllPlayersByLastName(string lastName)
        {
            var lastNameRecord = from con in _context.Players.Where(c => c.LastName.Contains(lastName))
                select con;

            return lastNameRecord.ToList();
        }

        private List<Team> RetrieveAllTeams()
        {
            var teamRecord = from c in _context.Teams
                select c;

            return teamRecord.ToList();
        }

        private List<Team> SortTeamName()
        {
            var teamRecord = from c in _context.Teams
                orderby c.Name
                select c;

            return teamRecord.ToList();
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // Post for Create a team

        // Post for Create a player

        // Query Player by Id
        [HttpGet("players/{id}")]
        public async Task<ActionResult<Player>> GetPlayersById(int id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // Get All players
        [HttpGet("players")]
        public List<Player> GetAllPlayers()
        {
            return RetrieveAllPlayers();
        }

        // Get Players by last name
        [HttpGet("players/lastname={lastname}")]
        public List<Player> GetPlayersByLastName(string lastName)
        {
            return RetrieveAllPlayersByLastName(lastName);
        }

        // Remove a player
        [HttpDelete("players/{id}")]
        public async Task<IActionResult> RemovePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Get players by team

        // Get teams by id
        [HttpGet("teams/{id}")]
        public async Task<ActionResult<Team>> GetTeamsById(int id)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // Get All teams
        [HttpGet("teams/")]
        public List<Team> GetAllTeams()
        {
            return RetrieveAllTeams();
        }

        // Get teams ordered by location or name
        //[HttpGet("teams")]
        //public List<Team> GetOrderedTeams([FromQuery(Name = "Name")]string orderBy)
        //{
            
        //}

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
