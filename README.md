# Consumo_API_CSharp

Aplicação de consola em C# (.NET) que consome a API pública
[https://dummyjson.com/quotes](https://dummyjson.com/quotes) e apresenta
na consola as 10 primeiras citações.

## O que o projeto faz

- Faz um pedido **GET** com `HttpClient` para `https://dummyjson.com/quotes`
- Converte o JSON para classes C# com **System.Text.Json**
  - `Quote` → `Id`, `Texto` e `Autor`
  - `QuoteResponse` → `Quotes` (lista), `Total`, `Skip` e `Limit`
- Mostra as **10 primeiras citações** de forma organizada
- Trata erros com `try-catch` para:
  - `HttpRequestException` → falha de rede / API indisponível
  - `JsonException` → resposta inválida ou malformada
  - `Exception` → qualquer outro erro inesperado

Todas as mensagens de erro são apresentadas em português.

## Como executar

Pré-requisito: [.NET SDK](https://dotnet.microsoft.com/download) 8.0 ou superior.

```bash
dotnet run
```

## Estrutura do projeto

```
Consumo_API_CSharp/
├── Program.cs                     # Código principal (modelos + lógica)
├── Consumo_API_CSharp.csproj      # Definição do projeto
└── README.md
```

## Licença

Projeto de demonstração — livre para uso e modificação.
