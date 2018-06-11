using Microsoft.AspNetCore.Http;
using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MyFinance.Models
{
    public class TransacaoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe a data da transacao!")]
        public string Data { get; set; }
        public string DataFinal { get; set; }
        public string Tipo { get; set; }
        [Required(ErrorMessage = "Informe o valor da transação!")]
        public string Valor { get; set; }
        [Required(ErrorMessage = "Informe a descrição da transação!")]
        public string Descricao { get; set; }
        public int Conta_Id { get; set; }
        public string NomeConta { get; set; }
        public int Plano_Contas_Id { get; set; }
        public string DescricaoPlanoConta { get; set; }
        public IHttpContextAccessor _httpContextAccessor { get; set; }

        public TransacaoModel()
        {
            Tipo = "D";
        }

        public TransacaoModel(IHttpContextAccessor httpContextAcessor)
        {
            _httpContextAccessor = httpContextAcessor;
        }

        public List<TransacaoModel> ListaTransacao()
        {
            string id_usuario_id = _httpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            List<TransacaoModel> lista = new List<TransacaoModel>();
            TransacaoModel item;

            //Extrato
            string filtro = "";
            if (Data != null)
                filtro += $" AND T.DATA >= '{DateTime.Parse(Data).ToString("yyyy/MM/dd")}' ";

            if (DataFinal != null)
                filtro += $" AND T.DATA <= '{DateTime.Parse(Data).ToString("yyyy/MM/dd")}' ";

            if (Tipo != null && Tipo != "A")
                    filtro += $" AND T.TIPO = '{Tipo}' ";

            if (Conta_Id != 0)
                filtro += $" AND T.CONTA_ID = {Conta_Id} ";
            //FIM

            string sql = "SELECT T.ID, T.DATA, T.TIPO, T.VALOR, T.DESCRICAO as HISTORICO, T.CONTA_ID, C.NOME as CONTA, T.PLANO_CONTAS_ID, PC.DESCRICAO as PLANO_CONTA " +
                            " FROM TRANSACAO T INNER JOIN " +
                            "     CONTA C ON T.CONTA_ID = C.ID INNER JOIN " +
                            "     PLANO_CONTAS PC ON T.PLANO_CONTAS_ID = PC.ID INNER JOIN " +
                            "     USUARIO U ON PC.USUARIO_ID = U.ID " +
                            $" WHERE U.ID = {id_usuario_id} " +
                            $" {filtro} " +
                            " ORDER BY T.DATA DESC " +
                            " LIMIT 10; ";

            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            foreach (DataRow row in dt.Rows)
            {
                item = new TransacaoModel()
                {
                    Id = int.Parse(row["ID"].ToString()),
                    Data = DateTime.Parse(row["DATA"].ToString()).ToString("dd/MM/yyyy"),
                    Tipo = row["TIPO"].ToString(),
                    Valor = row["VALOR"].ToString(),
                    Descricao = row["HISTORICO"].ToString(),
                    Conta_Id = int.Parse(row["CONTA_ID"].ToString()),
                    NomeConta = row["CONTA"].ToString(),
                    Plano_Contas_Id = int.Parse(row["PLANO_CONTAS_ID"].ToString()),
                    DescricaoPlanoConta = row["PLANO_CONTA"].ToString()
                };
                lista.Add(item);
            }

            return lista;
        }

        public TransacaoModel CarregaRegistro(int Id)
        {
            string id_usuario_id = _httpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            TransacaoModel item = new TransacaoModel();

            string sql = "SELECT T.ID, T.DATA, T.TIPO, T.VALOR, T.DESCRICAO as HISTORICO, T.CONTA_ID, C.NOME as CONTA, T.PLANO_CONTAS_ID, PC.DESCRICAO as PLANO_CONTA " +
                            " FROM TRANSACAO T INNER JOIN " +
                            "     CONTA C ON T.CONTA_ID = C.ID INNER JOIN " +
                            "     PLANO_CONTAS PC ON T.PLANO_CONTAS_ID = PC.ID INNER JOIN " +
                            "     USUARIO U ON PC.USUARIO_ID = U.ID " +
                            $" WHERE U.ID = {id_usuario_id} AND T.ID = {Id} " +
                            " ORDER BY T.DATA DESC " +
                            " LIMIT 10; ";

            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            foreach (DataRow row in dt.Rows)
            {
                item = new TransacaoModel()
                {
                    Id = int.Parse(row["ID"].ToString()),
                    Data = DateTime.Parse(row["DATA"].ToString()).ToString("yyyy-MM-dd"),
                    Tipo = row["TIPO"].ToString(),
                    Valor = row["VALOR"].ToString(),
                    Descricao = row["HISTORICO"].ToString(),
                    Conta_Id = int.Parse(row["CONTA_ID"].ToString()),
                    NomeConta = row["CONTA"].ToString(),
                    Plano_Contas_Id = int.Parse(row["PLANO_CONTAS_ID"].ToString()),
                    DescricaoPlanoConta = row["PLANO_CONTA"].ToString()
                };
                break;
            }

            return item;
        }

        public void Insert()
        {
            string dataTransacao = DateTime.Parse(Data).ToString("yyyy/MM/dd");
            var valorFormatado = Valor.Replace(".", string.Empty).Replace(",", ".");

            var sql = $"INSERT INTO Transacao (DATA, TIPO, VALOR, DESCRICAO, CONTA_ID, PLANO_CONTAS_ID) " +
                $"VALUES ('{dataTransacao}', '{Tipo}', {valorFormatado}, '{Descricao}', {Conta_Id}, {Plano_Contas_Id});";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandosSQL(sql);
        }

        public void Update()
        {
            string dataTransacao = DateTime.Parse(Data).ToString("yyyy/MM/dd");
            var valorFormatado = Valor.Replace(".", string.Empty).Replace(",", ".");

            var sql = $"UPDATE Transacao SET " +
                $"DATA = '{dataTransacao}', TIPO = '{Tipo}', VALOR = {valorFormatado}, DESCRICAO = '{Descricao}', CONTA_ID = {Conta_Id}, PLANO_CONTAS_ID = {Plano_Contas_Id} " +
                $"WHERE ID = {Id};";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandosSQL(sql);
        }

        public void Excluir(int id)
        {
            var sql = $"DELETE FROM Transacao WHERE Id = {id};";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandosSQL(sql);
        }
    }
}
