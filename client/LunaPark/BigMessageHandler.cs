using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;

namespace Client.net.LunaPark
{
	public class BigMessageHandler
	{
		private Scaleform _sc;

		private int _start;

		private int _timer;

		public async Task Load()
		{
			if (_sc == null)
			{
				_sc = new Scaleform("MP_BIG_MESSAGE_FREEMODE");
				int timeout = 1000;
				DateTime start = DateTime.Now;
				while (!API.HasScaleformMovieLoaded(_sc.Handle) && DateTime.Now.Subtract(start).TotalMilliseconds < (double)timeout)
				{
					await BaseScript.Delay(0);
				}
			}
		}

		public void Dispose()
		{
			// Libère le scaleform (SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED)
			Function.Call((Hash)2095068147598518289L, new OutputArgument(_sc.Handle));
			_sc = null;
		}

		public async void ShowMissionPassedMessage(string msg, string sub, int time = 5000)
		{
			await Load();
			_start = Game.GameTime;
			_sc.CallFunction("SHOW_MISSION_PASSED_MESSAGE", [msg, sub, 100, true, 0, true]);
			_timer = time;
		}

		public async void ShowColoredShard(string msg, string desc, HudColor textColor, HudColor bgColor, int time = 5000)
		{
			await Load();
			_start = Game.GameTime;
			_sc.CallFunction("SHOW_SHARD_CENTERED_MP_MESSAGE", [msg, desc, (int)bgColor, (int)textColor]);
			_timer = time;
		}

		public async void ShowOldMessage(string msg, int time = 5000)
		{
			await Load();
			_start = Game.GameTime;
			_sc.CallFunction("SHOW_MISSION_PASSED_MESSAGE", [msg]);
			_timer = time;
		}

		public async void ShowSimpleShard(string title, string subtitle, int time = 5000)
		{
			await Load();
			_start = Game.GameTime;
			_sc.CallFunction("SHOW_SHARD_CREW_RANKUP_MP_MESSAGE", [title, subtitle]);
			_timer = time;
		}

		public async void ShowRankupMessage(string msg, string subtitle, int rank, int time = 5000)
		{
			await Load();
			_start = Game.GameTime;
			_sc.CallFunction("SHOW_BIG_MP_MESSAGE", [msg, subtitle, rank, "", ""]);
			_timer = time;
		}

		public async void ShowWeaponPurchasedMessage(string bigMessage, string weaponName, WeaponHash weapon, int time = 5000)
		{
			await Load();
			_start = Game.GameTime;
			_sc.CallFunction("SHOW_WEAPON_PURCHASED", [bigMessage, weaponName, (int)weapon, "", 100]);
			_timer = time;
		}

		public async void ShowMpMessageLarge(string msg, string sub, int time = 5000)
		{
			await Load();
			_start = Game.GameTime;
			_sc.CallFunction("SHOW_CENTERED_MP_MESSAGE_LARGE", [msg, sub, 100, true, 100]);
			_sc.CallFunction("TRANSITION_IN", []);
			_timer = time;
		}

		public async void ShowMpWastedMessage(string msg, string sub, int time = 5000)
		{
			await Load();
			_start = Game.GameTime;
			_sc.CallFunction("SHOW_SHARD_WASTED_MP_MESSAGE", [msg, sub]);
			_timer = time;
		}

		public async void ShowCustomShard(string funcName, params object[] paremeters)
		{
			await Load();
			_sc.CallFunction(funcName, paremeters);
		}

		internal void Update()
		{
			if (_sc != null)
			{
				_sc.Render2D();
				if (_start != 0 && Game.GameTime - _start > _timer)
				{
					_sc.CallFunction("TRANSITION_OUT", []);
					_start = 0;
					Dispose();
				}
			}
		}
	}
}
