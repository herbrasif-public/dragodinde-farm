using DragoDinde_MudBlazor.Repositories;
using DragoDinde_MudBlazor.Models;

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

        public void SaveParent(bool isMother, string dradoDindeToSave, ref string saveError, ref List<DragodindeTreeCell> cells, ref List<(string droName, DragodindeOption droSaved)> dragodindesSaved) {
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

            dragodindesSaved.Add(new(dradoDindeToSave, dro));
            
            string geneticCode = GenerateGeneticCode(dro);

            if (!String.IsNullOrWhiteSpace(this.userRepository.CurrentUser))
            {
                this.dragodindeRepository.SaveDradoginde(dradoDindeToSave, geneticCode, this.userRepository.CurrentUser);
            }
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

        public DragodindeOption FillParentsSaved(DragodindeOption dro, int? cellPos, ref List<DragodindeTreeCell> cells)
        {
            if (dro == null || cellPos == null)
                return null;

            dro = dro.Clone() as DragodindeOption;

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