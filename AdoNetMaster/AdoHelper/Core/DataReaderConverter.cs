using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace AdoNetMaster.Infrastructure.AdoHelper.Core
{
    public class DataReaderConverter<T> where T : new()
    {
        private class Mapping
        {
            public int Index;

            public PropertyInfo Property;
        }

        private Mapping[] _mappings;

        private DbDataReader _lastReader;

        public T Convert(DbDataReader reader)
        {
            if (_mappings == null || reader != _lastReader)
            {
                _mappings = MapProperties(reader);
            }

            T val = new T();
            Mapping[] mappings = _mappings;
            foreach (Mapping mapping in mappings)
            {
                PropertyInfo property = mapping.Property;
                property.SetValue(value: DBConvert.To(value: reader.GetValue(mapping.Index), type: property.PropertyType), obj: val, index: null);
            }

            _lastReader = reader;
            return val;
        }

        private Mapping[] MapProperties(DbDataReader reader)
        {
            int fieldCount = reader.FieldCount;
            Dictionary<string, int> dictionary = new Dictionary<string, int>(fieldCount);
            for (int i = 0; i < fieldCount; i++)
            {
                dictionary.Add(reader.GetName(i).ToLowerInvariant(), i);
            }

            Type typeFromHandle = typeof(T);
            List<Mapping> list = new List<Mapping>(fieldCount);
            PropertyInfo[] properties = typeFromHandle.GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                string key = propertyInfo.Name.ToLowerInvariant();
                if (dictionary.TryGetValue(key, out var value))
                {
                    list.Add(new Mapping
                    {
                        Index = value,
                        Property = propertyInfo
                    });
                }
            }

            return list.ToArray();
        }
    }
}
