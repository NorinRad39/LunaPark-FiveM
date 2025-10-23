using System;
using System.Drawing;
using System.Text;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using GdiFont = System.Drawing.Font;
using UiFont = CitizenFX.Core.UI.Font; // utiliser GdiFont ou UiFont pour lever l'ambiguïté

namespace Client.net.LunaPark
{
	public class UIResText : Text
	{
		public Alignment TextAlignment { get; set; }

		public float Wrap { get; set; } = 0f;


		[Obsolete("Use UIResText.Wrap instead.", true)]
		public SizeF WordWrap
		{
			get
			{
				return new SizeF(Wrap, 0f);
			}
			set
			{
				Wrap = value.Width;
			}
		}

		public UIResText(string caption, PointF position, float scale)
			: base(caption, position, scale)
		{
			TextAlignment = (Alignment)1;
		}

		public UIResText(string caption, PointF position, float scale, Color color)
			: base(caption, position, scale, color)
		{
			TextAlignment = (Alignment)1;
		}

		public UIResText(string caption, PointF position, float scale, Color color, UiFont font, Alignment justify)
			: base(caption, position, scale, color, font)
		{
			TextAlignment = justify;
		}

		public static void AddLongString(string str)
		{
			int utf8ByteCount = Encoding.UTF8.GetByteCount(str);
			if (utf8ByteCount == str.Length)
			{
				AddLongStringForAscii(str);
			}
			else
			{
				AddLongStringForUtf8(str);
			}
		}

		private static void AddLongStringForAscii(string input)
		{
			for (int i = 0; i < input.Length; i += 99)
			{
				string substr = input.Substring(i, Math.Min(99, input.Length - i));
				API.AddTextComponentString(substr);
			}
		}

		internal static void AddLongStringForUtf8(string input)
		{
			bool flag = false;
			if (string.IsNullOrEmpty(input))
			{
				return;
			}
			Encoding enc = Encoding.UTF8;
			int utf8ByteCount = enc.GetByteCount(input);
			if (utf8ByteCount < 99)
			{
				API.AddTextComponentString(input);
				return;
			}
			int startIndex = 0;
			for (int i = 0; i < input.Length; i++)
			{
				int length = i - startIndex;
				if (enc.GetByteCount(input.Substring(startIndex, length)) > 99)
				{
					string substr = input.Substring(startIndex, length - 1);
					API.AddTextComponentString(substr);
					i--;
					startIndex = startIndex + length - 1;
				}
			}
			API.AddTextComponentString(input.Substring(startIndex, input.Length - startIndex));
		}

		[Obsolete("Use ScreenTools.GetTextWidth instead.", true)]
		public static float MeasureStringWidth(string str, UiFont font, float scale)
		{
			return ScreenTools.GetTextWidth(str, font, scale);
		}

		[Obsolete("Use ScreenTools.GetTextWidth instead.", true)]
		public static float MeasureStringWidthNoConvert(string str, UiFont font, float scale)
		{
			return ScreenTools.GetTextWidth(str, font, scale);
		}

		public override void Draw(SizeF offset)
		{
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Expected I4, but got Unknown
			//IL_00db: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ef: Invalid comparison between Unknown and I4
			int screenw = Screen.Resolution.Width;
			int screenh = Screen.Resolution.Height;
			float ratio = (float)screenw / (float)screenh;
			float width = 1080f * ratio;
			float x = Position.X / width;
			float y = Position.Y / 1080f;
			API.SetTextFont((int)this.Font);
			API.SetTextScale(1f, this.Scale);
			API.SetTextColour(this.Color.R, this.Color.G, this.Color.B, this.Color.A);
			if (this.Shadow)
			{
				API.SetTextDropShadow();
			}
			if (this.Outline)
			{
				API.SetTextOutline();
			}
			Alignment textAlignment = TextAlignment;
			Alignment val = textAlignment;
			if ((int)val != 0)
			{
				if ((int)val == 2)
				{
					API.SetTextRightJustify(true);
					API.SetTextWrap(0f, x);
				}
			}
			else
			{
				API.SetTextCentre(true);
			}
			if (Wrap != 0f)
			{
				float xsize = (Position.X + Wrap) / width;
				API.SetTextWrap(x, xsize);
			}
			API.SetTextEntry("jamyfafi");
			AddLongString(Caption);
			API.DrawText(x, y);
		}
	}
}
