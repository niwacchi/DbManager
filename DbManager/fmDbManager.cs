using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbManager.Common;

namespace DbManager
{
    public partial class frmDbManager : Form
    {
        public frmDbManager()
        {
            InitializeComponent();

            SetTableList(Utility.GetTableList());
        }

        private void SetTableList(List<string> tableList)
        {
            foreach(string table in tableList)
            {
                this.cmbTableList.Items.Add(table);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void cmbTableList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string table = this.cmbTableList.SelectedItem.ToString();

            DbAccess dbAccess = new DbAccess();
            dbAccess.Connect();
            DataTable dt = dbAccess.ExecuteSql("SELECT * FROM " + table, 300);
            dbAccess.Close();

            dgvTable.DataSource = dt;

        }
    }
}
