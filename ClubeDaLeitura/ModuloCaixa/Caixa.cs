using ClubeDaLeitura.ConsoleApp.Compartilhado;  

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa;

public class Caixa : EntidadeBase
{
    public string etiqueta { get; set; }
    public string cor { get; set; }
    public int diasDeEmprestimo { get; set; }

    public Caixa(string etiqueta, string cor)
    {
        this.etiqueta = etiqueta;
        this.cor = cor;
        this.diasDeEmprestimo = 7;
    }

    public Caixa(string etiqueta, string cor, int diasDeEmprestimo)
    {
        this.etiqueta = etiqueta;
        this.cor = cor;
        this.diasDeEmprestimo = diasDeEmprestimo;
    }

    public override void AtualizarRegistro(EntidadeBase registroAtualizado)
    {
        Caixa caixaAtualizada = (Caixa)registroAtualizado;

        this.etiqueta = caixaAtualizada.etiqueta;
        this.cor = caixaAtualizada.cor;
        this.diasDeEmprestimo = caixaAtualizada.diasDeEmprestimo;
    }

    public override string Validar()
    {
        string erros = string.Empty;

        if (string.IsNullOrWhiteSpace(etiqueta) || etiqueta.Length > 50)
            erros += "O campo \"Etiqueta\" é obrigatório e recebe no máximo 50 caracteres.";

        if (string.IsNullOrWhiteSpace(cor))
            erros += "O campo \"Cor\" é obrigatório.";

        if (diasDeEmprestimo < 1)
            erros += "O campo \"Dias de Empréstimo\" deve conter um valor maior que 0.";

        return erros;
    }
}