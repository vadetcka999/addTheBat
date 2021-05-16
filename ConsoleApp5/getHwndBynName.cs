using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ConsoleApp5
{
    
    class getHwndBynName
    {
        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);
        public static List<IntPtr> globalHwndList;
        public static IntPtr globalHwnd;
        public static string nameClass;
        public static string nameCaption;
        public static int i;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, Delegate lpEnumFunc, int lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        public delegate int Callback(int hWnd, int lParam);
        static Callback myCallBack = new Callback(EnumChildGetValue);

        public delegate int CallbackList(int hWnd, int lParam);
        static Callback myCallBackList = new Callback(EnumChildGetValueList);

        [DllImport("coredll.dll")]
        private static extern IntPtr GetDesktopWindow();


        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public static IntPtr getHwndByName(IntPtr hwndParents,string nameClasA, string nameCaptionA)
        {
            globalHwndList = new List<IntPtr>();
            nameClass = null;
            nameCaption = null;
            nameClass = nameClasA;
            nameCaption = nameCaptionA;
            globalHwnd = IntPtr.Zero;
            EnumChildWindows(hwndParents, myCallBack, 0);

            IntPtr hWnd;
            if (nameClasA.Equals("")&&nameCaptionA.Equals(""))
            {
                return hWnd = FindWindow("Progman", "Program Manager");
            }
            return globalHwnd;
            
        }
        public static List<IntPtr> getHwndByNameList(IntPtr hwndParents, string nameClasA, string nameCaptionA)
        {
            globalHwndList = new List<IntPtr>();
            nameClass = "";
            nameCaption = "";
            IntPtr hWnd;
            if (nameClasA.Equals("") && nameCaptionA.Equals(""))
            {
                hWnd = FindWindow("Progman", "Program Manager");
                globalHwndList.Add(hWnd);
            }
            i = 0;
            nameClass = nameClasA;
            nameCaption = nameCaptionA;
            EnumChildWindows(hwndParents, myCallBackList, 0);
            return globalHwndList;
           

        }

        public static int EnumChildGetValue(int hWnd, int lParam)
        {

            StringBuilder formDetails = new StringBuilder(256);
            StringBuilder caption = new StringBuilder(256);
            StringBuilder ClassName = new StringBuilder(256);
            GetClassName(new IntPtr(hWnd), ClassName, ClassName.Capacity);
            GetWindowText(new IntPtr(hWnd), caption, caption.Capacity);
            
            if (ClassName.ToString().Equals(nameClass) && caption.ToString().Equals(nameCaption))
            {
                globalHwnd = new IntPtr(hWnd);  
               Console.WriteLine(ClassName.ToString() + caption.ToString() + new IntPtr(hWnd).ToString()+" 1");
                
            }

        
            return 1;

        }

        public static int EnumChildGetValueList(int hWnd, int lParam)
        {

            StringBuilder formDetails = new StringBuilder(256);
            StringBuilder caption = new StringBuilder(256);
            StringBuilder ClassName = new StringBuilder(256);
            GetClassName(new IntPtr(hWnd), ClassName, ClassName.Capacity);
            GetWindowText(new IntPtr(hWnd), caption, caption.Capacity);

            if (ClassName.ToString().Equals(nameClass) && caption.ToString().Equals(nameCaption))
            {

                if (i >= 1)
                {
                    if (i > 1)
                    {
                        globalHwndList.Add(new IntPtr(hWnd));
                    }
                    else
                    {

                        globalHwndList.Add(globalHwnd);
                        globalHwndList.Add(new IntPtr(hWnd));
                    }
                }
                i++;
                globalHwnd = new IntPtr(hWnd);

                Console.WriteLine(ClassName.ToString() + caption.ToString() + new IntPtr(hWnd).ToString() + " 1");

            }     

            return 1;

        }
    }
   
}
