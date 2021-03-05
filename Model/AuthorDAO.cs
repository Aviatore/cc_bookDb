using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;

namespace Codecool.BookDb.Model
{
    public class AuthorDAO : IAuthorDao
    {
        private readonly string _connectionString;
        public AuthorDAO(string connectionString)
        {
            _connectionString = connectionString;

        }

        public void Add(Author author)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {

                    string sql = "INSERT INTO author (first_name, last_name, birth_date) VALUES (@first_name, @last_name, @birth_date) " +
                                 "SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();
                    
                    cmd.Parameters.AddWithValue("@first_name", author.FirstName);
                    cmd.Parameters.AddWithValue("@last_name", author.LastName);

                    string dateFormat = "yyy-MM-dd";
                    string birthDate = author.BirthDate.ToString(dateFormat);
                    cmd.Parameters.AddWithValue("@birth_date", birthDate);
                    //cmd.ExecuteNonQuery();

                    author.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }

        }

        public Author Get(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {

                    string sql = "SELECT first_name, last_name, birth_date FROM author WHERE id = @Id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    if (con.State == ConnectionState.Closed) con.Open();


                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    rdr.Read();

                    string first_name = rdr["first_name"] as string;
                    string last_name = rdr["last_name"] as string;
                    DateTime birthDate = (DateTime)rdr["birth_date"];

                    Author author = new Author(first_name, last_name, birthDate) { Id = id };
                    con.Close();

                    return author;
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public List<Author> GetAll()
        {
            try
            {
                List<Author> results = new List<Author>();

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT id,first_name, last_name, birth_date FROM author";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    if (con.State == ConnectionState.Closed) con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    if(rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            string first_name = rdr["first_name"] as string;
                            string last_name = rdr["last_name"] as string;
                            DateTime birthDate = (DateTime)rdr["birth_date"];
                            int id = (int)rdr["id"];

                            Author author = new Author(first_name, last_name, birthDate) { Id = id };
                            results.Add(author);
                        }
                    }
                    
                    con.Close();

                    return results;
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public void Update(Author author)
        {
            throw new NotImplementedException();
        }
    }
}
