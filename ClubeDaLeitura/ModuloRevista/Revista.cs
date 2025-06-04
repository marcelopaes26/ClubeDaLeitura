using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista;

public class Revista : EntidadeBase
{
    public string titulo { get; set; }
    public int numeroEdicao { get; set; }
    public int anoPublicacao { get; set; }
    public Caixa caixa { get; set; }
    public string status { get; set; }

    public Revista(string titulo, int numeroEdicao, int anoPublicacao, Caixa caixa)
    {
        this.titulo = titulo;
        this.numeroEdicao = numeroEdicao;
        this.anoPublicacao = anoPublicacao;
        this.caixa = caixa;
        this.status = "Disponível";
    }

    public override void AtualizarRegistro(EntidadeBase registroAtualizado)
    {
        Revista revistaAtualizada = (Revista)registroAtualizado;

        this.titulo = revistaAtualizada.titulo;
        this.numeroEdicao = revistaAtualizada.numeroEdicao;
        this.anoPublicacao = revistaAtualizada.anoPublicacao;
        this.caixa = revistaAtualizada.caixa;
    }

    public override string Validar()
    {
        string erros = string.Empty;

        if (titulo.Length < 2 || titulo.Length > 100)
            erros += "O campo \"Título\" deve conter entre 2 e 100 caracteres.";

        if (numeroEdicao < 1)
            erros += "O campo \"Número da Edição\" deve conter um valor maior que 0.";

        if (anoPublicacao < DateTime.MinValue.Year || anoPublicacao > DateTime.Now.Year)
            erros += "O campo \"Ano de Publicação\" deve conter um valor válido no passado ou presente.";

        if (caixa == null)
            erros += "O campo \"Caixa\" é obrigatório.";

        return erros;
    }
}