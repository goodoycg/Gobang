using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Drawing;

namespace SucceedSoft.Gobang
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //声明互斥体。
            bool initiallyOwned = false;            
            mutex = new System.Threading.Mutex(true, "SucceedSoftGobang", out initiallyOwned);
            //判断互斥体是否使用中。
            if (initiallyOwned)
            {
                //Bitmap splashImage = new Bitmap("SplashsBg.gif");
                //splashScreen = new SucceedSoft.Common.SplashScreen(splashImage);
                //System.Threading.Thread.Sleep(1000);
                Gobang f = new Gobang();
                //f.Activated += new EventHandler(f_Activated);
                Application.Run(f);
            }
            else
            {
                MessageBoxEx.Show("应用程序已经启动，请检查窗口是否最小化！", Const.SystemTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }            
        }
        static System.Threading.Mutex mutex;//要做成全局性的,否则回收
        static SucceedSoft.Common.SplashScreen splashScreen;
        static void f_Activated(object sender, EventArgs e)
        {
            splashScreen.Close();
        }
    }
}