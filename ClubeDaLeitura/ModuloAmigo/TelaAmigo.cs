using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo;

public class TelaAmigo : TelaBase
{

    public TelaAmigo(RepositorioAmigo repositorioAmigo) : base("Amigo", repositorioAmigo)
    {
    }
    
    public override void VisualizarRegistros(bool exibirCabecalho)
    {
        if (exibirCabecalho == true)
            ExibirCabecalho();

        Console.WriteLine("Visualização de Amigos");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -30} | {2, -30} | {3, -20}",
            "Id", "Nome", "Nome Responsável", "Telefone"
        );

        EntidadeBase[] amigos = repositorio.SelecionarRegistros();

        for (int i = 0; i < amigos.Length; i++)
        {
            Amigo a = (Amigo)amigos[i];

            if (a == null)
                continue;

            Console.WriteLine(
               "{0, -10} | {1, -30} | {2, -30} | {3, -20}",
                a.id, a.nome, a.nomeResponsavel, a.telefone
            );
        }

        Console.ReadLine();
    }

    protected override EntidadeBase ObterDados()
    {
        Console.Write("Digite o nome do amigo: ");
        string nome = Console.ReadLine();

        Console.Write("Digite o nome do responsável pelo amigo: ");
        string nomeResponsavel = Console.ReadLine();

        Console.Write("Digite o telefone do amigo ou responsável: ");
        string telefone = Console.ReadLine();

        Amigo amigo = new Amigo(nome, nomeResponsavel, telefone);

        return amigo;
    }
}