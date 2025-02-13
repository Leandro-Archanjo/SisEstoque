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

        public void PopularListaProduto()
        {
            Produtos.Clear();

            try
            {
                using (StreamReader sr = File.OpenText(CaminhoArquivo))
                {
                    string[] linha;
                    while (!sr.EndOfStream)
                    {
                        linha = sr.ReadLine().Split(',');
                        int id = int.Parse(linha[0]);
                        string nome = linha[1];
                        double preco = double.Parse(linha[2], CultureInfo.InvariantCulture);
                        int quantidade = int.Parse(linha[3]);
                        Categoria categoria = Enum.Parse<Categoria>(linha[4]);

                        Produtos.Add(new Produto(id, nome, preco, quantidade, categoria));
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
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
            PopularListaProduto();

            FileStream fs = null;
            StreamWriter sw = null;

            foreach (Produto prod in Produtos)
            {
                if (prod.Id == idProd)
                {
                    prod.Preco = precoNovo;
                }
            }
            File.Delete(CaminhoArquivo);
            using (fs = new FileStream(CaminhoArquivo, FileMode.Create))
            {
                using (sw = new StreamWriter(fs))
                {
                    foreach (Produto p in Produtos)
                    {
                        sw.WriteLine(p.ToString());
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
                        Console.WriteLine(linha[0] + " " + linha[1] + " " + linha[2] + " " + linha[3] + " " + linha[4]);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void FiltrarEstoque(int operacao)
        {
            PopularListaProduto();

            switch (operacao)
            {
                case 1:
                    Console.Write("Informe o Id: ");
                    int id = int.Parse(Console.ReadLine());

                    var filtrarId = (from p in Produtos
                                     where p.Id == id
                                     select p);

                    foreach (Produto p in filtrarId)
                    {
                        Console.WriteLine(p);
                    }

                    break;
                case 2:
                    Console.Write("Informe uma categoria: ");
                    string formatacao = Console.ReadLine();
                    Categoria categoria = Enum.Parse<Categoria>(formatacao.Substring(0, 1).ToUpper() + formatacao.Substring(1));

                    var filtroCategoria = (from p in Produtos
                                           where p.Categoria == categoria
                                           select p);

                    foreach (Produto p in filtroCategoria)
                    {
                        Console.WriteLine(p);
                    }

                    break;
                case 3:
                    Console.Write("Infore um nome: ");
                    string nome = Console.ReadLine();

                    var filtroNome = (from p in Produtos
                                       where p.Nome == nome
                                       select p);

                    foreach (Produto p in filtroNome)
                    {
                        Console.WriteLine(p);
                    }

                    break;
                case 4:
                    Console.Write("Valor maior que: ");
                    double precoMaior = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                    var maiorPreco = (from p in Produtos
                                       where p.Preco > precoMaior
                                       select p);

                    foreach (Produto p in maiorPreco)
                    {
                        Console.WriteLine(p);
                    }

                    break;
                case 5:
                    Console.Write("Valor menor que: ");
                    double precoMenor = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                    var menorPreco = (from p in Produtos
                                             where p.Preco < precoMenor
                                             select p);

                    foreach (Produto p in menorPreco)
                    {
                        Console.WriteLine(p);
                    }

                    break;
                default:
                    Console.WriteLine("Operação inválida");
                    break;
            }
        }
    }
}
