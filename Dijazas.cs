using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nobel_WPF
{
    internal class Dijazas
    {
        int evszam;
        String dij;
        String keresztNev;
        String vezetekNev;

        public Dijazas(String sor)
        {
            string[] mezok = sor.Split(';');
            int.TryParse(mezok[0], out this.evszam);
            this.dij = mezok[1];
            this.keresztNev = mezok[2];
            this.vezetekNev = mezok[3];
        }

        public int Evszam { get => evszam; }
        public string Dij { get => dij; }
        public string KeresztNev { get => keresztNev; }
        public string VezetekNev { get => vezetekNev; }
    }
}

