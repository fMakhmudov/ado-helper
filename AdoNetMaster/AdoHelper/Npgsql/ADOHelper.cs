using AdoNetMaster.Core.Interfaces;
using AdoNetMaster.Infrastructure.AdoHelper.Core;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;

namespace AdoNetMaster.Infrastructure.AdoHelper.Npgsql
{
    public static class ADOHelper
    {
        public static IADOHelper Instance(IConfiguration config, IAppLogger logger, DbProvider DbProvider = DbProvider.NpgSql)
        {
            if (DbProvider == DbProvider.NpgSql)
                return new NpgsqlHelper(config, logger);
            else
                throw new NotImplementedException();
        }

        /* public static NpgsqlParameter CreateParameter(string paramName, object paramValue, ParameterDirection direction = ParameterDirection.Input)
         {
             if (string.IsNullOrEmpty(paramName))
             {
                 throw new ArgumentNullException("paramName", "Parameter Name is null or empty.");
             }

             NpgsqlParameter npgsqlParameter = new NpgsqlParameter
             {
                 ParameterName = paramName,
                 Value = paramValue,
                 Direction = direction
             };
             if (npgsqlParameter.Value == null && (npgsqlParameter.Direction == ParameterDirection.Input || npgsqlParameter.Direction == ParameterDirection.InputOutput))
             {
                 npgsqlParameter.Value = DBNull.Value;
             }

             return npgsqlParameter;
         }*/

        public static NpgsqlParameter CreateParameter(string paramName, NpgsqlDbType dbType, object paramValue, int? size = null, ParameterDirection direction = ParameterDirection.Input)
        {
            if (string.IsNullOrEmpty(paramName))
            {
                throw new ArgumentNullException("paramName", "Parameter Name is null or empty.");
            }

            NpgsqlParameter npgsqlParameter = new NpgsqlParameter
            {
                ParameterName = paramName,
                Value = paramValue,
                NpgsqlDbType = dbType,
                Direction = direction
            };
            if (size.HasValue)
            {
                npgsqlParameter.Size = size.Value;
            }

            if (npgsqlParameter.Value == null && (npgsqlParameter.Direction == ParameterDirection.Input || npgsqlParameter.Direction == ParameterDirection.InputOutput))
            {
                npgsqlParameter.Value = DBNull.Value;
            }

            return npgsqlParameter;
        }

        public static NpgsqlParameter CreateParameter(string paramName, NpgsqlDbType dbType, int size, object paramValue, ParameterDirection direction = ParameterDirection.Input)
        {
            if (string.IsNullOrEmpty(paramName))
            {
                throw new ArgumentNullException("paramName", "Parameter Name is null or empty.");
            }

            NpgsqlParameter npgsqlParameter = new NpgsqlParameter
            {
                ParameterName = paramName,
                Value = paramValue,
                NpgsqlDbType = dbType,
                Direction = direction,
                Size = size
            };

            if (npgsqlParameter.Value == null && (npgsqlParameter.Direction == ParameterDirection.Input || npgsqlParameter.Direction == ParameterDirection.InputOutput))
            {
                npgsqlParameter.Value = DBNull.Value;
            }

            return npgsqlParameter;
        }

        public static NpgsqlParameter CreateOutParameter(string paramName, NpgsqlDbType dbType, int? size = null)
        {
            if (string.IsNullOrEmpty(paramName))
            {
                throw new ArgumentNullException("paramName", "Parameter Name is null or empty.");
            }

            NpgsqlParameter npgsqlParameter = new NpgsqlParameter
            {
                ParameterName = paramName,
                NpgsqlDbType = dbType,
                Direction = ParameterDirection.Output
            };
            if (size.HasValue)
            {
                npgsqlParameter.Size = size.Value;
            }

            return npgsqlParameter;
        }
    }
}
