using FinacyApi.Model;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


public class InvoiceDocument : IDocument
{
    private readonly ReportDto _report;

    public InvoiceDocument(ReportDto report)
    {
        _report = report;
    }

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(50);
            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
            page.Footer().Element(ComposeFooter);
        });
    }

    private void ComposeHeader(IContainer container)
    {
        container.Row(row=>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Text("Relatório Financeiro").FontSize(20).SemiBold();
                column.Item().Text(text=>{
                    text.Span("Nome:").SemiBold().FontSize(16);
                    text.Span($" {_report.User.Name}").FontSize(16);
                });
                 row.ConstantItem(100).Height(60).Image("/home/gabriel/Documentos/C#/FinacyApi/assets/img/logo2.png");

            });

        });
    }
    private void ComposeContent(IContainer container)
    {
        container.PaddingVertical(40).Column(col =>
        {
            col.Spacing(10);

            col.Item().Text("Despesa:").FontSize(16).SemiBold();
            var revenues = _report.Revenues.Select(dto => new Revenue
            {
                Description = dto.Description,
                value = dto.value,
                ReceiptDate = dto.ReceiptDate
            }).ToList();

            col.Item().Element(c => ComposeTableRevenue(c, revenues)); 

            col.Spacing(15);

            col.Item().Text("Receita:").FontSize(16).SemiBold();
            var expenses = _report.Expenses.Select(dto => new Expense
            {
                Description = dto.Description,
                value = dto.Value,
                ExpenseDate = dto.ExpenseDate
            }).ToList();
             col.Item().Element(c => ComposeTableExpense(c, expenses));

             col.Spacing(15);

            col.Item().Text("Metas Financeiras:").FontSize(16).SemiBold();
            
        });
    }

    private void ComposeTableRevenue(IContainer container, List<Revenue> revenues)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(25); 
                columns.RelativeColumn(3);  
                columns.RelativeColumn();   
                columns.RelativeColumn();   
            });
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("#").SemiBold();
                header.Cell().Element(CellStyle).Text("Descrição").SemiBold();
                header.Cell().Element(CellStyle).Text("Data").SemiBold();
                header.Cell().Element(CellStyle).Text("Valor").SemiBold();
            });
            
            if (revenues == null || !revenues.Any())
            {
                table.Cell().ColumnSpan(4).AlignCenter().Text("Nenhuma receita disponível").Italic();
                return;
            }

            int index = 1;
            foreach (var revenue in revenues) 
            {
                table.Cell().Element(CellStyle).Text(index++.ToString());
                table.Cell().Element(CellStyle).Text(revenue.Description ?? "Sem descrição");
                table.Cell().Element(CellStyle).Text(revenue.ReceiptDate != DateTime.MinValue 
                    ? revenue.ReceiptDate.ToShortDateString() 
                    : "-");
                table.Cell().Element(CellStyle).Text(revenue.value.ToString("C"));
            }
        });
    }

    private void ComposeTableExpense(IContainer container, List<Expense> expenses) 
    {
        container.Table(table =>
        {
            
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(25); 
                columns.RelativeColumn(3);  
                columns.RelativeColumn();   
                columns.RelativeColumn();   
            });

            
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("#").SemiBold();
                header.Cell().Element(CellStyle).Text("Descrição").SemiBold();
                header.Cell().Element(CellStyle).Text("Data").SemiBold();
                header.Cell().Element(CellStyle).Text("Valor").SemiBold();
            });

            
            if (expenses == null || !expenses.Any())
            {
                table.Cell().ColumnSpan(4).AlignCenter().Text("Nenhuma receita disponível").Italic();
                return;
            }

        
            int index = 1;
            foreach (var expense in expenses) 
            {
                table.Cell().Element(CellStyle).Text(index++.ToString());
                table.Cell().Element(CellStyle).Text(expense.Description ?? "Sem descrição");
                table.Cell().Element(CellStyle).Text(expense.ExpenseDate != DateTime.MinValue 
                    ? expense.ExpenseDate.ToShortDateString() 
                    : "-");
                table.Cell().Element(CellStyle).Text(expense.value.ToString("C"));
            }
        });
    }


    private static IContainer CellStyle(IContainer container)
    {
        return container
           .BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
    }


 
    private void ComposeFooter(IContainer container)
    {
        container.AlignRight().Text($"Gerado em: {DateTime.Now.ToShortDateString()}")
            .FontSize(10);
    }
}
