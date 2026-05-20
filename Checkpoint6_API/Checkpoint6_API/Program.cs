using Microsoft.ML;
using Microsoft.ML.Data;

var builder = WebApplication.CreateBuilder(args);

// --- LINHAS QUE ATIVAM O SWAGGER DO VISUAL STUDIO ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- CONFIGURA O SWAGGER PARA APARECER NA TELA ---
app.UseSwagger();
app.UseSwaggerUI();

// Endpoint para treinar e testar a API.
app.MapGet("/treinar", (double testFraction = 0.3) => {
    var mlContext = new MLContext(seed: 0);
    string dataPath = Path.Combine(Environment.CurrentDirectory, "DATA", "dados.csv");

    // Carrega os dados da pasta DATA
    var dataView = mlContext.Data.LoadFromTextFile<HouseData>(dataPath, separatorChar: ',', hasHeader: true);

    // Divide os dados (Treino/Teste)
    var splitData = mlContext.Data.TrainTestSplit(dataView, testFraction: testFraction);

    // Configura o Pipeline usando FastTree
    var pipeline = mlContext.Transforms.Concatenate("Features", new[] { "Tamanho", "Quartos" })
        .Append(mlContext.Regression.Trainers.FastTree(labelColumnName: "Preco", featureColumnName: "Features"));

    // Treina o modelo
    var model = pipeline.Fit(splitData.TrainSet);

    // Faz as previsões no conjunto de testes
    var predictions = model.Transform(splitData.TestSet);
    var metrics = mlContext.Regression.Evaluate(predictions, labelColumnName: "Preco");

    // Retorna as métricas para você comparar no vídeo
    return Results.Ok(new
    {
        Relacao = testFraction == 0.3 ? "70/30 (Treino/Teste)" : "60/40 (Treino/Teste)",
        RSquared = metrics.RSquared,
        RootMeanSquaredError = metrics.RootMeanSquaredError
    });
})
.WithName("GetTreinar")
.WithOpenApi();

app.Run();

// Classes de mapeamento do CSV
public class HouseData
{
    [LoadColumn(0)] public float Tamanho { get; set; }
    [LoadColumn(1)] public float Quartos { get; set; }
    [LoadColumn(2)] public float Preco { get; set; }
}