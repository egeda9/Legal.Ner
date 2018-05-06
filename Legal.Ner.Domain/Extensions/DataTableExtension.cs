using System.Data;

namespace Legal.Ner.Domain.Extensions
{
    public static class DataTableExtension
    {
        public static string ToHtml(this DataTable dataTable)
        {
            string html = "<table class=\"table table-striped table - bordered table-list\">";

            html += "<thead><tr>";
            for (int i = 0; i < dataTable.Columns.Count; i++)
                html += "<th style=\"text-align: center;\">" + dataTable.Columns[i].ColumnName + "</th>";
            html += "</tr>";

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dataTable.Columns.Count; j++)
                    html += "<td>" + dataTable.Rows[i][j] + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }
    }
}
