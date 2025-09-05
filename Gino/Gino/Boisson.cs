using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gino
{
    internal class Boisson : Produit
    {
        private readonly Random rand = new Random();
        private bool boissonChaude;
        public bool BoissonChaude { get => boissonChaude; set => boissonChaude = value; }

        private int compte;

        public Boisson(string nom_B, double prix_B, int tempsSurplus_B, bool boissonChuade_B, List<Ingredient> ingredients_B) : base(nom_B, prix_B, tempsSurplus_B, ingredients_B) {
            boissonChaude = boissonChuade_B;
        }

        public override async Task<Produit> Preparer(string _numCommande)
        {
            await base.Preparer(_numCommande);
            if (boissonChaude) {
                await ChaufferEau();
            }
            if (ingredients.Any(i => i.ContientCafeine)) {
                Console.WriteLine("Nous sommes en train d'infuser le breuvage...");
                await Task.Delay(5000);
            }
            Console.WriteLine("La boisson est en train d'etre verser dans la tasse...");
            await Task.Delay(1000);

            await Dressage();
            await FairePayer(_numCommande);
            return this;
        }

        public override async Task Dressage()
        {
            if (tempsPreparationSuppEnSec > 0)
            {
                Console.WriteLine("Nous sommes en train de faire le dressage de la boisson...");
                await Task.Delay(tempsPreparationSuppEnSec);
            }
        }

        public async Task ChaufferEau() {
            Console.WriteLine("L'eau est en train de chauffer...");
            await Task.Delay(rand.Next(5,11) * 1000);
            Console.WriteLine("L'eau est prete");
        }

        public void Compte(List<Boisson> _boissons, List<Ingredient> _ingredients) {
            var boissons = (from boi in _boissons
                           from ingr in boi.ingredients
                           where ingr.ContientCafeine == false
                           select boi).Distinct();

            compte = boissons.Count();
        }
    }
}
