using System;
using System.Data.SqlClient;

namespace Mission4.DAO
{
    public class DBHelper : IDisposable
    {
        public const string connectionStr = @"Data Source=DESKTOP-RIJACBF\SQLEXPRESS;Initial Catalog=Mission9C;User ID=sa;Password=1234";

        private SqlConnection con;

        public SqlConnection Connection
        {
            get
            {
                if (con == null)
                    con = new SqlConnection(connectionStr);

                return con;
            }
        }

        public void Dispose()
        {
            if (con != null)
            {
                con.Dispose();  //Panggil Close() secara internal
                con = null;
            }
        }
    }
}
