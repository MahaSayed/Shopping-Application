using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;

namespace Online_Shopping_Store
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        
        //static public int select_ID()
        //{
        //    string constr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id=hr;Password=hr;";
        //    OracleConnection conn = new OracleConnection(constr);
        //    int ID = 0;
        //    OracleCommand select = new OracleCommand();
        //    select.Connection = conn;
        //    select.CommandText = "select product_seq.nextval from products";
        //    select.CommandType = CommandType.Text;
        //    OracleDataReader dr = select.ExecuteReader();
        //    if (dr.Read())
        //    {
        //        ID = Convert.ToInt32(dr[0].ToString());
        //    }

        //    return ID;
        //}

        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Admin_Settings("ymaha46@yahoo.com"));

            Application.Run(new SignIn());
        }
    }
}
