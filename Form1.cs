using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDatabase
{
    public partial class Form1 : Form
    {
      
        static string strConn = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
        SqlConnection sqlConn = new SqlConnection(strConn);
        public Form1()
        {
            InitializeComponent();

            
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * From TB_Users;";
            cmd.Connection = sqlConn;

            try
            {
                sqlConn.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MessageBox.Show($"{dr[0].ToString()} {dr[1].ToString()} {dr[2].ToString()}");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error!! {ex}");
            }
            finally { sqlConn.Close(); }




        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string sql = "Insert Into TB_Users Values('" + txtName.Text + "','" + txtEmail.Text + "','" + txtPassword.Text + "');";
            SqlCommand sqlComm = new SqlCommand();
            sqlComm.CommandType = CommandType.Text;
            sqlComm.CommandText = sql;
            sqlComm.Connection = sqlConn;

            try
            {
                sqlConn.Open();
                sqlComm.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { sqlConn.Close(); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sql = $"DELETE FROM TB_Users WHERE name = '{txtName.Text}';  ";
            SqlCommand sqlComm = new SqlCommand(sql,sqlConn);
            //sqlComm.CommandType = CommandType.Text;
            //sqlComm.CommandText = sql;
            //sqlComm.Connection = sqlConn;

            try
            {
                sqlConn.Open();
                sqlComm.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { sqlConn.Close(); }
        }

        private void btnCount_Click(object sender, EventArgs e)
        {
            string sql = "SELECT COUNT(*) FROM TB_Users;";
            SqlCommand sqlComm = new SqlCommand(sql,sqlConn);

            sqlConn.Open();
            var strCount = sqlComm.ExecuteScalar();
            MessageBox.Show(strCount.ToString());
            sqlConn.Close() ;
        }
    }
}
