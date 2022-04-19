using Dapper;
using Model;
using ReadXLS.Config;
using ReadXLS.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadXLS.Service
{
    //public class RotasRepository : IRotasRepository
    //{
    //    private string _conn;

    //    public RotasRepository()
    //    {
    //        _conn = DataBaseConfiguration.Get();
    //    }

        //public bool Add(Rotas rota)
        //{
        //    bool status = false;
        //    using (var db = new SqlConnection(_conn))
        //    {

        //        db.Open();
        //        db.Execute(Rotas.INSERT, rota);
        //        status = true;

        //    }
        //    return status;
        //}


        //public List<Rotas> GetAll()
        //{
        //    using (var db = new SqlConnection(_conn))
        //    {
        //        db.Open();
        //        var rotas = db.Query<Rotas>(Rotas.GETALL);
        //        return (List<Rotas>)rotas;
        //    }
        //}


    //}
}
