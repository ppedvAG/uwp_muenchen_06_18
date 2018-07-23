using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemTemplate
{
    public class Flugzeug : INotifyPropertyChanged
    {
        public enum Hersteller
        {
            Boing, AirBus
        }

        public const int Breite_Max = 50;
        public const int Breite_Min = 0;


        public int BreiteMax => Breite_Max; //Property mit nur get-Methode
        public int BreiteMin => Breite_Min;
        public Array HerstellerWerte
        {
            get
            {
                return Enum.GetValues(typeof(Hersteller));
            }
        }


        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        private int _breite;

        public int Breite
        {
            get { return _breite; }
            set
            {
                _breite = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Breite)));
            }
        }

        private Hersteller _herstellerName;

        public Hersteller HerstellerName
        {
            get { return _herstellerName; }
            set
            {
                _herstellerName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HerstellerName)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _fotoUrl;
        public string FotoUrl
        {
            get { return _fotoUrl; }
            set
            {
                _fotoUrl = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FotoUrl)));
            }
        }

        public Flugzeug(string name, int breite, Hersteller herstellerName, string fotoUrl)
        {
            Name = name;
            Breite = breite;
            HerstellerName = herstellerName;
            FotoUrl = fotoUrl;
        }
    }
}
