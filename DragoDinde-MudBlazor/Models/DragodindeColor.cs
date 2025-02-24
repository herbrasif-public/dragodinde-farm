namespace DragoDinde_MudBlazor.Models
{
	public class DragodindeColor
	{
		public int Id { get; set; }

		public string Name { get; set; }

        public static List<DragodindeColor> GenerateColors() {
            return new List<DragodindeColor>() {
                new DragodindeColor() { Id = 0, Name = "Tous "},
                new DragodindeColor() { Id = 1, Name ="Amande"},
                new DragodindeColor() { Id = 2, Name  ="Dorée"},
                new DragodindeColor() { Id = 3, Name  ="Ebène"},
                new DragodindeColor() { Id = 4, Name  ="Emeraude"},
                new DragodindeColor() { Id = 5, Name  ="Indigo"},
                new DragodindeColor() { Id = 6, Name  ="Ivoire"},
                new DragodindeColor() { Id = 7, Name  ="Orchidée"},
                new DragodindeColor() { Id = 8, Name  ="Pourpre"},
                new DragodindeColor() { Id = 9, Name  ="Prune"},
                new DragodindeColor() { Id = 10, Name  ="Rousse"},
                new DragodindeColor() { Id = 11, Name  ="Turquoise"}
            };
        }
	}
}