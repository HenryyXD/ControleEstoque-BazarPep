using System;

namespace DTO {
    public class Venda {
        //public int idpv { get; set; } //id da tabela produtos_vendas
        public int id { get; set; } //id da tabela vendas
        public DateTime? dataVenda { get; set; }
        public double precoTotal { get; set; }
        public Produto[] produtosVendidos { get; set; }

        public Venda() {

        }

        public Venda(int size) {
            produtosVendidos = new Produto[size];
        }
    }
}
