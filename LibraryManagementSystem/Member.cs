using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection;
using System.Configuration;
using Microsoft.Win32.SafeHandles;
using System.Text.RegularExpressions;

namespace LibraryManagementSystem
{
    public  class Member
    {
        public int MemberID { get; set; }
        public string Name;

       
        public string Address { get; set; }

         
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime JoinDate { get; set; }

       

        public void addMember()
        {
            Member member = new Member();
            try
            {
                Console.WriteLine("Enter Member Name");
                Name = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(Name))
                {
                    Console.WriteLine("Name cannot be empty.Please enter a valid name");
                    Name = Console.ReadLine();

                }

                Console.WriteLine("Enter Members Resedintial Address");
                Address = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(Address))
                {
                    Console.WriteLine("Address cannot be empty . Please enter valid address");
                    Address = Console.ReadLine();

                }

                Console.WriteLine("Enter Members Email ID");
                //Email = Console.ReadLine() ?? string.Empty;
                Email = Console.ReadLine();
                while (!IsValidEmail(Email))
                {
                    Console.WriteLine("Invalid email format. Please enter a valid email");
                    Email = Console.ReadLine();

                }

                Console.WriteLine("Enter Members Phone Number");
                PhoneNumber = Console.ReadLine();
                while (!IsValidPhone(PhoneNumber))
                {
                    Console.WriteLine("Invalid phone number. Please enter valid number");
                    PhoneNumber = Console.ReadLine();

                }

                JoinDate = DateTime.Now;

                DBSource.addMemberA(member);
            }
            catch(Exception ex) 
            {
                Console.WriteLine("!OOPS Something went wrong" + ex);
            }
            
        }



//Display Member Method------------------------------------------------------------------------------
        public void GetAllMember()
        {
            try
            {
                string con = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();

                    string cmd = "spMemberDetail";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine($"{reader["MemberID"]} | {reader["Name"]} | {reader["Address"]} | {reader["Email"]} | {reader["PhoneNo"]} | {reader["JoinDate"]}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No record found");
                            }
                             
                           

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("!OOPS something went wrong" + ex);
            }
        }









          static bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
            return emailRegex.IsMatch(email);
        }

        static bool IsValidPhone(string phonenumber)
        {
            return phonenumber.Length == 10 && long.TryParse(phonenumber, out _);
        }









    }

}
