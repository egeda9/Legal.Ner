using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain;

namespace Legal.Ner.DataAccess.Implementations
{
    public class TermData : ITermData
    {
        private readonly IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);

        public TermData()
        {
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
                
            }
        }
    }
}
