using Microsoft.AspNetCore.Http;
using MyFinance.Util;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MyFinance.Models
{
    public class ContaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o nome da conta!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe o saldo da conta!")]
        public string Saldo { get; set; }
        public int Usuario_Id { get; set; }
        public IHttpContextAccessor _httpContextAccessor { get; set; }

        public ContaModel()
        {

        }

        public ContaModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<ContaModel> ListaConta()
        {
            List<ContaModel> lista = new List<ContaModel>();
            ContaModel item;

            string id_usuario_id = _httpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"SELECT ID, NOME, SALDO, USUARIO_ID FROM CONTA WHERE USUARIO_ID = {id_usuario_id}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            foreach (DataRow row in dt.Rows)
            {
                item = new ContaModel()
                {
                    Id = int.Parse(row["ID"].ToString()),
                    Nome = row["NOME"].ToString(),
                    Saldo = row["SALDO"].ToString(),
                    Usuario_Id = int.Parse(row["USUARIO_ID"].ToString())
                };
                lista.Add(item);
            }

            return lista;
        }

        public ContaModel CarregaRegistro(int Id)
        {
            ContaModel item = new ContaModel();

            string id_usuario_id = _httpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"SELECT ID, NOME, SALDO, USUARIO_ID FROM CONTA WHERE USUARIO_ID = {id_usuario_id} AND ID = {Id}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            foreach (DataRow row in dt.Rows)
            {
                item = new ContaModel()
                {
                    Id = int.Parse(row["ID"].ToString()),
                    Nome = row["NOME"].ToString(),
                    Saldo = row["SALDO"].ToString(),
                    Usuario_Id = int.Parse(row["USUARIO_ID"].ToString())
                };
                break;
            }

            return item;
        }

        public void Insert()
        {
            string id_usuario_id = _httpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            var sql = $"INSERT INTO CONTA (NOME, SALDO, Usuario_ID) VALUES ('{Nome}', {Saldo}, {id_usuario_id});";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandosSQL(sql);
        }

        public void Update()
        {
            string id_usuario_id = _httpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            var saldoFormatado = Saldo.Replace(".", string.Empty).Replace(",", ".");
            var sql = $"UPDATE CONTA SET NOME = '{Nome}', SALDO = {saldoFormatado} WHERE USUARIO_ID = {id_usuario_id} AND ID = {Id};";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandosSQL(sql);
        }

        public void Excluir(int id)
        {
            var sql = $"DELETE FROM CONTA WHERE Id = {id};";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandosSQL(sql);
        }
    }
}
