using System;
using System.Runtime.InteropServices;

namespace SucceedSoft.Common
{
	public class Util
	{
      public const int HWND_BROADCAST  = 0xffff;      
      public const int WM_USER = 0x400;

      [DllImport("user32",EntryPoint="SendMessage")]
      public static extern int SendMessage(IntPtr hwnd,int msg,int wparam,int lparam);
      
      [DllImport("user32",EntryPoint="PostMessage")]
      public static extern bool PostMessage(IntPtr hwnd,int msg,int wparam,int lparam);
      
      [DllImport("user32",EntryPoint="RegisterWindowMessage")]
      public static extern int RegisterWindowMessage(string msgString);
	}
}
