using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemTemplate
{
    public class FlugzeugListModel
    {
        public ObservableCollection<Flugzeug> _flugzeugListe = new ObservableCollection<Flugzeug>()
        {
            new Flugzeug("Boing 747", 30, Flugzeug.Hersteller.Boing, "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Pan_Am_Boeing_747-121_N732PA_Bidini.jpg/1200px-Pan_Am_Boeing_747-121_N732PA_Bidini.jpg"),
            new Flugzeug("AirBus", 20, Flugzeug.Hersteller.AirBus, "https://airbus-h.assetsadobe2.com/is/image/content/dam/products-and-solutions/commercial-aircraft/a380-family/A380_take_off_airbus_livery.jpg?wid=1920&fit=fit,1&qlt=85,0")
        };

        public ObservableCollection<Flugzeug> FlugzeugListe
        {
            get
            {
                return _flugzeugListe;
            }
        }
    }
}
