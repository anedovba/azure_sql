using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleAppDB
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "nedovba.database.windows.net";
                builder.UserID = "anedovba";
                builder.Password = "qwert12345!@#$%";
                builder.InitialCatalog = "anedovba";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("=========================================\n");
                    Console.WriteLine("Логін");
                    string login = Console.ReadLine();
                    Console.WriteLine("Пароль");
                    string pass = Console.ReadLine();
                    connection.Open();
                    string sql = "SELECT u.pass FROM [Users] u where u.login="+ "'"+login+"'";                    
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        string reader = (string)command.ExecuteScalar();
                        {
                            if (reader == pass)
                            {                               
                                sql = "SELECT b.author, b.book_name FROM [Books] b";
                                using (SqlCommand command2 = new SqlCommand(sql, connection))
                                {
                                    using (SqlDataReader reader2 = command2.ExecuteReader())
                                    {
                                        Console.WriteLine("Завдання на літо: ");
                                        while (reader2.HasRows)
                                        {                                            
                                            while (reader2.Read())
                                            {
                                                
                                                Console.WriteLine("Автор: "+reader2["author"] + "; Назва: " + reader2["book_name"]+"\n");
                                            }
                                           
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Невірний логін чи пароль");
                            }
                      
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
