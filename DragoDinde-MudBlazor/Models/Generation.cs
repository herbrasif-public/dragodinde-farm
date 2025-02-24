namespace DragoDinde_MudBlazor.Models
{
	public class Generation
	{
		public int Id { get; set; }

		public string Name { get; set; }

        public static List<Generation> GenerateGeneration() {
            var generations = new List<Generation>() { new Generation() { Id = 0, Name = "Tous " } };
                for (int i = 1; i < 11; i++)
                {
                    generations.Add(new Generation() { Id = i, Name = "Génération " + i });
                }
            return generations;
        }
	}
}