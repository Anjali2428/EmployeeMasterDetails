using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using EmpTask.Models;

namespace EmpTask.DAL
{
    public class EmployeeDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["EmployeeMaster"].ToString();
        public List<EmpMaster> GetAllEmployees()
        {
            List<EmpMaster> employeeList = new List<EmpMaster>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM EmployeeMaster";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtEmployees = new DataTable();

                connection.Open();
                sqlDA.Fill(dtEmployees);
                connection.Close();


                foreach (DataRow item in dtEmployees.Rows)
                {
                    employeeList.Add(new EmpMaster
                    {
                        EmpId = Convert.ToInt32(item["EmpId"]),
                        LastName = item["LastName"].ToString(),
                        FirstName = item["FirstName"].ToString(),
                        DOB = Convert.ToDateTime(item["DOB"]).Date,
                        Email = item["Email"].ToString(),
                        Mobile = item["Mobile"].ToString(),
                        Department = item["Department"].ToString(),
                        Designation = item["Designation"].ToString(),
                        Salary = Convert.ToDecimal(item["Salary"])


                    });

                }

            }

            return employeeList;
        }


        public bool IsEmailNotUnique(string email)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                string query = "SELECT COUNT(*) FROM EmployeeMaster WHERE Email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                connection.Close();

                return count == 0;
            }
        }




        //Insert Employee

        public bool InsertEmp(EmpMaster empMaster)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                string query = "INSERT INTO EmployeeMaster VALUES (@LastName,@FirstName,@DOB,@Email,@Mobile,@Department,@Designation,@Salary)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LastName", empMaster.LastName);
                command.Parameters.AddWithValue("@FirstName", empMaster.FirstName);
                command.Parameters.AddWithValue("@DOB", empMaster.DOB);
                command.Parameters.AddWithValue("@Email", empMaster.Email);
                command.Parameters.AddWithValue("@Mobile", empMaster.Mobile);
                command.Parameters.AddWithValue("@Department", empMaster.Department);
                command.Parameters.AddWithValue("@Designation", empMaster.Designation);
                command.Parameters.AddWithValue("@Salary", empMaster.Salary);


                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
        
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        //GetAllEmployee


        public List<EmpMaster> GetEmpById(int EmpId)
        {
            List<EmpMaster> employeeList = new List<EmpMaster>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM EmployeeMaster Where EmpId = @EmpId";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command); 
                command.Parameters.AddWithValue("@EmpId", EmpId);
                DataTable dtEmployees = new DataTable();

                connection.Open();
                sqlDA.Fill(dtEmployees);
                connection.Close();


                foreach (DataRow item in dtEmployees.Rows)
                {
                    employeeList.Add(new EmpMaster
                    {
                        EmpId = Convert.ToInt32(item["EmpId"]),
                        LastName = item["LastName"].ToString(),
                        FirstName = item["FirstName"].ToString(),
                        DOB = Convert.ToDateTime(item["DOB"]).Date,
                        Email = item["Email"].ToString(),
                        Mobile = item["Mobile"].ToString(),
                        Department = item["Department"].ToString(),
                        Designation = item["Designation"].ToString(),
                        Salary = Convert.ToDecimal(item["Salary"])




                    });

                }

            }

            return employeeList;
        }

        //update


        //update employee
        public bool UpdateEmp(EmpMaster empMaster)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                string query = "UPDATE EmployeeMaster SET  LastName= @LastName, FirstName= @FirstName,DOB = @DOB, Email=@Email,Mobile=@Mobile,Department=@Department,Designation=@Designation,Salary=@Salary Where EmpId =@EmpId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmpId", empMaster.EmpId);
                command.Parameters.AddWithValue("@LastName", empMaster.LastName);
                command.Parameters.AddWithValue("@FirstName", empMaster.FirstName);
                command.Parameters.AddWithValue("@DOB", empMaster.DOB);
                command.Parameters.AddWithValue("@Email", empMaster.Email);
                command.Parameters.AddWithValue("@Mobile", empMaster.Mobile);
                command.Parameters.AddWithValue("@Department", empMaster.Department);
                command.Parameters.AddWithValue("@Designation", empMaster.Designation);
                command.Parameters.AddWithValue("@Salary", empMaster.Salary);


                connection.Open();
                i = command.ExecuteNonQuery();
                connection.Close();
            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }





        //Delete Employee

        public bool DeleteEmp(int empId)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                string query = "DELETE FROM EmployeeMaster WHERE EmpId = @EmpId";
                SqlCommand command = new SqlCommand(query, connection);
               
                command.Parameters.AddWithValue("@EmpId", empId);


                connection.Open();
                i = command.ExecuteNonQuery();
                connection.Close();
            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }




    }
}