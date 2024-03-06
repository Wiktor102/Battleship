using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship {
	internal class Settings {
		public static Dictionary<int, int> ShipsConfiguration { get; private set; } = new Dictionary<int, int> {
			[1] = 4,
			[2] = 3,
			[3] = 2,
			[4] = 1,
		};

		private int _activeSetting = 0;
		private List<SettingRow> _settings = new List<SettingRow> {
			new ShipAmountSetting(1),
			new ShipAmountSetting(2),
			new ShipAmountSetting(3),
			new ShipAmountSetting(4),
			new SettingRow("Powrót"),
		};

		public Settings() {
			do {
				Display();
			} while (ReadKey());
		}

		private void Display() {
			Console.Clear();
			IO.DisplayTitle(new [] {
				"  _   _     _                 _            _         ",
				" | | | |___| |_ __ ___      _(_) ___ _ __ (_) __ _ _ ",
				" | | | / __| __/ _` \\ \\ /\\ / / |/ _ \\ '_ \\| |/ _` (_)",
				" | |_| \\__ \\ || (_| |\\ V  V /| |  __/ | | | | (_| |_ ",
				"  \\___/|___/\\__\\__,_| \\_/\\_/ |_|\\___|_| |_|_|\\__,_(_)\n"
			});

			for (int i = 0; i < _settings.Count; i++) {
				_settings[i].Display(_activeSetting == i);
			}

			DisplayNavigationHint();
		}

		private bool ReadKey() {
			do {
				var pressedKey = Console.ReadKey(true);
				if (_activeSetting == _settings.Count - 1 && pressedKey.Key == ConsoleKey.Enter) return false;

				if (pressedKey.Key == ConsoleKey.UpArrow) {
					_activeSetting = _activeSetting == 0 ? _settings.Count - 1 : _activeSetting - 1;
					return true;
				}

				if (pressedKey.Key == ConsoleKey.DownArrow) {
					_activeSetting = _activeSetting == _settings.Count - 1 ? 0 : _activeSetting + 1;
					return true;
				}

				if (pressedKey.Key == ConsoleKey.LeftArrow) {
					_settings[_activeSetting].ModifySetting(-1);
					return true;
				}
				
				if (pressedKey.Key == ConsoleKey.RightArrow) {
					_settings[_activeSetting].ModifySetting(1);
					return true;
				}

			} while (true);
		}

		private void DisplayNavigationHint() {
			IO.DisplayColored("\n↑/↓ nawigacja", ConsoleColor.DarkGray, ConsoleColor.Black, true);
			if (_settings[_activeSetting] is ShipAmountSetting) IO.DisplayColored("   ←/→ zmiana wartości", ConsoleColor.DarkGray, ConsoleColor.Black, true);
			else IO.DisplayColored("   ↲ wyjdź", ConsoleColor.DarkGray, ConsoleColor.Black, true);
		}

		class SettingRow {
			protected string _label;

			public SettingRow(string label) {
				_label = label;
			}

			public virtual void Display(bool active) {
				if (active) {
					IO.DisplayColored(_label, ConsoleColor.Black, ConsoleColor.White);
				} else {
					Console.WriteLine(_label);
				}
			}

			public virtual void ModifySetting(int dir) {
			}
			

		}

		class ShipAmountSetting : SettingRow {
			private int _shipSize;

			public ShipAmountSetting(int shipSize) : base($"Ilość {shipSize} masztowców") {
				_shipSize = shipSize;
			}

			public override void Display(bool active) {
				Console.Write(_label + ": ");
				var str = "◂ " + ShipsConfiguration[_shipSize] + " ▸";

				if (active) {
					IO.DisplayColored(str, ConsoleColor.Black, ConsoleColor.White);
				} else {
					Console.WriteLine(str);
				}
			}

			public override void ModifySetting(int dir) {
				if (ShipsConfiguration.Values.Sum() == 1 && dir == -1) return;
				if (ShipsConfiguration[_shipSize] == 0 && dir == -1) return;
				if (ShipsConfiguration[_shipSize] == 4 && dir == 1) return;
				ShipsConfiguration[_shipSize] += dir;
			}
		}
	}
}
