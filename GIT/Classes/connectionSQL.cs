using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace GIT
{
    public static class connectionSQL
    {
        public static string ConnectionString = "Data Source=localhost;Initial Catalog=QL_SinhVien;Persist Security Info=True;User ID=sa;Password=150280";

        private static void InitSqlCommand(SqlCommand command, string procedureName, SqlConnection connection)
        {
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;
        }
        private static DataTable GetParameter(string procedureName)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlDataAdapter dataAdapter = null;
            DataTable dt = new DataTable();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = " SELECT P.ORDINAL_POSITION, P.PARAMETER_NAME, P.DATA_TYPE "
                                 + " FROM INFORMATION_SCHEMA.PARAMETERS P "
                                 + " WHERE P.SPECIFIC_NAME = '" + procedureName + "'"
                                 + "  ORDER BY P.ORDINAL_POSITION ";


            //  InitSqlCommand(command, "LayDanhSachThamSo", conn);

            //SqlParameter para = new SqlParameter();
            //para.ParameterName = "@V_PROCNAME";
            //para.Value = procedureName;
            //command.Parameters.Add(para);

            dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(dt);

            return dt;
        }

        private static void AddParameters(string procudureName, SqlCommand command, object[] paraValue)
        {
            DataTable dt_para = new DataTable();
            dt_para = GetParameter(procudureName);

            for (int index = 0; index <= dt_para.Rows.Count - 1; index += 1)
            {
                SqlParameter para = new SqlParameter();
                para.ParameterName = dt_para.Rows[index]["PARAMETER_NAME"].ToString();
                para.Value = (paraValue[index] == null ? DBNull.Value : paraValue[index]);
                command.Parameters.Add(para);
            }
        }
        public static DataTable GetDataTable(string procudureName, object[] paraValue)
        {
            SqlConnection conn = new SqlConnection(ConnectionString); ;
            SqlDataAdapter dataAdapter = null;
            DataTable dt = new DataTable();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 100;
            InitSqlCommand(command, procudureName, conn);

            AddParameters(procudureName, command, paraValue);

            dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(dt);

            return dt;
        }
        public static DataSet GetDataSet(string procudureName, object[] paraValue)
        {
            SqlConnection conn = new SqlConnection(ConnectionString); ;
            SqlDataAdapter dataAdapter = null;
            DataSet ds = new DataSet();

            SqlCommand command = new SqlCommand();
            InitSqlCommand(command, procudureName, conn);

            AddParameters(procudureName, command, paraValue);

            dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(ds);

            return ds;
        }
        public static void ExecuteQuery(string procudureName, object[] paraValue)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand command = new SqlCommand();
            InitSqlCommand(command, procudureName, conn);
            AddParameters(procudureName, command, paraValue);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static void ExecuteUpdate(string table, string field, object[] paraValue)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "UPDATE " + table + " SET " + field + " = @Value WHERE ID = @Id";
            SqlParameter para = new SqlParameter("@Id", (paraValue[0] == null ? DBNull.Value : paraValue[0]));
            command.Parameters.Add(para);
            SqlParameter para1 = new SqlParameter("@Value", (paraValue[1] == null ? DBNull.Value : paraValue[1]));
            command.Parameters.Add(para1);

            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void ExecuteUpdate(string table, string Ma, string field, object[] paraValue)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "UPDATE " + table + " SET " + field + " = @Value WHERE " + Ma + "  = @" + Ma;
            SqlParameter para = new SqlParameter(Ma, (paraValue[0] == null ? DBNull.Value : paraValue[0]));
            command.Parameters.Add(para);
            SqlParameter para1 = new SqlParameter("@Value", (paraValue[1] == null ? DBNull.Value : paraValue[1]));
            command.Parameters.Add(para1);

            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void ExecuteDelete(string table, decimal Id)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "DELETE " + table + " WHERE ID = @Id";
            SqlParameter para = new SqlParameter("@Id", Id);
            command.Parameters.Add(para);

            command.ExecuteNonQuery();
            conn.Close();
        }


        public static DataTable ExecuteSelect(string table, string field, object[] paraValue)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            if (field != "")
            {
                command.CommandText = "SELECT * FROM " + table + " WHERE " + field + " = @Value ";
                SqlParameter para = new SqlParameter("@Value", (paraValue[0] == null ? DBNull.Value : paraValue[0]));
                command.Parameters.Add(para);
            }
            else
            {
                command.CommandText = "SELECT * FROM " + table;

            }
            SqlDataAdapter dataAdapter = null;
            DataTable dt = new DataTable();

            dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(dt);

            return dt;

        }

    }
}
