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

    public override char ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine($"1 - Cadastro de {nomeEntidade}");
        Console.WriteLine($"2 - Devolução de {nomeEntidade}");
        Console.WriteLine($"3 - Visualizar {nomeEntidade}s");
        Console.WriteLine($"S - Sair");

        Console.WriteLine();

        Console.Write("Digite uma opção válida: ");
        char opcaoEscolhida = Console.ReadLine().ToUpper()[0];

        return opcaoEscolhida;
    }

     public void CadastrarEmprestimo()
    {
        ExibirCabecalho();

        Console.WriteLine($"Cadastro de {nomeEntidade}");

        Console.WriteLine();

        Emprestimo novoRegistro = (Emprestimo)ObterDados();

        string erros = novoRegistro.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(erros);
            Console.ResetColor();

            Console.Write("\nDigite ENTER para continuar...");
            Console.ReadLine();

            CadastrarRegistro();

            return;
        }

        Emprestimo[] emprestimosAtivos = repositorioEmprestimo.SelecionarEmprestimosAtivos();

        for (int i = 0; i < emprestimosAtivos.Length; i++)
        {
            Emprestimo emprestimoAtivo = emprestimosAtivos[i];

            if (novoRegistro.amigo.id == emprestimoAtivo.amigo.id)
            {
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("O amigo selecionado já tem um empréstimo ativo!");
                Console.ResetColor();

                Console.Write("\nDigite ENTER para continuar...");
                Console.ReadLine();

                return;
            }
        }

        novoRegistro.revista.status = "Emprestada";

        repositorio.CadastrarRegistro(novoRegistro);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{nomeEntidade} cadastrado com sucesso!");
        Console.ResetColor();

        Console.ReadLine();
    }

    public void DevolverEmprestimo()
    {
        ExibirCabecalho();

        Console.WriteLine($"Devolução de {nomeEntidade}");

        Console.WriteLine();

        VisualizarEmprestimosAtivos();

        Console.Write("Digite o ID do emprestimo que deseja concluir: ");
        int idEmprestimo = Convert.ToInt32(Console.ReadLine());

        Emprestimo emprestimoSelecionado = (Emprestimo)repositorio.SelecionarRegistroPorId(idEmprestimo);

        if (emprestimoSelecionado == null)
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("O empréstimo selecionado não existe!");
            Console.ResetColor();

            Console.Write("\nDigite ENTER para continuar...");
            Console.ReadLine();

            return;
        }

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("\nDeseja confirmar a conclusão do empréstimo? Esta ação é irreversível. (s/N): ");
        Console.ResetColor();

        string resposta = Console.ReadLine()!;

        if (resposta.ToUpper() == "S")
        {
            emprestimoSelecionado.status = "Concluído";
            emprestimoSelecionado.revista.status = "Disponível";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{nomeEntidade} concluído com sucesso!");
            Console.ResetColor();

            Console.ReadLine();
        }
    }

    public override void VisualizarRegistros(bool exibirCabecalho)
    {
        if (exibirCabecalho == true)
            ExibirCabecalho();

        Console.WriteLine("Visualização de Empréstimos");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -25} | {5, -15}",
            "Id", "Amigo", "Revista", "Data do Empréstimo", "Data Prev. Devolução", "Status"
        );

        EntidadeBase[] emprestimos = repositorio.SelecionarRegistros();

        for (int i = 0; i < emprestimos.Length; i++)
        {
            Emprestimo e = (Emprestimo)emprestimos[i];

            if (e == null)
                continue;

            if (e.status == "Atrasado")
                Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine(
             "{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -25} | {5, -15}",
                e.id, e.amigo.nome, e.revista.titulo, e.dataEmprestimo.ToShortDateString(), e.dataDevolucao.ToShortDateString(), e.status
            );

            Console.ResetColor();
        }

        Console.ReadLine();
    }

    protected override EntidadeBase ObterDados()
    {
        VisualizarAmigos();

        Console.Write("Digite o ID do amigo que irá receber a revista: ");
        int idAmigo = Convert.ToInt32(Console.ReadLine());

        Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarRegistroPorId(idAmigo);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\nAmigo selecionado com sucesso!");
        Console.ResetColor();

        VisualizarRevistasDisponiveis();

        Console.Write("Digite o ID da revista que irá ser emprestada: ");
        int idRevista = Convert.ToInt32(Console.ReadLine());

        Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarRegistroPorId(idRevista);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\nRevista selecionada com sucesso!");
        Console.ResetColor();

        Emprestimo emprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada);

        return emprestimo;
    }

    private void VisualizarEmprestimosAtivos()
    {
        Console.WriteLine("Visualização de Empréstimos Ativos");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -25} | {5, -15}",
            "Id", "Amigo", "Revista", "Data do Empréstimo", "Data Prev. Devolução", "Status"
        );

        EntidadeBase[] emprestimosAtivos = repositorioEmprestimo.SelecionarEmprestimosAtivos();

        for (int i = 0; i < emprestimosAtivos.Length; i++)
        {
            Emprestimo e = (Emprestimo)emprestimosAtivos[i];

            if (e == null)
                continue;

            if (e.status == "Atrasado")
                Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine(
             "{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -25} | {5, -15}",
                e.id, e.amigo.nome, e.revista.titulo, e.dataEmprestimo.ToShortDateString(), e.dataDevolucao.ToShortDateString(), e.status
            );

            Console.ResetColor();
        }

        Console.WriteLine();
    }

    private void VisualizarAmigos()
    {
        Console.WriteLine("Visualização de Amigos");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -5} | {1, -30} | {2, -30} | {3, -20}",
            "Id", "Nome", "Responsável", "Telefone"
        );

        EntidadeBase[] amigos = repositorioAmigo.SelecionarRegistros();

        for (int i = 0; i < amigos.Length; i++)
        {
            Amigo a = (Amigo)amigos[i];

            if (a == null)
                continue;

            Console.WriteLine(
              "{0, -5} | {1, -30} | {2, -30} | {3, -20}",
                a.id, a.nome, a.nomeResponsavel, a.telefone
            );
        }

        Console.WriteLine();
    }

    private void VisualizarRevistasDisponiveis()
    {
        Console.WriteLine();

        Console.WriteLine("Visualização de Revistas");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -5} | {1, -20} | {2, -10} | {3, -20} | {4, -15} | {5, -15}",
            "Id", "Título", "Edição", "Ano de Publicação", "Caixa", "Status"
        );

        EntidadeBase[] revistasDisponiveis = repositorioRevista.SelecionarRevistasDisponiveis();

        for (int i = 0; i < revistasDisponiveis.Length; i++)
        {
            Revista r = (Revista)revistasDisponiveis[i];

            if (r == null)
                continue;

            Console.WriteLine(
            "{0, -5} | {1, -20} | {2, -10} | {3, -20} | {4, -15} | {5, -15}",
                r.id, r.titulo, r.numeroEdicao, r.anoPublicacao, r.caixa.etiqueta, r.status
            );
        }

        Console.WriteLine();
    }

}
