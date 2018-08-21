using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despair
{
    /// <summary>
    /// static class to manage the console game theme
    /// </summary>
    public static class ConsoleTheme
    {
        //
        // splash screen colors
        //
        public static ConsoleColor SplashScreenBackgroundColor = ConsoleColor.Black;
        public static ConsoleColor SplashScreenForegroundColor = ConsoleColor.Green;

        //
        // main console window colors
        //
        public static ConsoleColor WindowBackgroundColor = ConsoleColor.Black;
        public static ConsoleColor WindowForegroundColor = ConsoleColor.White;

        //
        // console window header colors
        //
        public static ConsoleColor HeaderBackgroundColor = ConsoleColor.DarkRed;
        public static ConsoleColor HeaderForegroundColor = ConsoleColor.White;

        //
        // console window footer colors
        //
        public static ConsoleColor FooterBackgroundColor = ConsoleColor.DarkRed;
        public static ConsoleColor FooterForegroundColor = ConsoleColor.White;

        //
        // menu box colors
        //
        public static ConsoleColor MenuBackgroundColor = ConsoleColor.Black;
        public static ConsoleColor MenuForegroundColor = ConsoleColor.Green;
        public static ConsoleColor MenuBorderColor = ConsoleColor.White;
        public static ConsoleColor MenuHeaderBackgroundColor = ConsoleColor.Black;
        public static ConsoleColor MenuHeaderForegroundColor = ConsoleColor.White;

        //
        // message box colors
        //
        public static ConsoleColor MessageBoxBackgroundColor = ConsoleColor.Black;
        public static ConsoleColor MessageBoxForegroundColor = ConsoleColor.White;
        public static ConsoleColor MessageBoxBorderColor = ConsoleColor.Black;
        public static ConsoleColor MessageBoxHeaderBackgroundColor = ConsoleColor.DarkRed;
        public static ConsoleColor MessageBoxHeaderForegroundColor = ConsoleColor.Red;

        //
        // status box colors
        //
        public static ConsoleColor StatusBoxBackgroundColor = ConsoleColor.Black;
        public static ConsoleColor StatusBoxForegroundColor = ConsoleColor.White;
        public static ConsoleColor StatusBoxBorderColor = ConsoleColor.White;
        public static ConsoleColor StatusBoxHeaderBackgroundColor = ConsoleColor.Black;
        public static ConsoleColor StatusBoxHeaderForegroundColor = ConsoleColor.White;

        //
        // input box colors
        //
        public static ConsoleColor InputBoxBackgroundColor = ConsoleColor.Black;
        public static ConsoleColor InputBoxForegroundColor = ConsoleColor.White;
        public static ConsoleColor InputBoxErrorMessageForegroundColor = ConsoleColor.Red;
        public static ConsoleColor InputBoxBorderColor = ConsoleColor.DarkRed;
        public static ConsoleColor InputBoxHeaderBackgroundColor = ConsoleColor.DarkRed;
        public static ConsoleColor InputBoxHeaderForegroundColor = ConsoleColor.White;
    }
}