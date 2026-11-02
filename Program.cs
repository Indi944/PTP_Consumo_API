using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Consumo_API_CSharp;

/// <summary>
/// Representa uma citação retornada pela API.
/// </summary>
public class Quote
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("quote")]
    public string Texto { get; set; } = string.Empty;

    [JsonPropertyName("author")]
    public string Autor { get; set; } = string.Empty;
}

/// <summary>
/// Representa a resposta completa da API, com a lista de citações
/// e os metadados de paginação.
/// </summary>
public class QuoteResponse
{
    [JsonPropertyName("quotes")]
    public List<Quote> Quotes { get; set; } = new();

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("skip")]
    public int Skip { get; set; }

    [JsonPropertyName("limit")]
    public int Limit { get; set; }
}

public static class Program
{
    private const string ApiUrl = "https://dummyjson.com/quotes";
    private const int QuantidadeAMostrar = 10;

    public static async Task Main()
    {
        Console.WriteLine("==============================================");
        Console.WriteLine("   Consumo de API - Citações (dummyjson.com)");
        Console.WriteLine("==============================================");
        Console.WriteLine();

        try
        {
            using HttpClient httpClient = new();

            Console.WriteLine($"A consultar a API: {ApiUrl} ...");
            string json = await httpClient.GetStringAsync(ApiUrl);

            QuoteResponse? resposta = JsonSerializer.Deserialize<QuoteResponse>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (resposta?.Quotes is null || resposta.Quotes.Count == 0)
            {
                Console.WriteLine("A API não devolveu nenhuma citação.");
                return;
            }

            ExibirCitações(resposta);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine();
            Console.WriteLine("[ERRO DE REDE] Não foi possível contactar a API.");
            Console.WriteLine($"Detalhes: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine();
            Console.WriteLine("[ERRO DE JSON] A resposta da API não pôde ser convertida.");
            Console.WriteLine($"Detalhes: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine("[ERRO INESPERADO] Ocorreu um erro ao processar o pedido.");
            Console.WriteLine($"Detalhes: {ex.Message}");
        }
    }

    private static void ExibirCitações(QuoteResponse resposta)
    {
        List<Quote> primeiras = resposta.Quotes.Take(QuantidadeAMostrar).ToList();

        Console.WriteLine();
        Console.WriteLine($"Total de citações disponíveis: {resposta.Total}");
        Console.WriteLine($"A mostrar as {primeiras.Count} primeiras citações:");
        Console.WriteLine();

        for (int i = 0; i < primeiras.Count; i++)
        {
            Quote q = primeiras[i];

            Console.WriteLine($"--- Citação #{i + 1} (ID: {q.Id}) ---");
            Console.WriteLine($"Texto : {q.Texto}");
            Console.WriteLine($"Autor : {q.Autor}");
            Console.WriteLine();
        }

        Console.WriteLine("==============================================");
        Console.WriteLine(" Concluído com sucesso!");
        Console.WriteLine("==============================================");
    }
}
