using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Gino
{
    internal class Program
    {
        //j'ai mit le Main en Async pour le Task.WhenAll et j'ai aussi pris la chance pour changer le thread.sleep pour un await Task.Delay
        static async Task Main(string[] args)
        {

            /*---------------------------------------------------------*/
            List<Task> commandes = new List<Task>();
            double montantTotal = 0;
            Random rand = new Random();
            int nbClients = rand.Next(5, 100);
            int choix;

            //Création des ingrédients de base
            Ingredient cafe = new Ingredient("café", true, true, true);
            Ingredient farine = new Ingredient("farine", false, true, false);
            Ingredient oeuf = new Ingredient("oeuf", false, false, true);
            Ingredient eau = new Ingredient("eau", false, true, true);
            Ingredient fruit = new Ingredient("fruit", false, true, true);
            Ingredient beurre = new Ingredient("beurre", false, false, true);
            Ingredient patate = new Ingredient("patate", false, true, true);
            Ingredient the = new Ingredient("thé", true, true, true);
            Ingredient steak = new Ingredient("steak", false, false, false);
            Ingredient biscuit = new Ingredient("biscuit", false, false, true);

            List<Ingredient> ingredients = new List<Ingredient>
            {
                cafe,
                farine,
                oeuf,
                eau,
                fruit,
                beurre,
                patate,
                the,
                steak,
                biscuit
            };


            //Création de tous les produits
            List<Produit> produits = new List<Produit>();
            produits.Add(new Nourriture("Croissant", 2.49, 0, false, new List<Ingredient> { farine, beurre }));
            produits.Add(new Nourriture("Muffin aux fruits", 1.99, 0, true, new List<Ingredient> { farine, fruit }));
            produits.Add(new Nourriture("Sandwich déjeuner", 4.79, 3, false, new List<Ingredient> { farine, beurre, oeuf }));
            produits.Add(new Nourriture("Brunch", 6.99, 12, false, new List<Ingredient> { steak, patate, fruit }));
            produits.Add(new Nourriture("Combo Equilibre", 4.97, 4, true, new List<Ingredient> { biscuit, fruit }));
            produits.Add(new Boisson("Chocolat chaud", 2.49, 0, true, new List<Ingredient> { eau }));
            produits.Add(new Boisson("Latté", 4.29, 2, true, new List<Ingredient> { eau, cafe }));
            produits.Add(new Boisson("Espresso", 1.79, 0, true, new List<Ingredient> { eau, cafe }));
            produits.Add(new Boisson("Thé glacé maison", 2.49, 0, false, new List<Ingredient> { eau, the, fruit }));
            produits.Add(new Boisson("Jus de fruits", 1.49, 0, false, new List<Ingredient> { fruit }));
            produits.Add(new Boisson("Pourquoi?", 5.89, 5, true, new List<Ingredient> { steak, biscuit, eau }));
            produits.Add(new Boisson("Eau", 0.99, 0, false, new List<Ingredient> { eau }));

            Console.WriteLine("Bienvenu au Café Chez Gino !");

            //Pour chaque client, procéder à une commande
            for (int i = 0; i < nbClients; i++)
            {
                //Faire attendre une seconde
                await Task.Delay(1000);

                //Choisir aléatoirement un produit à commander
                Produit randCom = produits[rand.Next(0, produits.Count)];

                //Afficher quel produit la personne veut commander
                Console.WriteLine("Vous avez commander le produit :" + randCom.ToString());

                //Préparer le produit, le numérode de la commande doit être sous le format Com001
                //Astuce, utiliser ToString("D3")
                commandes.Add(Task.Run(()=>randCom.Preparer("com" + i.ToString("D3"))));

                //Augmenter le total par la valeur du produit
                montantTotal += randCom.Prix;

            }

            await Task.WhenAll(commandes);

            Console.WriteLine("Merci d'avoir travaillé au Café Chez Gino !");
            Console.WriteLine("Aujourd'hui, le café a fait " + Math.Round(montantTotal, 2) + "$!\n\n");

            /*---------------------------------------------------------*/

            Console.WriteLine("\n------------------------Nourriture------------------------\n");

            var nourritures = from prod in produits
                              where prod is Nourriture
                              select (Nourriture)prod;

            foreach(var i in nourritures)
                Console.WriteLine(i);

            Console.WriteLine("\n-----------------------Sans Gluten-------------------------\n");

            var gluten = (from prod in nourritures
                          from ingr in prod.Ingredients
                          where ingr.SansGluten == false
                          select prod).Distinct();

            var sansGluten = from nour in nourritures
                             where !(from g in gluten select g).Contains(nour)
                             select nour;

            foreach (var i in sansGluten)
                Console.WriteLine(i);

            Console.WriteLine("\n------------------------Vegan------------------------\n");

            var notVegan = (from nour in nourritures
                            from ingr in nour.Ingredients
                            where ingr.Vegan == false
                            select nour).Distinct();

            var vegan = from nour in nourritures
                        where !(from v in notVegan select v).Contains(nour)
                        select nour;

            foreach (var i in vegan)
                Console.WriteLine(i);

            Console.WriteLine("\n-----------------------Boissons-------------------------\n");

            var boissons = from prod in produits
                           where prod is Boisson
                           select (Boisson)prod;

            foreach(var i in boissons)
                Console.WriteLine(i);


            /*Console.WriteLine("\n---------------------Contains---------------------------\n");

            var boissonCafeine = from prod in boissons
                                 where prod.Ingredients.Contains(cafe) || prod.Ingredients.Contains(the)
                                 select prod;

            foreach (var i in boissonCafeine)
                Console.WriteLine(i);*/

            Console.WriteLine("\n-----------------------Cafeine-------------------------\n");

            //avec select many

            var boissonCafeine2 = (from prod in boissons
                                  from ingr in prod.Ingredients
                                  where ingr.ContientCafeine == true
                                  select prod).Distinct();

            foreach (var i in boissonCafeine2)
                Console.WriteLine(i);

            Console.WriteLine("\n---------------------Boissons Chaudes---------------------------\n");

            var boissonChaude = from prod in boissons
                                where prod.BoissonChaude == true
                                select prod;

            foreach (var i in boissonChaude)
                Console.WriteLine(i);

            Console.WriteLine("\n---------------------Combinaisons---------------------------\n");

            var combinaisons = from nour in nourritures
                               where nour.Collation == false
                               from boi in boissons
                               select new List<string> { nour.Nom, boi.Nom };

            foreach (var i in combinaisons) {
                foreach (var j in i) { 
                    Console.Write(j + " | ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n------------------------------------------------\n");

        }
    }
}
