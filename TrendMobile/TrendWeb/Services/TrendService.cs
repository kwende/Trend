using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendWeb.DataContracts;

namespace TrendWeb.Services
{
    public class TrendService
    {
        public string ConnectionString { get; set; }

        public TrendService(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<EntryType> InsertJSON(string userId, DateTime syncDate, string json)
        {
            List<EntryType> ret = new List<EntryType>();

            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (MySqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "insert into ClientSyncs (Objects, SyncedOn, UserId) values (@objects, @syncedOn, @userId)";
                    command.Parameters.AddWithValue("objects", json);
                    command.Parameters.AddWithValue("syncedOn", syncDate);
                    command.Parameters.AddWithValue("userId", userId);

                    command.ExecuteNonQuery(); 
                }
            }

            return ret;
        }
    }
}
