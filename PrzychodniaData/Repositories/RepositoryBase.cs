using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
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

        protected NpgsqlParameter Parameter<T>(string fieldName, T value)
        {
            return Parameter(fieldName, value, getDbType(value));
        }

        protected NpgsqlParameter Parameter<T>(string fieldName, T value, NpgsqlDbType type)
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

        

        protected NpgsqlDbType getDbType<T>(T param)
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
    }
}
