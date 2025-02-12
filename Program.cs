using System;
using System.Globalization;

using SisEstoque.Entities;
using SisEstoque.Entities.Enums;

namespace SisEstoque
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("<-- SisEstoque seu programa de estoque -->\n");
            Console.WriteLine("Operações: ");
            Console.WriteLine("1 - Cadastrar produto 2 - Alterar preço do produto 3 - Listar estoque");
            int operacao = int.Parse(Console.ReadLine());
            Console.Clear();

            Estoque estoque = new Estoque();

            switch (operacao)
            {
                case 1:
                    string nome;
                    double preco;
                    int id = estoque.IncrementarId();
                    int quantidade;
                    int nProd = 0;
                    Categoria categoria;

                    Console.Write("Quantos produtos serão cadastrados: ");
                    nProd = int.Parse(Console.ReadLine());

                    for (int i = 0; i < nProd; i++)
                    {
                        id += + 1;
                        Console.Write("Nome: ");
                        nome = Console.ReadLine();
                        Console.Write("Preco: ");
                        preco = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                        Console.Write("Quantidade: ");
                        quantidade = int.Parse(Console.ReadLine());
                        Console.Write("Categorias:\n1 - Alimenticio 2 - Higienie 3 - Ferramenta 4 - Limpera 5 - Brinquedo\n");
                        categoria = Enum.Parse<Categoria>(Console.ReadLine());

                        Produto produto = new Produto(id, nome, preco, quantidade, categoria);
                        estoque.AdicionarProduto(produto);
                    }
                    estoque.SalvarNoEstoque();

                    break;
                case 2:
                    Console.WriteLine("Infome o Id do produto: ");
                    id = int.Parse(Console.ReadLine());
                    Console.WriteLine("Preço novo: ");
                    preco = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                    estoque.AlterarPreco(id, preco);
                    break;
                case 3:
                    Console.WriteLine("Produtos em estoque: ");
                    estoque.ListarProdutosEmEstoque();
                    break;
                default:
                    Console.WriteLine("Operação inválida");
                break;
            }
        }
    }
}