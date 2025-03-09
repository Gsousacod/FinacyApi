
// using ScottPlot;
// namespace FinacyApi.Services
// {
//     public class GenerateChart
//     {
//         public static GenerateChart(string caminhoImagem)
//         {
//             var plt = new ScottPlot.Plot(600,
//                                          400);
//             double[] valores = { 5000, -1500, 800 };
//             string[] categorias = { "Salário", "Aluguel", "Investimentos" };

//             var barras = plt.AddBar(valores);
//             barras.Labels = categorias;
//             barras.FillColor = Color.Blue;

//             plt.XTicks(categorias);
//             plt.Title("Resumo Financeiro");
//             plt.YLabel("Valor (R$)");
//             plt.SaveFig(caminhoImagem);
//         }
//     }
// }