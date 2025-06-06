using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

public class TelaEmprestimo : TelaBase
{
    private RepositorioEmprestimo repositorioEmprestimo;
    private RepositorioAmigo repositorioAmigo;
    private RepositorioRevista repositorioRevista;

    public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioAmigo repositorioAmigo,
    RepositorioRevista repositorioRevista) : base("Emprestimo", repositorioEmprestimo)
    {
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
    }

    public override void VisualizarRegistros(bool exibirCabecalho)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("Visualização de Empréstimos\n");

        Console.WriteLine(
            "{0, -5} | {1, -25} | {2, -25} | {3, -15} | {4, -15} | {5, -10}",
            "Id", "Amigo", "Revista", "Empréstimo", "Devolução", "Status");

        EntidadeBase[] emprestimos = repositorio.SelecionarRegistros();

        for (int i = 0; i < emprestimos.Length; i++)
        {

            Emprestimo e = (Emprestimo)emprestimos[i];

            if (e == null)
                continue;

            string atraso = e.status == "Aberto" && DateTime.Now > e.dataDevolucao ? " (ATRASADO)" : "";

            Console.WriteLine(
                "{0, -5} | {1, -25} | {2, -25} | {3, -15:dd/MM/yyyy} | {4, -15:dd/MM/yyyy} | {5, -10}",
                e.id, e.amigo.nome, e.revista.titulo, e.dataEmprestimo, e.dataDevolucao, e.status + atraso);
        }

        Console.ReadLine();
    }

    protected override EntidadeBase ObterDados()
    {
        Console.WriteLine("Cadastro de Empréstimo:\n");

        Console.WriteLine("Amigos disponíveis:");
        
        EntidadeBase[] amigos = repositorioAmigo.SelecionarRegistros();

        for (int i = 0; i < amigos.Length; i++)
        {

            Amigo a = (Amigo)amigos[i];

            if (a == null)
                continue;

            Console.WriteLine($"ID: {a.id} | Nome: {a.nome} | Responsável: {a.nomeResponsavel} | Telefone: {a.telefone}");
        }

        Console.Write("\nDigite o ID do amigo: ");
        int idAmigo = int.Parse(Console.ReadLine());

        Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarRegistroPorId(idAmigo);

        bool jaTemEmprestimo = repositorioEmprestimo.SelecionarRegistros()
            .OfType<Emprestimo>()
            .Any(e => e.amigo.id == idAmigo && e.status == "Aberto");

        if (jaTemEmprestimo)
        {
            Console.WriteLine("Erro: o amigo já possui um empréstimo em aberto.");
            return null;
        }

        Console.WriteLine("\nRevistas disponíveis:");

        EntidadeBase[] revistas = repositorioRevista.SelecionarRegistros();

        for (int i = 0; i < revistas.Length; i++)
        {

            Revista r = (Revista)revistas[i];

            if (r == null || r.status != "Disponível")
                continue;

            if (r.status == "Disponível")
                Console.WriteLine($"ID: {r.id} | Título: {r.titulo} | Edição: {r.numeroEdicao} | Ano: {r.anoPublicacao} | Status: {r.status} | Caixa: {r.caixa.etiqueta}");
        }

        Console.Write("\nDigite o ID da revista: ");
        int idRevista = int.Parse(Console.ReadLine());

        Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarRegistroPorId(idRevista);

        Emprestimo novoEmprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada);
        revistaSelecionada.status = "Emprestada";

        return novoEmprestimo;
    }

    public void RegistrarDevolucao()
    {
        VisualizarRegistros(false);

        Console.Write("\nDigite o ID do empréstimo para registrar devolução: ");
        int id = int.Parse(Console.ReadLine());

        Emprestimo emprestimo = (Emprestimo)repositorioEmprestimo.SelecionarRegistroPorId(id);

        if (emprestimo == null || emprestimo.status != "Aberto")
        {
            Console.WriteLine("Empréstimo não encontrado ou já devolvido.");
            return;
        }

        emprestimo.RegistrarDevolucao();
        Console.WriteLine("Devolução registrada com sucesso!");
        Console.ReadLine();
    }
}
