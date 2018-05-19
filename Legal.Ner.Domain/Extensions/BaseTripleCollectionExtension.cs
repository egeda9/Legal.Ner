using System;
using VDS.RDF;

namespace Legal.Ner.Domain.Extensions
{
    public static class BaseTripleCollectionExtension
    {
        public static string ToHtml(this BaseTripleCollection tripleCollection)
        {
            string html = "<table id=\"table_result\" class=\"display\">";

            html += "<thead><tr>";
            html += "<th style=\"text-align: center;\">sujeto</th>";
            html += "<th style=\"text-align: center;\">predicado</th>";
            html += "<th style=\"text-align: center;\">objeto</th>";
            html += "</tr>";

            foreach (Triple triple in tripleCollection)
            {
                html += "<tr>";
                foreach (INode node in triple.Nodes)
                {
                    bool isUri = Uri.IsWellFormedUriString(node.ToString(), UriKind.RelativeOrAbsolute);
                    string text = isUri ? $"<td><a href=\"{node.ToString()}\">{node.ToString()}</a></td>" : $"<td>{node.ToString()}</td>";
                    html += text;
                }
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }
    }
}
