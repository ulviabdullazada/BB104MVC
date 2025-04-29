using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MVCEmpty.Models;

namespace MVCEmpty.Controllers
{
    public class PizzaController : Controller
    {
        const string connectionString = @"Server=.\SQLEXPRESS;Database=PizzaMizzaConsole;Trusted_Connection=True;TrustServerCertificate=True";
        public async Task<IActionResult> Index()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var pizzas = await conn.QueryAsync<Pizza>("SELECT * FROM Products");
                ViewData["Pizzas"] = pizzas;
            }
            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            if (id < 1) return BadRequest();
            string query = """
                SELECT [i].* FROM PizzaIngredients as [pi]
                JOIN Ingredients as i
                ON i.Id = pi.IngredientId
                WHERE pi.ProductId =
            """ + id;
            using (SqlConnection conn = new(connectionString))
            {
                ViewBag.Ingredients = await conn.QueryAsync<Ingredient>(query!);
            }
            ViewBag.Id = id;
            return View();
        }
        public async Task<IActionResult> Add(int id)
        {
            using (SqlConnection conn = new(connectionString))
            {
                ViewBag.Ingredients = await conn.QueryAsync<Ingredient>("SELECT * FROM Ingredients");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id, IEnumerable<int> ids)
        {
            string query = "SELECT Id FROM Ingredients WHERE Id in @ids";
            IEnumerable<int> existIds = [];
            using (SqlConnection conn = new(connectionString))
            {
                existIds = (await conn.QueryAsync<int>(query, new { ids = ids }));
            }
            if (existIds.Count() == 0) return NotFound();
            using (SqlConnection conn = new(connectionString))
            {
                foreach (var item in existIds)
                {
                    await conn.ExecuteAsync("INSERT INTO PizzaIngredients VALUES (@pizzaId, @ingredientId)", new
                    {
                        pizzaId = id,
                        ingredientId = item
                    });
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
