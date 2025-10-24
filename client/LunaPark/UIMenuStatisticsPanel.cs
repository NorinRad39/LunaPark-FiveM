using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using CitizenFX.Core.UI;
using Font = CitizenFX.Core.UI.Font; // Ajout de cette ligne pour lever l'ambiguïté

namespace Client.net.LunaPark
{
	public class UIMenuStatisticsPanel : UIMenuPanel
	{
		private List<StatisticsForPanel> Items = new List<StatisticsForPanel>();

		private bool Divider;

		public UIMenuStatisticsPanel()
		{
			Background = new UIResRectangle(new Point(0, 0), new Size(431, 47), Color.FromArgb(170, 0, 0, 0));
			Divider = true;
		}

		public void AddStatistics(string Name)
		{
			StatisticsForPanel statisticsForPanel = new StatisticsForPanel();
			statisticsForPanel.Text = new UIResText(Name ?? "", new Point(0, 0), 0.35f, Color.FromArgb(255, 255, 255), (Font)0, (Alignment)1);
			statisticsForPanel.BackgroundProgressBar = new UIResRectangle(new Point(0, 0), new Size(200, 10), Color.FromArgb(100, 255, 255, 255));
			statisticsForPanel.ProgressBar = new UIResRectangle(new Point(0, 0), new Size(100, 10), Color.FromArgb(255, 255, 255, 255));
			statisticsForPanel.Divider = new UIResRectangle[5]
			{
				new UIResRectangle(new Point(0, 0), new Size(2, 10), Color.FromArgb(255, 0, 0, 0)),
				new UIResRectangle(new Point(0, 0), new Size(2, 10), Color.FromArgb(255, 0, 0, 0)),
				new UIResRectangle(new Point(0, 0), new Size(2, 10), Color.FromArgb(255, 0, 0, 0)),
				new UIResRectangle(new Point(0, 0), new Size(2, 10), Color.FromArgb(255, 0, 0, 0)),
				new UIResRectangle(new Point(0, 0), new Size(2, 10), Color.FromArgb(255, 0, 0, 0))
			};
			StatisticsForPanel items = statisticsForPanel;
			Items.Add(items);
		}

		public float GetPercentage(int ItemId)
		{
			return ((CitizenFX.Core.UI.Rectangle)Items[ItemId].ProgressBar).Size.Width * 2f;
		}

		public void SetPercentage(int ItemId, float number)
		{
			if (number <= 0f)
			{
				((CitizenFX.Core.UI.Rectangle)Items[ItemId].ProgressBar).Size = new SizeF(0f, ((CitizenFX.Core.UI.Rectangle)Items[ItemId].ProgressBar).Size.Height);
			}
			else if (number <= 100f)
			{
				((CitizenFX.Core.UI.Rectangle)Items[ItemId].ProgressBar).Size = new SizeF(number * 2f, ((CitizenFX.Core.UI.Rectangle)Items[ItemId].ProgressBar).Size.Height);
			}
			else
			{
				((CitizenFX.Core.UI.Rectangle)Items[ItemId].ProgressBar).Size = new SizeF(200f, ((CitizenFX.Core.UI.Rectangle)Items[ItemId].ProgressBar).Size.Height);
			}
		}

		internal override void Position(float y)
		{
			float ParentOffsetX = base.ParentItem.Offset.X;
			int ParentOffsetWidth = base.ParentItem.Parent.WidthOffset;
			Background.Position = new PointF(ParentOffsetX, y);
			for (int i = 0; i < Items.Count; i++)
			{
				int OffsetItemCount = 40 * (i + 1);
				((Text)Items[i].Text).Position = new PointF(ParentOffsetX + (float)(ParentOffsetWidth / 2) + 13f, y - 34f + (float)OffsetItemCount);
				((CitizenFX.Core.UI.Rectangle)Items[i].BackgroundProgressBar).Position = new PointF(ParentOffsetX + (float)(ParentOffsetWidth / 2) + 200f, y - 22f + (float)OffsetItemCount);
				((CitizenFX.Core.UI.Rectangle)Items[i].ProgressBar).Position = new PointF(ParentOffsetX + (float)(ParentOffsetWidth / 2) + 200f, y - 22f + (float)OffsetItemCount);
				if (Divider)
				{
					for (int _ = 0; _ < Items[i].Divider.Length; _++)
					{
						int DividerOffsetWidth = _ * 40;
						((CitizenFX.Core.UI.Rectangle)Items[i].Divider[_]).Position = new PointF(ParentOffsetX + (float)(ParentOffsetWidth / 2) + 200f + (float)DividerOffsetWidth, y - 22f + (float)OffsetItemCount);
						Background.Size = new SizeF(431 + base.ParentItem.Parent.WidthOffset, 47 + OffsetItemCount - 39);
					}
				}
			}
		}

		internal override async Task Draw()
		{
			Background.Draw();
			for (int i = 0; i < Items.Count; i++)
			{
				((Text)Items[i].Text).Draw();
				((CitizenFX.Core.UI.Rectangle)Items[i].BackgroundProgressBar).Draw();
				((CitizenFX.Core.UI.Rectangle)Items[i].ProgressBar).Draw();
				for (int _ = 0; _ < Items[i].Divider.Length; _++)
				{
					((CitizenFX.Core.UI.Rectangle)Items[i].Divider[_]).Draw();
				}
			}
			await Task.FromResult(0);
		}
	}
}
