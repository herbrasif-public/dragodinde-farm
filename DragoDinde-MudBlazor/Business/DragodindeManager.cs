using DragoDinde_MudBlazor.Repositories;
using DragoDinde_MudBlazor.Models;
using System.Runtime.InteropServices.Marshalling;
using System.Text.RegularExpressions;

namespace DragoDinde_MudBlazor.Business
{
    public class DragodindeManager
    {
        private readonly DragodindeGenealogic dragodindeGenealogic;
        private readonly DragodindeRepository dragodindeRepository;
        private readonly UserRepository userRepository;

        public DragodindeManager(DragodindeGenealogic dragodindeGenealogic
        , DragodindeRepository dragodindeRepository
        , UserRepository userRepository) {
            this.dragodindeGenealogic = dragodindeGenealogic;
            this.dragodindeRepository = dragodindeRepository;
            this.userRepository = userRepository;
        }

        public List<(string index, string name, DragodindeOption dragodindeOption)> LoadDradogindes(string userName, ref List<DragodindeOption> dragodindeOptions)
        {
            List<(string index, string name, DragodindeOption dragodindeOption)> result = new List<(string index, string name, DragodindeOption dragodindeOption)>();
            var dragodindes = this.dragodindeRepository.LoadDragodinde(userName);
            foreach (var dro in dragodindes)
            {
                result.Add(new (Guid.NewGuid().ToString(), dro.Name, ReconstructGeneticCode(dro.GeneticCode, ref dragodindeOptions)));
            }
            return result;
        }

        public void SaveParent(bool isMother, string dradoDindeToSave, ref string saveError, ref List<DragodindeTreeCell> cells, ref List<(string index, string droName, DragodindeOption droSaved)> dragodindesSaved) {
            if (String.IsNullOrWhiteSpace(dradoDindeToSave)) {
                saveError = "Pas de nom de dragodinde indiqué";
                return;
            }

            var posTarget = !isMother ? 29 : 30;

            var dro = this.dragodindeGenealogic.getDroByCellPos(posTarget, ref cells).Clone() as DragodindeOption;

            if (dro == null)
            {
                saveError = "Pas de dragodinde trouvé dans l'arbre";
                return;
            }
            
            dro = FillParentsSaved(dro, posTarget,ref cells);

            dragodindesSaved.Add(new(Guid.NewGuid().ToString(), dradoDindeToSave, dro));
            
            string geneticCode = GenerateGeneticCode(dro);

            if (!String.IsNullOrWhiteSpace(this.userRepository.CurrentUser))
            {
                this.dragodindeRepository.SaveDradoginde(dradoDindeToSave, geneticCode, this.userRepository.CurrentUser);
            }
        }

        public void DeleteDroSaved(string name)
        {
            this.dragodindeRepository.DeleteDradoginde(name, this.userRepository.CurrentUser);
        }

        public string GenerateGeneticCode(DragodindeOption dro, string posTree = "")
        {
            string result = "";
            if (dro == null)
                return result;
            result = posTree + dro.Id.ToString();
            if (dro.FatherSaved != default(DragodindeOption))
                result += GenerateGeneticCode(dro.FatherSaved, posTree + "P");
            if (dro.MotherSaved != default(DragodindeOption))
                result += GenerateGeneticCode(dro.MotherSaved, posTree + "M");
            return result;
        }


        public DragodindeOption ReconstructGeneticCode(string geneticCode, ref List<DragodindeOption> dragodindeOptions, string posGenealogic = "") //in-progress
        {
            DragodindeOption dro = null;
            Regex regex = new Regex(@"^\d+");
            Match match = regex.Match(geneticCode);
            if (match.Success)
            {
                string idNumber = match.Value;
                int id = int.Parse(idNumber);
                geneticCode = geneticCode.Substring(idNumber.Length);
                dro = dragodindeOptions.FirstOrDefault(x => x.Id == id).Clone() as DragodindeOption;
                dro.GeneticCode = geneticCode;
                Regex regexFindFather = new Regex(@$"({posGenealogic}[P]+)\d");
                Match matchFindFather = regexFindFather.Match(geneticCode);
                if (matchFindFather.Success)
                {
                    string newPosGenealogic = matchFindFather.Groups[1].Value;
                    int position = matchFindFather.Index;
                    string geneticCodeFather = geneticCode.Substring(position + newPosGenealogic.Length);
                    dro.FatherSaved = ReconstructGeneticCode(geneticCodeFather, ref dragodindeOptions, newPosGenealogic);
                }
                Regex regexFindMother = new Regex(@$"({posGenealogic}[M]+)\d");
                Match matchFindMother = regexFindMother.Match(geneticCode);
                if (matchFindMother.Success)
                {
                    string newPosGenealogic = matchFindMother.Groups[1].Value;
                    int position = matchFindMother.Index;
                    string geneticCodeMother = geneticCode.Substring(position + newPosGenealogic.Length);
                    dro.MotherSaved = ReconstructGeneticCode(geneticCodeMother, ref dragodindeOptions, newPosGenealogic);
                }
            }
            return dro;
        }

        public DragodindeOption ReconstructCreatureFromGeneticCode(string geneticCode, List<DragodindeOption> dragodindeOptions)
        {
            int pos = 0;
            return ReconstructCreatureFromGeneticCodeRecursively(geneticCode, ref dragodindeOptions, ref pos);
        }

        private DragodindeOption ReconstructCreatureFromGeneticCodeRecursively(string geneticCode, ref List<DragodindeOption> dragodindeOptions, ref int pos)
        {
            if (pos >= geneticCode.Length)
                return null;

            string currentIdstring = string.Empty;
            while (pos < geneticCode.Length && char.IsDigit(geneticCode[pos]))
            {
                currentIdstring += geneticCode[pos];
                pos++;
            }

            DragodindeOption currentCreature = null;
            if (!string.IsNullOrWhiteSpace(currentIdstring))
            {
                int currentId = int.Parse(currentIdstring);

                if (!dragodindeOptions.Any(o => o.Id == currentId))
                    return null;

                currentCreature = dragodindeOptions.FirstOrDefault(dro => dro.Id == currentId).Clone() as DragodindeOption;

                if (currentCreature == null)
                {
                    return currentCreature;
                }
            }

            while (pos < geneticCode.Length && (geneticCode[pos] == 'P' || geneticCode[pos] == 'M'))
            {
                if (geneticCode[pos] == 'P')
                {
                    pos++;
                    currentCreature.FatherSaved = ReconstructCreatureFromGeneticCodeRecursively(geneticCode, ref dragodindeOptions, ref pos);
                }
                else if (geneticCode[pos] == 'M')
                {
                    pos++;
                    currentCreature.MotherSaved = ReconstructCreatureFromGeneticCodeRecursively(geneticCode, ref dragodindeOptions, ref pos);
                }
            }

            return currentCreature;
        }


        public DragodindeOption FillParentsSaved(DragodindeOption dro, int? cellPos, ref List<DragodindeTreeCell> cells)
        {
            if (dro == null || cellPos == null)
                return null;

            dro = dro.Clone() as DragodindeOption;
            dro.FatherSaved = null;
            dro.MotherSaved = null;

            var parentsPos = this.dragodindeGenealogic.getParentPos(cellPos.Value);
            var parents = this.dragodindeGenealogic.getParent(cellPos.Value, ref cells);

            if (parents.father != null) {
                dro.FatherSaved = FillParentsSaved(parents.father, parentsPos.boxPosFather, ref cells);
            }
            if (parents.mother != null) {
                dro.MotherSaved = FillParentsSaved(parents.mother, parentsPos.boxPosMother, ref cells);
            }

            return dro;
        }
    }
}