using Microsoft.AspNetCore.Mvc;
using Meals.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace Meals.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProviderController : Controller
    {
        private readonly IConfiguration _configuration;
        public ProviderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<Provider> Get()
        {
            string query = @"
                select *
                from
                provider";

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
                return table.AsEnumerable().Select(row => new Provider
                {
                    ProviderId = Convert.ToInt32(row["providerId"]),
                    ProviderName = row["providerName"].ToString()

                });
            }
        }
    }
}