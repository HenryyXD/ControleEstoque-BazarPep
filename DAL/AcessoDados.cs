using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using DTO;
using Exceptions;

namespace DAL {
    public class AcessoDados {
        private string strCon;
        private MySqlConnection con = null;

        //Pega a string de conexão em app.config e inicializa a classe
        public AcessoDados() {
            strCon = ConfigurationManager.AppSettings["ConexaoBD"];
        }

        //Abre a conexao e retorna
        public void AbrirConexao() {
            try {
                con = new MySqlConnection(strCon);
                con.Open();
            } catch(Exception ex) {
                throw new DbConnectionException(ex.Message, ex);
            }
        }

        #region Funções Fornecedores

        //Insere um novo fornecedor
        public int Insert(Fornecedor f) {
            try {
                AbrirConexao();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO fornecedores (nome,telefone,rua,numero,complemento,cep,bairro) VALUES (?nome,?tel,?rua,?num,?comp,?cep,?bairro)";
                CmdParametersAddFornecedor(ref cmd, ref f);
                return cmd.ExecuteNonQuery();
            } finally {
                con.Close();
            }
        }

        private void CmdParametersAddFornecedor(ref MySqlCommand cmd, ref Fornecedor f) {
            cmd.Parameters.AddWithValue("?nome", f.nome);
            cmd.Parameters.AddWithValue("?tel", f.telefone);
            cmd.Parameters.AddWithValue("?rua", f.rua);
            cmd.Parameters.AddWithValue("?num", f.numero);
            cmd.Parameters.AddWithValue("?comp", f.complemento);
            cmd.Parameters.AddWithValue("?cep", f.cep);
            cmd.Parameters.AddWithValue("?bairro", f.bairro);
        }

        //Atualiza um fornecedor pelo id
        public int Update(Fornecedor f) {
            try {
                AbrirConexao();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText =
                    "UPDATE fornecedores SET nome=?nome, telefone=?tel, rua=?rua, numero=?num, complemento=?comp, cep=?cep, bairro=?bairro where idFornec=?id";
                CmdParametersAddFornecedor(ref cmd, ref f);
                cmd.Parameters.AddWithValue("?id", f.id);
                return cmd.ExecuteNonQuery();
            } finally {
                con.Close();
            }
        }

        //Apaga um fornecedor pelo id
        public int Delete(Fornecedor f) {
            try {
                AbrirConexao();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText =
                    "DELETE FROM fornecedores WHERE idFornec = ?id";
                cmd.Parameters.AddWithValue("?id", f.id);
                return cmd.ExecuteNonQuery();
            } finally {
                con.Close();
            }
        }

        public DataTable SelectFornecedor() {
            try {
                AbrirConexao();
                MySqlCommand cmd = new MySqlCommand
                    ("select idFornec as ID, f.Nome, " +
                    "(select count(fk_idFornec) from produtos where f.idFornec = fk_idFornec) as `Quantidade de Produtos`, " +
                    "Telefone, Rua, Numero, Complemento, CEP, Bairro FROM fornecedores f;", con);
                DataTable dt = new DataTable();
                using(MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                    da.Fill(dt);
                }
                return dt;
            } finally {
                con.Close();
            }
        }

        public DataTable SearchFornecedores(string pesquisa) {
            try {
                AbrirConexao();
                MySqlCommand cmd = new MySqlCommand(
                    "select * from fornecedores where " +
                    "idFornec like ?str " +
                    "or nome like ?str " +
                    "or telefone like ?str " +
                    "or rua like ?str " +
                    "or numero like ?str " +
                    "or complemento like ?str " +
                    "or cep like ?str " +
                    "or bairro like ?str"
               , con);

                cmd.Parameters.AddWithValue("?str", "%" + pesquisa + "%");
                DataTable dt = new DataTable();
                using(MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                    da.Fill(dt);
                }
                return dt;
            } finally {
                con.Close();
            }
        }

        public DataTable getFornecedoresNames() {
            try {
                AbrirConexao();
                MySqlCommand cmd = new MySqlCommand
                    ("SELECT idFornec, Nome FROM fornecedores order by nome",
                    con);
                DataTable dt = new DataTable();
                using(MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                    da.Fill(dt);
                }
                return dt;
            } finally {
                con.Close();
            }
        }
        #endregion

        #region Funções Produtos

        public int Insert(Produto p) {
            try {
                AbrirConexao();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO produtos (fk_idFornec,nome,preco,qtd,data_aquisicao,descricao) VALUES (?fk,?nome,?preco,?qtd,?data,?desc)";
                CmdParametersAddProduto(ref cmd, ref p);
                return cmd.ExecuteNonQuery();
            } finally {
                con.Close();
            }
        }

        private void CmdParametersAddProduto(ref MySqlCommand cmd, ref Produto p) {
            cmd.Parameters.AddWithValue("?fk", p.fornecedor.id);
            cmd.Parameters.AddWithValue("?nome", p.nome);
            cmd.Parameters.AddWithValue("?preco", p.preco);
            cmd.Parameters.AddWithValue("?qtd", p.qtd);
            cmd.Parameters.AddWithValue("?data", p.dataAquisicao);
            cmd.Parameters.AddWithValue("?desc", p.descricao);
        }

        public int Update(Produto p) {
            try {
                AbrirConexao();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE produtos SET fk_idFornec=?fk,nome=?nome," +
                    "preco=?preco,qtd=?qtd,data_aquisicao=?data,descricao=?desc " +
                    "WHERE idProd = ?id";
                CmdParametersAddProduto(ref cmd, ref p);
                cmd.Parameters.AddWithValue("?id", p.id);
                return cmd.ExecuteNonQuery();
            } finally {
                con.Close();
            }
        }

        public int Delete(Produto p) {
            try {
                AbrirConexao();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM produtos WHERE idProd = ?id";
                cmd.Parameters.AddWithValue("?id", p.id);
                return cmd.ExecuteNonQuery();
            } finally {
                con.Close();
            }
        }

        public DataTable SelectProduto() {
            try {
                AbrirConexao();
                MySqlCommand cmd = new MySqlCommand
                    ("select idProd, " +
                    "fornecedores.Nome as fornecedor, " +
                    "Produtos.Nome, " +
                    "Preco, qtd, data_aquisicao, descricao from produtos " +
                    "inner join fornecedores on fornecedores.idFornec = fk_idFornec", con);
                DataTable dt = new DataTable();
                using(MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                    da.Fill(dt);
                }
                return dt;
            } finally {
                con.Close();
            }
        }

        public DataTable SearchProdutos(string pesquisa) {
            try {
                AbrirConexao();
                MySqlCommand cmd = new MySqlCommand(
                    "select * from (select idProd, " +
                    "fornecedores.Nome as fornecedor, " +
                    "Produtos.Nome, " +
                    "Preco, qtd, data_aquisicao, descricao from produtos " +
                    "inner join fornecedores on fornecedores.idFornec = fk_idFornec) as sub where " +
                    "idProd like ?str " +
                    "or fornecedor like ?str " +
                    "or nome like ?str " +
                    "or preco like ?str " +
                    "or qtd like ?str " +
                    "or date_format(data_aquisicao, '%d/%m/%Y') like ?str " +
                    "or descricao like ?str"
               , con);
                cmd.Parameters.AddWithValue("?str", "%" + pesquisa + "%");
                DataTable dt = new DataTable();
                using(MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                    da.Fill(dt);
                }
                return dt;
            } finally {
                con.Close();
            }
        }

        public int GetProdutoFk(int id) {
            try {
                AbrirConexao();
                MySqlCommand cmd = new MySqlCommand(
                    "select fk_idFornec from produtos where idProd = ?id", con);
               
                cmd.Parameters.AddWithValue("?id", id);
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                return dr.GetInt32(0);
            } finally {
                con.Close();
            }
        }

        public DataTable GetProdutosNames() {
            try {
                AbrirConexao();
                MySqlCommand cmd = new MySqlCommand
                    ("select idProd, nome, preco, qtd from produtos order by nome",
                    con);
                DataTable dt = new DataTable();
                using(MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                    da.Fill(dt);
                }
                return dt;
            } finally {
                con.Close();
            }
        }

        public int GetQtdProdutoById(int id) {
            try {
                AbrirConexao();
                MySqlCommand cmd = new MySqlCommand
                    ("select qtd from produtos where idProd = ?id",
                    con);
                cmd.Parameters.AddWithValue("?id", id);
                MySqlDataReader dr = cmd.ExecuteReader();    
                dr.Read();
                if(dr.HasRows)
                    return dr.GetInt32(0);
                else
                    return 0;
            } finally {
                con.Close();
            }
        }

        #endregion

        #region Funções Vendas
        public int Insert(Venda v) {
            MySqlTransaction t = null;
            try {
                int rows = 0;
                AbrirConexao();
                MySqlCommand cmd = con.CreateCommand();
                cmd.Transaction = t = con.BeginTransaction();

                cmd.CommandText = "INSERT INTO vendas (dataVenda,precoTotal) VALUES (?dataVenda,?precoTotal)";
                cmd.Parameters.AddWithValue("?dataVenda", v.dataVenda);
                cmd.Parameters.AddWithValue("?precoTotal", v.precoTotal);
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                        "INSERT INTO produtos_vendas (fk_idVenda,fk_idProd,qtd,precoUni,nomeProd) " +
                        "VALUES (?fkVenda,?fkProd,?qtd,?precoUni,?nomeProd)";

                for(int i = 0; i < v.produtosVendidos.Length; i++) {
                    cmd.Parameters.Clear();
                    CmdParametersAddVenda(ref cmd, ref v, i);
                    rows += cmd.ExecuteNonQuery();
                }

                cmd.CommandText = "UPDATE produtos SET qtd=?qtd WHERE idProd = ?id";

                for(int i = 0; i < v.produtosVendidos.Length; i++) {
                    cmd.Parameters.Clear();
                    int qtd = GetQtdProdutoById(v.produtosVendidos[i].id) - v.produtosVendidos[i].qtd;
                    cmd.Parameters.AddWithValue("?qtd", qtd);
                    cmd.Parameters.AddWithValue("?id", v.produtosVendidos[i].id);
                    cmd.ExecuteNonQuery();
                }

                t.Commit();
                return rows;
            } catch(Exception) {
                t.Rollback();
                throw;
            } finally {
                con.Close();
            }
        }

        private void CmdParametersAddVenda(ref MySqlCommand cmd, ref Venda v, int i) {
            cmd.Parameters.AddWithValue("?fkVenda", v.id);
            cmd.Parameters.AddWithValue("?fkProd", v.produtosVendidos[i].id);
            cmd.Parameters.AddWithValue("?qtd", v.produtosVendidos[i].qtd);
            cmd.Parameters.AddWithValue("?precoUni", v.produtosVendidos[i].preco);
            cmd.Parameters.AddWithValue("?nomeProd", v.produtosVendidos[i].nome);
        }

        /*public int Update(Venda v) {
            MySqlTransaction t = null;
            try {
                int rows = 0;
                AbrirConexao();
                MySqlCommand cmd = con.CreateCommand();
                cmd.Transaction = t = con.BeginTransaction();

                cmd.CommandText = "UPDATE vendas SET dataVenda=?dataVenda,precoTotal=?precoTotal WHERE idVenda = ?idVenda";
                cmd.Parameters.AddWithValue("?dataVenda", v.dataVenda);
                cmd.Parameters.AddWithValue("?precoTotal", v.precoTotal);
                cmd.Parameters.AddWithValue("?idVenda", v.id);
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                        "UPDATE produtos_vendas SET fk_idVenda=?fkVenda,fk_idProd=?fkProd,qtd=?qtd,precoUni=?precoUni,nomeProd=?nomeProd WHERE idpv=?idpv";

                for(int i = 0; i < v.produtosVendidos.Length; i++) {
                    cmd.Parameters.Clear();
                    CmdParametersAddVenda(ref cmd, ref v, i);
                    cmd.Parameters.AddWithValue("?idpv", v.idpv);
                    rows += cmd.ExecuteNonQuery();
                }

                t.Commit();
                return rows;
            } catch(Exception) {
                t.Rollback();
                throw;
            } finally {
                con.Close();
            }
        }*/

        public int Delete(Venda v) {
            MySqlTransaction t = null;
            try {
                AbrirConexao();
                MySqlCommand cmd = con.CreateCommand();
                cmd.Transaction = t = con.BeginTransaction();

                cmd.CommandText = "UPDATE produtos SET qtd=?qtd WHERE idProd = ?id";

                for(int i = 0; i < v.produtosVendidos.Length; i++) {
                    int qtd = GetQtdProdutoById(v.produtosVendidos[i].id) + v.produtosVendidos[i].qtd;
                    cmd.Parameters.AddWithValue("?qtd", qtd);
                    cmd.Parameters.AddWithValue("?id", v.produtosVendidos[i].id);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }


                cmd.CommandText = "DELETE FROM vendas WHERE idVenda = ?id";
                cmd.Parameters.AddWithValue("?id", v.id);
                int row = cmd.ExecuteNonQuery();

                t.Commit();
                return row;
            } catch(Exception) {
                t.Rollback();
                throw;
            } finally {
                con.Close();
            }
        }

        public DataTable SelectVenda() {
            try {
                AbrirConexao();
                MySqlCommand cmd = new MySqlCommand
                    ("select * from vendas ", con);
                DataTable dt = new DataTable();
                using(MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                    da.Fill(dt);
                }
                return dt;
            } finally {
                con.Close();
            }
        }

        public DataTable SelectVendaBetweenDate(DateTime di, DateTime df) {
            try {
                AbrirConexao();
                MySqlCommand cmd = new MySqlCommand
                    ("select * from vendas where dataVenda >= ?di - interval 1 day and dataVenda <= ?df + interval 1 day", con);
                cmd.Parameters.AddWithValue("?di", di);
                cmd.Parameters.AddWithValue("?df", df);

                DataTable dt = new DataTable();
                using(MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                    da.Fill(dt);
                }
                return dt;
            } finally {
                con.Close();
            }
        }

        public DataTable SearchVendas(string pesquisa) {
            try {
                AbrirConexao();
                MySqlCommand cmd = new MySqlCommand(
                    "select * from vendas where idVenda IN " +
                    "(select fk_idVenda from vendas " +
                    "left join produtos_vendas on produtos_vendas.fk_idVenda = vendas.idVenda " +
                    "where fk_idVenda like ?str " +
                    "or fk_idProd like ?str " +
                    "or qtd like ?str " +
                    "or precoUni like ?str " +
                    "or nomeProd like ?str " +
                    "or date_format(dataVenda, '%d/%m/%Y %H:%i:%s') like ?str " +
                    "or precoTotal like ?str " +
                    "group by fk_idVenda)", con);

                cmd.Parameters.AddWithValue("?str", "%" + pesquisa + "%");
                DataTable dt = new DataTable();
                using(MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                    da.Fill(dt);
                }
                return dt;
            } finally {
                con.Close();
            }
        }

        public DataTable SelectItemVenda(int id) {
            try {
                AbrirConexao();
                MySqlCommand cmd = new MySqlCommand(
                    "select fk_idProd as ID, nomeProd as Nome,qtd as Quantidade, precoUni from produtos_vendas " +
                    "where fk_idVenda = ?id", con);

                cmd.Parameters.AddWithValue("?id", id);
                DataTable dt = new DataTable();
                using(MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                    da.Fill(dt);
                }
                return dt;
            } finally {
                con.Close();
            }
        }
         
        #endregion

        #region Funções Relatório
        public Relatorio gerarNovo(DateTime inicio, DateTime fim) {
            try {
                AbrirConexao();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText =
                    "select count(idVenda) as qtd, ifnull(sum(precoTotal), 0) as fat from (select * from vendas where dataVenda >= ?dataInicio - interval 1 day and dataVenda <= ?dataFim + interval 1 day) as datasPeriodo";
                cmd.Parameters.AddWithValue("?dataInicio", inicio);
                cmd.Parameters.AddWithValue("?dataFim", fim);

                Relatorio r;

                using(MySqlDataReader dr = cmd.ExecuteReader()) {
                    dr.Read();
                    r = new Relatorio(dr.GetDouble("fat"), dr.GetInt32("qtd"), inicio, fim);
                }

                return r;
            } finally {
                con.Close();
            }
        }
        #endregion

        public UInt64 GetAutoIncrement(int table) {
            string tableName;
            switch(table) {
                case 1:
                    tableName = "fornecedores";
                    break;
                case 2:
                    tableName = "produtos";
                    break;
                case 3:
                    tableName = "vendas";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            try {
                AbrirConexao();
                MySqlCommand cmd = new MySqlCommand("SELECT auto_increment FROM information_schema.tables WHERE " +
                    "table_schema = 'controleestoque' AND table_name = ?table", con);
                cmd.Parameters.AddWithValue("?table", tableName);
                MySqlDataReader r = cmd.ExecuteReader();
                r.Read();
                return r.GetUInt64(0);
            } finally {
                con.Close();
            }
        }
    }
}
