using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calc
{
    public partial class UserControlDays : UserControl
    {
        public static String static_day;
        // DB 설정 MySQL 기준
        string server = "localhost";
        int port = 3306;
        string database = "sakila";
        string id = "root";
        string pw = "1234";
        string connectionAddress = "";

        public UserControlDays()
        {
            InitializeComponent();
            connectionAddress = String.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4}", server, port, database, id, pw);
        }

        public void UserControlDays_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connectionAddress);
            conn.Open();
            String sql = "SELECT * FROM db_calendar where date = ?";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("date", Form1.static_year + "/" + Form1.static_month + "/" + lbl_days.Text);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lbl_event.Text = reader["event"].ToString();
            }
            reader.Dispose();
            cmd.Dispose();
            conn.Close();
        }
        public void days(int numday)
        {
            lbl_days.Text = numday + "";
        }

        private void UserControlDays_Click(object sender, EventArgs e)
        {
            static_day = lbl_days.Text;
            timer1.Start();
            EventForm eventform = new EventForm();
            eventform.Show();
        }


        //event label
        public void displayEvent()
        {
            MySqlConnection conn = new MySqlConnection(connectionAddress);
            conn.Open();
            String sql = "SELECT * FROM db_calendar where date = ?";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("date", Form1.static_year + "/" + Form1.static_month + "/" + lbl_days.Text);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lbl_event.Text = reader["event"].ToString();
            }
            reader.Dispose();
            cmd.Dispose();
            conn.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            displayEvent();
        }
    }
}
