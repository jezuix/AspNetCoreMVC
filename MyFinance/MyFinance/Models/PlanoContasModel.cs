using Microsoft.AspNetCore.Http;
using MyFinance.Util;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MyFinance.Models
{
    public class PlanoContaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe a descrição!")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Informe o tipo!")]
        public string Tipo { get; set; }
        public int Usuario_Id { get; set; }
        public IHttpContextAccessor _httpContextAccessor { get; set; }

        public PlanoContaModel()
        {
            Tipo = "D";
        }

        public PlanoContaModel(IHttpContextAccessor httpContextAcessor)
        {
            _httpContextAccessor = httpContextAcessor;
        }

        public List<PlanoContaModel> ListaPlanoConta()
        {
            List<PlanoContaModel> lista = new List<PlanoContaModel>();
            PlanoContaModel item;

            string id_usuario_id = _httpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"SELECT ID, DESCRICAO, TIPO, USUARIO_ID FROM PLANO_CONTAS WHERE USUARIO_ID = {id_usuario_id}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            foreach (DataRow row in dt.Rows)
            {
                item = new PlanoContaModel()
                {
                    Id = int.Parse(row["ID"].ToString()),
                    Descricao = row["DESCRICAO"].ToString(),
                    Tipo = row["TIPO"].ToString(),
                    Usuario_Id = int.Parse(row["USUARIO_ID"].ToString())
                };
                lista.Add(item);
            }

            return lista;
        }

        public PlanoContaModel CarregaRegistro(int Id)
        {
            PlanoContaModel item = new PlanoContaModel();

            string id_usuario_id = _httpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"SELECT ID, DESCRICAO, TIPO, USUARIO_ID FROM PLANO_CONTAS WHERE USUARIO_ID = {id_usuario_id} AND ID = {Id}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            foreach (DataRow row in dt.Rows)
            {
                item = new PlanoContaModel()
                {
                    Id = int.Parse(row["ID"].ToString()),
                    Descricao = row["DESCRICAO"].ToString(),
                    Tipo = row["TIPO"].ToString(),
                    Usuario_Id = int.Parse(row["USUARIO_ID"].ToString())
                };
                break;
            }

            return item;
        }

        public void Insert()
        {
            string id_usuario_id = _httpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            var sql = $"INSERT INTO PLANO_CONTAS (DESCRICAO, TIPO, Usuario_ID) VALUES ('{Descricao}', '{Tipo}', {id_usuario_id});";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandosSQL(sql);
        }

        public void Update()
        {
            string id_usuario_id = _httpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            var sql = $"UPDATE PLANO_CONTAS SET DESCRICAO = '{Descricao}', TIPO = '{Tipo}' WHERE USUARIO_ID = {id_usuario_id} AND ID = {Id};";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandosSQL(sql);
        }

        public void Excluir()
        {
            var sql = $"DELETE FROM PLANO_CONTAS WHERE Id = {Id};";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandosSQL(sql);
        }
    }
}
