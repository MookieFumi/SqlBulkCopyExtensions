# SqlBulkCopyExtensions #

    public static DataTable PrepareDataTable<T>(this SqlBulkCopy sqlBulkCopy, DbContext context, IEnumerable<T> items, string connectionString) where T : class    
    public DataTable PrepareDataTable<T>(this SqlBulkCopy sqlBulkCopy, string tableName, IEnumerable<T> items, string connectionString) where T : class