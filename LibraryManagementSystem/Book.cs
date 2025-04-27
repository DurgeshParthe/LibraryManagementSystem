using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int ISBN { get; set; }
        public bool isAvalable { get; set; }

        public int Quantity { get; set; }


        public void AddBook()
        {
            Book book = new Book();    
            try
            {
                Console.WriteLine("Enter Book Title");
                book.Title = Console.ReadLine();
                //while (string.IsNullOrEmpty(Title))
                //{
                //    Console.WriteLine("Name cannot be empty. Please enter valid name");
                //    book.Title = Console.ReadLine();
                //}

                Console.WriteLine("Enter Book Author");
                book.Author = Console.ReadLine();
                //while (string.IsNullOrEmpty(Author))
                //{
                //    Console.WriteLine("Author cannot be empty. Please enter valid author");
                //    book.Author = Console.ReadLine();
                //}

                Console.WriteLine("Enter Book ISBN");
                book.ISBN = Convert.ToInt32(Console.ReadLine());

                //while (ISBN == null)
                //{
                //    Console.WriteLine("ISBN cannot be empty. Enter a valid ISBN");
                //    book.ISBN = Convert.ToInt32(Console.ReadLine());
                //}

                Console.WriteLine("Enter Book Quantity");
                book.Quantity = Convert.ToInt32(Console.ReadLine());

                DBSource.addBookDB(book);
            }

            catch (Exception ex)
            {
                Console.WriteLine("!OOPS something went wrong. Enter Valid Inputs");
            }
            

            
        }
        //Display All Books ------------------------------------------------------------------------------
        public  void getAllBook()
        {
            try
            {
                string con = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con))
                {


                    connection.Open();

                    string selectQuery = "displayAllBooks";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure; 
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                Console.WriteLine($"BookID:{reader["BookId"]} |Title:{reader["Title"]} |Author:{reader["Author"]} |ISBN:{reader["ISBN"]} |Quantity:{reader["Quantity"]}");
                            }
                        }
                    }
                }
            }

            catch (System.Exception ex)
            {
                Console.WriteLine("!OOPS something went wrong" + ex);
            }
           
        }

        //Search Book By Title---------------------------------------------------------------------------------
        public void getBookByTitle()
        {
            string con = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();

                string searchQuery = "spBookDetailbyTitle";
                using (SqlCommand command = new SqlCommand(searchQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    string title;

                    Console.WriteLine("Enter Book Title To Search");
                    title = Console.ReadLine();
                    command.Parameters.AddWithValue("@title", title);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("Book Found");
                                Console.WriteLine($"BookID:{reader["BookId"]} |Title:{reader["Title"]} |Author:{reader["Author"]} |ISBN:{reader["ISBN"]} |Quantity:{reader["Quantity"]}");
                            }
                           
                        }
                        else
                        {
                            Console.WriteLine("No Book Found By Title Specified By You");
                        }
                        connection.Close();
                    }
                       
                }

                    
            }
               
        }

        //Search Book By ID------------------------------------------------------------------------
        public void getBookById()
        {
            try
            {
                string con = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();

                    string searchQuery = "spBookDetailbyID";
                    using (SqlCommand command = new SqlCommand(searchQuery, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        string bookId;

                        Console.WriteLine("Enter Book ID To Search");
                        bookId = Console.ReadLine();

                        command.Parameters.AddWithValue("@id", bookId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine("Book Found");
                                    Console.WriteLine($"BookID:{reader["BookId"]} |Title:{reader["Title"]} |Author:{reader["Author"]} |ISBN:{reader["ISBN"]} |Quantity:{reader["Quantity"]}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Book With Entered Id Not Found");
                            }

                        }


                    }
                }

            }

            catch(Exception ex) 
            {
                Console.WriteLine("!OOPS something went wrong" + ex);
            }
           
        }

        






        //public   void SqlCommandPrepareEx()
        //{
        //    string con = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
         
        //    using (SqlConnection connection1 = new SqlConnection(con))
        //    {
        //        connection1.Open();
        //        SqlCommand command = new SqlCommand(null, connection1);

        //        // Create and prepare an SQL statement.
        //        command.CommandText =
        //            "INSERT INTO Member ( MemberName , JoinDate) " +
        //            "VALUES ( @name ,@date)";
        //        SqlParameter nameParam = new SqlParameter("@name", SqlDbType.VarChar, 50);
        //        SqlParameter dateParam = new SqlParameter("@date",SqlDbType.DateTime);

        //        nameParam.Value = "Om";
        //        dateParam.Value = DateTime.Now;
        //        command.Parameters.Add(nameParam);
        //        command.Parameters.Add(dateParam);

        //        // Call Prepare after setting the Commandtext and Parameters.
        //        command.Prepare();
        //        command.ExecuteNonQuery();

        //        // Change parameter values and call ExecuteNonQuery.
        //        command.Parameters[0].Value = "Tejas";
        //        command.Parameters[1].Value = DateTime.Now;
        //        command.ExecuteNonQuery();
        //    }
        //}



    }
}
