using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWEntities.Production
{
    public class Product
    {
        public int Idarticulo { get; set; }
        public int Idcategoria { get; set; }
        public string Categoria { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Condicion { get; set; }

    }
}
