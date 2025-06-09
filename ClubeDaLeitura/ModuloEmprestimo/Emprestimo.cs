using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

public class Emprestimo : EntidadeBase
{
    public Amigo amigo { get; set; }
    public Revista revista { get; set; }
    public DateTime dataEmprestimo { get; set; }
    public DateTime dataDevolucao { get; set; }
    public string status { get; set; }

    public Emprestimo(Amigo amigo, Revista revista)
    {
        this.amigo = amigo;
        this.revista = revista;
        this.dataEmprestimo = DateTime.Now;
        this.dataDevolucao = dataEmprestimo.AddDays(revista.caixa.diasDeEmprestimo);
        this.status = "Aberto";
    }

    public override void AtualizarRegistro(EntidadeBase registroAtualizado)
    {
        this.status = "Concluído";
    }

    public override string Validar()
    {
        string erros = string.Empty;

        if (amigo == null)
            erros += "O campo \"Amigo\" é obrigatório.";

        if (revista == null)
            erros += "O campo \"Revista\" é obrigatório.";

        return erros;
    }
    
}
