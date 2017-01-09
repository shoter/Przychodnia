using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaData.Repositories
{
    public class RepositoryBase : IRepository
    {
        public PrzychodniaContext Context { get; set; }
        
        public RepositoryBase(PrzychodniaContext context)
        {
            Context = context;
        }

        internal DisposableConnection DisposableConnection
        {
            get
            {
                return Context.DisposableConnection;
            }
        }

        internal static NpgsqlParameter Parameter<T>(string fieldName, T value)
        {
            return Parameter(fieldName, value, getDbType(value));
        }

        internal static NpgsqlParameter Parameter<T>(string fieldName, T value, NpgsqlDbType type)
        {
            var param = new NpgsqlParameter(fieldName, type);

            if (value == null)
            {
                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = value;
            }

            return param;
        }

        

        internal static NpgsqlDbType getDbType<T>(T param)
        {
            Type type = typeof(T);

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = Nullable.GetUnderlyingType(type);
            }

            if (type == typeof(string))
                return NpgsqlDbType.Text;
            if (type == typeof(int))
                return NpgsqlDbType.Integer;
            if (type == typeof(DateTime))
                return NpgsqlDbType.Timestamp;
            if (type == typeof(bool))
                return NpgsqlDbType.Boolean;
            

            throw new NotImplementedException();
        }





    }

    public static class DBExtensions
    {
        public static void Add(this NpgsqlCommand cmd, NpgsqlParameter param)
        {
            cmd.Parameters.Add(param);
        }

        public static void Add<T>(this DbCommand cmd, string fieldName, T value)
        {
            var parameter = RepositoryBase.Parameter(fieldName, value);
            cmd.Parameters.Add(parameter);
        }

        public static void Add<T>(this DbCommand cmd, string fieldName, T value, NpgsqlDbType type)
        {
            var parameter = RepositoryBase.Parameter(fieldName, value, type);
            cmd.Parameters.Add(parameter);
        }

        public static int ToInt(this DbDataReader reader, string what)
        {
            return int.Parse(reader[what].ToString());
        }

        public static int ToInt(this DbDataReader reader, int what)
        {
            return int.Parse(reader[what].ToString());
        }

        public static string ToString(this DbDataReader reader, string what)
        {
            return reader[what].ToString();
        }

        public static string ToString(this DbDataReader reader, int what)
        {
            return reader[what].ToString();
        }

        public static DateTime? ToDate(this DbDataReader reader, string what)
        {
            if (string.IsNullOrWhiteSpace(reader[what].ToString()))
                return null;

            return DateTime.Parse(reader[what].ToString());
        }

        public static bool Exists(this DbDataReader reader, string what)
        {
            return string.IsNullOrWhiteSpace(reader[what].ToString()) == false;
        }

        public static bool toBool(this DbDataReader reader, string what)
        {
            var val = reader.ToString(what).Trim().ToLower();

            return val == "true";
        }

        public static DateTime? ToDate(this DbDataReader reader, int what)
        {
            if (string.IsNullOrWhiteSpace(reader[what].ToString()))
                return null;

            return DateTime.Parse(reader[what].ToString());
        }

        public static DbCommand CreateCommand(this DbConnection connection, string commandText)
        {
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = commandText;
            return cmd;
        }
    }
}
