using Microsoft.AspNetCore.Mvc;
using Meals.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace Meals.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly IConfiguration _configuration;
        public CategoryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            string query = @"
                select *
                from
                category";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MealsAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mmyCommand = new MySqlCommand(query, mycon))
                {
                    myReader = mmyCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
                return table.AsEnumerable().Select(row => new Category
                {
                    CategoryId = Convert.ToInt32(row["categoryId"]),
                    CategoryName = row["categoryName"].ToString()

                });
            }
        }
    }
}