using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Configuration;

namespace JqueryDemo
{
    /// <summary>
    /// Summary description for Student
    /// </summary>
    public class Student : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
           string term = context.Request["term"] ?? "";

            string cs = ConfigurationManager.ConnectionStrings["studentcs"].ConnectionString;

            List<string> studentsnames = new List<string>();

            using (SqlConnection con = new SqlConnection(cs))
            {

                con.Open();

                SqlCommand cmd = new SqlCommand("spGetStudentNames", con);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("term", term.ToString());
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    studentsnames.Add(rdr["Name"].ToString());

                }

            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(studentsnames));

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}