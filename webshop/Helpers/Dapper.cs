using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webshop.Models;

namespace webshop.Helpers
{
    internal class Dapper
    {
        public string connString = "Server=tcp:johannesdb.database.windows.net,1433;Initial Catalog=johannesdb;Persist Security Info=False;User ID=jojoSlice;Password=Skolan12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public List<Models.Garment> GetGraments()
        {
            string sql = "SELECT * FROM Garment";

            List<Models.Garment> garments = new List<Models.Garment>();

            using (var connection = new SqlConnection(connString))
            {
                garments = connection.Query<Models.Garment>(sql).ToList();
            }
            return garments;
        }

        public void RemoveGarment(int id)
        {
            string sql = "DELETE FROM Garment WHERE Id = @Id";

            using (var connection = new SqlConnection(connString))
            {
                connection.Execute(sql, new { Id = id });
            }
        }
    }
}

