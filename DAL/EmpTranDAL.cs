using EmpTask.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace EmpTask.DAL
{
    public class EmpTranDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["EmployeeMaster"].ToString();
       

        // Get all Projects

        public List<EMPTran> GetAlEmpTran()
        {
            List<EMPTran> tranList = new List<EMPTran>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM EmpWiseProjectTrans";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProject = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProject);
                connection.Close();


                foreach (DataRow item in dtProject.Rows)
                {
                    tranList.Add(new EMPTran
                    {
                             RowId = Convert.ToInt32(item["RowId"]),
                             MasterId = Convert.ToInt32(item["MasterId"]),
                             ProjectId = Convert.ToInt32(item["ProjectId"]),
                             Qty = Convert.ToDecimal(item["Qty"]),
                             Rate = Convert.ToDecimal(item["Rate"]),
                             Amount = Convert.ToDecimal(item["Amount"])


                    });

                }

            }

            return tranList;
        }


        //Insert Project


        public bool InsertTran(EMPTran eMPTran)
        {
            int id = 0;
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {

                string query = "INSERT INTO EmpWiseProjectTrans VALUES (@MasterId,@ProjectId,@Qty,@Rate,@Amount) ";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@MasterId", eMPTran.MasterId);
                sqlCmd.Parameters.AddWithValue("@ProjectId", eMPTran.ProjectId);
                sqlCmd.Parameters.AddWithValue("@Qty", eMPTran.Qty);
                sqlCmd.Parameters.AddWithValue("@Rate", eMPTran.Rate);
                sqlCmd.Parameters.AddWithValue("@Amount", eMPTran.Amount);
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

        public List<EMPTran> GetAllTran(int RowId)
        {
            List<EMPTran> tranList = new List<EMPTran>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM EmpWiseProjectTrans Where RowID = @RowId";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                sqlDA.SelectCommand.Parameters.AddWithValue("@RowId", RowId);
                DataTable dtProject = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProject);
                connection.Close();


                foreach (DataRow item in dtProject.Rows)
                {
                    tranList.Add(new EMPTran
                    {
                         RowId = Convert.ToInt32(item["RowId"]),
                         MasterId = Convert.ToInt32(item["MasterId"]),
                         ProjectId = Convert.ToInt32(item["ProjectId"]),
                         Qty = Convert.ToDecimal(item["Qty"]),
                         Rate = Convert.ToDecimal(item["Rate"]),
                        Amount = Convert.ToDecimal(item["Amount"])





                    });

                }

            }

            return tranList;

        }

        //Update Project
        public bool UpdateTran(EMPTran eMPTran)
        {
            int i = 0;
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {

                string query = "UPDATE EmpWiseProjectTrans SET  MasterId=@MasterId,ProjectId=@ProjectId,Qty=@Qty, Rate=@Rate, Amount=@Amount WHERE RowId = @RowId ";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@MasterId", eMPTran.MasterId);
                sqlCmd.Parameters.AddWithValue("@ProjectId", eMPTran.ProjectId);
                sqlCmd.Parameters.AddWithValue("@Qty", eMPTran.Qty);
                sqlCmd.Parameters.AddWithValue("@Rate", eMPTran.Rate);
                sqlCmd.Parameters.AddWithValue("@Amount", eMPTran.Amount);
                sqlCmd.Parameters.AddWithValue("@RowId", eMPTran.RowId);


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

        public bool DeleteTran(int RowId)
        {
            int i = 0;
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {
                string query = "DELETE FROM EmpWiseProjectTrans  WHERE RowId = @RowId";
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
