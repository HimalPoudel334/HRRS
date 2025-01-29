using System.Data;
namespace HRRS.Persistence.Context;

public static class TvpExtensions
{
    public static DataTable ToDataTable<T>(this IEnumerable<T> data, string tableName)
    {
        var dataTable = new DataTable(tableName);

        // Get properties of the type
        var properties = typeof(T).GetProperties();

        foreach (var prop in properties)
        {
            dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        }

        foreach (var item in data)
        {
            var row = dataTable.NewRow();
            foreach (var prop in properties)
            {
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            }
            dataTable.Rows.Add(row);
        }

        return dataTable;
    }
}

