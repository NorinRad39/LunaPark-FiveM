using System;
using System.Drawing;
using System.Threading.Tasks;
using CitizenFX.Core.UI;
using Font = CitizenFX.Core.UI.Font;

namespace Client.net.LunaPark
{
	public class UIMenuDynamicListItem : UIMenuItem, IListItem
	{
		public enum ChangeDirection
		{
			Left,
			Right
		}

		public delegate string DynamicListItemChangeCallback(UIMenuDynamicListItem sender, ChangeDirection direction);

		protected UIResText _itemText;

		protected Sprite _arrowLeft;

		protected Sprite _arrowRight;

		public string CurrentListItem { get; internal set; }

		public DynamicListItemChangeCallback Callback { get; set; }

		public UIMenuDynamicListItem(string text, string startingItem, DynamicListItemChangeCallback changeCallback)
			: this(text, null, startingItem, changeCallback)
		{
		}

		public UIMenuDynamicListItem(string text, string description, string startingItem, DynamicListItemChangeCallback changeCallback)
			: base(text, description)
		{
			_arrowLeft = new Sprite("commonmenu", "arrowleft", new PointF(110f, 105f), new SizeF(30f, 30f));
			_arrowRight = new Sprite("commonmenu", "arrowright", new PointF(280f, 105f), new SizeF(30f, 30f));
			_itemText = new UIResText("", new PointF(290f, 104f), 0.35f, Colors.White, (Font)0, (Alignment)2);
			CurrentListItem = startingItem;
			Callback = changeCallback;
		}

		public override void Position(int y)
		{
			_arrowLeft.Position = new PointF(300f + base.Offset.X + (float)base.Parent.WidthOffset, (float)(147 + y) + base.Offset.Y);
			_arrowRight.Position = new PointF(400f + base.Offset.X + (float)base.Parent.WidthOffset, (float)(147 + y) + base.Offset.Y);
			_itemText.Position = new PointF(300f + base.Offset.X + (float)base.Parent.WidthOffset, (float)(y + 147) + base.Offset.Y);
			base.Position(y);
		}

		public override async Task Draw()
		{
			base.Draw();
			string caption = CurrentListItem;
			float offset = ScreenTools.GetTextWidth(caption, _itemText.Font, _itemText.Scale);
			_itemText.Color = (!Enabled) ? Color.FromArgb(163, 159, 148) : (Selected ? Colors.Black : Colors.WhiteSmoke);
			_itemText.Caption = caption;
			_arrowLeft.Color = ((!Enabled) ? Color.FromArgb(163, 159, 148) : (Selected ? Colors.Black : Colors.WhiteSmoke));
			_arrowRight.Color = ((!Enabled) ? Color.FromArgb(163, 159, 148) : (Selected ? Colors.Black : Colors.WhiteSmoke));
			_arrowLeft.Position = new PointF((float)(375 - (int)offset) + base.Offset.X + (float)base.Parent.WidthOffset, _arrowLeft.Position.Y);
			if (Selected)
			{
				_arrowLeft.Draw();
				_arrowRight.Draw();
				_itemText.Position = new PointF(403f + base.Offset.X + (float)base.Parent.WidthOffset, _itemText.Position.Y);
			}
			else
			{
				_itemText.Position = new PointF(418f + base.Offset.X + (float)base.Parent.WidthOffset, _itemText.Position.Y);
			}
			_itemText.Draw();
		}

		public override void SetRightBadge(BadgeStyle badge)
		{
			throw new Exception("UIMenuListItem cannot have a right badge.");
		}

		public override void SetRightLabel(string text)
		{
			throw new Exception("UIMenuListItem cannot have a right label.");
		}

		public string CurrentItem()
		{
			return CurrentListItem;
		}
	}
}
