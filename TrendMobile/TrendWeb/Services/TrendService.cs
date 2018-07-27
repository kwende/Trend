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

        public List<EntryType> GetEntryTypes()
        {
            List<EntryType> ret = new List<EntryType>();

            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open(); 
                using (MySqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "select UniqueId,Name from EntryType";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            ret.Add(new EntryType
                            {
                                Name = reader["Name"].ToString(),
                                UniqueId = reader["UniqueId"].ToString(),
                            });
                        }
                    }
                }
            }

            return ret;
        }

        public void SyncEntryTypes(string[] entryTypeNames, string[] uniqueIds)
        {
            if (entryTypeNames.Length != uniqueIds.Length)
            {
                throw new ArgumentOutOfRangeException("length of arrays must match.");
            }

            entryTypeNames = entryTypeNames.Select(n => n.ToLower()).ToArray();
            uniqueIds = uniqueIds.Select(n => n.ToLower()).ToArray();

            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                List<string> existingIds = new List<string>();

                using (MySqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "select uniqueid from EntryType";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            existingIds.Add(reader["uniqueid"].ToString().ToLower());
                        }
                    }
                }

                // delete no longer used ones. 
                foreach (string existingId in existingIds)
                {
                    if (!uniqueIds.Contains(existingId))
                    {
                        using (MySqlCommand command = conn.CreateCommand())
                        {
                            command.CommandText = "delete from EntryType where UniqueId=@uniqueid";
                            command.Parameters.AddWithValue("uniqueid", existingId);
                            command.ExecuteNonQuery();

                            command.CommandText = "delete from Entry where EntryTypeUniqueId=@uniqueid";

                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("uniqueid", existingId);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                // add new ones. 
                for (int c = 0; c < uniqueIds.Length; c++)
                {
                    string uniqueId = uniqueIds[c];
                    string name = entryTypeNames[c];

                    if (!existingIds.Contains(uniqueId))
                    {
                        using (MySqlCommand command = conn.CreateCommand())
                        {
                            command.CommandText = "insert into EntryType (Name, UniqueId) values (@name,@uniqueid)";
                            command.Parameters.AddWithValue("name", name);
                            command.Parameters.AddWithValue("uniqueid", uniqueId);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public void AddEntryType(string entryTypeName, string uniqueId)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                entryTypeName = entryTypeName.ToLower();
                uniqueId = uniqueId.ToLower();

                conn.Open();

                bool goAhead = false;
                using (MySqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "select count(0) from EntryType where name = @name";
                    command.Parameters.AddWithValue("name", entryTypeName);

                    goAhead = ((long)command.ExecuteScalar()) == 0;
                }

                if (goAhead)
                {
                    using (MySqlCommand command = conn.CreateCommand())
                    {
                        command.CommandText = "insert into EntryType (Name, UniqueId) values (@name,@uniqueid)";
                        command.Parameters.AddWithValue("name", entryTypeName);
                        command.Parameters.AddWithValue("uniqueid", uniqueId);
                        command.ExecuteNonQuery();
                    }
                }

                return;
            }
        }

        public void DeleteEntryType(string uniqueId)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                uniqueId = uniqueId.ToLower();

                using (MySqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "delete from EntryType where UniqueId=@uniqueid";
                    command.Parameters.AddWithValue("uniqueid", uniqueId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
