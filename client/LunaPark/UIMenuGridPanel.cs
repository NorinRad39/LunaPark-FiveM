using System;
using System.Drawing;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;

// Ajoutez cette directive using pour �viter l'ambigu�t�
using Font = CitizenFX.Core.UI.Font;

namespace Client.net.LunaPark
{
	public class UIMenuGridPanel : UIMenuPanel
	{
		private UIResText Top;

		private UIResText Left;

		private UIResText Right;

		private UIResText Bottom;

		private Sprite Grid;

		private Sprite Circle;

		private UIMenuGridAudio Audio;

		private PointF SetCirclePosition;

		protected bool CircleLocked = false;

		protected bool Pressed;

		public PointF CirclePosition
		{
			get
			{
				return new PointF((float)Math.Round((Circle.Position.X - (Grid.Position.X + 20f) + Circle.Size.Width / 2f) / (Grid.Size.Width - 40f), 2), (float)Math.Round((Circle.Position.Y - (Grid.Position.Y + 20f) + Circle.Size.Height / 2f) / (Grid.Size.Height - 40f), 2));
			}
			set
			{
				Circle.Position.X = Grid.Position.X + 20f + (Grid.Size.Width - 40f) * ((value.X >= 0f && value.X <= 1f) ? value.X : 0f) - Circle.Size.Width / 2f;
				Circle.Position.Y = Grid.Position.Y + 20f + (Grid.Size.Height - 40f) * ((value.Y >= 0f && value.Y <= 1f) ? value.Y : 0f) - Circle.Size.Height / 2f;
			}
		}

		public UIMenuGridPanel(string TopText, string LeftText, string RightText, string BottomText, PointF circlePosition)
		{
			Enabled = true;
			Background = new Sprite("commonmenu", "gradient_bgd", new Point(0, 0), new Size(431, 275));
			Grid = new Sprite("pause_menu_pages_char_mom_dad", "nose_grid", new Point(0, 0), new Size(200, 200), 0f, Color.FromArgb(255, 255, 255));
			Circle = new Sprite("mpinventory", "in_world_circle", new PointF(0f, 0f), new SizeF(20f, 20f), 0f, Color.FromArgb(255, 255, 255));
			Audio = new UIMenuGridAudio("CONTINUOUS_SLIDER", "HUD_FRONTEND_DEFAULT_SOUNDSET", 0);
			Top = new UIResText(TopText ?? "Up", new Point(0, 0), 0.35f, Color.FromArgb(255, 255, 255), (Font)0, (Alignment)0);
			Left = new UIResText(LeftText ?? "Left", new Point(0, 0), 0.35f, Color.FromArgb(255, 255, 255), (Font)0, (Alignment)0);
			Right = new UIResText(RightText ?? "Right", new Point(0, 0), 0.35f, Color.FromArgb(255, 255, 255), (Font)0, (Alignment)0);
			Bottom = new UIResText(BottomText ?? "Down", new Point(0, 0), 0.35f, Color.FromArgb(255, 255, 255), (Font)0, (Alignment)0);
			SetCirclePosition = new PointF((circlePosition.X != 0f) ? circlePosition.X : 0.5f, (circlePosition.Y != 0f) ? circlePosition.Y : 0.5f);
		}

		internal override void Position(float y)
		{
			float ParentOffsetX = base.ParentItem.Offset.X;
			int ParentOffsetWidth = base.ParentItem.Parent.WidthOffset;
			Background.Position = new PointF(ParentOffsetX, y);
			Grid.Position = new PointF(ParentOffsetX + 115.5f + (float)(ParentOffsetWidth / 2), 37.5f + y);
			Top.Position = new PointF(ParentOffsetX + 215.5f + (float)(ParentOffsetWidth / 2), 5f + y);
			Left.Position = new PointF(ParentOffsetX + 57.75f + (float)(ParentOffsetWidth / 2), 120f + y);
			Right.Position = new PointF(ParentOffsetX + 373.25f + (float)(ParentOffsetWidth / 2), 120f + y);
			Bottom.Position = new PointF(ParentOffsetX + 215.5f + (float)(ParentOffsetWidth / 2), 240f + y);
			if (!CircleLocked)
			{
				CircleLocked = true;
				CirclePosition = SetCirclePosition;
			}
		}

		internal void UpdateParent(float X, float Y)
		{
			base.ParentItem.Parent.ListChange(base.ParentItem, base.ParentItem.Index);
			base.ParentItem.ListChangedTrigger(base.ParentItem.Index);
		}

		internal async void Functions()
		{
			PointF safezoneOffset = ScreenTools.SafezoneBounds;
			if (ScreenTools.IsMouseInBounds(new PointF(Grid.Position.X + 20f + safezoneOffset.X, Grid.Position.Y + 20f + safezoneOffset.Y), new SizeF(Grid.Size.Width - 40f, Grid.Size.Height - 40f)) && API.IsDisabledControlPressed(0, 24) && !Pressed)
			{
				Pressed = true;
				Audio.Id = API.GetSoundId();
				API.PlaySoundFrontend(Audio.Id, Audio.Slider, Audio.Library, true);
				while (API.IsDisabledControlPressed(0, 24) && ScreenTools.IsMouseInBounds(new PointF(Grid.Position.X + 20f + safezoneOffset.X, Grid.Position.Y + 20f + safezoneOffset.Y), new SizeF(Grid.Size.Width - 40f, Grid.Size.Height - 40f)))
				{
					await BaseScript.Delay(0);
					SizeF res = ScreenTools.ResolutionMaintainRatio;
					float mouseX = API.GetDisabledControlNormal(0, 239) * res.Width;
					float mouseY = API.GetDisabledControlNormal(0, 240) * res.Height;
					mouseX -= Circle.Size.Width / 2f + safezoneOffset.X;
					mouseY -= Circle.Size.Height / 2f + safezoneOffset.Y;
					PointF Position = new PointF((mouseX > Grid.Position.X + 10f + Grid.Size.Width - 40f) ? (Grid.Position.X + 10f + Grid.Size.Width - 40f) : ((mouseX < Grid.Position.X + 20f - Circle.Size.Width / 2f) ? (Grid.Position.X + 20f - Circle.Size.Width / 2f) : mouseX), (mouseY > Grid.Position.Y + 10f + Grid.Size.Height - 40f) ? (Grid.Position.Y + 10f + Grid.Size.Height - 40f) : ((mouseY < Grid.Position.Y + 20f - Circle.Size.Height / 2f) ? (Grid.Position.Y + 20f - Circle.Size.Height / 2f) : mouseY));
					Circle.Position = Position;
					float resultX = (float)Math.Round((Circle.Position.X - (Grid.Position.X + 20f) + (Circle.Size.Width + 20f)) / (Grid.Size.Width - 40f), 2) + safezoneOffset.X;
					float resultY = (float)Math.Round((Circle.Position.Y - (Grid.Position.Y + 20f) + (Circle.Size.Height + 20f)) / (Grid.Size.Height - 40f), 2) + safezoneOffset.Y;
					UpdateParent(((resultX >= 0f && resultX <= 1f) ? resultX : (((resultX <= 0f) ? 0f : 1f) * 2f)) - 1f, ((resultY >= 0f && resultY <= 1f) ? resultY : (((resultY <= 0f) ? 0f : 1f) * 2f)) - 1f);
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
				Background.Size = new Size(431 + base.ParentItem.Parent.WidthOffset, 275);
				Background.Draw();
				Grid.Draw();
				Circle.Draw();
				((Text)Top).Draw();
				((Text)Left).Draw();
				((Text)Right).Draw();
				((Text)Bottom).Draw();
				Functions();
				await Task.FromResult(0);
			}
		}
	}
}
