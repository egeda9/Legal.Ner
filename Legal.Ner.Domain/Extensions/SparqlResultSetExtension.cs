using System;
using System.Data;
using VDS.RDF;
using VDS.RDF.Query;

namespace Legal.Ner.Domain.Extensions
{
    public static class SparqlResultSetExtensions
    {
        public static DataTable ToDataTable(this SparqlResultSet results)
        {
            DataTable table = new DataTable();
            DataRow row;

            switch (results.ResultsType)
            {
                case SparqlResultsType.VariableBindings:
                    foreach (string var in results.Variables)
                        table.Columns.Add(new DataColumn(var, typeof(INode)));

                    foreach (SparqlResult r in results)
                    {
                        row = table.NewRow();

                        foreach (string var in results.Variables)
                        {
                            if (r.HasValue(var))
                                row[var] = r[var];

                            else
                                row[var] = null;
                        }
                        table.Rows.Add(row);
                    }
                    break;
                case SparqlResultsType.Boolean:
                    table.Columns.Add(new DataColumn("ASK", typeof(bool)));
                    row = table.NewRow();
                    row["ASK"] = results.Result;
                    table.Rows.Add(row);
                    break;

                case SparqlResultsType.Unknown:
                default:
                    throw new InvalidCastException("Unable to cast a SparqlResultSet to a DataTable as the ResultSet has yet to be filled with data and so has no SparqlResultsType which determines how it is cast to a DataTable");
            }

            return table;
        }
    }
}
