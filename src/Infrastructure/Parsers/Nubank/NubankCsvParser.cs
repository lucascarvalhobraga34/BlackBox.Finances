using Application.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Infrastructure.Parsers.Nubank
{
    public class NubankCsvParser : IFaturaParser
    {
        private static readonly Regex RegexParcela =
         new Regex(@"(?<!\d)(?<parcela>\d{1,2})\s*\/\s*(?<total>\d{1,2})(?!\d)",
             RegexOptions.Compiled);

        public List<LancamentoFatura> Parse(Fatura fatura, string mesAno, Stream arquivo)
        {
            using var reader = new StreamReader(arquivo);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            });

            csv.Context.RegisterClassMap<NubankCsvMap>();

            var registros = csv.GetRecords<NubankCsvRow>();
            var lista = new List<LancamentoFatura>();

            foreach (var r in registros)
            {
                var (parcela, total) = ExtrairParcela(r.Title);

                lista.Add(new LancamentoFatura
                (
                    fatura,
                    DateTime.ParseExact(r.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    LimparDescricao(r.Title),
                    Math.Abs(r.Amount),
                    parcela ?? 0,
                    total ?? 0
                ));
            }

            return lista;
        }

        private static (int? parcela, int? total) ExtrairParcela(string descricao)
        {
            var match = RegexParcela.Match(descricao);
            if (!match.Success)
                return (null, null);

            return (
                int.Parse(match.Groups["parcela"].Value),
                int.Parse(match.Groups["total"].Value)
            );
        }

        private static string LimparDescricao(string descricao)
        {
            // Remove o trecho de parcela da descrição
            descricao = RegexParcela.Replace(descricao, "").Trim();

            // Remove sujeiras comuns
            descricao = Regex.Replace(descricao, @"\s{2,}", " ");

            return descricao;
        }

        private sealed class NubankCsvMap : ClassMap<NubankCsvRow>
        {
            public NubankCsvMap()
            {
                Map(m => m.Date).Name("date");
                Map(m => m.Title).Name("title");
                Map(m => m.Amount).Name("amount");
            }
        }

        private class NubankCsvRow
        {
            public string Date { get; set; } = string.Empty;
            public string Title { get; set; } = string.Empty;
            public decimal Amount { get; set; }
        }
    }
}
