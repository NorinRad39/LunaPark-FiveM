using CitizenRectangle = CitizenFX.Core.UI.Rectangle;
using System.Drawing;
using CitizenFX.Core.UI;
using CitizenFXFont = CitizenFX.Core.UI.Font;

namespace Client.net.LunaPark
{
	public class UITimerBarProgressItem : UITimerBarItem
	{
		public UIResRectangle BackgroundProgressBar;

		public UIResRectangle ProgressBar;

		protected float _max;

		protected float _value;

		protected int _multiplier = 5;

		public float Percentage
		{
			get
			{
				return _value;
			}
			set
			{
				if (value > _max)
				{
					_value = _max;
				}
				else if (value < 0f)
				{
					_value = 0f;
				}
				else
				{
					_value = value;
				}
				((CitizenRectangle)ProgressBar).Size = new SizeF(150f / _max * _value, ((CitizenRectangle)ProgressBar).Size.Height);
			}
		}

		public UITimerBarProgressItem(string text, float max, float starting, Color color)
			: base(text, color)
		{
			_max = max;
			_value = starting;
			Background = new Sprite("timerbars", "all_black_bg", new PointF(0f, 0f), new SizeF(350f, 35f), 0f, color);
			Text = new UIResText((text != string.Empty) ? text : "N/A", new PointF(0f, 0f), 0.35f, Color.FromArgb(255, 255, 255, 255), (CitizenFXFont)0, (Alignment)2);
			BackgroundProgressBar = new UIResRectangle(new PointF(0f, 0f), new SizeF(150f, 17f), Color.FromArgb(100, 255, 0, 0));
			ProgressBar = new UIResRectangle(new PointF(0f, 0f), new SizeF(0f, 17f), Color.FromArgb(255, 255, 0, 0));
			Position = new PointF(1580f, 1082f);
		}

		public override async void Draw(float offset)
		{
			if (Enabled)
			{
				base.Draw(offset);
				((CitizenRectangle)BackgroundProgressBar).Position = new PointF(Position.X + 190f, Position.Y - offset + 10f);
				((CitizenRectangle)ProgressBar).Position = new PointF(Position.X + 190f, Position.Y - offset + 10f);
				((CitizenRectangle)BackgroundProgressBar).Draw();
				((CitizenRectangle)ProgressBar).Draw();
			}
		}
	}
}
