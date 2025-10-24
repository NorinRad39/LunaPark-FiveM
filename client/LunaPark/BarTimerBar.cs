using System.Drawing;
using CitizenFX.Core.UI;

namespace Client.net.LunaPark
{
	public class BarTimerBar : TimerBarBase
	{
		public float Percentage { get; set; }

		public Color BackgroundColor { get; set; } = Color.DarkRed;

		public Color ForegroundColor { get; set; } = Color.Red;

		public BarTimerBar(string label) : base(label)
		{
		}

		public override void Draw(int interval)
		{
			Size res = Screen.Resolution;
			float safeX = 0f;
			float safeY = 0f;

			base.Draw(interval);

			float x = (float)(int)res.Width - safeX - 160f;
			float y = (float)(int)res.Height - safeY - (float)(28 + 4 * interval);

			DrawRectangle(x, y, 150, 15, BackgroundColor);
			DrawRectangle(x, y, (int)(150f * Percentage), 15, ForegroundColor);
		}

		private void DrawRectangle(float x, float y, int width, int height, Color color)
		{
			Size res = Screen.Resolution;
			float normalizedWidth = width / (float)res.Width;
			float normalizedHeight = height / (float)res.Height;
			float normalizedX = (x + width / 2f) / (float)res.Width;
			float normalizedY = (y + height / 2f) / (float)res.Height;

			var sprite = new CitizenFX.Core.UI.Sprite(
				"commonmenu",
				"gradient_bgd",
				new System.Drawing.SizeF(normalizedWidth, normalizedHeight),
				new System.Drawing.PointF(normalizedX, normalizedY),
				color
			);
			sprite.Draw();
		}
	}
}
