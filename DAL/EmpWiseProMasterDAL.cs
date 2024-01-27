using EmpTask.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Security.Cryptography.X509Certificates;

namespace EmpTask.DAL
{
    public class EmpWiseProMasterDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["EmployeeMaster"].ToString();

        // Get all Projects

        public List<employeeWiseProMaster> GetEmpProjects()
        {
            List<employeeWiseProMaster> empWisetList = new List<employeeWiseProMaster>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM EmpWiseProjectMaster";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProject = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProject);
                connection.Close();


                foreach (DataRow item in dtProject.Rows)
                {
                    empWisetList.Add(new employeeWiseProMaster
                    {
                        RowId = Convert.ToInt32(item["RowId"]),
                        ProjectNo = Convert.ToInt32(item["ProjectNo"]),
                        AssignDate = Convert.ToDateTime(item["AssignDate"]).Date,
                        EmployeeId = Convert.ToInt32(item["EmployeeId"]),
                        TotalAmount = Convert.ToDecimal(item["TotalAmount"]),
                        DiscPerc = Convert.ToDecimal(item["DiscPerc"]),
                        DiscAmount = Convert.ToDecimal(item["DiscPerc"]),
                        NetAmount = Convert.ToDecimal(item["NetAmount"])

                    });


                }

            }
            return empWisetList;
        }


        //Insert Project


        public bool InsertPro(employeeWiseProMaster empWiseMaster)
        {
            int id = 0;
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {

                string query = "INSERT INTO EmpWiseProjectMaster VALUES (@ProjectNo,@AssignDate,@EmployeeId,@TotalAmount,@DiscPerc,@DiscAmount,@NetAmount)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ProjectNo", empWiseMaster.ProjectNo);
                sqlCmd.Parameters.AddWithValue("@AssignDate", empWiseMaster.AssignDate);
                sqlCmd.Parameters.AddWithValue("@EmployeeId", empWiseMaster.EmployeeId);
                sqlCmd.Parameters.AddWithValue("@TotalAmount", empWiseMaster.TotalAmount);
                sqlCmd.Parameters.AddWithValue("@DiscPerc", empWiseMaster.DiscPerc);
                sqlCmd.Parameters.AddWithValue("@DiscAmount", empWiseMaster.DiscAmount);
                sqlCmd.Parameters.AddWithValue("@NetAmount", empWiseMaster.NetAmount);



                sqlCon.Open();
                id = sqlCmd.ExecuteNonQuery();

                sqlCon.Close();

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








        public List<employeeWiseProMaster> GetAllEmpProjects(int RowId)
            {
                List<employeeWiseProMaster> empWisetList = new List<employeeWiseProMaster>();
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT * FROM EmpWiseProjectMaster Where RowID = @RowId";
                    SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                    sqlDA.SelectCommand.Parameters.AddWithValue("@RowId", RowId);
                    DataTable dtProject = new DataTable();

                    connection.Open();
                    sqlDA.Fill(dtProject);
                    connection.Close();


                    foreach (DataRow item in dtProject.Rows)
                    {
                        empWisetList.Add(new employeeWiseProMaster
                        {
                            RowId = Convert.ToInt32(item["RowId"]),
                            ProjectNo = Convert.ToInt32(item["ProjectNo"]),
                            AssignDate = Convert.ToDateTime(item["AssignDate"]).Date,
                            EmployeeId = Convert.ToInt32(item["EmployeeId"]),
                            TotalAmount = Convert.ToDecimal(item["TotalAmount"]),
                            DiscPerc = Convert.ToDecimal(item["DiscPerc"]),
                            DiscAmount = Convert.ToDecimal(item["DiscPerc"]),
                            NetAmount = Convert.ToDecimal(item["NetAmount"])

                        });


                    }

                }
                return empWisetList;
            }


        //Update Data

        public bool UpdateAllProject(employeeWiseProMaster empWiseMaster)
        {
            int i = 0;
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {

                string query = "UPDATE EmpWiseProjectMaster SET ProjectNo=@ProjectNo,AssignDate=@AssignDate,EmployeeId=@EmployeeId,TotalAmount=@TotalAmount,DiscPerc=@DiscPerc,DiscAmount=@DiscAmount,NetAmount=@NetAmount WHERE RowId = @RowId";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ProjectNo", empWiseMaster.ProjectNo);
                sqlCmd.Parameters.AddWithValue("@AssignDate", empWiseMaster.AssignDate);
                sqlCmd.Parameters.AddWithValue("@EmployeeId", empWiseMaster.EmployeeId);
                sqlCmd.Parameters.AddWithValue("@TotalAmount", empWiseMaster.TotalAmount);
                sqlCmd.Parameters.AddWithValue("@DiscPerc", empWiseMaster.DiscPerc);
                sqlCmd.Parameters.AddWithValue("@DiscAmount", empWiseMaster.DiscAmount);
                sqlCmd.Parameters.AddWithValue("@NetAmount", empWiseMaster.NetAmount);
                sqlCmd.Parameters.AddWithValue("@RowId", empWiseMaster.RowId);

                sqlCon.Open();
                i = sqlCmd.ExecuteNonQuery();

                sqlCon.Close();

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


        public bool DeleteEMPPro(int RowId)
        {
            int i = 0;
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {
                string query = "DELETE FROM EmpWiseProjectMaster WHERE RowId = @RowId";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@RowId", RowId);

                sqlCon.Open();
                i = sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
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