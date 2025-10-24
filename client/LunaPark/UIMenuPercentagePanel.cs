using System;
using System.Drawing;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;

namespace Client.net.LunaPark
{
	public class UIMenuPercentagePanel : UIMenuPanel
	{
		private UIResRectangle ActiveBar;

		private UIResRectangle BackgroundBar;

		private UIMenuGridAudio Audio;

		private UIResText Min;

		private UIResText Max;

		private UIResText Title;

		private bool Pressed;

		public float Percentage
		{
			get
			{
				SizeF res = ScreenTools.ResolutionMaintainRatio;
				float progress = (float)Math.Round(API.GetDisabledControlNormal(0, 239) * res.Width) - ActiveBar.Position.X;
				return (float)Math.Round(((progress >= 0f && progress <= 413f) ? progress : ((float)((!(progress < 0f)) ? 413 : 0))) / Background.Size.Width, 2);
			}
			set
			{
				float percent = ((value < 0f) ? 0f : ((value > 1f) ? 1f : value));
				ActiveBar.Size = new SizeF(BackgroundBar.Size.Width * percent, ActiveBar.Size.Height);
			}
		}

		public UIMenuPercentagePanel(string title, string MinText, string MaxText)
		{
			Enabled = true;
			Background = new Sprite("commonmenu", "gradient_bgd", new Point(0, 0), new Size(431, 275));
			ActiveBar = new UIResRectangle(new Point(0, 0), new Size(413, 10), Color.FromArgb(245, 245, 245));
			BackgroundBar = new UIResRectangle(new Point(0, 0), new Size(413, 10), Color.FromArgb(80, 80, 80));
			Min = new UIResText((MinText != "" || MinText != null) ? MinText : "0%", new Point(0, 0), 0.35f, Color.FromArgb(255, 255, 255), CitizenFX.Core.UI.Font.ChaletLondon, CitizenFX.Core.UI.Alignment.Center);
			Max = new UIResText((MaxText != "" || MaxText != null) ? MaxText : "100%", new Point(0, 0), 0.35f, Color.FromArgb(255, 255, 255), CitizenFX.Core.UI.Font.ChaletLondon, CitizenFX.Core.UI.Alignment.Center);
			Title = new UIResText((title != "" || title != null) ? title : "Opacity", new Point(0, 0), 0.35f, Color.FromArgb(255, 255, 255), CitizenFX.Core.UI.Font.ChaletLondon, CitizenFX.Core.UI.Alignment.Center);
			Audio = new UIMenuGridAudio("CONTINUOUS_SLIDER", "HUD_FRONTEND_DEFAULT_SOUNDSET", 0);
		}

		internal override void Position(float y)
		{
			float ParentOffsetX = base.ParentItem.Offset.X;
			int ParentOffsetWidth = base.ParentItem.Parent.WidthOffset;
			Background.Position = new PointF(ParentOffsetX, 35f + y);
			ActiveBar.Position = new PointF(ParentOffsetX + (float)(ParentOffsetWidth / 2) + 9f, 85f + y);
			BackgroundBar.Position = ActiveBar.Position;
			Min.Position = new PointF(ParentOffsetX + (float)(ParentOffsetWidth / 2) + 25f, 50f + y);
			Max.Position = new PointF(ParentOffsetX + (float)(ParentOffsetWidth / 2) + 398f, 50f + y);
			Title.Position = new PointF(ParentOffsetX + (float)(ParentOffsetWidth / 2) + 215.5f, 50f + y);
		}

		public void UpdateParent(float Percentage)
		{
			base.ParentItem.Parent.ListChange(base.ParentItem, base.ParentItem.Index);
			base.ParentItem.ListChangedTrigger(base.ParentItem.Index);
		}

		private async void Functions()
		{
			PointF safezoneOffset = ScreenTools.SafezoneBounds;
			if (ScreenTools.IsMouseInBounds(
					new PointF(BackgroundBar.Position.X + safezoneOffset.X, BackgroundBar.Position.Y - 4f + safezoneOffset.Y),
					new SizeF(BackgroundBar.Size.Width, BackgroundBar.Size.Height + 8f))
				&& API.IsDisabledControlPressed(0, 24) && !Pressed)
			{
				Pressed = true;
				Audio.Id = API.GetSoundId();
				API.PlaySoundFrontend(Audio.Id, Audio.Slider, Audio.Library, true);
				while (API.IsDisabledControlPressed(0, 24) && ScreenTools.IsMouseInBounds(
					new PointF(BackgroundBar.Position.X + safezoneOffset.X, BackgroundBar.Position.Y - 4f + safezoneOffset.Y),
					new SizeF(BackgroundBar.Size.Width, BackgroundBar.Size.Height + 8f)))
				{
					await BaseScript.Delay(0);
					SizeF res = ScreenTools.ResolutionMaintainRatio;
					float Progress = API.GetDisabledControlNormal(0, 239) * res.Width;
					Progress -= ActiveBar.Position.X + safezoneOffset.X;
					ActiveBar.Size = new SizeF(
						(Progress >= 0f && Progress <= 413f) ? Progress : ((!(Progress < 0f)) ? 413 : 0),
						ActiveBar.Size.Height);
					UpdateParent((float)Math.Round(
						((Progress >= 0f && Progress <= 413f) ? Progress : ((!(Progress < 0f)) ? 413 : 0)) / BackgroundBar.Size.Width, 2));
				}
				API.StopSound(Audio.Id);
				API.ReleaseSoundId(Audio.Id);
				Pressed = false;
			}
		}

		internal override async Task Draw()
		{
			if (Enabled)
			{
				Background.Size = new Size(431 + base.ParentItem.Parent.WidthOffset, 76);
				Background.Draw();
				((CitizenFX.Core.UI.Rectangle)BackgroundBar).Draw();
				((CitizenFX.Core.UI.Rectangle)ActiveBar).Draw();
				((CitizenFX.Core.UI.Text)Min).Draw();
				((CitizenFX.Core.UI.Text)Max).Draw();
				((CitizenFX.Core.UI.Text)Title).Draw();
				Functions();
			}
			await Task.FromResult(0);
		}
	}
}
