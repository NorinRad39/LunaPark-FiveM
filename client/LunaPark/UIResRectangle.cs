using System.Drawing;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using GdiRect = System.Drawing.Rectangle;
using UiRect = CitizenFX.Core.UI.Rectangle;

namespace Client.net.LunaPark
{
	public class UIResRectangle : UiRect
	{
		public UIResRectangle()
			: base(new PointF(0, 0), new SizeF(0, 0), Color.White)
		{
		}

		public UIResRectangle(PointF pos, SizeF size)
			: base(pos, size, Color.White)
		{
		}

		public UIResRectangle(PointF pos, SizeF size, Color color)
			: base(pos, size, color)
		{
		}

		public override void Draw(SizeF offset)
		{
			if (this.Enabled)
			{
				int screenw = Screen.Resolution.Width;
				int screenh = Screen.Resolution.Height;
				float ratio = (float)screenw / (float)screenh;
				float width = 1080f * ratio;
				float w = this.Size.Width / width;
				float h = this.Size.Height / 1080f;
				float x = (this.Position.X + offset.Width) / width + w * 0.5f;
				float y = (this.Position.Y + offset.Height) / 1080f + h * 0.5f;
				API.DrawRect(x, y, w, h, (int)this.Color.R, (int)this.Color.G, (int)this.Color.B, (int)this.Color.A);
			}
		}

		public static void Draw(float xPos, float yPos, int boxWidth, int boxHeight, Color color)
		{
			int screenw = Screen.Resolution.Width;
			int screenh = Screen.Resolution.Height;
			float ratio = (float)screenw / (float)screenh;
			float width = 1080f * ratio;
			float w = (float)boxWidth / width;
			float h = (float)boxHeight / 1080f;
			float x = xPos / width + w * 0.5f;
			float y = yPos / 1080f + h * 0.5f;
			API.DrawRect(x, y, w, h, (int)color.R, (int)color.G, (int)color.B, (int)color.A);
		}
	}
}
