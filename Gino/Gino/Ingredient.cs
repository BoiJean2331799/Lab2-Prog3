using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gino
{
    internal class Ingredient
    {
        private string nom;
        private bool contientCafeine;
        private bool vegan;
        private bool sansGluten;

        public string Nom { get => nom; set => nom = value; }
        public bool Vegan { get => vegan; set => vegan = value; }
        public bool ContientCafeine { get => contientCafeine; set => contientCafeine = value; }
        public bool SansGluten { get => sansGluten; set => sansGluten = value; }

        public Ingredient(string _nom, bool _contientCafeine, bool _vegan, bool _sansGluten) {
            nom = _nom;
            contientCafeine = _contientCafeine;
            vegan = _vegan;
            sansGluten = _sansGluten;
        }
    }
}
