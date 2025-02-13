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
            char opcao;
            do
            {
                Console.WriteLine("<-- SisEstoque seu programa de estoque -->\n");
                Console.WriteLine("Operações: ");
                Console.WriteLine("1 - Cadastrar produto \n2 - Alterar preço do produto \n3 - Listar estoque \n4 - Filtrar estoque");
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
                            id += +1;
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
                        preco = double.Parse(Console.ReadLine().Trim(), CultureInfo.InvariantCulture);

                        estoque.AlterarPreco(id, preco);

                        break;
                    case 3:
                        Console.WriteLine("Produtos em estoque: ");

                        estoque.ListarProdutosEmEstoque();

                        break;
                    case 4:
                        Console.WriteLine("Filtar produtos por:");
                        Console.WriteLine("1 - Id \n2 - Categoria \n3 - Nome \n4 - preco maior que (valor) \n5 - preco menor que (valor)");

                        operacao = int.Parse(Console.ReadLine());
                        estoque.FiltrarEstoque(operacao);

                        break;
                    default:
                        Console.WriteLine("Operação inválida");
                    break;
                }

                Console.WriteLine();
                Console.Write("Deseja sair do sistema Sim(S) ou Não(N)");
                opcao = char.Parse(Console.ReadLine());
            } while (opcao == 'N' || opcao == 'n');
        }
    }
}