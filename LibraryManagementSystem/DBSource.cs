using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Data;

namespace LibraryManagementSystem
{


    public interface DetailsInterface
    {
        void DisplayDetails();
    }

    public class DBSource
    {
        //public static List<Book>  BookList = new List<Book>();




        //public  static void AddBookl(Book book)

        //{
        //    BookList.Add(book);
        //    addBookA();
        //    Console.WriteLine("Book Added Successfully");
        //}

        //Insert Book==========================================================================================
        public static void addBookDB(Book book)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string insertSP = "spAddBook";
                    using (SqlCommand command = new SqlCommand(insertSP, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@title", book.Title);

                        command.Parameters.AddWithValue("@author", book.Author);

                        command.Parameters.AddWithValue("@ISBN", book.ISBN);

                        command.Parameters.AddWithValue("@quantity" ,book.Quantity);
                        int rows = command.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            Console.WriteLine("Book Added Successfully");
                        }
                        else
                        {
                            Console.WriteLine(" !OOPS Something Went Wrong");
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(" !OOPS Something went wrong" + ex);
            }
           
        }

        //Insert Member==========================================================================================
        public static void addMemberA(Member member)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = "spAddMember";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Name", member.Name);
                        command.Parameters.AddWithValue("@Address", member.Address);
                        command.Parameters.AddWithValue("@Email", member.Email);
                        command.Parameters.AddWithValue("@PhoneNo", member.PhoneNumber);
                        command.Parameters.AddWithValue("@DateTime", member.JoinDate);


                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Member Added Successfully");

                        }
                        else
                        {
                            Console.WriteLine("OOPs! Something Went Wrong");
                        }

                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("!OOPS Something Went Wrong");
            }
        }



        public static void editBook()
        {
            try
            {
                string con = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();

                    string updateQuery = "spEditBook";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        int bkId;

                        Console.WriteLine("Enter Book ID To Edit");
                        bkId = Convert.ToInt32(Console.ReadLine());
                        command.Parameters.AddWithValue("@bookid", bkId);

                        string displayString = "spBookDetailbyID";
                        SqlCommand command1 = new SqlCommand(displayString, connection);
                        command1.CommandType = System.Data.CommandType.StoredProcedure;
                        command1.Parameters.AddWithValue("@id", bkId);

                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("Confirm that you want to edit following book ");
                                Console.WriteLine($"BookID:{reader["BookId"]} |Title:{reader["Title"]} |Author:{reader["Author"]} |ISBN:{reader["ISBN"]} |Quantity:{reader["Quantity"]}");

                            }


                        }
                        int choice;
                        Console.WriteLine("Enter 1 to edit and 2 to cancel");
                        choice = Convert.ToInt32(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:

                                Console.WriteLine("Enter new Book Title");
                                String Title = Console.ReadLine();
                                command.Parameters.AddWithValue("@title", Title);

                                Console.WriteLine("Enter new Book Author");
                                String Author = Console.ReadLine();
                                command.Parameters.AddWithValue("@author", Author);

                                Console.WriteLine("Enter new Book ISBN");
                                int ISBN = Convert.ToInt32(Console.ReadLine());
                                command.Parameters.AddWithValue("@isbn", ISBN);

                                Console.WriteLine("Enter new Book Quantity");
                                int Quantity = Convert.ToInt32(Console.ReadLine());
                                command.Parameters.AddWithValue("@quantity", Quantity);

                                break;

                            case 2:
                                Console.WriteLine("Edit Request Cancelled");
                                break;
                        }

                        int rows = command.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            Console.WriteLine("Book Edited Successfully");
                        }
                        else
                        {
                            Console.WriteLine("There is issue while editing book .Try Again");
                        }
                    }
                }
            }

            catch(Exception ex)
            {
                Console.WriteLine("!OOPS something went wrong");
            }
         


        }


//Remove Book===========================================================================================================
        public static void removeBook()
        {
            try
            {
                string con = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    string query = "spRemoveBook";

                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        Console.WriteLine("Enter Book ID to delete");
                        int id = Convert.ToInt32(Console.ReadLine());

                        string query1 = "spBookDetailbyID";
                        using (SqlCommand cmd = new SqlCommand(query1, connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id", id);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine($"BookID:{reader["BookId"]} |Title:{reader["Title"]} |Author:{reader["Author"]} |ISBN:{reader["ISBN"]} |Quantity:{reader["Quantity"]}");
                                }
                            }

                           
                        }
                        command.Parameters.AddWithValue("@id",id);


                        int row = command.ExecuteNonQuery();

                        if (row > 0)
                        {
                           Console.WriteLine("Book Deleted Successfully");
                        }
                        else
                        {
                            Console.WriteLine("!OOPS something went wrong");
                        }

                    }
                }
            }

            catch(Exception ex)
            {
                Console.WriteLine("Delete Books Transaction History To Remove Book");
            }
        }

//Remove Member=========================================================================================================

        public static void removeMember()
        {
            try
            {
                string con = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(con))
                {
                    string cmd = "spRemoveMember";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(cmd,connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        Console.WriteLine("Enter member ID to remove");
                        int id = Convert.ToInt32(Console.ReadLine());
                        command.Parameters.AddWithValue("@id",id);
                        int row = command.ExecuteNonQuery();

                        if (row > 0) 
                        {
                            Console.WriteLine("Member removed successfully");
                        }
                        else
                        {
                            Console.WriteLine("There is an issue while removing member. Please try again");
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine("Remove Member's Transaction History To Proceed\n \n" + ex);
            }
           
        }



        //class End=============================================================================

    }



} 


   

