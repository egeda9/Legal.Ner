using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain;
using Legal.Ner.Log;

namespace Legal.Ner.DataAccess.Implementations
{
    public class TreeEdgeData : ITreeEdgeData
    {
        private readonly IDbConnection _db;
        private readonly ILogger _logger;

        public TreeEdgeData(ILogger logger)
        {
            _logger = logger;
            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
        }

        public void Insert(List<TreeEdge> treeEdges, int fileKeyId, int treeId)
        {
            const string insertTreeEdge = "INSERT INTO TreeEdge(Id,FromNode,ToNode,Head,FileKey_Id,Tree_Id) values (@Id,@FromNode,@ToNode,@Head,@FileKey_Id,@Tree_Id);";
            try
            {
                foreach (TreeEdge treeEdge in treeEdges)
                    _db.Execute(insertTreeEdge, new { Id = treeEdge.Id, FromNode = treeEdge.FromNode, ToNode = treeEdge.ToNode, Head = treeEdge.Head, FileKey_Id = fileKeyId, Tree_Id = treeId });
            }

            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
