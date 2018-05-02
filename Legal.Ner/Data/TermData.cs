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
    public class TermData
    {
        private readonly IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
        private readonly ILog _log;

        public TermData()
        {
            log4net.Config.BasicConfigurator.Configure();
            _log = LogManager.GetLogger(typeof(TermData));
        }

        public void Insert(List<Term> terms, int fileKeyId)
        {
            const string insertEntity = "INSERT INTO Term(Tid,Type,Lemma,Pos,Morphofeat,Wid,FileKey_Id) values (@Tid,@Type,@Lemma,@Pos,@Morphofeat,@Wid,@FileKey_Id);";
            try
            {
                foreach (Term term in terms)
                    _db.Execute(insertEntity, new { Tid = term.Tid, Type = term.Type, Lemma = term.Lemma, Pos = term.Pos, Morphofeat = term.Morphofeat, Wid = term.Wid, FileKey_Id = fileKeyId });
            }

            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
        }
    }
}
