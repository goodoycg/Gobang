using System;
using System.Threading;
using System.Windows.Forms;

namespace SucceedSoft.Common
{
   public class SingletonApp 
   {
      static Mutex m_Mutex;

      public static void Run()
      {
         if(IsFirstInstance())
         {
            Application.ApplicationExit += new EventHandler(OnExit);
            Application.Run();
         }
      }
      public static void Run(ApplicationContext context)
      {
         if(IsFirstInstance())
         {
            Application.ApplicationExit += new EventHandler(OnExit);
            Application.Run(context);
         }
      }
      public static void Run(Form mainForm)
      {
         if(IsFirstInstance())
         {
            Application.ApplicationExit += new EventHandler(OnExit);
            Application.Run(mainForm);
         }
      }
      static bool IsFirstInstance()
      {
         m_Mutex = new Mutex(false,"SingletonApp Mutext");
         bool owned = false;
         owned = m_Mutex.WaitOne(TimeSpan.Zero,false);
         return owned ;
      }
      static void OnExit(object sender,EventArgs args)
      {
         m_Mutex.ReleaseMutex();
         m_Mutex.Close();
      }
   }
}
