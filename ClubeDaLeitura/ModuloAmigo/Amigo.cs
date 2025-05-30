using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo;

public class Amigo : EntidadeBase
{
    public string nome;
    public string nomeResponsavel;
    public string telefone;

    public Amigo(string nome, string nomeResponsavel, string telefone)
    {
        this.nome = nome;
        this.nomeResponsavel = nomeResponsavel;
        this.telefone = telefone;
    }

    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(nome))
            erros += "O nome é obrigatório!\n";

        else if (nome.Length < 3 || nome.Length > 100)
            erros += "O nome deve conter entre 3 e 100 caracteres!\n";

        if (nomeResponsavel.Length < 3 || nomeResponsavel.Length > 100)
            erros += "O nome do responsável deve conter entre 3 e 100 caracteres!\n";

        if (string.IsNullOrWhiteSpace(telefone))
            erros += "O telefone é obrigatório!\n";

        else if (telefone.Length < 9)
            erros += "O telefone deve conter no mínimo 9 caracteres!\n";

        return erros;
    }

    public override void AtualizarRegistro(EntidadeBase registroAtualizado)
    {
        Amigo amigoAtualizado = (Amigo)registroAtualizado;

        this.nome = amigoAtualizado.nome;
        this.nomeResponsavel = amigoAtualizado.nomeResponsavel;
        this.telefone = amigoAtualizado.telefone;
    }
}