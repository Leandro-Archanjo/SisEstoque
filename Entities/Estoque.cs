using SisEstoque.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SisEstoque.Entities
{
    class Estoque
    {
        public string CaminhoArquivo = @"C:\Users\leand\Documents\Projetos\MeusProjetos\SisEstoque\ArmazenamentoEstoque\estoque.csv";
        public int IncrementoId { get; set; }
        public List<Produto> Produtos { get; set; } = new List<Produto>();

        public int IncrementarId()
        {
            try
            {
                string[] linha;
                using (StreamReader sr = File.OpenText(CaminhoArquivo))
                {
                    int ultimoId = 0;

                    if (sr.ReadLine() == null)
                    {
                        return ultimoId;
                    }
                    else
                    {
                        while (!sr.EndOfStream)
                        {
                            linha = sr.ReadLine().Split(',');
                            ultimoId = int.Parse(linha[0]);
                        }

                        return ultimoId;
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Erro ao percorrer arquivo: " + ex.Message);
                return 0;
            }
        }
        public void AdicionarProduto(Produto prod)
        {
            Produtos.Add(prod);
        }

        public void SalvarNoEstoque()
        {
            try
            {
                using (FileStream fs = new FileStream(CaminhoArquivo, FileMode.Append))
                {
                    using (StreamWriter fw = new StreamWriter(fs))
                    {
                        foreach (Produto produto in Produtos)
                        {
                            fw.WriteLine(produto);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Erro tentar gravar no arquivo estoque: " + ex.Message);
            }
        }

        public void AlterarPreco(int idProd, double precoNovo)
        {
            Produtos.Clear();

            StreamReader sr = null;
            FileStream fs = null;
            StreamWriter sw = null;

            string[] linha;
            int id = 0;
            string nome;
            double preco;
            int quantidade = 0;
            Categoria categoria = new Categoria();

            using (sr = File.OpenText(CaminhoArquivo))
            {
                while (!sr.EndOfStream)
                {
                    linha = sr.ReadLine().Split(',');
                    id = int.Parse(linha[0]);
                    nome = linha[1];
                    preco = double.Parse(linha[2], CultureInfo.InvariantCulture);
                    quantidade = int.Parse(linha[3]);
                    categoria = Enum.Parse<Categoria>(linha[4]);

                    Produtos.Add(new Produto(id, nome, preco, quantidade, categoria));
                }
            }
            File.Delete(CaminhoArquivo);
            using (fs = new FileStream(CaminhoArquivo, FileMode.Create))
            {
                using (sw = new StreamWriter(fs))
                {
                    foreach (Produto p in Produtos)
                    {
                        sw.WriteLine(p);
                    }
                }
            }
        }
        public void ListarProdutosEmEstoque()
        {
            try
            {
                using (StreamReader sr = File.OpenText(CaminhoArquivo))
                {
                    string[] linha = null;
                    while (!sr.EndOfStream)
                    {
                        linha = sr.ReadLine().Split(',');
                        Console.WriteLine(linha[0] + linha[1] + linha[2] + linha[3] + linha[4]);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
