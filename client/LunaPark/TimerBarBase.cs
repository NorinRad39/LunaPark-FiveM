using System.Drawing;
using CitizenFX.Core.UI;
using CitizenFX.Core; // Ajout de cet using pour accéder à Hud
using static CitizenFX.Core.Native.API; // Ajouté pour accéder à API

namespace Client.net.LunaPark
{
	public abstract class TimerBarBase
	{
		public string Label { get; set; }

		public TimerBarBase(string label)
		{
			Label = label;
		}

		public virtual void Draw(int interval)
		{
			SizeF res = ScreenTools.ResolutionMaintainRatio;
			PointF safe = ScreenTools.SafezoneBounds;
			((Text)new UIResText(Label, new PointF((float)(int)res.Width - safe.X - 180f, (float)(int)res.Height - safe.Y - (float)(30 + 4 * interval)), 0.3f, Colors.White, CitizenFX.Core.UI.Font.ChaletLondon, (Alignment)2)).Draw();
			new Sprite("timerbars", "all_black_bg", new PointF((float)(int)res.Width - safe.X - 298f, (float)(int)res.Height - safe.Y - (float)(40 + 4 * interval)), new SizeF(300f, 37f), 0f, Color.FromArgb(180, 255, 255, 255)).Draw();
			HideHudComponentThisFrame((int)HudComponent.AreaName);
			HideHudComponentThisFrame((int)HudComponent.StreetName);
			HideHudComponentThisFrame((int)HudComponent.VehicleName);
		}
	}
}
