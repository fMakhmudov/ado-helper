using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace AdoNetMaster.Infrastructure.AdoHelper.Core
{
    public interface IADOHelper : IDisposable
    {
        DbProvider DbProvider { get; }

        string ConnectionString { get; }

        public void BeginTransaction();
        public void Commit();
        public void Rollback();

        int ExecuteNonQuery(string spName, params DbParameter[] dbParameters);

        int ExecuteNonQuery(string cmdText, CommandType cmdType, params DbParameter[] dbParameters);

        object ExecuteScalar(string spName, params DbParameter[] dbParameters);

        object ExecuteScalar(string cmdText, CommandType cmdType, params DbParameter[] dbParameters);

        T ExecuteScalar<T>(string spName, params DbParameter[] dbParameters);

        T ExecuteScalar<T>(string cmdText, CommandType cmdType, params DbParameter[] dbParameters);

        T ExecuteScalar<T>(string cmdText, CommandType cmdType, Converter<object, T> converter, params DbParameter[] dbParameters);

        DataTable GetDataTable(string spName, params DbParameter[] dbParameters);

        DataTable GetDataTable(string cmdText, CommandType cmdType, params DbParameter[] dbParameters);

        DataSet GetDataSet(string spName, params DbParameter[] dbParameters);

        DataSet GetDataSet(string cmdText, CommandType cmdType, params DbParameter[] dbParameters);

       /* DbDataReader ExecuteReader(string spName, params DbParameter[] dbParameters);

        DbDataReader ExecuteReader(string cmdText, CommandType cmdType, params DbParameter[] dbParameters);*/

        T GetSingle<T>(string spName, params DbParameter[] dbParameters) where T : new();

        T GetSingle<T>(string cmdText, CommandType cmdType, params DbParameter[] dbParameters) where T : new();

        T GetSingle<T>(string cmdText, CommandType cmdType, Converter<DbDataReader, T> converter, params DbParameter[] dbParameters);

        List<T> GetList<T>(string spName, params DbParameter[] dbParameters) where T : new();

        List<T> GetList<T>(string cmdText, CommandType cmdType, params DbParameter[] dbParameters) where T : new();

        List<T> GetList<T>(string cmdText, CommandType cmdType, Converter<DbDataReader, T> converter, params DbParameter[] dbParameters);

        T[] GetArray<T>(string spName, params DbParameter[] dbParameters);

        T[] GetArray<T>(string cmdText, CommandType cmdType, params DbParameter[] dbParameters);

        T[] GetArray<T>(string cmdText, CommandType cmdType, Converter<object, T> converter, params DbParameter[] dbParameters);

        Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string spName, params DbParameter[] dbParameters);

        Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string cmdText, CommandType cmdType, params DbParameter[] dbParameters);

        Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string cmdText, CommandType cmdType, Converter<object, TKey> keyConverter, Converter<object, TValue> valueConverter, params DbParameter[] dbParameters);
    }
}
