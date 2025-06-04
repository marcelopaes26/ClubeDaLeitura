using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Text.RegularExpressions;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo;

public class Amigo : EntidadeBase
{
    public string nome { get; set; }
    public string nomeResponsavel { get; set; }
    public string telefone { get; set; }

    public Amigo(string nome, string nomeResponsavel, string telefone)
    {
        this.nome = nome;
        this.nomeResponsavel = nomeResponsavel;
        this.telefone = telefone;
    }

    public override void AtualizarRegistro(EntidadeBase registroAtualizado)
    {
        Amigo amigoAtualizado = (Amigo)registroAtualizado;

        this.nome = amigoAtualizado.nome;
        this.nomeResponsavel = amigoAtualizado.nomeResponsavel;
        this.telefone = amigoAtualizado.telefone;
    }

    public override string Validar()
    {
        string erros = string.Empty;

        if (nome.Length < 3 || nome.Length > 100)
            erros += "O campo \"Nome\" deve conter entre 3 e 100 caracteres.";

        if (nomeResponsavel.Length < 3 || nomeResponsavel.Length > 100)
            erros += "O campo \"Nome do Responsável\" deve conter entre 3 e 100 caracteres.";   

        if (!Regex.IsMatch(telefone, @"^\(?\d{2}\)?\s?(9\d{4}|\d{4})-?\d{4}$"))
            erros += "O campo \"Telefone\" deve seguir o padrão (DDD) 90000-0000.";

        return erros;
    }

}