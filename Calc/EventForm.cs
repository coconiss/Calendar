using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Calc
{
    public partial class EventForm : Form
    {
        // DB 설정 MySQL 기준
        string server = "localhost";
        int port = 3306;
        string database = "sakila";
        string id = "root";
        string pw = "1234";
        string connectionAddress = "";
        // DB안에 값이 있는지 여부 없으면 false 있으면 true
        Boolean check = false;

        public EventForm()
        {
            InitializeComponent();
            connectionAddress = String.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4}", server, port, database, id, pw);
        }


        private void EventForm_Load(object sender, EventArgs e)
        {
            txt_date.Text = Form1.static_year + "/" + Form1.static_month + "/" + UserControlDays.static_day;
            
            // db 접속하기위한 설정 입력
            MySqlConnection conn = new MySqlConnection(connectionAddress);
            // db 접속
            conn.Open();
            String sql = "SELECT event FROM db_calendar where date = ?";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("date", txt_date.Text);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                check = true;
                txt_event.Text = reader["event"].ToString();
            }
            reader.Dispose();
            cmd.Dispose();
            conn.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (check == true)
            {
                try
                {
                    using (MySqlConnection mysql = new MySqlConnection(connectionAddress))
                    {
                        mysql.Open();
                        String insertQuery = String.Format("UPDATE db_calendar SET event ='{0}' WHERE date = '{1}';", txt_event.Text, txt_date.Text);

                        MySqlCommand command = new MySqlCommand(insertQuery, mysql);

                        if (command.ExecuteNonQuery() != 1)
                            MessageBox.Show("Failed update data");
                        mysql.Close();

                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            else
            {
                try
                {
                    using (MySqlConnection mysql = new MySqlConnection(connectionAddress))
                    {
                        mysql.Open();
                        String insertQuery = String.Format("INSERT INTO db_calendar VALUES ('{0}', '{1}');", txt_date.Text, txt_event.Text);

                        MySqlCommand command = new MySqlCommand(insertQuery, mysql);

                        if (command.ExecuteNonQuery() != 1)
                            MessageBox.Show("Failed insert data");
                        mysql.Close();

                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
    }
}


