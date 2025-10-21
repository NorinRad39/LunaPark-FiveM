using System;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Client.net.LunaPark
{
	public static class Controls
	{
		// Liste blanche des contrôles à réactiver quand on désactive les autres.
		// Clavier/souris: navigation menu + clics + molette + épaules pour l'onglet
		private static readonly Control[] NecessaryControlsKeyboard =
		[
			// Navigation
			(Control)172, // Up
			(Control)173, // Down
			(Control)174, // Left
			(Control)175, // Right
			(Control)201, // Select/Accept
			(Control)177, // Back/Cancel
			(Control)199, // Pause/Menu Back (PC)
			(Control)205, // Shoulder Left (onglet précédent)
			(Control)206, // Shoulder Right (onglet suivant)

			// Souris
			(Control)239, // Mouse X
			(Control)240, // Mouse Y
			(Control)241, // Wheel Up
			(Control)242, // Wheel Down
			(Control)237, // Mouse LMB
			(Control)238, // Mouse RMB
		];

		// Manette: navigation menu + épaules
		private static readonly Control[] NecessaryControlsGamePad =
		[
			(Control)172, // Up
			(Control)173, // Down
			(Control)174, // Left
			(Control)175, // Right
			(Control)201, // Select/Accept (A)
			(Control)177, // Back/Cancel (B)
			(Control)205, // LB
			(Control)206, // RB
		];

		public static void Toggle(bool toggle)
		{
			if (toggle)
			{
				Game.EnableAllControlsThisFrame(2);
				return;
			}

			Game.DisableAllControlsThisFrame(2);

			Control[] list = (Game.CurrentInputMode == InputMode.GamePad)
				? NecessaryControlsGamePad
				: NecessaryControlsKeyboard;

			foreach (Control control in list)
			{
				API.EnableControlAction(0, (int)control, true);
			}
		}
	}
}
