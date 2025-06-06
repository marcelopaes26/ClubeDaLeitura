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
        this.status = "Aberto";

        CalcularDataDevolucao();
    }

    public override void AtualizarRegistro(EntidadeBase registroAtualizado)
    {
        Emprestimo emprestimoAtualizado = (Emprestimo)registroAtualizado;

        this.amigo = emprestimoAtualizado.amigo;
        this.revista = emprestimoAtualizado.revista;
        this.dataEmprestimo = emprestimoAtualizado.dataEmprestimo;
        this.dataDevolucao = emprestimoAtualizado.dataDevolucao;
        this.status = emprestimoAtualizado.status;
    }

    public override string Validar()
    {
        string erros = string.Empty;

        if (amigo == null)
            erros += "O campo \"Amigo\" é obrigatório.";

        if (revista == null)
            erros += "O campo \"Revista\" é obrigatório.";
            
        else if (revista.status != "Disponível")
            erros += "A revista deve estar disponível para empréstimo.";

        return erros;
    }

    public void CalcularDataDevolucao()
    {
        if (revista != null && revista.caixa != null)
            this.dataDevolucao = this.dataEmprestimo.AddDays(revista.caixa.diasDeEmprestimo);
    }

    public void RegistrarDevolucao()
    {
        this.status = "Concluído";
        this.revista.status = "Disponível";
    }
}
