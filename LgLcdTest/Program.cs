using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using LgLcd;
using LgBackLight;

namespace LgLcdTest {
	
	class Program {
        public static LogitechKeyboard LogitechKeyboard;
		static void Main(string[] args) {
			var ctrl = new LgLcdTestForm();
            LogitechKeyboard.BackLightColor = Color.HotPink;
			System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
		}
	}

}
