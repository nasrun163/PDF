using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDF
{
    public partial class Form2 : Form
    {
        Employee _employee;
        List<EmployeeDetail> _list;
        //
        public Form2(Employee employee, List<EmployeeDetail> list)
        {
            InitializeComponent();
            _employee = employee;
            _list = list;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
                new Microsoft.Reporting.WinForms.ReportParameter("pEmployeeID", _employee.EmployeeID.ToString()),
                new Microsoft.Reporting.WinForms.ReportParameter("pFirstName", _employee.FirstName.ToString()),
                new Microsoft.Reporting.WinForms.ReportParameter("pLastName", _employee.LastName.ToString()),
                new Microsoft.Reporting.WinForms.ReportParameter("pEmailID", _employee.EmailID.ToString()),
                new Microsoft.Reporting.WinForms.ReportParameter("pCity", _employee.City.ToString()),
                new Microsoft.Reporting.WinForms.ReportParameter("pCountry", _employee.Country.ToString()),
                new Microsoft.Reporting.WinForms.ReportParameter("pStartContract", _employee.StartContract.ToString("dd/MM/yyyy")),
                new Microsoft.Reporting.WinForms.ReportParameter("pEndContract", _employee.EndContract.ToString("dd/MM/yyyy"))
            };
            this.reportViewer.LocalReport.SetParameters(p);
            this.reportViewer.RefreshReport();
        }        
    }
}
