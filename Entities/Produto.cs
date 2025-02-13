using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using SisEstoque.Entities.Enums;

namespace SisEstoque.Entities
{
    class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public int Quantidade { get; set; }
        public Categoria Categoria { get; set; }
        public Produto(int id, string nome, double preco, int quantidade, Categoria categoria)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
            Categoria = categoria;
        }

        public override string ToString()
        {
            return
                Id + "," +Nome + "," + Preco.ToString("F2", CultureInfo.InvariantCulture) + "," + Quantidade + "," + Categoria;
        }
    }
}
