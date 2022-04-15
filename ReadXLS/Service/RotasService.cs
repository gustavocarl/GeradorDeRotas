using Model;
using ReadXLS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadXLS.Repository
{
    public class RotasService
    {
        private IRotasRepository _rotasRepository;

        public RotasService()
        {
            _rotasRepository = new RotasRepository();
        }

        public bool Add(Rotas rota)
        {
            return _rotasRepository.Add(rota);
        }

        public List<Rotas> GetAll()
        {
            return _rotasRepository.GetAll();
        }
    }
}
