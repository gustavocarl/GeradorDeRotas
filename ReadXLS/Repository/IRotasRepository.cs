using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadXLS.Service
{
    public interface IRotasRepository
    {
        bool Add(Rotas rota);

        List<Rotas> GetAll();

    }
}
