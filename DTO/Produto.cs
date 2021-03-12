using System;

namespace DTO {
    public class Produto {
        public int id { get; set; }
        public Fornecedor fornecedor { get; set; }
        public string nome { get; set; }
        public double preco { get; set; }
        public int qtd { get; set; }
        public DateTime ? dataAquisicao { get; set; }
        public string descricao { get; set; }

        public Produto() {
        }

        //usado para operação de delete
        public Produto(int id) {
            this.id = id;
        }

        //usado para operação de insert
        public Produto(string nome, Fornecedor fornecedor, double preco, int qtd, DateTime dataAquisicao, string descricao) {
            this.nome = nome ?? throw new ArgumentNullException(nameof(nome));
            this.descricao = descricao;
            this.preco = preco;
            this.qtd = qtd;
            this.dataAquisicao = dataAquisicao;
            this.fornecedor = fornecedor ?? throw new ArgumentNullException(nameof(fornecedor));
        }
        //usado para operação de update
        public Produto(int id, string nome, Fornecedor fornecedor, double preco, int qtd, DateTime dataAquisicao, string descricao) {
            this.id = id;
            this.nome = nome ?? throw new ArgumentNullException(nameof(nome));
            this.descricao = descricao;
            this.preco = preco;
            this.qtd = qtd;
            this.dataAquisicao = dataAquisicao;
            this.fornecedor = fornecedor ?? throw new ArgumentNullException(nameof(fornecedor));
        }        
    }
}
