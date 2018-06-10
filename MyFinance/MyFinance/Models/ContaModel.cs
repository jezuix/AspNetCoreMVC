using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class ContaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Saldo { get; set; }
        public int Usuario_Id { get; set; }

        public List<ContaModel> ListaConta()
        {
            List<ContaModel> lista = new List<ContaModel>();
            ContaModel item;

            string id_usuario_id = "1";
            string sql = $"SELECT ID, NOME, SALDO, USUARIO_ID FROM CONTA WHERE USUARIO_ID = {id_usuario_id}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            foreach (DataRow row in dt.Rows)
            {
                item = new ContaModel()
                {
                    Id = int.Parse(row["ID"].ToString()),
                    Nome = row["NOME"].ToString(),
                    Saldo = double.Parse(row["SALDO"].ToString()),
                    Usuario_Id = int.Parse(row["USUARIO_ID"].ToString())
                };
                lista.Add(item);
            }

            return lista;
        }
    }
}
