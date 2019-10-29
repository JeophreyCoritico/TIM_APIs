using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using ExampleWebAPI.Models;

namespace ExampleWebAPI.Controllers
{
    public class EmployeeController : ApiController
    {


        // GET: api/Employee
        public IEnumerable<string> Get()
        {
            //return new string[] { "value1"};

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            SqlDataReader rdr;
            string query;
            List<string> output = new List<string>();

            try
            {

                conn.Open();

                query = "select * from employee";
                cmd = new SqlCommand(query, conn);

                //read the data for that command
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    output.Add(
                        "{staffid: " + rdr.GetValue(0) +
                        ", givenname: \"" + rdr.GetValue(1) + "\"" +
                        ", surname: \"" + rdr.GetValue(2) + "\"}");
                }

            }
            catch (Exception e)
            {
                output.Clear();
                output.Add(e.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }

            conn.Close();

            return output;
        }

        // GET: api/Employee/5
        public string Get(int id)
        {
            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            SqlDataReader rdr;
            string query;
            string output = "no record found";


            try
            {
                conn.Open();

                query = "select * from employee where staffID = " + id;
                cmd = new SqlCommand(query, conn);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    output = "{staffId: " + rdr.GetValue(0) +
                    ", givenname: \"" + rdr.GetValue(1) + "\"" +
                    ", surname: \"" + rdr.GetValue(2) + "\"}";
                }
            }
            catch (Exception e)
            {
                output = e.Message;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            conn.Close();

            return output;
        }
        // POST: api/Employee
        public string Post(XEmployee employee)
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            string query;
            string output;

            try
            {

                conn.Open();

                query = "insert into employee(staffid, givenname, surname) values ("
                    + employee.StaffId + ", '"
                    + employee.GivenName + "', '"
                    + employee.Surname + "')";


                cmd = new SqlCommand(query, conn);

                //read the data for that command
                output = cmd.ExecuteNonQuery().ToString() + " Rows Inserted";

            }
            catch (Exception e)
            {
                output = e.Message;
            }

            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }

            return output;
        }

        // PUT: api/Employee/5
        public string Put(int id, XEmployee employee)
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            string query;
            string output;

            try
            {

                conn.Open();

                query = "update employee set Givenname = '" +  employee.GivenName + 
                    "', Surname = '" +  employee.Surname + 
                    "' where StaffID = " + id;


                cmd = new SqlCommand(query, conn);

                //read the data for that command
                output = cmd.ExecuteNonQuery().ToString() + " Rows updated";

            }
            catch (Exception e)
            {
                output = e.Message;
            }

            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            conn.Close();

            return output;
        }

        // DELETE: api/Employee/5
        public string Delete(int id)
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            string query;
            string output = "No employee found";

            try
            {

                conn.Open();

                query = "Delete From employee where staffID = " + id;

                cmd = new SqlCommand(query, conn);

                //read the data for that command
                output = cmd.ExecuteNonQuery().ToString() + " Row Deleted";

            }
            catch (Exception e)
            {
                output = e.Message;
            }

            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            conn.Close();

            return output;
        }
    }
}


