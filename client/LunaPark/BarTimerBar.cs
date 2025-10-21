using System.Drawing;
using CitizenFX.Core.UI;
// using Client.net.LunaPark.Utils; // Supprim� car le namespace Utils n'existe pas

namespace Client.net.LunaPark
{
	public abstract class TimerBarBase(string label)
	{
		protected string Label { get; } = label;
		public virtual void Draw(int interval) { /* Impl�mentation de base vide */ }
	}

	public class BarTimerBar(string label) : TimerBarBase(label)
	{
		public float Percentage { get; set; }

		public Color BackgroundColor { get; set; } = Color.DarkRed;

		public Color ForegroundColor { get; set; } = Color.Red;

		public override void Draw(int interval)
		{
			// Remplacement de ScreenTools par Screen
			Size res = Screen.Resolution;
			float safeX = 0f;
			float safeY = 0f;

			base.Draw(interval);

			float x = (float)(int)res.Width - safeX - 160f;
			float y = (float)(int)res.Height - safeY - (float)(28 + 4 * interval);

			// Fond
			// Remplacement de UIResRectangle par une impl�mentation locale car UIRectangle n'existe pas
			DrawRectangle(x, y, 150, 15, BackgroundColor);
			// Barre de progression
			DrawRectangle(x, y, (int)(150f * Percentage), 15, ForegroundColor);
		}

		// Remplacez la m�thode DrawRectangle par une impl�mentation utilisant CitizenFX.Core.UI.Sprite
		private void DrawRectangle(float x, float y, int width, int height, Color color)
		{
			// Calcul des coordonn�es et dimensions normalis�es pour l'�cran
			Size res = Screen.Resolution;
			float normalizedWidth = width / (float)res.Width;
			float normalizedHeight = height / (float)res.Height;
			float normalizedX = (x + width / 2f) / (float)res.Width;
			float normalizedY = (y + height / 2f) / (float)res.Height;

			// Utilisation de Sprite pour dessiner un rectangle color�
			var sprite = new CitizenFX.Core.UI.Sprite(
				"commonmenu", // nom du dictionnaire de texture (par exemple)
				"gradient_bgd", // nom de la texture (par exemple)
				new System.Drawing.SizeF(normalizedWidth, normalizedHeight),
				new System.Drawing.PointF(normalizedX, normalizedY),
				color // Correction ici�: passer la couleur � la place de la rotation
			);
			sprite.Draw();
		}
	}
}
