using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteE_Turn.Modelos
{
    class Trilha
    {
       public List<Palestra> PalestrasManha { get; set; }
        public List<Palestra> PalestrasTarde { get; set; }

        public Trilha()
        {
            PalestrasManha = new List<Palestra>();
            PalestrasTarde = new List<Palestra>();
        }
    }
}
