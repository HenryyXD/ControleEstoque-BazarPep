using System;
using System.Data;
using DTO;
using DAL;

namespace BLL
{
    public class FornecedorBLL 
    {
        private readonly AcessoDados ad = new AcessoDados();
        public bool Insert(Fornecedor f) {
            return ad.Insert(f) > 0;
        }
        public bool Update(Fornecedor f) {
            return ad.Update(f) > 0;
        }
        public bool Delete(Fornecedor f) {
            return ad.Delete(f) > 0;
        }
        public DataTable Select() {
            return ad.SelectFornecedor();
        }

        public DataTable getFornecedoresNames() {
            return ad.getFornecedoresNames();
        }

        public DataTable Search(string pesquisa) {
            return ad.SearchFornecedores(pesquisa);
        }
        public UInt64 GetAutoIncrement() {
            return ad.GetAutoIncrement(1);
        }
    }
}
