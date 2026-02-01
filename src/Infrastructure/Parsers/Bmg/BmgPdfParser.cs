using Application.Interfaces;
using Domain.Entities;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace Infrastructure.Parsers.Bmg
{
    public class BmgPdfParser : IFaturaParser
    {
        private static readonly Regex RegexLancamento =
            new Regex(
                @"(?<data>\d{2}/\d{2})\s+(?<descricao>[A-Z0-9\s\.\-\/]{3,}?)(?:\s+Parc\.?\s*(?<parcela>\d+)\s*\/\s*(?<total>\d+))?\s+(?<valor>\d{1,3}(?:\.\d{3})*,\d{2})",
                RegexOptions.Compiled);

        public List<LancamentoFatura> Parse(Fatura fatura, string mesAno, Stream arquivo)
        {
            var lancamentos = new List<LancamentoFatura>();

            var texto = ExtractText(arquivo);

            foreach (Match match in RegexLancamento.Matches(texto))
            {
                var data = DateTime.ParseExact(
                    $"{match.Groups["data"].Value}/{2026}",
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);

                var valor = decimal.Parse(
                    match.Groups["valor"].Value,
                    new CultureInfo("pt-BR"));

                lancamentos.Add(new LancamentoFatura
                    (
                        fatura,
                        data,
                        NormalizarDescricao(match.Groups["descricao"].Value),
                        valor,
                       match.Groups["parcela"].Success
                       ? int.Parse(match.Groups["parcela"].Value)
                       : 0,
                           match.Groups["total"].Success
                       ? int.Parse(match.Groups["total"].Value)
                       : 0
                    ));
            }

            return lancamentos;
        }

        private static string ExtractText(Stream arquivo)
        {
            var sb = new StringBuilder();

            using var document = PdfDocument.Open(arquivo);

            foreach (Page page in document.GetPages())
            {
                var words = page.GetWords();

                foreach (var word in words)
                {
                    sb.Append(word.Text);
                    sb.Append(" ");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        private static string NormalizarDescricao(string descricao)
        {
            // Remove espaços excessivos e sujeira comum do PDF
            descricao = Regex.Replace(descricao, @"\s{2,}", " ");
            descricao = descricao.Replace(" SA", "")
                                 .Replace(" MO", "")
                                 .Replace(" OS", "")
                                 .Trim();

            return descricao;
        }
    }
}
