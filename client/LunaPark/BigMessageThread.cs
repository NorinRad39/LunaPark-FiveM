using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using Client.net.LunaPark;

namespace Client.net.LunaPark
{
	public class BigMessageThread : BaseScript
	{
		public static BigMessageHandler MessageInstance { get; set; }

		public BigMessageThread()
		{
			MessageInstance = new BigMessageHandler();
			this.Tick += BigMessageThread_Tick;
		}

		private async Task BigMessageThread_Tick()
		{
			await Task.Yield();
			MessageInstance.Update();
		}
	}
}
