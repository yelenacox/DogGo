using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public WalkRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Walk> GetAllWalks()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT wa.Id, wa.Date, wa.Duration, wa.WalkerId, wa.DogId
                        FROM Walks wa
                        JOIN Walker w on wa.WalkerId = w.Id
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Walk> walks = new List<Walk>();
                        while (reader.Read())
                        {
                            Walk walk = new Walk
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                                DogId = reader.GetInt32(reader.GetOrdinal("DogId"))
                            };

                            walks.Add(walk);
                        }

                        return walks;
                    }
                }
            }
        }

        public Walk GetWalkById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT wa.Id, wa.Date, wa.Duration, wa.WalkerId, wa.DogId
                         from Walks wa
                         WHERE wa.Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Walk walk = new Walk
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                                DogId = reader.GetInt32(reader.GetOrdinal("DogId"))
                            };

                            return walk;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public List<Walk> GetWalkByWalkerId(int walkerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT wa.Id, wa.Date, wa.Duration, wa.WalkerId, wa.DogId
                         from Walks wa
                         JOIN Walker w on wa.WalkerId = w.Id
                         WHERE w.Id = @walkerId
                    ";

                    cmd.Parameters.AddWithValue("@walkerId", walkerId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Walk> walks = new List<Walk>();
                        while (reader.Read())
                        {
                            Walk walk = new Walk
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                                DogId = reader.GetInt32(reader.GetOrdinal("DogId"))
                            };

                            walks.Add(walk);
                        }
                        return walks;
                      }
                }
            }
        }

        public string WalkTimeByWalker(int id)
        {
            using (SqlConnection conn = Connection) 
            {
            conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Walks.Id, Duration, WalkerId
                        FROM Walks
                        WHERE WalkerId = @id
                    ";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int walkTime = 0;
                        while (reader.Read())
                        {
                            Walk walk = new Walk
                            {
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            };
                            walkTime += walk.Duration;
                        }
                    walkTime = walkTime / 60;
                        string totalWalkTime = $"{walkTime / 60}hr {walkTime% 60}min";
                        return totalWalkTime;
                    }

                }
            }
        }
    }
}

