using DragoDinde_MudBlazor.Repositories;
using DragoDinde_MudBlazor.Models;


namespace DragoDinde_MudBlazor.Business
{
    public class DragodindeGenealogic
    {
        public List<DragodindeOption> getParents(DragodindeOption dro, ref List<DragodindeTreeCell> cells)
        {	
            List<DragodindeOption> parentsFound = new List<DragodindeOption>();

            var parents = getParent(dro, ref cells);

            if (parents.father != null){
                parentsFound.Add(parents.father);
                parentsFound.AddRange(getParents(parents.father, ref cells));
            }
            if (parents.mother != null)
            {
                parentsFound.Add(parents.mother);
                parentsFound.AddRange(getParents(parents.mother, ref cells));
            }

            return parentsFound;
        }

        public (DragodindeOption father, DragodindeOption mother) getParent(DragodindeOption dro, ref List<DragodindeTreeCell> cells)
        {
            string CellId = cells.FirstOrDefault(x => x.dragodindeOption == dro)?.CellId;
            if (String.IsNullOrWhiteSpace(CellId))
                return (null, null);
            int boxPos = int.Parse(CellId.Replace("box", ""));
            return getParent(boxPos, ref cells);
        }

        public (DragodindeOption father, DragodindeOption mother) getParent(int boxPos, ref List<DragodindeTreeCell> cells)
        {		if (boxPos < 17)
			return (null, null);
            switch (boxPos)
            {
                case 17:
                    return (getDroByCellPos(1, ref cells), getDroByCellPos(1, ref cells));
                case 18:
                    return (getDroByCellPos(3, ref cells), getDroByCellPos(4, ref cells));
                case 19:
                    return (getDroByCellPos(5, ref cells), getDroByCellPos(6, ref cells));
                case 20:
                    return (getDroByCellPos(7, ref cells), getDroByCellPos(8, ref cells));
                case 21:
                    return (getDroByCellPos(9, ref cells), getDroByCellPos(10, ref cells));
                case 22:
                    return (getDroByCellPos(11, ref cells), getDroByCellPos(12, ref cells));
                case 23:
                    return (getDroByCellPos(13, ref cells), getDroByCellPos(14, ref cells));
                case 24:
                    return (getDroByCellPos(15, ref cells), getDroByCellPos(16, ref cells));
                case 25:
                    return (getDroByCellPos(17, ref cells), getDroByCellPos(18, ref cells));
                case 26:
                    return (getDroByCellPos(19, ref cells), getDroByCellPos(20, ref cells));
                case 27:
                    return (getDroByCellPos(21, ref cells), getDroByCellPos(22, ref cells));
                case 28:
                    return (getDroByCellPos(23, ref cells), getDroByCellPos(24, ref cells));
                case 29:
                    return (getDroByCellPos(25, ref cells), getDroByCellPos(26, ref cells));
                case 30:
                    return (getDroByCellPos(27, ref cells), getDroByCellPos(28, ref cells));
                case 31:
                    return (getDroByCellPos(29, ref cells), getDroByCellPos(30, ref cells));
                default:
                    return (null, null);
            }
        }

        public DragodindeOption getDroByCellPos(int cellPos, ref List<DragodindeTreeCell> cells)
        {
            var result = cells.FirstOrDefault(x => x.CellId == "box" + cellPos)?.dragodindeOption ?? null;
            int gswScore = cellPos < 17 ? 1 : cellPos < 25 ? 3 : cellPos < 29 ? 6 : 10;
            if (result != null)
                result.gsw = gswScore;
            return result;
        }

        public (int? boxPosFather, int? boxPosMother) getParentPos(int boxPos)
        {
            if (boxPos < 17)
                return (null, null);
            switch (boxPos)
            {
                case 17:
                    return (1, 1);
                case 18:
                    return (3, 4);
                case 19:
                    return (5, 6);
                case 20:
                    return (7, 8);
                case 21:
                    return (9, 10);
                case 22:
                    return (11, 12);
                case 23:
                    return (13, 14);
                case 24:
                    return (15, 16);
                case 25:
                    return (17, 18);
                case 26:
                    return (19, 20);
                case 27:
                    return (21, 22);
                case 28:
                    return (23, 24);
                case 29:
                    return (25, 26);
                case 30:
                    return (27, 28);
                case 31:
                    return (29, 30);
                default:
                    return (null, null);
            }
        }
    }
}