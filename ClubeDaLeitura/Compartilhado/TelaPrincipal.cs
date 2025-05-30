using GestaoDeEquipamentos.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;

namespace ClubeDaLeitura.ConsoleApp.Compartilhado;

public class TelaPrincipal
{
    private char opcaoEscolhida;

    private RepositorioAmigo repositorioAmigo;
    private RepositorioEquipamento repositorioEquipamento;
    private RepositorioChamado repositorioChamado;

    private TelaAmigo telaAmigo;
    private TelaEquipamento telaEquipamento;
    private TelaChamado telaChamado;

    public TelaPrincipal()
    {
        repositorioAmigo = new RepositorioAmigo();
        repositorioEquipamento = new RepositorioEquipamento();
        repositorioChamado = new RepositorioChamado();

        telaAmigo = new TelaAmigo(repositorioAmigo);

        telaEquipamento = new TelaEquipamento(
            repositorioEquipamento,
            repositorioAmigo
        );

        telaChamado = new TelaChamado(repositorioChamado, repositorioEquipamento);
    }

    public void ApresentarMenuPrincipal()
    {
        Console.Clear();

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("|           Clube da Leitura           |");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        Console.WriteLine("1 - Controle de Amigos");
        Console.WriteLine("2 - Controle de Caixas");
        Console.WriteLine("3 - Controle de Revistas");
        Console.WriteLine("4 - Controle de Empréstimos");
        Console.WriteLine("S - Sair");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        opcaoEscolhida = Console.ReadLine()[0];
    }

    public TelaBase ObterTela()
    {
        if (opcaoEscolhida == '1')
            return telaAmigo;

        else if (opcaoEscolhida == '2')
            return telaCaixa;

        else if (opcaoEscolhida == '3')
            return telaRevista;
        else if (opcaoEscolhida == '4')
            return telaEmprestimo;
        
        return null;
    }
}