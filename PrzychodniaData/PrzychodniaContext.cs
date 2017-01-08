using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData
{
    public class PrzychodniaContext : DbContext
    {

        public NpgsqlConnection NpgsqlConnection
        {
            get
            {
                if (Database.Connection is NpgsqlConnection)
                    return Database.Connection as NpgsqlConnection;
                return null;
            }
        }

        internal DisposableConnection DisposableConnection
        {
            get
            {
                return new DisposableConnection(Database.Connection);
            }
        }
    

        public PrzychodniaContext() : base()
        {

        }

        

    }

    internal class DisposableConnection : IDisposable
    {
        public DbConnection Connection { get; set; }
        public DisposableConnection(DbConnection connection)
        {
            Connection = connection;
            if(connection.State != System.Data.ConnectionState.Open)
                connection.Open();
        }
        public void Dispose()
        {
            if(Connection.State != System.Data.ConnectionState.Closed)
                Connection.Close();
        }

        public DbCommand CreateCommand(string commandText)
        {
            DbCommand cmd = Connection.CreateCommand();
            cmd.CommandText = commandText;
            return cmd;
        }
    }
}
