﻿using Microsoft.Data.SqlClient;
using Roommates43.Models;
using System.Collections.Generic;

namespace Roommates43.Repositories
{

    class RoommamteRepository : BaseRepository
    {
        class RoommateRepository : BaseRepository
        {
            public RoommateRepository(string connectionString) : base(connectionString) { }

            public Roommate GetById(int id)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        //don't forget this SQL query
                        cmd.CommandText = @"SELECT Firstname, RentPortion, r.Name FROM Roommate
LEFT JOIN Room r on r.Id = Roommate.RoomId
WHERE Roommate.Id = @id; ";
                        cmd.Parameters.AddWithValue("@id", id);
                        SqlDataReader reader = cmd.ExecuteReader();

                        Roommate roommate = null;

                        if (reader.Read())
                        {
                            roommate = new Roommate
                            {
                                Id = id,
                                Firstname = reader.GetString(reader.GetOrdinal("Firstname")),
                                RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                                Room = new Room
                                {
                                    Name = reader.GetString(reader.GetOrdinal("Name"))
                                }
                            };
                        }
                        reader.Close();
                        return roommate;
                    }
                }
            }
            public List<Roommate> GetAll()
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM Roommate";
                        SqlDataReader reader = cmd.ExecuteReader();
                        List<Roommate> roommates = new List<Roommate>();
                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int idValue = reader.GetInt32(idColumnPosition);
                            int nameColumnPosition = reader.GetOrdinal("Firstname");
                            string nameValue = reader.GetString(nameColumnPosition);

                            Roommate roommate = new Roommate
                            {
                                Id = idValue,
                                Firstname = nameValue
                            };

                            roommates.Add(roommate);
                        }
                        reader.Close();
                        return roommates;
                    }
                }
            }
        }
    }
}
