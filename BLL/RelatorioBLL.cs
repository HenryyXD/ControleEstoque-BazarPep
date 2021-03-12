using System;
using DTO;
using DAL;

namespace BLL {
    public class RelatorioBLL
    {
        public Relatorio gerarNovo(DateTime inicio, DateTime fim) {
            return new AcessoDados().gerarNovo(inicio, fim); 
        }

    }
}
