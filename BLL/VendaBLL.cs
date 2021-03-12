using System;
using DTO;
using DAL;
using System.Data;

namespace BLL
{
    public class VendaBLL
    {
        private readonly AcessoDados ad = new AcessoDados();
        public int Insert(Venda v) {
            return ad.Insert(v);
        }
        /*public bool Update(Venda v) {
            return ad.Update(v) > 0;
        }*/
        public bool Delete(Venda v) {
            return ad.Delete(v) > 0;
        }
        public UInt64 GetAutoIncrement() {
            return ad.GetAutoIncrement(3);
        }

        public DataTable Select() {
            return ad.SelectVenda();
        }

        public DataTable Search(string pesquisa) {
            return ad.SearchVendas(pesquisa);
        }

        public DataTable SelectItemVenda(int id) {
            return ad.SelectItemVenda(id);
        }

        public DataTable SelectBetweenDate(DateTime di, DateTime df) {
            return ad.SelectVendaBetweenDate(di, df);
        }
    }
}
