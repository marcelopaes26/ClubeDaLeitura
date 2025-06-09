using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista;

public class RepositorioRevista : RepositorioBase
{
    public Revista[] SelecionarRevistasDisponiveis()
    {
        int contadorRevistasDisponiveis = 0;

        for (int i = 0; i < registros.Length; i++)
        {
            Revista revistaAtual = (Revista)registros[i];

            if (revistaAtual == null)
                continue;

            if (revistaAtual.status == "Disponível")
                contadorRevistasDisponiveis++;
        }

        Revista[] revistasDisponiveis = new Revista[contadorRevistasDisponiveis];

        int contadorAuxiliar = 0;

        for (int i = 0; i < registros.Length; i++)
        {
            Revista revistaAtual = (Revista)registros[i];

            if (revistaAtual == null)
                continue;

            if (revistaAtual.status == "Disponível")
            {
                revistasDisponiveis[contadorAuxiliar++] = (Revista)registros[i];
            }
        }

        return revistasDisponiveis;
    }
}