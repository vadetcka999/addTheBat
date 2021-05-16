using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        public static IntPtr hwndMenuGlobal;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);


        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, Delegate lpEnumFunc, int lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(IntPtr hwnd, String lpString);

        public static IntPtr MakeLParam(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
        }
                [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet= CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        public static UInt32 WM_MOUSEMOVE = 0x0200;
        public static UInt32 MK_LBUTTON = 0x0001;
        public static UInt32 WM_LBUTTONDOWN = 0x0201;
        public static UInt32 WM_LBUTTONUP = 0x0202;
        public static UInt32 WM_CHAR = 0x0102;
        public static UInt32 WM_NCLBUTTONUP = 0x00A2;
        public static UInt32 WM_NCLBUTTONDOWN = 0x00A1;
        public static UInt32 WM_DESTROY = 0x0002;
        public static UInt32 WM_NCDESTROY = 0x0082;
        public static UInt32 WM_CLOSE = 0x0010;
        public static UInt32 WM_COMMAND = 0x0111;
        public static int BN_CLICKED = 245;
        public static int IDOK = 1;
        public static UInt32 WM_PARENTNOTIFY = 0x0210;
        public static UInt32 WM_MOUSEACTIVATE = 0x0021;
        public static UInt32 CB_SETCURSEL = 0x014E;

        public static void RemoveFirstLineFromFile(string inputFile, string outputFile)
        {
            string[] lines = File.ReadAllLines(inputFile);
            File.WriteAllLines(outputFile, lines.Skip(1));
        }
        static void Main(string[] args)
        {

            //Console.Write("Введите имя: ");
            //string name = Console.ReadLine();

            do
            {
                string path = @"emails.txt";
                StreamReader sr = File.OpenText(path);
                String s = sr.ReadLine();

                string[] strok = File.ReadAllLines("emails.txt");

                if (strok.Length == 0)
                {
                    Console.WriteLine("Файл пуст");
                    Console.Read();
                    Environment.Exit(0);

                }
                
                string[] logPass = s.Split(':');
                                
                Console.WriteLine(logPass[0], logPass[1]);
                sr.Close();
                IntPtr hwndDesktopTop = GetDesktopWindow();
                
                IntPtr hwndTheBat = FindWindow("TMailerForm", null);//хэндл бата
                activateWind(hwndTheBat, 3);
                IntPtr hwndDesktop = getHwndBynName.getHwndByName(IntPtr.Zero, "", "");//хэндл рабочего стола

                IntPtr hwndMenu = getHwndBynName.getHwndByName(hwndTheBat, "TSpTBXToolbar", "Главное меню");//хэндл блока меню
                clickMouseNotUp(hwndMenu, 230, 20);

                Thread.Sleep(100);
                IntPtr hwndListmenu = getHwndBynName.getHwndByName(hwndDesktopTop, "TSpTBXPopupWindowS", "");//хэнд выпадающего меню почта
                if (hwndListmenu.Equals(0))
                {
                    clickMouseNotUp(hwndMenu, 230, 20);
                }
                clickMouse(hwndListmenu, 374, 70);
                
                Thread.Sleep(3000);

                
                IntPtr hwndWindNewEmail = getHwndBynName.getHwndByName(hwndDesktopTop, "TNewAccountCreator", "Создание нового почтового ящика"); //хэндл окна нового эмейла
                IntPtr hwndWindNewEmailSmallWindow = getHwndBynName.getHwndByName(hwndWindNewEmail, "TTabSheet", "Основное");

                IntPtr hwndWindNewEmailEdit = getHwndBynName.getHwndByName(hwndWindNewEmailSmallWindow, "Edit", ""); //хэндл поля ввода
                IntPtr hwndWindNewEmailTEdit = getHwndBynName.getHwndByName(hwndWindNewEmail, "TEdit", "");
                activateWind(hwndWindNewEmail, 1);

                Thread.Sleep(1000);
                IntPtr hwndWindNewEmailButtonOk = getHwndBynName.getHwndByName(hwndWindNewEmail, "TButton", "&Далее   >");
                List<IntPtr> hwndWindNewEmailEditList = getHwndBynName.getHwndByNameList(hwndWindNewEmailSmallWindow, "Edit", "");

                writeWind(hwndWindNewEmailEditList.ElementAt(0), logPass[4]);
                writeWind(hwndWindNewEmailEditList.ElementAt(1), logPass[0]);
                writeWind(hwndWindNewEmailTEdit, logPass[1]);
                List<IntPtr> hwndWindNewEmailTComboBox = getHwndBynName.getHwndByNameList(hwndWindNewEmailSmallWindow, "TComboBox", "");
                //clickMouse(hwndWindNewEmailTComboBox, 1002, 515);
                Thread.Sleep(1000);
                SendMessage(hwndWindNewEmailTComboBox[0], CB_SETCURSEL, 1, 0);



                activateWind(hwndWindNewEmail, 1);

                clickMouse(hwndWindNewEmailButtonOk, 72, 7);
                IntPtr hwndWindNewEmailCheckBoxPOP = getHwndBynName.getHwndByName(hwndWindNewEmail, "TGroupButton", "POP  -  Post Office Protocol v3");

                IntPtr hwndWindNewEmailCheckBoxIMAP = getHwndBynName.getHwndByName(hwndWindNewEmail, "TGroupButton", "IMAP - Internet Mail Access Protocol v4");

                clickMouse(hwndWindNewEmailCheckBoxPOP, 931, 383);
                Thread.Sleep(1000);

                //PostMessage(hwndWindNewEmailCheckBoxPOP, WM_MOUSEMOVE, IntPtr.Zero, MakeLParam(56, 37));
                //PostMessage(hwndWindNewEmailCheckBoxPOP, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, MakeLParam(56, 37));
                //PostMessage(hwndWindNewEmailCheckBoxPOP, WM_LBUTTONUP, (IntPtr)MK_LBUTTON, MakeLParam(56, 37));

                //clickMouse(hwndWindNewEmailCheckBoxIMAP, 189, 22);
                //clickMouse(hwndWindNewEmailCheckBoxPOP, 77, 34);
                //clickMouse(hwndWindNewEmailCheckBoxIMAP, 189, 22);
                //clickMouse(hwndWindNewEmailCheckBoxPOP, 77, 34);

                clickMouse(hwndWindNewEmailButtonOk, 72, 7);
                clickMouse(hwndWindNewEmailButtonOk, 72, 7);
                Thread.Sleep(1000);
                IntPtr hwndWindNewEmailButtonReady = getHwndBynName.getHwndByName(hwndWindNewEmail, "TButton", "&Готово");
                clickMouse(hwndWindNewEmailButtonReady, 72, 7);



                //IntPtr windscribeWind = FindWindow("Qt5QWindowIcon", "Windscribe");
                //activateWind(windscribeWind, 1);
                // windscribeOffOn(windscribeWind);

               
                
                
                RemoveFirstLineFromFile("emails.txt", "emails.txt");
                Console.ReadKey();




            } while (true);
        }

        public static void writeWind(IntPtr hwnd, string text)
        {
            string number = text;

            for (int i = 0; i < number.Length; i++)
                SendMessage(hwnd, WM_CHAR, (IntPtr)number[i], (IntPtr)(MapVirtualKey('6', 0) << 16 | 1));
        }

        public static void activateWind(IntPtr hwnd, int x)
        {
            ShowWindow(hwnd, x);
            SetForegroundWindow(hwnd);
        }

        public static void clickMouseNotUp(IntPtr hWnd, int x, int y)
        {
            PostMessage(hWnd, WM_MOUSEMOVE, IntPtr.Zero, MakeLParam(x, y));
            PostMessage(hWnd, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, MakeLParam(x, y));
            //PostMessage(hWnd, WM_LBUTTONUP, (IntPtr)MK_LBUTTON, MakeLParam(x, y));

        }

        public static void windscribeOffOn(IntPtr hWnd)
        {
            PostMessage(hWnd, WM_MOUSEMOVE, (IntPtr)MK_LBUTTON, MakeLParam(312, 150));
            PostMessage(hWnd, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, MakeLParam(312, 150));
            PostMessage(hWnd, WM_LBUTTONUP, IntPtr.Zero, MakeLParam(312, 150));
            Thread.Sleep(5000);
            PostMessage(hWnd, WM_MOUSEMOVE, (IntPtr)MK_LBUTTON, MakeLParam(312, 150));
            PostMessage(hWnd, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, MakeLParam(312, 150));
            PostMessage(hWnd, WM_LBUTTONUP, IntPtr.Zero, MakeLParam(312, 150));


        }
        public static void clickMouse(IntPtr hWnd, int x, int y)
        {
            PostMessage(hWnd, WM_MOUSEMOVE, IntPtr.Zero, MakeLParam(x, y));
            PostMessage(hWnd, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, MakeLParam(x, y));
            PostMessage(hWnd, WM_LBUTTONUP, (IntPtr)MK_LBUTTON, MakeLParam(x, y));

        }
        public static void closeWindow(IntPtr hWnd)
        {
            PostMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
           
            

        }


    }
}
