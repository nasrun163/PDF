using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDF
{
    public partial class Form1 : Form
    {
        private List<EmployeeDetail> list;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnfilter_Click(object sender, EventArgs e)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BankEntiti"].ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                string a = dateTimePicker1.Value.ToString("yyyymmdd");
                string query = $"select EmployeeID, FirstName, LastName, EmailID, City, Country, StartContract, EndContract from Employee where StartContract between '{dateTimePicker1.Value}' and '{dateTimePicker2.Value}'";
                employeeBindingSource.DataSource = db.Query<Employee>(query, commandType: CommandType.Text);
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            Employee obj = employeeBindingSource.Current as Employee;
            if (obj != null)
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BankEntiti"].ConnectionString))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    string query = $"select EmployeeID, FirstName, LastName, EmailID, City, Country, StartContract, EndContract from Employee where EmployeeID = '{obj.EmployeeID}'";
                    List<EmployeeDetail> list = db.Query<EmployeeDetail>(query, commandType: CommandType.Text).ToList();
                    using (Form2 frm = new Form2(obj, list))
                    {
                        frm.ShowDialog();
                     }
                }
            }
        }
    }
}
