using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gino
{
    internal class Nourriture : Produit
    {
        private bool collation;

        public bool Collation { get => collation; set => collation = value; }

        public Nourriture(string nom_N, double prix_N, int tempsSurplus_N, bool collation_N, List<Ingredient> ingredients_N) : base(nom_N, prix_N, tempsSurplus_N, ingredients_N) {
            collation = collation_N;
        }

        public override async Task<Produit> Preparer(string _numCommande) {
            await base.Preparer(_numCommande);
            Console.WriteLine("La nourriture est en train d'etre depose dans l'assiette...");
            await Task.Delay(1000);
            await Cuisson();
            await Dressage();
            await FairePayer(_numCommande);
            return this;
        }

        public override async Task Dressage() {
            if (tempsPreparationSuppEnSec == 1) {
                Console.WriteLine("Nous sommes en train de faire le dressage du plat...");
                await Task.Delay(1000);
            }
            else if (tempsPreparationSuppEnSec > 2) { 
                Console.WriteLine("Nous sommes en train de faire le dressage du plat...");
                await Task.Delay(tempsPreparationSuppEnSec - 2);
            }
        }

        public async Task Cuisson() {
            if (tempsPreparationSuppEnSec >= 3) {
                Console.WriteLine("La nourriture est en cuisson...");
                await Task.Delay(2000);
            }
        }
    }
}
