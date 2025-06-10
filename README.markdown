# Clube Da Leitura

Um aplicativo de console em C# para gerenciar um Clube Da Leitura, permitindo o cadastro e controle de amigos, caixas, revistas e empréstimos.

## Descrição

O **Clube Da Leitura** é um sistema desenvolvido em C# que facilita a gestão de um Clube Da Leitura. Ele permite cadastrar amigos, caixas (contêineres para revistas), revistas e empréstimos, com regras de negócio específicas para garantir a integridade dos dados. O projeto segue o modelo de arquitetura em 3 camadas (Apresentação, Domínio e Dados) e implementa boas práticas de programação orientada a objetos, como encapsulamento, reutilização de código e tratamento de erros.

## Funcionalidades

### Módulo de Amigos
- Cadastro, edição, exclusão e visualização de amigos.
- Visualização dos empréstimos associados a um amigo.
- Validações: Nome e responsável (3 a 100 caracteres), telefone no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX, sem duplicatas de nome e telefone.
- Restrição: Não é possível excluir amigos com empréstimos vinculados.

### Módulo de Caixas
- Cadastro, edição, exclusão e visualização de caixas.
- Validações: Etiqueta única (máx. 50 caracteres), cor (paleta ou hexadecimal), dias de empréstimo (padrão 7).
- Restrição: Não é possível excluir caixas com revistas vinculadas.

### Módulo de Revistas
- Cadastro, edição, exclusão e visualização de revistas.
- Status: Disponível, Emprestada ou Reservada.
- Validações: Título (2 a 100 caracteres), número da edição (positivo), ano de publicação (válido), caixa obrigatória, sem duplicatas de título e edição.

### Módulo de Empréstimos
- Registro de empréstimos e devoluções.
- Visualização de empréstimos abertos e fechados.
- Validações: Cada amigo só pode ter um empréstimo ativo por vez; revistas devem estar disponíveis.
- Cálculo automático da data de devolução (data do empréstimo + dias da caixa).
- Destaque visual para empréstimos atrasados.

## Estrutura do Projeto
```
ClubeDaLeitura/
├── Dominio/
│   ├── Amigo.cs                # Classe para gerenciar amigos
│   ├── Caixa.cs                # Classe para gerenciar caixas
│   ├── Revista.cs              # Classe para gerenciar revistas
│   ├── Emprestimo.cs           # Classe para gerenciar empréstimos
├── Dados/
│   ├── RepositorioAmigo.cs     # Repositório para amigos
│   ├── RepositorioCaixa.cs     # Repositório para caixas
│   ├── RepositorioRevista.cs   # Repositório para revistas
│   ├── RepositorioEmprestimo.cs # Repositório para empréstimos
├── Apresentacao/
│   ├── TelaAmigo.cs            # Interface para amigos
│   ├── TelaCaixa.cs            # Interface para caixas
│   ├── TelaRevista.cs          # Interface para revistas
│   ├── TelaEmprestimo.cs       # Interface para empréstimos
├── Program.cs                  # Ponto de entrada do aplicativo
├── ClubeDaLeitura.csproj        # Arquivo de configuração do projeto
```

## Pré-requisitos
- .NET SDK 6.0 ou superior ([baixe aqui](https://dotnet.microsoft.com/download)).
- Um terminal para executar comandos.
- Opcional: Visual Studio ou outro IDE compatível com C#.

## Instalação
1. Clone o repositório do GitHub:
   ```bash
   git clone https://github.com/marcelopaes26/ClubeDaLeitura.git
   ```
2. Navegue até a pasta do projeto:
   ```bash
   cd ClubeDaLeitura
   ```
3. Restaure as dependências:
   ```bash
   dotnet restore
   ```

## Como Executar
1. Na pasta do projeto, execute:
   ```bash
   dotnet run
   ```
2. Siga as instruções no console para navegar pelos menus e gerenciar amigos, caixas, revistas e empréstimos.

## Exemplo de Uso
![](https://i.imgur.com/bZWIAxQ.gif)

## Tecnologias Utilizadas
[![My Skills](https://skillicons.dev/icons?i=cs,dotnet,git,github,vscode)](https://skillicons.dev)