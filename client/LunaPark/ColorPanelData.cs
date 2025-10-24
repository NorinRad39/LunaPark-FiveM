using System.Collections.Generic;
using System.Drawing;

namespace Client.net.LunaPark
{
	public class Pagination
	{
		// Ajoutez ici les membres nécessaires selon vos besoins
		public int Min { get; set; }
		public int Max { get; set; }
		public int Total { get; set; }
	}

	public class ColorPanelData
	{
		public Pagination Pagination = new();

		public int Index;

		public List<Color> Items;

		public string Title;

		public bool Enabled;

		public int Value;
	}
}
