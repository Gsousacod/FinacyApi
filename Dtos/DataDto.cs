public class ReportDto
{
    public List<ExpenseDto> Expenses { get; set; }
    public List<RevenueDto> Revenues { get; set; }
    public List<MetaFinancialDto> MetaFinancial { get; set; }

}

public class ExpenseDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime ExpenseDate { get; set; }
    public decimal Value { get; set; }

    public string Category { get; set; }
}

public class RevenueDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal value { get; set; }
    public DateTime ReceiptDate { get; set; }
}

public class MetaFinancialDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal TotalValue { get; set; }
}
