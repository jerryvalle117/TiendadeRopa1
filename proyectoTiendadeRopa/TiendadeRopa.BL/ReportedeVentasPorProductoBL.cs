using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendadeRopa.BL
{
    public class ReportedeVentasPorProductoBL
    {
        Contexto _Contexto;
        public List<ReporteVentasPorProducto> ListadeVentasPorProducto { get; set; }

        public ReportedeVentasPorProductoBL()
        {
            _Contexto = new Contexto();
            ListadeVentasPorProducto = new List<ReporteVentasPorProducto>();
        }

        public List<ReporteVentasPorProducto> ObtenerVentasPorProducto()
        {
            ListadeVentasPorProducto = _Contexto.OrdenDetalle
                .Include("Producto")
                .Where(r => r.Orden.Activo)
                .GroupBy(r => r.Producto.Descripcion)
                .Select(r => new ReporteVentasPorProducto()
                {
                    Producto = r.Key,
                    Cantidad = r.Sum(s => s.Cantidad),
                    Total = r.Sum(s => s.Total)
                }).ToList();

            return ListadeVentasPorProducto;
        }
    }
}
