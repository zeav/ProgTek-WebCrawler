using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgTek_WebCrawler
{
    class Nyhetsbyraa
    {
        private int id, sokefrekvens;
        private string navn, type, url, brodTagStart, brodTagStop;
        private string kanViseBrodtekst;

        public Nyhetsbyraa(int Id, int Sokefrekvens, string Navn, string Type, string URL, string BrodTagStart, string BrodTagStop, string KanViseBrodtekst)
        {
            //Integers
            this.id = Id;
            this.sokefrekvens = Sokefrekvens;
            //Strings
            this.navn = Navn;
            this.type = Type;
            this.url = URL;
            this.brodTagStart = BrodTagStart;
            this.brodTagStop = BrodTagStop;
            //Bool
            this.kanViseBrodtekst = KanViseBrodtekst;
        }

        public string KanViseBrodtekst
        {
            get { return kanViseBrodtekst; }
        }

        public int Id
        {
            get { return id; }
        }

        public int Sokefrekvens
        {
            get { return sokefrekvens; }
        }

        public string Navn
        {
            get { return navn; }
        }

        public string Type
        {
            get { return type; }
        }

        public string URL
        {
            get { return url; }
        }

        public string BrodTagStart
        {
            get { return brodTagStart; }
        }

        public string BrodTagStop
        {
            get { return brodTagStop; }
        }
        //int Id, int Sokefrekvens, string Navn, string Type, string URL, string BrodTagStart, string BrodTagStop, bool KanViseBrodtekst
    }
}
