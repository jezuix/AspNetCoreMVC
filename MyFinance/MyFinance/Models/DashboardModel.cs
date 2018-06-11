using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class DashboardModel
    {
        public double Total { get; set; }
        public string PlanoConta { get; set; }

        public List<DashboardModel> RetornarDadosGraficoPie()
        {
            List<DashboardModel> lista = new List<DashboardModel>();

            var sql = $"SELECT SUM(T.VALOR) AS TOTAL, PC.DESCRICAO " +
                        " FROM TRANSACAO T INNER JOIN " +
                        "     PLANO_CONTAS PC ON T.PLANO_CONTAS_ID = PC.ID " +
                        " WHERE T.TIPO = 'D' " +
                        " GROUP BY PC.DESCRICAO; ";

            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            foreach (DataRow row in dt.Rows)
            {
                var item = new DashboardModel()
                {
                    Total = double.Parse(row["TOTAL"].ToString()),
                    PlanoConta = row["DESCRICAO"].ToString()
                };
                lista.Add(item);
            }

            return lista;
        }
    }
}
