using System;

namespace DTO {
    public class Fornecedor
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string rua { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string cep { get; set; }
        public string bairro { get; set; }
        public string telefone { get; set; }

        public Fornecedor() {
        }
        //usado para operação de delete
        public Fornecedor(int id) {
            this.id = id;
        }

        //usado para operação de insert
        public Fornecedor(string nome, string rua, string numero, string complemento, string cep, string bairro, string telefone) {
            this.nome = nome ?? throw new ArgumentNullException(nameof(nome));
            this.rua = rua;
            this.numero = numero;
            this.complemento = complemento;
            this.cep = cep;
            this.bairro = bairro;
            this.telefone = telefone;
        }
        //usado para operação de update
        public Fornecedor(int id, string nome, string rua, string numero, string complemento, string cep, string bairro, string telefone) {
            this.id = id;
            this.nome = nome ?? throw new ArgumentNullException(nameof(nome));
            this.rua = rua;
            this.numero = numero;
            this.complemento = complemento;
            this.cep = cep;
            this.bairro = bairro;
            this.telefone = telefone;
        }

    }
}
