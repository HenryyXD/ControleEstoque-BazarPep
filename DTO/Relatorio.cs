using System;

namespace DTO {
    public class Relatorio {
        public Relatorio(double faturamento, int qtdVenda, DateTime? filtroDataInicio, DateTime? filtroDataFim) {
            this.faturamento = faturamento;
            this.qtdVenda = qtdVenda;
            this.filtroDataInicio = filtroDataInicio;
            this.filtroDataFim = filtroDataFim;
        }

        public double faturamento { get; set; }
        public int qtdVenda { get; set; }
        public DateTime? filtroDataInicio { get; set; }
        public DateTime? filtroDataFim { get; set; }
        //public Venda[] vendas { get; set; }
    }
}
