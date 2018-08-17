using System.Data;
using System.Reflection;
using System.Collections.Generic;

namespace Coursework.Utils
{
    class DataConversion
    {
        public static DataView ToDataView<T>(List<T> list)
        {
            var dataTable = new DataTable(typeof(T).Name);
            var columns = typeof(T).GetProperties();

            foreach (PropertyInfo column in columns)
                dataTable.Columns.Add(column.Name);

            foreach (T row in list)
            {
                var values = new object[columns.Length];

                for (int i = 0; i < columns.Length; i++)
                    values[i] = columns[i].GetValue(row);

                dataTable.Rows.Add(values);
            }

            return dataTable.AsDataView();
        }
    }
}
