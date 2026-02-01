namespace Application.DTOs
{
    public class LancamentoDto
    {
        public DateTime DataLancamento { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int? NumeroParcela { get; set; }
        public int? TotalParcelas { get; set; }
    }
}
