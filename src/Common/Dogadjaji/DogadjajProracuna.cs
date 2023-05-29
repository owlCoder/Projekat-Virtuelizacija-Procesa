﻿using Common.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dogadjaji
{
    public delegate double ProracunDelegat(IEnumerable<Load> podaci);

    [DataContract]
    public class InterakcijaDogadjajem
    {
        public event ProracunDelegat IzvrsiProracun;

        public List<Proracun> Proracuni;

        public string[] Tip = { "Max Load: ", "Min Load: ", "Stand Deviation: " } ;
        public ushort TipBrojac = 0;

        public InterakcijaDogadjajem()
        {
            IzvrsiProracun = null;
            Proracuni = new List<Proracun>()
            {
                new Proracun(Tip[0], -1.0),
                new Proracun(Tip[1], -1.0),
                new Proracun(Tip[2], -1.0)
            };
        }

        public void Objavi(IEnumerable<Load> podaci)
        {
            if (IzvrsiProracun != null)
            {
                IzvrsiProracun(podaci);
                AzuriranjeProracuna(podaci);
            }
        }

        public Delegate[] Invokacija
        {
            get
            {
                return IzvrsiProracun.GetInvocationList();
            }
        }

        public void AzuriranjeProracuna(IEnumerable<Load> podaci)
        {
           
            foreach (ProracunDelegat p in IzvrsiProracun.GetInvocationList().Cast<ProracunDelegat>())
            {
                Proracuni[TipBrojac++ % 3].VrednostProracuna = p(podaci);
            }
        }
    }
}
