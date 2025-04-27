using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace LibraryManagementSystem
{
    public  class PracticeAdapter
    {
        public static void practiceA()
        {
            string con = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(con))
                {


                    string cmd = "Select * from Book";

                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd, connection))
                    {
                        DataTable dt = new DataTable();
                        ad.Fill(dt);

                        foreach (DataRow dr in dt.Rows)
                        {
                            Console.WriteLine(dr["Title"] + " " + dr["Author"] + " " + dr["ISBN"]);
                        }


                        using (DataSet ds = new DataSet())
                        {
                            ad.Fill(ds, "Book");
                            Console.WriteLine("Using Dataset");

                            foreach (DataRow dr in ds.Tables["Book"].Rows)
                            {
                                Console.WriteLine(dr["Title"] + " " + dr["Author"] + " " + dr["ISBN"]);
                            }
                        }
                    }

                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Oops Something Went Wrong" + ex);
            }

        }
    }
}
