using Meals.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace Meals.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MealController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MealController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<Meal> Get()
        {
            try
            {
                string query = @"
                select * from Meal m
                inner join Category c on m.categoryId = c.categoryId
                inner join Provider p on m.providerId = p.providerId";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("MealsAppCon");
                MySqlDataReader myReader;
                using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
                {
                    mycon.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                    }
                    return table.AsEnumerable().Select(row => new Meal
                    {
                        MealId = Convert.ToInt32(row["mealId"]),
                        MealName = row["mealName"].ToString(),
                        Price = Convert.ToDecimal(row["price"]),
                        Provider = new Provider()
                        {
                            ProviderId = Convert.ToInt32(row["providerId"]),
                            ProviderName = row["providerName"].ToString()
                        },
                        Category = new Category()
                        {
                            CategoryId = Convert.ToInt32(row["categoryId"]),
                            CategoryName = row["categoryName"].ToString()
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<Meal>();
            }
        }

        [HttpPost]
        public JsonResult Post(Meal meal)
        {
            if (meal.MealName == "" || meal.Category.CategoryId == 0 || meal.Provider.ProviderId == 0 || meal.Price == 0)
            {
                return new JsonResult(false);
            }
            else
            {
                try
                {
                    string query = @"
                    insert into 
                    Meal(mealName, categoryId, providerId, Price) 
                    values
                    (@mealName, @categoryId, @providerId, @price)";
                    DataTable table = new DataTable();
                    string sqlDataSource = _configuration.GetConnectionString("MealsAppCon");
                    MySqlDataReader myReader;
                    using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
                    {
                        mycon.Open();
                        using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                        {
                            myCommand.Parameters.AddWithValue("@mealName", meal.MealName);
                            myCommand.Parameters.AddWithValue("@categoryId", meal.Category.CategoryId);
                            myCommand.Parameters.AddWithValue("@providerId", meal.Provider.ProviderId);
                            myCommand.Parameters.AddWithValue("@price", meal.Price);
                            myReader = myCommand.ExecuteReader();
                            table.Load(myReader);

                            myReader.Close();
                            mycon.Close();
                        }
                    }
                    return new JsonResult(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return new JsonResult(false);
                }
            }
        }

        [HttpPut]
        public JsonResult Put(Meal meal)
        {
            if (meal.MealId == 0 || meal.MealName == "" || meal.Category.CategoryId == 0 || meal.Provider.ProviderId == 0 || meal.Price == 0)
            {
                return new JsonResult(false);
            }
            else
            {
                try
                {
                    string query = @"
                    update Meal set
                    mealName = @mealName,
                    categoryId = @categoryId,
                    providerId = @providerId, 
                    price =@price
                    where mealId = @mealId";
                    DataTable table = new DataTable();
                    string sqlDataSource = _configuration.GetConnectionString("MealsAppCon");
                    MySqlDataReader myReader;
                    using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
                    {
                        mycon.Open();
                        using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                        {
                            myCommand.Parameters.AddWithValue("@mealId", meal.MealId);
                            myCommand.Parameters.AddWithValue("@mealName", meal.MealName);
                            myCommand.Parameters.AddWithValue("@categoryId", meal.Category.CategoryId);
                            myCommand.Parameters.AddWithValue("@providerId", meal.Provider.ProviderId);
                            myCommand.Parameters.AddWithValue("@price", meal.Price);
                            myReader = myCommand.ExecuteReader();
                            table.Load(myReader);

                            myReader.Close();
                            mycon.Close();
                        }
                    }
                    return new JsonResult(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return new JsonResult(false);
                }
            }
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            if (id == 0)
            {
                return new JsonResult(false);
            }
            else
            {
                try
                {
                    string query = @"
                    delete from Meal 
                    where mealId = @mealId";
                    DataTable table = new DataTable();
                    string sqlDataSource = _configuration.GetConnectionString("MealsAppCon");
                    MySqlDataReader myReader;
                    using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
                    {
                        mycon.Open();
                        using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                        {
                            myCommand.Parameters.AddWithValue("@mealId", id);
                            myReader = myCommand.ExecuteReader();
                            table.Load(myReader);

                            myReader.Close();
                            mycon.Close();
                        }
                    }
                    return new JsonResult(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return new JsonResult(false);
                }
            }
        }
    }
}
