using ProjetLinq.BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TPLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            InitialiserDatas();


            var prenomsG = ListeAuteurs.Where(a => a.Nom.ToUpper().StartsWith("G")).Select(a => a.Prenom);

            Console.WriteLine("Liste Auteurs qui commence par\"G\"");

            foreach (var prenom in prenomsG)
            {
                Console.WriteLine(prenom);
            }
            Console.WriteLine();

         
            var auteurPlusDeLivres = ListeLivres.GroupBy(l => l.Auteur).OrderByDescending(g => g.Count()).FirstOrDefault().Key;
            Console.WriteLine("Auteur qui a ecrit le plus de livre");

            Console.WriteLine($"{auteurPlusDeLivres.Prenom} {auteurPlusDeLivres.Nom}");

            Console.WriteLine();


            var livresparAuteur = ListeLivres.GroupBy(l => l.Auteur);

            Console.WriteLine("nombre page moyen par auteur");
            foreach (var item in livresparAuteur)
            {
                Console.WriteLine($"{item.Key.Prenom} {item.Key.Nom} moyennes des pages={item.Average(l => l.NbPages)}");
            }
            Console.WriteLine();

            var livreMaxPage = ListeLivres.OrderByDescending(l => l.NbPages).First();

            Console.WriteLine("livre avec plus de page");

            Console.WriteLine(livreMaxPage.Titre);

            Console.WriteLine();


            var moyenne = ListeAuteurs.Average(a => a.Factures.Sum(f => f.Montant));

            Console.WriteLine("Combien ont gagne en moyenne");

            Console.WriteLine(moyenne);

            Console.WriteLine();

            Console.WriteLine("liste auteurs et de leurs livres");

            var livresparAuteur2 = ListeLivres.GroupBy(l => l.Auteur);
            foreach (var livres in livresparAuteur2)
            {
                Console.WriteLine($"Auteur : {livres.Key.Prenom} {livres.Key.Nom} ");
                foreach (var livre in livres)
                {
                    Console.WriteLine($" - {livre.Titre}");
                }
            }
            Console.WriteLine();

            Console.WriteLine(" livres par ordre alphabethique");
            ListeLivres.Select(l => l.Titre).OrderBy(t => t).ToList().ForEach(Console.WriteLine);
            Console.WriteLine();

            Console.WriteLine("");
            var moyennePages = ListeLivres.Average(l => l.NbPages);
            var livresPagesSupMoy = ListeLivres.Where(l => l.NbPages > moyennePages);
            foreach (var livre in livresPagesSupMoy)
            {
                Console.WriteLine($" - {livre.Titre}");
            }
            Console.WriteLine();

            //- Afficher l'auteur ayant écrit le moins de livres
            Console.WriteLine("- Auteur ayant ecrit le moins de livres");
            //   var auteurMoinsDeLivres = ListeLivres.GroupBy(l => l.Auteur).OrderBy(g => g.Count()).FirstOrDefault().Key;

            var auteurMoinsDeLivres = ListeAuteurs.OrderBy(a => ListeLivres.Count(l => l.Auteur == a)).FirstOrDefault();
            Console.WriteLine($"{auteurMoinsDeLivres.Prenom} {auteurMoinsDeLivres.Nom}");
            Console.ReadKey();
        }

        private static readonly List<Auteur> ListeAuteurs = new List<Auteur>();
        private static readonly List<Livre> ListeLivres = new List<Livre>();

        private static void InitialiserDatas()
        {
            ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
            ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
            ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
            ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
            ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
            ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
            ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
            ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
            ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
            ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
            ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
            ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(1).addFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
            ListeAuteurs.ElementAt(2).addFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
            ListeAuteurs.ElementAt(3).addFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
        }

    }
}
