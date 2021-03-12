using System;
using DTO;
using DAL;
using System.Data;

namespace BLL
{
    public class ProdutoBLL
    {
        private readonly AcessoDados ad = new AcessoDados();
        public bool Insert(Produto p) {
            return ad.Insert(p) > 0;
        }
        public bool Update(Produto p) {
            return ad.Update(p) > 0;
        }
        public bool Delete(Produto p) {
            return ad.Delete(p) > 0;
        }
        public DataTable Select() {
            return ad.SelectProduto();
        }

        public DataTable GetProdutosNames() {
            return ad.GetProdutosNames();
        }

        public DataTable Search(string pesquisa) {
            return ad.SearchProdutos(pesquisa);
        }
        public int getFk(int id) {
            return ad.GetProdutoFk(id);
        }
        public UInt64 GetAutoIncrement() {
            return ad.GetAutoIncrement(2);
        }
    }
}
