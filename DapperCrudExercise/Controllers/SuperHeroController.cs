using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DapperCrudExercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly IConfiguration config;

        public SuperHeroController(IConfiguration config)
        {
            this.config = config;
        }
        [HttpGet]
        public async Task<ActionResult
            <List<SuperHero>>> GetAllSuperHeroes()
        {
            using var connection = new SqlConnection
                (config.GetConnectionString("DapperExercise"));
            IEnumerable<SuperHero> heroes = await SelectAllHeroes(connection);
            return Ok(heroes);
        }

        private static async Task<IEnumerable<SuperHero>> SelectAllHeroes(SqlConnection connection)
        {
            return await connection.QueryAsync<SuperHero>("select * from SuperHero.SuperHeroes");
        }
        [HttpPost]
        public async Task<ActionResult
         <List<SuperHero>>> CreateHero(SuperHero hero)
        {
            using var connection = new SqlConnection
                (config.GetConnectionString("DapperExercise"));
            await connection.ExecuteAsync("insert into " +
                "SuperHero.SuperHeroes(name,firstname,lastname,place)" +
                "values (@Name, @Firstname, @LastName, @Place)", hero);
            return Ok(await SelectAllHeroes(connection));
        }
        [HttpPut]
        public async Task<ActionResult
         <List<SuperHero>>> UpdateHero(SuperHero hero)
        {
            using var connection = new SqlConnection
                (config.GetConnectionString("DapperExercise"));
            await connection.ExecuteAsync("update SuperHero.SuperHeroes set name = @Name, firstname= @FirstName, lastname = @LastName, place = @Place where id = @Id", hero);
            return Ok(await SelectAllHeroes(connection));
        }
        [HttpDelete("{heroId}")]
        public async Task<ActionResult
         <List<SuperHero>>> DeleteHero(int heroId)
        {
            using var connection = new SqlConnection
                (config.GetConnectionString("DapperExercise"));
            await connection.ExecuteAsync("delete from SuperHero.SuperHeroes where id = @Id", new {Id = heroId});
            return Ok(await SelectAllHeroes(connection)); 
        }

        [HttpGet("{heroId}")]
        public async Task<ActionResult
           <SuperHero>> GetHeroe(int heroId)
        {
            using var connection = new SqlConnection
                (config.GetConnectionString("DapperExercise"));
            var hero = await connection.QueryFirstAsync<SuperHero>
                ("select * from SuperHero.SuperHeroes where id = @Id",
                new {Id = heroId});
            return Ok(hero);
        }  
    }
}
