using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisEstoque.Entities
{
    class Nota
    {
        public int Codigo { get; set; }
        public List<Produto> produtos { get; set; } = new List<Produto>();

        public Nota(int codigo)
        {
            Codigo = codigo;
        }

        public void AdicionarProdutosANota()
        {
        }
    }
}
