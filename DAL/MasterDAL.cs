using EmpTask.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


namespace EmpTask.DAL
{
    public class MasterDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["EmployeeMaster"].ToString();

        // Get all Projects

        public List<ProjectMaster> GetAllProjects()
        {
            List<ProjectMaster> projectList = new List<ProjectMaster>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM ProjectMaster";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProject = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProject);
                connection.Close();


                foreach (DataRow item in dtProject.Rows)
                {
                    projectList.Add(new ProjectMaster
                    {
                        RowId = Convert.ToInt32(item["RowId"]),
                        ProjectName = item["ProjectName"].ToString(),

                        Rate = Convert.ToDecimal(item["Rate"])


                    });

                }

            }

            return projectList;
        }


        //Insert Project


        public bool InsertProject(ProjectMaster projectMaster)
        {
            int id = 0;
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {
               
                string query = "INSERT INTO ProjectMaster VALUES (@ProjectName,@Rate) ";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ProjectName", projectMaster.ProjectName);
                sqlCmd.Parameters.AddWithValue("@Rate", projectMaster.Rate);
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

        //Get project by Id

        public List<ProjectMaster> GetAllProjects(int RowId)
        {
            List<ProjectMaster> projectList = new List<ProjectMaster>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM ProjectMaster Where RowID = @RowId";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                sqlDA.SelectCommand.Parameters.AddWithValue("@RowId", RowId);
                DataTable dtProject = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProject);
                connection.Close();


                foreach (DataRow item in dtProject.Rows)
                {
                    projectList.Add(new ProjectMaster
                    {
                        RowId = Convert.ToInt32(item["RowId"]),
                        ProjectName = item["ProjectName"].ToString(),

                        Rate = Convert.ToDecimal(item["Rate"])


                    });

                }

            }

            return projectList;

        }

        //Update Project
        public bool UpdateProject(ProjectMaster projectMaster)
        {
            int i = 0;
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {

                string query = "UPDATE ProjectMaster SET  ProjectName=@ProjectName,Rate=@Rate WHERE RowId = @RowId ";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ProjectName", projectMaster.ProjectName);
                sqlCmd.Parameters.AddWithValue("@Rate", projectMaster.Rate);
                sqlCmd.Parameters.AddWithValue("@RowId", projectMaster.RowId);
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

        //Delete Employee

        public bool DeletePro(int RowId)
        {
            int i = 0;
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {
                string query = "DELETE FROM ProjectMaster WHERE RowId = @RowId";
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