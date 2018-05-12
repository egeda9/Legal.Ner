namespace Legal.Ner.DataAccess.Interfaces
{
    public interface ISparqlData
    {
        string Get(string query);
        string GetRemote(string query, string endpoint);
    }
}
