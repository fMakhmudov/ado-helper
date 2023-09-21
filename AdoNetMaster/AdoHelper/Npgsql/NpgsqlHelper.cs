using AdoNetMaster.Core.Interfaces;
using AdoNetMaster.Infrastructure.AdoHelper.Core;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace AdoNetMaster.Infrastructure.AdoHelper.Npgsql
{
    public class NpgsqlHelper : IADOHelper, IDisposable
    {
        private bool _disposed = false;
        protected IDbConnection _conn = null;
        protected IDbTransaction _trans = null;
        private readonly IAppLogger _logger;
        public DbProvider DbProvider => DbProvider.NpgSql;
        public string ConnectionString { get; private set; }

        private readonly string outError = "\"Расмийлаштиришда ҳатолик юз берди\" илтимос сайт яратувчиларга мурожат қилинг.";
        private readonly string connError = "Маълумотлар базаси билан алоқа мавжуд эмас!";

        public NpgsqlHelper(IConfiguration config, IAppLogger logger)
        {
            ConnectionString = config.GetConnectionString("NpgsConnection");
            _logger = logger;
        }

        protected void Connect()
        {
            if (_conn is null)
            {
                try
                {
                    _conn = new NpgsqlConnection(ConnectionString);
                    _conn.Open();
                }
                catch (Exception ex)
                {
                    _logger.Critical(connError + "\nex:" + ex);
                    throw new Exception(connError);
                }
            }
        }

        public void BeginTransaction()
        {
            Rollback();
            Connect();
            _trans = _conn.BeginTransaction();
            //return Transaction;
        }
        public void Commit()
        {
            if (_trans != null)
            {
                _trans.Commit();
                _trans = null;
            }
        }
        public void Rollback()
        {
            if (_trans != null)
            {
                _trans.Rollback();
                _trans = null;
            }
        }

        ~NpgsqlHelper()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_conn != null)
                    {
                        Rollback();
                        _conn.Dispose();
                        _conn = null;
                    }
                }
                _disposed = true;
            }
        }

        public int ExecuteNonQuery(string spName, params DbParameter[] dbParameters)
        {
            return ExecuteNonQuery(spName, CommandType.StoredProcedure, dbParameters);
        }

        public int ExecuteNonQuery(string cmdText, CommandType cmdType, params DbParameter[] dbParameters)
        {
            try
            {
                using NpgsqlCommand npgsqlCommand = CreateCommand(cmdText, cmdType, true, dbParameters);
                int result = 0;

                if (cmdText.Trim().ToUpper().IndexOf(") RETURNING ") > 0)
                {
                    object obj = npgsqlCommand.ExecuteScalar();
                    if (obj != null && int.TryParse("" + obj, out result)) { }
                }
                else
                    result = npgsqlCommand.ExecuteNonQuery();

                return result;
            }
            catch (NpgsqlException ex)
            {
                _logger.DbCritical(ex);
                if (ex.Message.StartsWith("P0001"))
                    throw;
                else
                    throw new Exception(outError);
            }
            catch (Exception ex)
            {
                _logger.DbCritical(ex);
                throw;
            }
        }

        public object ExecuteScalar(string spName, params DbParameter[] dbParameters)
        {
            return ExecuteScalar(spName, CommandType.StoredProcedure, dbParameters);
        }

        public object ExecuteScalar(string cmdText, CommandType cmdType, params DbParameter[] dbParameters)
        {
            try
            {
                using NpgsqlCommand npgsqlCommand = CreateCommand(cmdText, cmdType, true, dbParameters);
                object result = npgsqlCommand.ExecuteScalar();

                return result;
            }
            catch (NpgsqlException ex)
            {
                _logger.DbCritical(ex);
                if (ex.Message.StartsWith("P0001"))
                    throw;
                else
                    throw new Exception(outError);
            }
            catch (Exception ex)
            {
                _logger.DbCritical(ex);
                throw;
            }
        }

        public T ExecuteScalar<T>(string spName, params DbParameter[] dbParameters)
        {
            return ExecuteScalar<T>(spName, CommandType.StoredProcedure, dbParameters);
        }

        public T ExecuteScalar<T>(string cmdText, CommandType cmdType, params DbParameter[] dbParameters)
        {
            return ExecuteScalar(cmdText, cmdType, GetTypeConverter<T>(), dbParameters);
        }

        public T ExecuteScalar<T>(string cmdText, CommandType cmdType, Converter<object, T> converter, params DbParameter[] dbParameters)
        {
            try
            {
                object input = ExecuteScalar(cmdText, cmdType, dbParameters);

                return converter(input);
            }
            catch (NpgsqlException ex)
            {
                _logger.DbCritical(ex);
                if (ex.Message.StartsWith("P0001"))
                    throw;
                else
                    throw new Exception(outError);
            }
            catch (Exception ex)
            {
                _logger.DbCritical(ex);
                throw;
            }
        }

        public DataTable GetDataTable(string spName, params DbParameter[] dbParameters)
        {
            return GetDataTable(spName, CommandType.StoredProcedure, dbParameters);
        }

        public DataTable GetDataTable(string cmdText, CommandType cmdType, params DbParameter[] dbParameters)
        {
            try
            {
                using DbDataReader dr = ExecuteReader(cmdText, cmdType, dbParameters);
                DataTable dt = new();
                if (dr.HasRows)
                    dt.Load(dr);

                dr.Close();

                return dt;
            }
            catch (NpgsqlException ex)
            {
                _logger.DbCritical(ex);
                if (ex.Message.StartsWith("P0001"))
                    throw;
                else
                    throw new Exception(outError);
            }
            catch (Exception ex)
            {
                _logger.DbCritical(ex);
                throw;
            }
        }

        public DataSet GetDataSet(string spName, params DbParameter[] dbParameters)
        {
            return GetDataSet(spName, CommandType.StoredProcedure, dbParameters);
        }

        public DataSet GetDataSet(string cmdText, CommandType cmdType, params DbParameter[] dbParameters)
        {
            try
            {
                using NpgsqlCommand npgsqlCommand = CreateCommand(cmdText, cmdType, false, dbParameters);
                using var npgsqlDataAdapter = new NpgsqlDataAdapter(npgsqlCommand);
                DataSet dataSet = new();
                npgsqlDataAdapter.Fill(dataSet);

                return dataSet;
            }
            catch (NpgsqlException ex)
            {
                _logger.DbCritical(ex);
                if (ex.Message.StartsWith("P0001"))
                    throw;
                else
                    throw new Exception(outError);
            }
            catch (Exception ex)
            {
                _logger.DbCritical(ex);
                throw;
            }
        }

        private DbDataReader ExecuteReader(string spName, params DbParameter[] dbParameters)
        {
            return ExecuteReader(spName, CommandType.StoredProcedure, dbParameters);
        }

        private DbDataReader ExecuteReader(string cmdText, CommandType cmdType, params DbParameter[] dbParameters)
        {
            NpgsqlCommand npgsqlCommand = CreateCommand(cmdText, cmdType, false, dbParameters);
            NpgsqlDataReader result = npgsqlCommand.ExecuteReader();

            return result;
        }

        public T GetSingle<T>(string spName, params DbParameter[] dbParameters) where T : new()
        {
            return GetSingle<T>(spName, CommandType.StoredProcedure, dbParameters);
        }

        public T GetSingle<T>(string cmdText, CommandType cmdType, params DbParameter[] dbParameters) where T : new()
        {
            return GetSingle(cmdText, cmdType, GetDataReaderConverter<T>(), dbParameters);
        }

        public T GetSingle<T>(string cmdText, CommandType cmdType, Converter<DbDataReader, T> converter, params DbParameter[] dbParameters)
        {
            try
            {
                using DbDataReader dbDataReader = ExecuteReader(cmdText, cmdType, dbParameters);
                T result = (!dbDataReader.Read()) ? default : converter(dbDataReader);
                dbDataReader.Close();

                return result;
            }
            catch (NpgsqlException ex)
            {
                _logger.DbCritical(ex);
                if (ex.Message.StartsWith("P0001"))
                    throw;
                else
                    throw new Exception(outError);
            }
            catch (Exception ex)
            {
                _logger.DbCritical(ex);
                throw;
            }
        }

        public List<T> GetList<T>(string spName, params DbParameter[] dbParameters) where T : new()
        {
            return GetList<T>(spName, CommandType.StoredProcedure, dbParameters);
        }

        public List<T> GetList<T>(string cmdText, CommandType cmdType, params DbParameter[] dbParameters) where T : new()
        {
            return GetList(cmdText, cmdType, GetDataReaderConverter<T>(), dbParameters);
        }

        public List<T> GetList<T>(string cmdText, CommandType cmdType, Converter<DbDataReader, T> converter, params DbParameter[] dbParameters)
        {
            List<T> list = new();
            using DbDataReader reader = ExecuteReader(cmdText, cmdType, dbParameters);
            try
            {
                FillFromReader(reader, delegate
                {
                    list.Add(converter(reader));
                });
                reader.Close();
            }
            catch (NpgsqlException ex)
            {
                _logger.DbCritical(ex);
                if (ex.Message.StartsWith("P0001"))
                    throw;
                else
                    throw new Exception(outError);
            }
            catch (Exception ex)
            {
                _logger.DbCritical(ex);
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    ((IDisposable)reader).Dispose();
                }
            }

            return list;
        }

        public T[] GetArray<T>(string spName, params DbParameter[] dbParameters)
        {
            return GetArray<T>(spName, CommandType.StoredProcedure, dbParameters);
        }

        public T[] GetArray<T>(string cmdText, CommandType cmdType, params DbParameter[] dbParameters)
        {
            return GetArray(cmdText, cmdType, GetTypeConverter<T>(), dbParameters);
        }

        public T[] GetArray<T>(string cmdText, CommandType cmdType, Converter<object, T> converter, params DbParameter[] dbParameters)
        {
            try
            {
                List<T> list = new();
                using (DbDataReader dbDataReader = ExecuteReader(cmdText, cmdType, dbParameters))
                {
                    FillFromReader(dbDataReader, delegate (DbDataReader r)
                    {
                        list.Add(converter(r.GetValue(0)));
                    });
                    dbDataReader.Close();
                }

                return list.ToArray();
            }
            catch (NpgsqlException ex)
            {
                _logger.DbCritical(ex);
                if (ex.Message.StartsWith("P0001"))
                    throw;
                else
                    throw new Exception(outError);
            }
            catch (Exception ex)
            {
                _logger.DbCritical(ex);
                throw;
            }
        }

        public Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string spName, params DbParameter[] dbParameters)
        {
            return GetDictionary<TKey, TValue>(spName, CommandType.StoredProcedure, dbParameters);
        }

        public Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string cmdText, CommandType cmdType, params DbParameter[] dbParameters)
        {
            return GetDictionary(cmdText, cmdType, GetTypeConverter<TKey>(), GetTypeConverter<TValue>(), dbParameters);
        }

        public Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string cmdText, CommandType cmdType, Converter<object, TKey> keyConverter, Converter<object, TValue> valueConverter, params DbParameter[] dbParameters)
        {
            try
            {
                Dictionary<TKey, TValue> dict = new();
                using (DbDataReader dbDataReader = ExecuteReader(cmdText, cmdType, dbParameters))
                {
                    FillFromReader(dbDataReader, delegate (DbDataReader r)
                    {
                        dict.Add(keyConverter(r.GetValue(0)), valueConverter(r.GetValue(1)));
                    });
                    dbDataReader.Close();
                }

                return dict;
            }
            catch (NpgsqlException ex)
            {
                _logger.DbCritical(ex);
                if (ex.Message.StartsWith("P0001"))
                    throw;
                else
                    throw new Exception(outError);
            }
            catch (Exception ex)
            {
                _logger.DbCritical(ex);
                throw;
            }
        }

        private NpgsqlCommand CreateCommand(string cmdText, CommandType cmdType, bool isWithTransaction = false, DbParameter[] dbParameters = null)
        {
            if (string.IsNullOrEmpty(cmdText))
            {
                throw new ArgumentNullException(nameof(cmdText), "Command text or procedure is null or empty.");
            }

            Connect();

            if (cmdType == CommandType.Text)
            {
                cmdText = cmdText.Replace("\r\n", " ");
            }

            NpgsqlCommand npgsqlCommand = new(cmdText, (NpgsqlConnection)_conn)
            {
                CommandType = cmdType
            };

            if (isWithTransaction && _trans != null)
                npgsqlCommand.Transaction = (NpgsqlTransaction)_trans;

            if (dbParameters != null)
            {
                AttachParameters(npgsqlCommand, dbParameters);
            }

            return npgsqlCommand;
        }

        private static void AttachParameters(DbCommand command, params DbParameter[] dbParameters)
        {
            command.Parameters.AddRange(dbParameters);
        }

        private static Converter<object, T> GetTypeConverter<T>()
        {
            return (object o) => DBConvert.To<T>(o);
        }

        private static Converter<DbDataReader, T> GetDataReaderConverter<T>() where T : new()
        {
            return new DataReaderConverter<T>().Convert;
        }

        private static void FillFromReader(DbDataReader reader, Action<DbDataReader> action)
        {
            while (reader.Read())
            {
                action(reader);
            }
        }
    }
}
