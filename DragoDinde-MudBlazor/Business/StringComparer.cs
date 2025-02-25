using System.Text;
using System.Globalization;

namespace DragoDinde_MudBlazor.Business
{
	public class StringComparerCustom
	{
		public bool CompareIgnoringAccents(string str1, string str2)
		{
			if (str1 == null || str2 == null)
				return str1 == str2;

			string normalizedStr1 = str1.Normalize(NormalizationForm.FormD);
			string normalizedStr2 = str2.Normalize(NormalizationForm.FormD);

			string withoutAccentsStr1 = RemoveAccents(normalizedStr1);
			string withoutAccentsStr2 = RemoveAccents(normalizedStr2);

			return string.Equals(withoutAccentsStr1, withoutAccentsStr2, StringComparison.OrdinalIgnoreCase);
		}

		public bool ContainsIgnoringAccents(string source, string toCheck)
		{
			if (source == null || toCheck == null)
				return false;

			string normalizedSource = source.Normalize(NormalizationForm.FormD);
			string normalizedToCheck = toCheck.Normalize(NormalizationForm.FormD);

			string withoutAccentsSource = RemoveAccents(normalizedSource);
			string withoutAccentsToCheck = RemoveAccents(normalizedToCheck);

			return withoutAccentsSource.IndexOf(withoutAccentsToCheck, StringComparison.OrdinalIgnoreCase) >= 0;
		}


		private string RemoveAccents(string input)
		{
			var chars = input.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
			return new string(chars);
		}
	}
}