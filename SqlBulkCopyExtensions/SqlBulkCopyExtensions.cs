using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Dapper;
using EntityFramework.MappingAPI.Extensions;

namespace SqlBulkCopyExtensions
{
    public static class SqlBulkCopyExtensions
    {
        public static DataTable PrepareDataTable<T>(this SqlBulkCopy sqlBulkCopy, DbContext context, IEnumerable<T> items, string connectionString) where T : class
        {
            return PrepareDataTable(sqlBulkCopy, context.Db<T>().TableName, items, connectionString);            
        }

        public static DataTable PrepareDataTable<T>(this SqlBulkCopy sqlBulkCopy, string tableName, IEnumerable<T> items, string connectionString) where T : class
        {
            var dataTable = ConvertToDataTable(items, tableName);
            var columnNames = GetOrderedColumnNames(tableName, connectionString);
            SortColumns(dataTable, columnNames);
            RemoveNotExistingColumns(dataTable, columnNames);
            return dataTable;
        }

        private static DataTable ConvertToDataTable<T>(IEnumerable<T> items, string tableName = null) where T : class
        {
            var dataTable = new DataTable(tableName ?? typeof(T).Name);

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                dataTable.Columns.Add(property.Name, GetDataColumnType(property));
            }

            foreach (var item in items)
            {
                var values = new object[properties.Length];
                for (var i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        private static Type GetDataColumnType(PropertyInfo property)
        {
            var type = property.PropertyType;
            if (Nullable.GetUnderlyingType(property.PropertyType) != null)
            {
                type = Nullable.GetUnderlyingType(property.PropertyType);
            }
            if (type.IsEnum)
            {
                type = type.GetEnumUnderlyingType();
            }
            return type;
        }

        private static IEnumerable<string> GetOrderedColumnNames(string tableName, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<string>(
                    $@"SELECT  COLUMN_NAME AS ColumnName
                FROM    INFORMATION_SCHEMA.COLUMNS
                WHERE   TABLE_NAME = '{tableName}'
                ORDER BY ORDINAL_POSITION").ToList();
            }
        }

        private static void SortColumns(DataTable dataTable, IEnumerable<string> columnNames)
        {
            var columns = columnNames.ToList();
            foreach (var columnName in columns)
            {
                var ordinal = columns.IndexOf(columnName);
                dataTable.Columns[columnName].SetOrdinal(ordinal);
            }
        }

        private static void RemoveNotExistingColumns(DataTable dataTable, IEnumerable<string> columnNames)
        {
            for (var i = dataTable.Columns.Count - 1; i >= 0; i--)
            {
                var dataColumn = dataTable.Columns[i];
                var removeColumn = !columnNames.Contains(dataColumn.ColumnName);
                if (removeColumn)
                {
                    dataTable.Columns.Remove(dataColumn.ColumnName);
                }
            }
        }
    }
}