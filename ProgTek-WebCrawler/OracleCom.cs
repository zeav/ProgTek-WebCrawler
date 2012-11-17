using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OracleClient;
namespace SQLtest
{
    public partial class SQLtest : Form
    {
        //metode for å koble til serveren
        private string GenerateConnectionString()
        {
            return "Data Source=( DESCRIPTION = ( ADDRESS_LIST = ( ADDRESS = ( PROTOCOL = TCP )( HOST = sitas.hin.no )( PORT = 1521 ) ) )( CONNECT_DATA = ( SERVER = DEDICATED )( SERVICE_NAME = orcl.sitas.hin.no ) ) ); User Id= M42; Password = marvin;";
        }
        public SQLtest()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
{   //Hente ut "NYHETSBYRAA" og viser det i Data Grid View
    using ( OracleConnection connection = new OracleConnection( GenerateConnectionString() ) )
    {
        connection.Open();
        lbState.Text = connection.State.ToString();
 
        OracleCommand oc = connection.CreateCommand();
        oc.CommandText = "SELECT * FROM NYHETSBYRAA";
 
        OracleDataReader reader = oc.ExecuteReader();
 
        bsOracle.DataSource = reader;
        gvOracle.DataSource = bsOracle;
 
        gvOracle.BorderStyle = BorderStyle.Fixed3D;
        gvOracle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
 
    }
}
catch ( Exception ex )
{
//  Feilmelding
    lbState.Text = ex.Message;
}
        }
    }
}
