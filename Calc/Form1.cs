using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using MySql.Data.MySqlClient;

namespace Calc
{
    public partial class Form1 : Form
    {
        int month, year;
        public static int static_month, static_year;

        string server = "localhost";
        int port = 3306;
        string database = "sakila";
        string id = "root";
        string pw = "1234";
        string connectionAddress = "";

        public Form1()
        {
            InitializeComponent();
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            displaDays();
            comboAdd();

        }
        private void comboAdd()
        {
            // 콤보박스 값 입력
            for (int i = 2000; i<=2030; i++) {
                cbYear.Items.Add(i);
            }
            for (int i = 1; i <= 12; i++)
            {
                cbMonth.Items.Add(i);                
            }
        }

        private void displaDays()
        {
            //현재 기준 날짜를 now 에 저장
            DateTime now = DateTime.Now;
            //now Date에서 현재 년도, 월 을 int값으로 저장
            month = now.Month;
            year = now.Year;
            String monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbl_Date.Text = year + "년 " + monthname;
           

            //다른 폼과 데이터를 공유하기 위한 값 입력
            static_month = month;
            static_year = year;

            //콤보박스에 변경된 값 입력
            cbYear.Text = year.ToString();
            cbMonth.Text = month.ToString();

            // 저장된 년도 월 1일 로 Time값 저장
            DateTime startofthemonth = new DateTime(year, month,1);
            
            // 년도 월에 해당되는 일수 int값으로 저장
            int days = DateTime.DaysInMonth(year, month);
            
            // 첫번째주 1일 시작전 일요일 기준으로 비어있는 갯수 저장
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d"))+1;

            // 패널에 해당 컨트룰 하나씩 배치
            for (int i = 1; i<dayoftheweek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                daycontainer.Controls.Add(ucblank);
            }

            for(int i=1; i<=days; i++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(i);
                daycontainer.Controls.Add(ucdays);
            }

            

        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            year = Convert.ToInt32(cbYear.Text);

            daycontainer.Controls.Clear();
            static_month = month;
            static_year = year;
            String monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbl_Date.Text = year + "년 " + monthname;

            DateTime startofthemonth = new DateTime(year, month, 1);

            int days = DateTime.DaysInMonth(year, month);
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i < dayoftheweek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                daycontainer.Controls.Add(ucblank);
            }

            for (int i = 1; i <= days; i++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(i);
                daycontainer.Controls.Add(ucdays);
            }
        }

        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            month = Convert.ToInt32(cbMonth.Text);

            daycontainer.Controls.Clear();
            static_month = month;
            static_year = year;
            String monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbl_Date.Text = year + "년 " + monthname;

            DateTime startofthemonth = new DateTime(year, month, 1);

            int days = DateTime.DaysInMonth(year, month);
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i < dayoftheweek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                daycontainer.Controls.Add(ucblank);
            }

            for (int i = 1; i <= days; i++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(i);
                daycontainer.Controls.Add(ucdays);
            }
        }

        private void btn_previous_Click(object sender, EventArgs e)
        {
            // 년도 바꾸기 위한 if
            if (month == 1)
            {
                month = 12;
                year -= 1;
            }
            else
            {
                month--;
            }

            cbYear.Text = year.ToString();
            cbMonth.Text = month.ToString();

            daycontainer.Controls.Clear();
            static_month = month;
            static_year = year;
            String monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbl_Date.Text = year + "년 " + monthname;

            DateTime startofthemonth = new DateTime(year, month, 1);

            int days = DateTime.DaysInMonth(year, month);
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i < dayoftheweek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                daycontainer.Controls.Add(ucblank);
            }

            for (int i = 1; i <= days; i++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(i);
                daycontainer.Controls.Add(ucdays);
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (month == 12)
            {
                month = 1;
                year += 1;
            }
            else
            {
                month++;
            }

            cbYear.Text = year.ToString();
            cbMonth.Text = month.ToString();

            daycontainer.Controls.Clear();
            static_month = month;
            static_year = year;
            String monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbl_Date.Text = year + "년 " + monthname;

            DateTime startofthemonth = new DateTime(year, month, 1);

            int days = DateTime.DaysInMonth(year, month);
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i < dayoftheweek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                daycontainer.Controls.Add(ucblank);
            }

            for (int i = 1; i <= days; i++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(i);
                daycontainer.Controls.Add(ucdays);
            }
        }
    }
}
