using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gino
{
    internal abstract class Produit
    {
        protected string nom;
        protected double prix;
        protected int tempsPreparationSuppEnSec;
        protected List<Ingredient> ingredients;

        public string Nom { get => nom; set => nom = value; }
        public double Prix { get => prix; set => prix = value; }
        public int TempsPreparationSuppEnSec { get => tempsPreparationSuppEnSec; set => tempsPreparationSuppEnSec = value; }
        public List<Ingredient> Ingredients { get => ingredients; set => ingredients = value; }

        protected Produit(string _nom, double _prix, int _tempsSurplus, List<Ingredient> _ingredients) {
            nom = _nom;
            prix = _prix;
            tempsPreparationSuppEnSec = _tempsSurplus;
            ingredients = _ingredients;
        }

        public virtual async Task<Produit> Preparer(string _numCommande) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Debut de la preparation de la commande : " + _numCommande + "\nNom du produit : " + nom);
            Console.ForegroundColor = ConsoleColor.White;
            await Task.Delay(1000);
            return this;
        }
        public abstract Task Dressage();
        //J'ai aussi changer La fonction Dressage pour qu'elle retourne une Task
        public async Task FairePayer(string _numCommande) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("La Commande : " + _numCommande + " pour le Produit : " + nom + " est prete a etre Payer : "+ prix + "$\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            await Task.Delay(1000);
            //Vue qu'il fallait que je retourne une Task, j'ai pas eu le choix de rajouter un Task.Delay.
        }

        public override string ToString() {
            return nom;
        }
    }
}
