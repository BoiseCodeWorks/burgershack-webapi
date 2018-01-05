using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using burgershack_c.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace burgershack_c.Repositories
{
    public class BurgerRepository
    {
        private readonly IDbConnection _db;

        public BurgerRepository(IDbConnection db)
        {
            _db = db;
        }

        // Find One Find Many add update delete
        public IEnumerable<Burger> GetAll()
        {
            return _db.Query<Burger>("SELECT * FROM Burgers");
        }

        public Burger GetById(int id)
        {
            return _db.QueryFirstOrDefault<Burger>($"SELECT * FROM Burgers WHERE id = @id", id);
        }

        public Burger Add(Burger burger)
        {

            int id = _db.ExecuteScalar<int>("INSERT INTO Burgers (Name, Description, Price)"
                        + " VALUES(@Name, @Description, @Price); SELECT LAST_INSERT_ID()", new
                        {
                            burger.Name,
                            burger.Description,
                            burger.Price
                        });
            burger.Id = id;
            return burger;

        }

        public Burger GetOneByIdAndUpdate(int id, Burger burger)
        {
            return _db.QueryFirstOrDefault<Burger>($@"
                UPDATE Burgers SET  
                    Name = @Name,
                    Description = @Description,
                    Price = @Price
                WHERE Id = {id};
                SELECT * FROM Burgers WHERE id = {id};", burger);
        }

        public string FindByIdAndRemove(int id)
        {
            var success = _db.Execute(@"
                DELETE FROM Burgers WHERE Id = @id
            ", id);
            return success > 0 ? "success" : "umm that didnt work";
        }
    }
}
