using System;
using System.Drawing;
using System.Threading.Tasks;
using CitizenFX.Core.UI;
using Font = CitizenFX.Core.UI.Font; // Ajout de cette ligne pour lever l'ambigu�t�

namespace Client.net.LunaPark
{
	[Obsolete("UIMenuColoredItem is deprecated: use UIMenuItem(text, description, mainColor, hilightColor) instead", true)]
	public class UIMenuColoredItem : UIMenuItem
	{
		public new Color MainColor { get; set; }

		public new Color HighlightColor { get; set; }

		public new Color TextColor { get; set; }

		public new Color HighlightedTextColor { get; set; }

		public UIMenuColoredItem(string label, Color color, Color highlightColor)
			: base(label, "")
		{
			MainColor = color;
			HighlightColor = highlightColor;
			TextColor = Colors.White;
			HighlightedTextColor = Colors.Black;
			Init();
		}

		public UIMenuColoredItem(string label, string description, Color color, Color highlightColor)
			: base(label, description)
		{
			MainColor = color;
			HighlightColor = highlightColor;
			TextColor = Colors.White;
			HighlightedTextColor = Colors.Black;
			Init();
		}

		protected void Init()
		{
			_selectedSprite = new Sprite("commonmenu", "gradient_nav", new PointF(0f, 0f), new SizeF(431f, 38f), 0f, HighlightColor);
			_rectangle = new UIResRectangle(new PointF(0f, 0f), new SizeF(431f, 38f), Color.FromArgb(150, 0, 0, 0));
			_text = new UIResText(base.Text, new PointF(8f, 0f), 0.33f, Colors.WhiteSmoke, (Font)0, (Alignment)1);
			Description = Description;
			_badgeLeft = new Sprite("commonmenu", "", new PointF(0f, 0f), new SizeF(40f, 40f));
			_badgeRight = new Sprite("commonmenu", "", new PointF(0f, 0f), new SizeF(40f, 40f));
			_labelText = new UIResText("", new PointF(0f, 0f), 0.35f)
			{
				TextAlignment = (Alignment)2
			};
		}

		public override async Task Draw()
		{
			_rectangle.Size = new SizeF(431 + base.Parent.WidthOffset, 38f);
			_selectedSprite.Size = new SizeF(431 + base.Parent.WidthOffset, 38f);
			if (Hovered && !Selected)
			{
				_rectangle.Color = Color.FromArgb(20, 255, 255, 255);
				_rectangle.Draw();
			}
			if (Selected)
			{
				_selectedSprite.Color = HighlightColor;
				_selectedSprite.Draw();
			}
			else
			{
				_selectedSprite.Color = MainColor;
				_selectedSprite.Draw();
			}
			_text.Color = (!Enabled) ? Color.FromArgb(163, 159, 148) : (Selected ? HighlightedTextColor : TextColor);
			if (LeftBadge != 0)
			{
				_text.Position = new PointF(35f + base.Offset.X, _text.Position.Y);
				_badgeLeft.TextureDict = UIMenuItem.BadgeToSpriteLib(LeftBadge);
				_badgeLeft.TextureName = UIMenuItem.BadgeToSpriteName(LeftBadge, Selected);
				_badgeLeft.Color = UIMenuItem.BadgeToColor(LeftBadge, Selected);
				_badgeLeft.Draw();
			}
			else
			{
				_text.Position = new PointF(8f + base.Offset.X, _text.Position.Y);
			}
			if (RightBadge != 0)
			{
				_badgeRight.Position = new PointF(385f + base.Offset.X + (float)base.Parent.WidthOffset, _badgeRight.Position.Y);
				_badgeRight.TextureDict = UIMenuItem.BadgeToSpriteLib(RightBadge);
				_badgeRight.TextureName = UIMenuItem.BadgeToSpriteName(RightBadge, Selected);
				_badgeRight.Color = UIMenuItem.BadgeToColor(RightBadge, Selected);
				_badgeRight.Draw();
			}
			if (!string.IsNullOrWhiteSpace(RightLabel))
			{
				_labelText.Position = new PointF(420f + base.Offset.X + (float)base.Parent.WidthOffset, _labelText.Position.Y);
				_labelText.Caption = RightLabel;
				UIResText labelText = _labelText;
				Color color;
				color = ((!Enabled) ? Color.FromArgb(163, 159, 148) : (Selected ? HighlightedTextColor : TextColor));
				_text.Color = color;
				labelText.Color = color;
				((Text)_labelText).Draw();
			}
			((Text)_text).Draw();
		}
	}
}
