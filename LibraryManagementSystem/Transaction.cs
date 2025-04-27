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
    public class Transaction
    {
        public int BorrowID;
        public int BookID;
        public int MemberId;
        public DateTime BorrowDate;
        public DateOnly ReturnDate;
        public string Status;


        public  void addBorrowDetails()
        {
            try
            {
                string con = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con))
                {
                    string query = "spBorrowBook";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        
                        connection.Open();

                        Console.WriteLine("Please enter Book ID to borrow ");
                        BookID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Please enter Member Id");
                        MemberId = Convert.ToInt32(Console.ReadLine());

                        command.Parameters.AddWithValue("@bookid", BookID);
                        command.Parameters.AddWithValue("@memberid", MemberId);
                        command.Parameters.AddWithValue("@borrowdate", DateTime.Now);
                        command.Parameters.AddWithValue("@returndate", DateTime.Now.AddDays(10));

                        int row = command.ExecuteNonQuery();
                        if (row > 0)
                        {
                            Console.WriteLine("Book borrowed successfully");
                        }
                        else
                        {
                            Console.WriteLine("There is an issue while borrowing book or Member borrowed book alraedy");
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Oops! something went wrong" + ex);
            }

        }


        public void addReturnDetails()
        {
            try
            {
                string con = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(con))
                {
                    string query = "spReturnBook";

                    using (SqlCommand command = new SqlCommand(query , connection))
                    {
                         command.CommandType = CommandType.StoredProcedure;
                         connection.Open();
                         Console.WriteLine("Enter book Id to return");
                         BookID = Convert.ToInt32(Console.ReadLine());
                         Console.WriteLine("Enter Member Id ");
                         MemberId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Return Date");
                        DateTime ReturnDate = DateTime.Parse(Console.ReadLine());


                        command.Parameters.AddWithValue("@bookid", BookID);
                        command.Parameters.AddWithValue("@memberid", MemberId);
                        command.Parameters.AddWithValue("@returnDate" , ReturnDate);

                        using (SqlCommand command1 = new SqlCommand("spCheckReturnDate", connection))
                        {
                            command1.CommandType = CommandType.StoredProcedure;
                            command1.Parameters.AddWithValue("@bookid", BookID);
                            command1.Parameters.AddWithValue("@memberid", MemberId);

                            SqlParameter outputReturnDate = new SqlParameter("@tableReturnDate", SqlDbType.Date);
                            outputReturnDate.Direction = ParameterDirection.Output;
                            command1.Parameters.Add(outputReturnDate);

                             command1.ExecuteNonQuery();
     

                                DateTime tableReturnDate = (DateTime)outputReturnDate.Value;
                            if (ReturnDate <= tableReturnDate)
                            {
                                int row = command.ExecuteNonQuery();
                                if (row > 0)
                                {
                                    Console.WriteLine("Book Returned Successfully");
                                }
                                else
                                {
                                    Console.WriteLine("There is error while returning Book");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Member have to pay penalty");
                            }

                        }
                    }
                }
            }

            catch(Exception ex) 
            {
                Console.WriteLine("Something Went wrong" + ex);
            }
        }


        public void getBorrowedBooks()
        {

            try
            {
                string con = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con))
                {
                    using (SqlCommand command = new SqlCommand("spGetBorrowDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"BorrowID:{reader["Borrowid"]} |BookID:{reader["BookID"]} |MemberID:{reader["Memberid"]} |Borrow Date:{reader["BorrowDate"]} |Return Date:{reader["ReturnDate"]} |Status:{reader["Status"]} |Penalty:{reader["Penalty"]}");
                            }


                        }
                    }
                }
            }
            catch(Exception EX)
            {
                Console.WriteLine("Something Went Wrong" + EX);
            }
        }
    }
}
