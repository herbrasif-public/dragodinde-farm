
namespace DragoDinde_MudBlazor.Models
{
    public class DragodindeOption : ICloneable
    {
        public int Id { get; set; }
        public string title { get; set; }

        public string abbr { get; set; }

        public string colorA { get; set; }

        public string colorB { get; set; }

        public int stage { get; set; }

        public double ls { get; set; }
        public int gsw { get; set; }



        public string Selector { get; set; } = "zone1";

        public object Clone()
        {
            DragodindeOption clone = new DragodindeOption();

            clone.Id = this.Id;
            clone.title = this.title;
            clone.abbr = this.abbr;
            clone.colorA = this.colorA;
            clone.colorB = this.colorB;
            clone.stage = this.stage;
            clone.ls = this.ls;
            clone.gsw = this.gsw;
            clone.Selector = this.Selector;
            clone.MotherSaved = this.MotherSaved;
            clone.FatherSaved = this.FatherSaved;

            return clone;
        }

        public DragodindeOption MotherSaved { get; set; }
        public DragodindeOption FatherSaved { get; set; }

        public static List<DragodindeOption> GenerateList() {
            return new List<DragodindeOption>() {
                new DragodindeOption { Id = 1, title = "Rousse", abbr = "Ro", colorA = "ff8b00", colorB = "ff8b00", stage = 1, ls = 1 },
                new DragodindeOption { Id = 2, title = "Amande", abbr = "Am", colorA = "ffebcd", colorB = "ffebcd", stage = 1, ls = 1 },
                new DragodindeOption { Id = 3, title = "Dorée", abbr = "Do", colorA = "ffd700", colorB = "ffd700", stage = 1, ls = 0.2 },
                new DragodindeOption { Id = 4, title = "Rousse-Amande", abbr = "Ro-Am", colorA = "ff8b00", colorB = "ffebcd", stage = 2, ls = 0.8 },
                new DragodindeOption { Id = 5, title = "Rousse-Dorée", abbr = "Ro-Do", colorA = "ff8b00", colorB = "ffd700", stage = 2, ls = 0.8 },
                new DragodindeOption { Id = 6, title = "Amande-Dorée", abbr = "Am-Do", colorA = "ffebcd", colorB = "ffd700", stage = 2, ls = 0.8 },
                new DragodindeOption { Id = 7, title = "Indigo", abbr = "In", colorA = "4a0081", colorB = "4a0081", stage = 3, ls = 0.8 },
                new DragodindeOption { Id = 8, title = "Ébène", abbr = "Eb", colorA = "130000", colorB = "130000", stage = 3, ls = 0.8 },
                new DragodindeOption { Id = 9, title = "Rousse-Indigo", abbr = "Ro-In", colorA = "ff8b00", colorB = "4a0081", stage = 4, ls = 0.8 },
                new DragodindeOption { Id = 10, title = "Rousse-Ébène", abbr = "Ro-Eb", colorA = "ff8b00", colorB = "130000", stage = 4, ls = 0.8 },
                new DragodindeOption { Id = 11, title = "Amande-Indigo", abbr = "Am-In", colorA = "ffebcd", colorB = "4a0081", stage = 4, ls = 0.8 },
                new DragodindeOption { Id = 12, title = "Amande-Ébène", abbr = "Am-Eb", colorA = "ffebcd", colorB = "130000", stage = 4, ls = 0.8 },
                new DragodindeOption { Id = 13, title = "Dorée-Indigo", abbr = "Do-In", colorA = "ffd700", colorB = "4a0081", stage = 4, ls = 0.8 },
                new DragodindeOption { Id = 14, title = "Dorée-Ébène", abbr = "Do-Eb", colorA = "ffd700", colorB = "130000", stage = 4, ls = 0.8 },
                new DragodindeOption { Id = 15, title = "Indigo-Ébène", abbr = "In-Eb", colorA = "4a0081", colorB = "130000", stage = 4, ls = 0.8 },
                new DragodindeOption { Id = 16, title = "Pourpre", abbr = "Po", colorA = "dc133b", colorB = "dc133b", stage = 5, ls = 0.6 },
                new DragodindeOption { Id = 17, title = "Orchidée", abbr = "Or", colorA = "f200f2", colorB = "f200f2", stage = 5, ls = 0.6 },
                new DragodindeOption { Id = 18, title = "Rousse-Pourpre", abbr = "Ro-Po", colorA = "ff8b00", colorB = "dc133b", stage = 6, ls = 0.6 },
                new DragodindeOption { Id = 19, title = "Rousse-Orchidée", abbr = "Ro-Or", colorA = "ff8b00", colorB = "f200f2", stage = 6, ls = 0.6 },
                new DragodindeOption { Id = 20, title = "Amande-Pourpre", abbr = "Am-Po", colorA = "ffebcd", colorB = "dc133b", stage = 6, ls = 0.6 },
                new DragodindeOption { Id = 21, title = "Amande-Orchidée", abbr = "Am-Or", colorA = "ffebcd", colorB = "f200f2", stage = 6, ls = 0.6 },
                new DragodindeOption { Id = 22, title = "Dorée-Pourpre", abbr = "Do-Po", colorA = "ffd700", colorB = "dc133b", stage = 6, ls = 0.6 },
                new DragodindeOption { Id = 23, title = "Dorée-Orchidée", abbr = "Do-Or", colorA = "ffd700", colorB = "f200f2", stage = 6, ls = 0.6 },
                new DragodindeOption { Id = 24, title = "Indigo-Pourpre", abbr = "In-Po", colorA = "4a0081", colorB = "dc133b", stage = 6, ls = 0.6 },
                new DragodindeOption { Id = 25, title = "Indigo-Orchidée", abbr = "In-Or", colorA = "4a0081", colorB = "f200f2", stage = 6, ls = 0.6 },
                new DragodindeOption { Id = 26, title = "Ébène-Pourpre", abbr = "Eb-Po", colorA = "130000", colorB = "dc133b", stage = 6, ls = 0.6 },
                new DragodindeOption { Id = 27, title = "Ébène-Orchidée", abbr = "Eb-Or", colorA = "130000", colorB = "f200f2", stage = 6, ls = 0.6 },
                new DragodindeOption { Id = 28, title = "Pourpre-Orchidée", abbr = "Po-Or", colorA = "dc133b", colorB = "f200f2", stage = 6, ls = 0.6 },
                new DragodindeOption { Id = 29, title = "Ivoire", abbr = "Iv", colorA = "fffff0", colorB = "fffff0", stage = 7, ls = 0.6 },
                new DragodindeOption { Id = 30, title = "Turquoise", abbr = "Tu", colorA = "3fe0d0", colorB = "3fe0d0", stage = 7, ls = 0.6 },
                new DragodindeOption { Id = 31, title = "Rousse-Ivoire", abbr = "Ro-Iv", colorA = "ff8b00", colorB = "fffff0", stage = 8, ls = 0.4 },
                new DragodindeOption { Id = 32, title = "Rousse-Turquoise", abbr = "Ro-Tu", colorA = "ff8b00", colorB = "3fe0d0", stage = 8, ls = 0.4 },
                new DragodindeOption { Id = 33, title = "Amande-Ivoire", abbr = "Am-Iv", colorA = "ffebcd", colorB = "fffff0", stage = 8, ls = 0.4 },
                new DragodindeOption { Id = 34, title = "Amande-Turquoise", abbr = "Am-Tu", colorA = "ffebcd", colorB = "3fe0d0", stage = 8, ls = 0.4 },
                new DragodindeOption { Id = 35, title = "Dorée-Ivoire", abbr = "Do-Iv", colorA = "ffd700", colorB = "fffff0", stage = 8, ls = 0.4 },
                new DragodindeOption { Id = 36, title = "Dorée-Turquoise", abbr = "Do-Tu", colorA = "ffd700", colorB = "3fe0d0", stage = 8, ls = 0.4 },
                new DragodindeOption { Id = 37, title = "Indigo-Ivoire", abbr = "In-Iv", colorA = "4a0081", colorB = "fffff0", stage = 8, ls = 0.4 },
                new DragodindeOption { Id = 38, title = "Indigo-Turquoise", abbr = "In-Tu", colorA = "4a0081", colorB = "3fe0d0", stage = 8, ls = 0.4 },
                new DragodindeOption { Id = 39, title = "Ébène-Ivoire", abbr = "Eb-Iv", colorA = "130000", colorB = "fffff0", stage = 8, ls = 0.4 },
                new DragodindeOption { Id = 40, title = "Ébène-Turquoise", abbr = "Eb-Tu", colorA = "130000", colorB = "3fe0d0", stage = 8, ls = 0.4 },
                new DragodindeOption { Id = 41, title = "Pourpre-Ivoire", abbr = "Po-Iv", colorA = "dc133b", colorB = "fffff0", stage = 8, ls = 0.4 },
                new DragodindeOption { Id = 42, title = "Pourpre-Turquoise", abbr = "Po-Tu", colorA = "dc133b", colorB = "3fe0d0", stage = 8, ls = 0.4 },
                new DragodindeOption { Id = 43, title = "Orchidée-Ivoire", abbr = "Or-Iv", colorA = "f200f2", colorB = "fffff0", stage = 8, ls = 0.4 },
                new DragodindeOption { Id = 44, title = "Orchidée-Turquoise", abbr = "Or-Tu", colorA = "f200f2", colorB = "3fe0d0", stage = 8, ls = 0.4 },
                new DragodindeOption { Id = 45, title = "Ivoire-Turquoise", abbr = "Iv-Tu", colorA = "fffff0", colorB = "3fe0d0", stage = 8, ls = 0.4 },
                new DragodindeOption { Id = 46, title = "Émeraude", abbr = "Em", colorA = "31cd31", colorB = "31cd31", stage = 9, ls = 0.4 },
                new DragodindeOption { Id = 47, title = "Prune", abbr = "Pr", colorA = "dd9fdd", colorB = "dd9fdd", stage = 9, ls = 0.4 },
                new DragodindeOption { Id = 48, title = "Rousse-Émeraude", abbr = "Ro-Em", colorA = "ff8b00", colorB = "31cd31", stage = 10, ls = 0.2 },
                new DragodindeOption { Id = 49, title = "Rousse-Prune", abbr = "Ro-Pr", colorA = "ff8b00", colorB = "dd9fdd", stage = 10, ls = 0.2 },
                new DragodindeOption { Id = 50, title = "Amande-Émeraude", abbr = "Am-Em", colorA = "ffebcd", colorB = "31cd31", stage = 10, ls = 0.2 },
                new DragodindeOption { Id = 51, title = "Amande-Prune", abbr = "Am-Pr", colorA = "ffebcd", colorB = "dd9fdd", stage = 10, ls = 0.2 },
                new DragodindeOption { Id = 52, title = "Dorée-Émeraude", abbr = "Do-Em", colorA = "ffd700", colorB = "31cd31", stage = 10, ls = 0.2 },
                new DragodindeOption { Id = 53, title = "Dorée-Prune", abbr = "Do-Pr", colorA = "ffd700", colorB = "dd9fdd", stage = 10, ls = 0.2 },
                new DragodindeOption { Id = 54, title = "Indigo-Émeraude", abbr = "In-Em", colorA = "4a0081", colorB = "31cd31", stage = 10, ls = 0.2 },
                new DragodindeOption { Id = 55, title = "Indigo-Prune", abbr = "In-Pr", colorA = "4a0081", colorB = "dd9fdd", stage = 10, ls = 0.2 },
                new DragodindeOption { Id = 56, title = "Ébène-Émeraude", abbr = "Eb-Em", colorA = "130000", colorB = "31cd31", stage = 10, ls = 0.2 },
                new DragodindeOption { Id = 57, title = "Ébène-Prune", abbr = "Eb-Pr", colorA = "130000", colorB = "dd9fdd", stage = 10, ls = 0.2 },
                new DragodindeOption { Id = 58, title = "Pourpre-Émeraude", abbr = "Po-Em", colorA = "dc133b", colorB = "31cd31", stage = 10, ls = 0.2 },
                new DragodindeOption { Id = 59, title = "Pourpre-Prune", abbr = "Po-Pr", colorA = "dc133b", colorB = "dd9fdd", stage = 10, ls = 0.2 },
                new DragodindeOption { Id = 60, title = "Orchidée-Émeraude", abbr = "Or-Em", colorA = "f200f2", colorB = "31cd31", stage = 10, ls = 0.2 },
                new DragodindeOption { Id = 61, title = "Orchidée-Prune", abbr = "Or-Pr", colorA = "f200f2", colorB = "dd9fdd", stage = 10, ls = 0.2 },
                new DragodindeOption { Id = 62, title = "Ivoire-Émeraude", abbr = "Iv-Em", colorA = "fffff0", colorB = "31cd31", stage = 10, ls = 0.2 },
                new DragodindeOption { Id = 63, title = "Ivoire-Prune", abbr = "Iv-Pr", colorA = "fffff0", colorB = "dd9fdd", stage = 10, ls = 0.2 },
                new DragodindeOption { Id = 64, title = "Turquoise-Émeraude", abbr = "Tu-Em", colorA = "3fe0d0", colorB = "31cd31", stage = 10, ls = 0.2 },
                new DragodindeOption { Id = 65, title = "Turquoise-Prune", abbr = "Tu-Pr", colorA = "3fe0d0", colorB = "dd9fdd", stage = 10, ls = 0.2 },
                new DragodindeOption { Id = 66, title = "Émeraude-Prune", abbr = "Em-Pr", colorA = "31cd31", colorB = "dd9fdd", stage = 10, ls = 0.2 }
            };
        }
    }
}