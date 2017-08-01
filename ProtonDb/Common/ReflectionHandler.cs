using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProtonDb.Common
{
    public static class ReflectionHandler
    {
        /// <summary>
        /// Convert SqlDataReader object to generic list.
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="dr">SqlDataReader Object</param>
        /// <returns></returns>
        public static List<T> DataReaderMapToList<T>(this IDataReader dr) where T : new()
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();

                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (prop.PropertyType.Name == "List`1")
                    {
                        continue;
                    }
                    if (ColumnExists(dr, prop.Name))
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                }

                list.Add(obj);
            }
            return list;
        }

        /// <summary>
        /// Convert SqlDataReader object to object.
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="dr">SqlDataReader Object</param>
        /// <returns></returns>
        public static T DataReaderMapToObject<T>(this IDataReader dr) where T : new()
        {
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (prop.PropertyType.Name == "List`1")
                    {
                        continue;
                    }
                    if (ColumnExists(dr, prop.Name))
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                }
            }
            return obj;
        }

        /// <summary>
        /// System variables.
        /// </summary>
        /// <typeparam name="T">int, string etc.</typeparam>
        /// <param name="dr">SqlDataReader Object</param>
        /// <returns></returns>
        public static List<T> DataReaderMapToGenericVariableList<T>(this IDataReader dr)
        {
            List<T> list = new List<T>();
            while (dr.Read())
            {
                list.Add((T)dr[0]);
            }
            return list;
        }

        /// <summary>
        /// Copy object to another object
        /// </summary>
        /// <typeparam name="T">int, string etc.</typeparam>
        /// <param name="propertyExcludeList">excluding propety names with string list</param>
        /// <returns></returns>
        public static TU CloneObject<T, TU>(T original, List<string> propertyExcludeList)
        {
            TU copy = Activator.CreateInstance<TU>();
            PropertyInfo[] propertyInfoList = typeof(TU).GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfoList)
            {
                if (!propertyExcludeList.Contains(propertyInfo.Name))
                {
                    PropertyInfo p = typeof(T).GetProperties().FirstOrDefault(d => d.Name == propertyInfo.Name);
                    if (p == null) continue;
                    var value = p.GetValue(original, null);

                    propertyInfo.SetValue(copy, value, null);

                }
            }
            return copy;
        }

        private static bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i) == columnName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
