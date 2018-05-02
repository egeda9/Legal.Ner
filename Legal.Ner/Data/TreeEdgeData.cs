using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using log4net;
using Legal.Ner.DTO;

namespace Legal.Ner.Data
{
    public class TreeEdgeData
    {
        private readonly IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
        private readonly ILog _log;

        public TreeEdgeData()
        {
            log4net.Config.BasicConfigurator.Configure();
            _log = LogManager.GetLogger(typeof(TreeEdgeData));
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
                _log.Error(ex.Message);
            }
        }
    }
}
