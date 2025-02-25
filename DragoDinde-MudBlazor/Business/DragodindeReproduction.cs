using DragoDinde_MudBlazor.Repositories;
using DragoDinde_MudBlazor.Models;


namespace DragoDinde_MudBlazor.Business
{
    public class DragodindeReproduction
    {
        public static Dictionary<int, Dictionary<int, int>> COMBINATIONS = new Dictionary<int, Dictionary<int, int>>
        {
            { 0, new Dictionary<int, int> { { 1, 3 }, { 2, 4 }, { 6, 8 }, { 7, 9 }, { 15, 17 }, { 16, 18 }, { 28, 30 }, { 29, 31 }, { 45, 47 }, { 46, 48 } } },
            { 1, new Dictionary<int, int> { { 0, 3 }, { 2, 5 }, { 6, 10 }, { 7, 11 }, { 15, 19 }, { 16, 20 }, { 28, 32 }, { 29, 33 }, { 45, 49 }, { 46, 50 } } },
            { 2, new Dictionary<int, int> { { 0, 4 }, { 1, 5 }, { 6, 12 }, { 7, 13 }, { 15, 21 }, { 16, 22 }, { 28, 34 }, { 29, 35 }, { 45, 51 }, { 46, 52 } } },
            { 6, new Dictionary<int, int> { { 0, 8 }, { 1, 10 }, { 2, 12 }, { 7, 14 }, { 15, 23 }, { 16, 24 }, { 28, 36 }, { 29, 37 }, { 45, 53 }, { 46, 54 } } },
            { 7, new Dictionary<int, int> { { 0, 9 }, { 1, 11 }, { 2, 13 }, { 6, 14 }, { 15, 25 }, { 16, 26 }, { 28, 38 }, { 29, 39 }, { 45, 55 }, { 46, 56 } } },
            { 15, new Dictionary<int, int> { { 0, 17 }, { 1, 17 }, { 2, 21 }, { 6, 23 }, { 7, 25 }, { 16, 27 }, { 28, 40 }, { 29, 41 }, { 45, 57 }, { 46, 58 } } },
            { 16, new Dictionary<int, int> { { 0, 18 }, { 1, 20 }, { 2, 22 }, { 6, 24 }, { 7, 26 }, { 15, 27 }, { 28, 42 }, { 29, 43 }, { 45, 59 }, { 46, 60 } } },
            { 28, new Dictionary<int, int> { { 0, 30 }, { 1, 30 }, { 2, 34 }, { 6, 36 }, { 7, 38 }, { 15, 40 }, { 16, 42 }, { 29, 44 }, { 45, 61 }, { 46, 62 } } },
            { 29, new Dictionary<int, int> { { 0, 31 }, { 1, 31 }, { 2, 35 }, { 6, 37 }, { 7, 39 }, { 15, 41 }, { 16, 43 }, { 28, 44 }, { 45, 63 }, { 46, 64 } } },
            { 45, new Dictionary<int, int> { { 0, 47 }, { 1, 49 }, { 2, 51 }, { 6, 53 }, { 7, 55 }, { 15, 57 }, { 16, 59 }, { 28, 61 }, { 29, 63 }, { 46, 65 } } },
            { 46, new Dictionary<int, int> { { 0, 48 }, { 1, 50 }, { 2, 52 }, { 6, 54 }, { 7, 56 }, { 15, 58 }, { 16, 60 }, { 28, 62 }, { 29, 64 }, { 45, 65 } } },
            { 3, new Dictionary<int, int> { { 5, 6 }, { 14, 15 } } },
            { 4, new Dictionary<int, int> { { 5, 7 }, { 14, 16 } } },
            { 5, new Dictionary<int, int> { { 3, 6 }, { 4, 7 } } },
            { 14, new Dictionary<int, int> { { 3, 15 }, { 4, 16 } } },
            { 23, new Dictionary<int, int> { { 27, 28 } } },
            { 26, new Dictionary<int, int> { { 27, 29 } } },
            { 27, new Dictionary<int, int> { { 23, 28 }, { 26, 29 } } },
            { 40, new Dictionary<int, int> { { 44, 45 } } },
            { 43, new Dictionary<int, int> { { 44, 46 } } },
            { 44, new Dictionary<int, int> { { 40, 45 }, { 43, 46 } } }
        };

        private readonly DragodindeGenealogic dragodindeGenealogic;
        public DragodindeReproduction(DragodindeGenealogic dragodindeGenealogic){
            this.dragodindeGenealogic = dragodindeGenealogic;
        }

        public Dictionary<int, double> MateWith(DragodindeOption source, DragodindeOption turkey, ref List<DragodindeOption> dragodindeOptions, ref List<DragodindeTreeCell> cells) // last implement
	    {
            // Vérifier les paramètres
            if (turkey == null || !(turkey is DragodindeOption))
            {
                throw new ArgumentException("Wrong parameter");
            }

            // Somme des poids de sélection génétique de chaque arbre généalogique
            // 1) this/father
            Dictionary<int, double> fatherWeights = new Dictionary<int, double>();
            double fatherWeightsSum = 0;
            List<DragodindeOption> fathertree = new List<DragodindeOption>() { turkey };
            fathertree.AddRange(dragodindeGenealogic.getParents(turkey, ref cells));

            foreach (var v in fathertree)
            {
                if (!fatherWeights.ContainsKey(v.Id))
                {
                    fatherWeights[v.Id] = 0;
                }
                fatherWeights[v.Id] += v.gsw;
                fatherWeightsSum += v.gsw;
            }

            // 2) turkey/mother
            Dictionary<int, double> motherWeights = new Dictionary<int, double>();
            double motherWeightsSum = 0;
            List<DragodindeOption> mothertree = new List<DragodindeOption>() { source };
            mothertree.AddRange(dragodindeGenealogic.getParents(source, ref cells));
            foreach (var v in mothertree)
            {
                if (!motherWeights.ContainsKey(v.Id))
                {
                    motherWeights[v.Id] = 0;
                }
                motherWeights[v.Id] += v.gsw;
                motherWeightsSum += v.gsw;
            }

            Dictionary<int, double> births = new Dictionary<int, double>();

            // Pour chaque sélection possible
            foreach (var fatherSortWeight in fatherWeights)
            {
                foreach (var motherSortWeight in motherWeights)
                {
                    // Calculer la probabilité de cette sélection
                    double p1 = fatherSortWeight.Value / fatherWeightsSum;
                    double p2 = motherSortWeight.Value / motherWeightsSum;
                    double p = p1 * p2;

                    // Déterminer les poids de combinaison génétique
                    double gcw1 = getGCW(fatherSortWeight.Key, ref dragodindeOptions);
                    double gcw2 = getGCW(motherSortWeight.Key, ref dragodindeOptions);
                    double gcwm = 0; // Poids de mélange

                    // Le mélange est-il possible ?
                    if (COMBINATIONS.ContainsKey(fatherSortWeight.Key) &&
                        COMBINATIONS[fatherSortWeight.Key].ContainsKey(motherSortWeight.Key))
                    {
                        int mix = COMBINATIONS[fatherSortWeight.Key][motherSortWeight.Key];
                        gcwm = getGCW(mix, ref dragodindeOptions) / 2;
                    }

                    // Calculer et sauvegarder les probabilités résultantes
                    // Couleur 1
                    if (!births.ContainsKey(fatherSortWeight.Key))
                    {
                        births[fatherSortWeight.Key] = 0;
                    }
                    births[fatherSortWeight.Key] += p * gcw1 / (gcw1 + gcw2 + gcwm);

                    // Couleur 2
                    if (!births.ContainsKey(motherSortWeight.Key))
                    {
                        births[motherSortWeight.Key] = 0;
                    }
                    births[motherSortWeight.Key] += p * gcw2 / (gcw1 + gcw2 + gcwm);

                    // Mélange
                    if (gcwm > 0)
                    {
                        int mix = COMBINATIONS[fatherSortWeight.Key][motherSortWeight.Key];
                        if (!births.ContainsKey(mix))
                        {
                            births[mix] = 0;
                        }
                        births[mix] += p * gcwm / (gcw1 + gcw2 + gcwm);
                    }
                }
            }
            
            return births;
        }

        public List<(DragodindeOption, double)> UpdateProbabilities(ref List<DragodindeOption> dragodindeoptions, bool fatherPredisp, bool motherPredisp, ref List<DragodindeTreeCell> cells)
        {
            List<(DragodindeOption, double)> matchesPossibilities = new List<(DragodindeOption, double)>();
            var parents = dragodindeGenealogic.getParent(31, ref cells);

            // Check if the father and mother are set
            if (parents.father == null || parents.mother == null)
            {
                Console.WriteLine("Children not set.");
                return null;
            }

            List<DragodindeOption> fatherTree = new List<DragodindeOption>();
            fatherTree.Add(parents.father);
            fatherTree.AddRange(dragodindeGenealogic.getParents(parents.father, ref cells));

            // Get the father tree
            foreach (var el in fatherTree)
            {
                if (dragodindeoptions.Any(dro => dro.Id == el.Id))
                {
                    var gsw = el.gsw;
                    if (gsw == 10 && fatherPredisp)
                        el.gsw = 20;
                }
            }

            // Get the mother tree
            List<DragodindeOption> motherTree = new List<DragodindeOption>();
            motherTree.Add(parents.mother);
            motherTree.AddRange(dragodindeGenealogic.getParents(parents.mother, ref cells));

            foreach (var el in motherTree)
            {
                if (dragodindeoptions.Any(dro => dro.Id == el.Id))
                {
                    var gsw = el.gsw;
                    if (gsw == 10 && motherPredisp)
                        el.gsw = 20;
                }
            }
            
            // Mate the parents & display the result
            var children = MateWith(parents.father, parents.mother, ref dragodindeoptions, ref cells);
            
            foreach (var child in children.OrderByDescending(c => c.Value).ToList())
            {
                matchesPossibilities.Add(new(dragodindeoptions.FirstOrDefault(x => x.Id == child.Key), child.Value));
            }
            
            return matchesPossibilities;
        }

        public double getGCW(int sortId, ref List<DragodindeOption> dragodindeoptions)
        {
            var dro = dragodindeoptions.FirstOrDefault(s => s.Id == sortId);
            if (dro == null)
                return 0;
            return (100 * dro.ls) / (2 - dro.stage % 2);
        }
    }    
}