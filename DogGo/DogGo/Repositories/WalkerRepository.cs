﻿using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using DogGo.Utils;


namespace DogGo.Repositories
{
    public class WalkerRepository
    {
        
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public WalkerRepository(IConfiguration config)
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

        public List<Walker> GetAllWalkers()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name], ImageUrl, NeighborhoodId
                        FROM Walker 
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walker> walkers = new List<Walker>();
                    while (reader.Read())
                    {
                        Walker walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };

                        if (reader.IsDBNull(reader.GetOrdinal("ImageUrl")) == false)
                        {
                            walker.ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl"));
                        }
                        if (reader.IsDBNull(reader.GetOrdinal("NeighborhoodId")) == false)
                        {
                            walker.NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"));
                        }

                        walkers.Add(walker);
                    }

                    reader.Close();

                    return walkers;
                }
            }
        }
        public List<Walker> GetWalkersInNeighborhood(int neighborhoodId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT w.Id, w.[Name], w.ImageUrl, w.NeighborhoodId
                FROM Walker w
                JOIN Neighborhood n ON n.Id = w.NeighborhoodId
                WHERE NeighborhoodId = @neighborhoodId
            ";

                    cmd.Parameters.AddWithValue("@neighborhoodId", neighborhoodId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walker> walkers = new List<Walker>();
                    while (reader.Read())
                    {
                        Walker walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                            
                        };

                        if (reader.IsDBNull(reader.GetOrdinal("ImageUrl")) == false)
                        {
                            walker.ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl"));
                        }
                        if (reader.IsDBNull(reader.GetOrdinal("NeighborhoodId")) == false)
                        {
                            walker.NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"));
                        }

                        walkers.Add(walker);
                    }

                    reader.Close();

                    return walkers;
                }
            }
        }

        public void UpdateWalker(Walker walker)
        {
            using (SqlConnection conn = Connection)
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                conn.Open();

                    cmd.CommandText = @"
                            UPDATE Walker
                            SET 
                                [Name] = @name, 
                                ImageUrl = @imageUrl, 
                                NeighborhoodId = @neighborhoodId
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@name", walker.Name);
                    UpdateNullable.SetNullableString(cmd, "@imageUrl", walker.ImageUrl);
                    cmd.Parameters.AddWithValue("@neighborhoodId", walker.NeighborhoodId);
                    cmd.Parameters.AddWithValue("@id", walker.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteWalker(int walkerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Walker
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", walkerId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddWalker(Walker walker)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Walker ( [Name], ImageUrl, NeighborhoodId)
                    OUTPUT INSERTED.ID
                    VALUES (@name, @imageUrl, @neighborhoodId);
                ";

                    cmd.Parameters.AddWithValue("@name", walker.Name);
                    cmd.Parameters.AddWithValue("@imageUrl", walker.ImageUrl);
                    cmd.Parameters.AddWithValue("@neighborhoodId", walker.NeighborhoodId);

                    int id = (int)cmd.ExecuteScalar();

                    walker.Id = id;
                }
            }
        }

        public Walker GetWalkerById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name], ImageUrl, NeighborhoodId
                        FROM Walker
                        WHERE Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Walker walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };

                        if (reader.IsDBNull(reader.GetOrdinal("ImageUrl")) == false)
                        {
                            walker.ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl"));
                        }
                        if (reader.IsDBNull(reader.GetOrdinal("NeighborhoodId")) == false)
                        {
                            walker.NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"));
                        }

                        reader.Close();
                        return walker;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }
    }
}