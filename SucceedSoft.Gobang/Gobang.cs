using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Reflection;
using System.Resources;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Rendering;
using System.Media;

namespace SucceedSoft.Gobang
{
    public partial class Gobang : Office2007RibbonForm
    {
        #region 自定义变量
       /// <summary>
       /// 棋盘
       /// </summary>
        private ChessBoard m_ChessBoard;//定义棋盘类
        private bool m_BDisplayStep = false;
        private string m_strAppPath = string.Empty;
        private SoundPlayer m_PlaySound;
        private Point m_MouseOffset;        //记录鼠标指针的坐标
        private bool m_BMouseDown = false; //记录鼠标按键是否按下
        private ButtonItem m_PopupFromCode = null;
        private bool m_BOpenSound = false;
        private bool m_ColorSelected = false;
        private eOffice2007ColorScheme m_BaseColorScheme = eOffice2007ColorScheme.Blue;
        private string[] Coordinate = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q" };
        private Font f10 = new Font("宋体", 10f); 
        private SolidBrush sb2 = new SolidBrush(Color.FromArgb(0, 0, 0));
        private Image imgChessBoard;
        private Graphics m_Graphics = null;
        private int m_dPCSpendTime = 0;
        private int m_dMANSpendTime = 0;
        #endregion

        #region 构造函数
        public Gobang()
        {
            InitializeComponent();
            this.m_Graphics = this.CreateGraphics();
            this.m_ChessBoard = new ChessBoard(m_Graphics);
            this.m_ChessBoard.OnGameOver += new ChessBoard.GameOver(ChessBoard_OnGameOver);
            this.m_ChessBoard.OnPlayerChange += new ChessBoard.PlayerChange(ChessBoard_OnPlayerChange);
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.StandardClick 
                | ControlStyles.StandardDoubleClick | ControlStyles.UserMouse
                | ControlStyles.OptimizedDoubleBuffer, true); 
            this.UpdateStyles();
        }
        #endregion

        #region 加载窗体
        private void Gobang_Load(object sender, EventArgs e)
        {            
            ResourceManager rm = new ResourceManager("SucceedSoft.Gobang.My", Assembly.GetExecutingAssembly());
            imgChessBoard = ((System.Drawing.Image)(rm.GetObject("ChessBoardbg.gif")));
            this.Width = 383;
            this.Height = 555;
            this.ribbonTop.Dock = DockStyle.Top;
            this.ribbonTop.Height = 156;

            m_PlaySound = new SoundPlayer("Sound\\PlayChess.wav");
            //m_ChessBoard.StartPlay(Const.bFirstPlayerFlag, m_BDisplayStep, m_Graphics);
            this.buttonNew_Click(null, null);

            #region 执行下面的代码生成资源文件
            //System.Resources.ResourceWriter rw = new System.Resources.ResourceWriter("My.resources");
            //Icon ControlIcon = new Icon("ControlIcon.ico");
            //Image BlackChess = Image.FromFile("BlackChess.gif");
            //Image WhiteChess = Image.FromFile("WhiteChess.gif");
            //Image ChessBoardbg = Image.FromFile("ChessBoardbg.gif");
            //Image SplashsBg = Image.FromFile("SplashsBg.gif");

            //rw.AddResource("ControlIcon.ico", ControlIcon);

            //rw.AddResource("BlackChess.gif", BlackChess);
            //rw.AddResource("WhiteChess.gif", WhiteChess);
            //rw.AddResource("ChessBoardbg.gif", ChessBoardbg);
            //rw.AddResource("SplashsBg.gif", SplashsBg);

            //rw.Generate();
            //rw.Close();
            #endregion

            SucceedSoft.Common.Win32API.AnimateWindow(this.Handle, 200, SucceedSoft.Common.Win32API.AW_CENTER);
            //Application.DoEvents();
        }

        private void Gobang_FormClosing(object sender, FormClosingEventArgs e)
        {
            SucceedSoft.Common.Win32API.AnimateWindow(this.Handle, 400, SucceedSoft.Common.Win32API.AW_CENTER | SucceedSoft.Common.Win32API.AW_HIDE);
        }
        #endregion

        #region 开始新游戏
        private void buttonNew_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_dPCSpendTime = 0;
            this.m_dMANSpendTime = 0;
            this.labMANSpendTime.Text = "00:00";
            this.labPCSpendTime.Text = "00:00";
            Const.iPlayedChessCount = 0;
            Const.bGameOver = false;
            this.timeTotalTime.Enabled = true;
           
            Const.bCurrPlayer = Const.FirstPlayer;            
            Const.GameOverResult = 0;
            Const.Result = CurrChessBoardState.None;
            this.labSetPlayer.Text = Const.FirstPlayer == Player.Computer ? Const.ComputerPlaying : Const.PersonPlaying;
            m_ChessBoard.StartPlay(Player.Computer, m_BDisplayStep, m_Graphics);
            this.Refresh(); 
            lstHistory.DataSource = m_ChessBoard.StarckHistory.ToArray();
        }
        #endregion    
   
        #region 改变下棋者
        private void ChessBoard_OnPlayerChange(object sender, PlayerChangeEventArgs e)
        {            
            if (Const.bGameOver)
                return;
            if (e.CurrPlayer == Player.Person)
                this.labSetPlayer.Text = "玩家正在下棋";
            else
                this.labSetPlayer.Text = "电脑正在下棋";
            this.Refresh();
        }

        #endregion

        #region 游戏结束
        private void ChessBoard_OnGameOver(object sender, GameOverEventArgs e)
        {
            Application.DoEvents();
            Const.Xmin = 5;
            Const.Xmax = 9;
            Const.Ymin = 5;
            Const.Ymax = 9;
            this.Cursor = System.Windows.Forms.Cursors.No;
            if (e.Result == CurrChessBoardState.Dogfall)
                this.labSetPlayer.Text = "游戏结束,平局";
            else
            {
                if ((Const.FirstPlayer == Player.Computer) == (e.ChessColorFlag == ChessColor.Black))
                {
                    this.labSetPlayer.Text = "游戏结束," + (e.ChessColorFlag == ChessColor.Black ? "白棋" : "黑棋") + "[玩家]胜利";
                    PrintScreen();
                }
                else
                {
                    this.labSetPlayer.Text = "游戏结束," + (e.ChessColorFlag == ChessColor.Black ? "白棋" : "黑棋") + "[电脑]胜利";                    
                }
            }
            this.Refresh();
        }
        #endregion

        #region 打印屏幕
        private void PrintScreen()
        {
            bool b = m_BDisplayStep;
            m_BDisplayStep = true;
            this.Refresh();

            Bitmap bmp = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));
            string strPath = Const.Runpath() + @"Screen\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".JPG";
            bmp.Save(strPath);

            m_BDisplayStep = b;
            this.Refresh();
        }

        private void PrintScreen2()
        {
            bool b = m_BDisplayStep;
            m_BDisplayStep = true;
            this.Refresh();
            try
            {
                SendKeys.SendWait("%{PRTSC}");
                // 模拟按键，将当前窗口图像截取到剪贴板   Alt+PrtSc，
                //如果要截取整个屏幕，把   Alt   (%)   去掉 
                object o = Clipboard.GetDataObject().GetData(DataFormats.Bitmap);//为null
                Bitmap myCapture = o as Bitmap;
                string strPath = Const.Runpath() + @"Screen\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".JPG";
                myCapture.Save(@strPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch(System.Exception err)
            {
                Console.Write(err.Message + err.StackTrace);
                MessageBox.Show(err.Message + err.StackTrace);
            }
            m_BDisplayStep = b;
            this.Refresh();
        }

        private void Gobang_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(imgChessBoard, Const.LeftBoardWidth, Const.TopTitleHeight, imgChessBoard.Width, imgChessBoard.Height);
            DrawCoordinate(e.Graphics);
            m_ChessBoard.DrawChess(this.m_BDisplayStep, e.Graphics);            
        }
        #endregion           

        #region 绘制坐标
        /// <summary>
        /// 绘制坐标
        /// </summary>       
        public void DrawCoordinate(Graphics m_Graphics)
        {
            for (int i = 1; i <= 15; i++)
            {
                m_Graphics.DrawString(Convert.ToString(16 - i),
                    f10,
                    sb2,
                    Const.LeftBoardWidth + Const.ExcursionX - (16 - i) / 10 * 2,
                    (i - 1) * Const.ChessCellSize + Const.ExcursionY + Const.TopTitleHeight + 5);

                m_Graphics.DrawString(Coordinate[i - 1],
                    f10,
                    sb2,
                    (i - 1) * Const.ChessCellSize + Const.LeftBoardWidth + Const.ExcursionX + 8,
                    14 * Const.ChessCellSize + Const.ExcursionY + Const.TopTitleHeight + 15);
            }

        }
        #endregion        

        #region 播放棋声音
        private void SetMouseSound()
        {
            Application.DoEvents();
            if (Const.bCurrPlayer == Player.Person)
            {
                m_PlaySound.Play();
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
            else
            {
                this.Cursor = System.Windows.Forms.Cursors.No;
            }
        }
        #endregion

        #region 按下鼠标
        private void Gobang_MouseDown(object sender, MouseEventArgs e)
        {
            if(Const.LeftBoardWidth <= e.X && e.X <= Const.RightChessLimit &&
               Const.TopTitleHeight <= e.Y && e.Y <= Const.BottomChessLimit)
            {//棋盘区域内
                if (Const.bCurrPlayer == Player.Person)
                {
                    Const.bCurrPlayer =  Player.Computer;
                    SetMouseSound();
                    if (!Const.bGameOver)
                    {                        
                        int m = (e.X - Const.LeftBoardWidth) / Const.ChessCellSize;
                        int n = (e.Y - Const.TopTitleHeight) / Const.ChessCellSize;

                        m_ChessBoard.ManPlayChess(m, n, m_BDisplayStep, m_Graphics);
                        this.Refresh();
                        lstHistory.DataSource = m_ChessBoard.StarckHistory.ToArray();

                        Application.DoEvents();
                        m_ChessBoard.ComputerPlayChess(m_BDisplayStep, m_Graphics);
                        this.Refresh();
                        SetMouseSound();
                        lstHistory.DataSource = m_ChessBoard.StarckHistory.ToArray();
                    }
                }
            }
        }
        #endregion

        #region 抬起鼠标
        private void Gobang_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_BMouseDown = false;
            }
        }
        #endregion

        #region 移动鼠标
        private void Gobang_MouseMove(object sender, MouseEventArgs e)
        {
            if (Const.LeftBoardWidth <= e.X && e.X <= Const.RightChessLimit &&
                    Const.TopTitleHeight <= e.Y && e.Y <= Const.BottomChessLimit &&
                    Const.bGameOver)
            {
                this.Cursor = System.Windows.Forms.Cursors.No;
            }
            else
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
            
            if (m_BMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(m_MouseOffset.X, m_MouseOffset.Y);
                Location = mousePos;
            }
            Application.DoEvents();
        }
        #endregion               

        #region 是否显示步骤,声音
        private void chkDispalyStep_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            m_BDisplayStep = this.chkDispalyStep.Checked;
            this.Refresh();
        }

        private void chkOpenSound_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            m_BOpenSound = this.chkSound.Checked;
        }
        #endregion
        
        #region 风格
        private void StyleChange(object sender, System.EventArgs e)
        {
            ButtonItem item = sender as ButtonItem;
            if (item == buttonStyleOffice2007Blue)
            {
                ribbonTop.Office2007ColorTable = eOffice2007ColorScheme.Blue;
            }
            else if (item == buttonStyleOffice2007Black)
            {
                ribbonTop.Office2007ColorTable = eOffice2007ColorScheme.Black;
            }
            else if (item == buttonStyleOffice2007Silver)
            {
               ribbonTop.Office2007ColorTable = eOffice2007ColorScheme.Silver;
            }
            this.Invalidate();
        }
        private void buttonStyleCustom_ExpandChange(object sender, System.EventArgs e)
        {
            if (buttonStyleCustom.Expanded)
            {
                m_ColorSelected = false;
                m_BaseColorScheme = ((Office2007Renderer)GlobalManager.Renderer).ColorTable.InitialColorScheme;
            }
            else
            {
                if (!m_ColorSelected)
                {
                    ribbonTop.Office2007ColorTable = m_BaseColorScheme;
                }
            }
        }

        private void buttonStyleCustom_ColorPreview(object sender, DevComponents.DotNetBar.ColorPreviewEventArgs e)
        {
            RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(this, m_BaseColorScheme, e.Color);
        }

        private void buttonStyleCustom_SelectedColorChanged(object sender, System.EventArgs e)
        {
            m_ColorSelected = true;
            RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(this, m_BaseColorScheme, buttonStyleCustom.SelectedColor);
        }
        #endregion        

        #region 撤销一步棋
        private void buttonUndo_Click(object sender, EventArgs e)
        {//棋盘上只有一个棋子时就不能后退二次,所以分开

            if (Const.bGameOver || Const.iPlayedChessCount < 2)
                return;
            this.m_ChessBoard.BackChess(m_BDisplayStep);
            Array arr = m_ChessBoard.StarckHistory.ToArray();
            lstHistory.DataSource = arr;
            this.Refresh();
        }
        #endregion

        #region 保存棋局
        private void buttonSave_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.Title = "保存棋局";
            this.saveFileDialog1.SupportMultiDottedExtensions = true;
            this.saveFileDialog1.OverwritePrompt = true;
            this.saveFileDialog1.FileName = "棋局";
            this.saveFileDialog1.InitialDirectory =System.Environment.SpecialFolder.DesktopDirectory.ToString();
            this.saveFileDialog1.Filter = "文本文件(*.txt)|*.txt";
            this.saveFileDialog1.DefaultExt = "txt";
            if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;            
            
            try
            {
                System.IO.File.Copy(Const.Runpath() + "save.dll", this.saveFileDialog1.FileName,true);
                System.IO.StreamWriter w = System.IO.File.AppendText(this.saveFileDialog1.FileName);
                string strText = string.Empty;

                object[] m_Text = lstHistory.DataSource as object[];
                for (int i = 0; i < m_Text.Length; i++)
                {
                    strText += m_Text[i].ToString().Replace("● ", string.Empty).Replace("○ ", string.Empty) + "-";
                }
                strText = strText.Substring(0,strText.Length - 1);
                w.Write(strText);
                w.Flush();
                w.Close();
                MessageBoxEx.Show(this, "保存成功！", Const.SystemTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(System.Exception err)
            {
                MessageBoxEx.Show(this, err.Message + err.StackTrace + "保存失败！", Const.SystemTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 帮助
        private void buttonHelp_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.itsanrx.com");
            }
            catch
            {
                MessageBoxEx.Show("未找到帮助文件！");
            }            
        }

        #endregion

        #region 统计时间
        private void timeTotalTime_Tick(object sender, EventArgs e)
        {
            if (Const.bGameOver)
                return;

            if (Const.bCurrPlayer ==  Player.Person)
            {
                m_dMANSpendTime = m_dMANSpendTime + 1;
                this.labMANSpendTime.Text = Convert.ToInt32(m_dMANSpendTime / 60).ToString("D2") +
                    ":" + Convert.ToInt32(m_dMANSpendTime % 60).ToString("D2");
            }
            else
            {
                m_dPCSpendTime = m_dPCSpendTime + 1;
                this.labPCSpendTime.Text = Convert.ToInt32(m_dPCSpendTime / 60).ToString("D2") +
                    ":" + Convert.ToInt32(m_dPCSpendTime % 60).ToString("D2");
            }

        }

        #endregion

        #region 设置人先下棋,电脑后下棋
        private void chkManFirst_CheckedChanged(object sender, EventArgs e)
        {
            Const.FirstPlayer = this.chkManFirst.Checked ? Player.Person : Player.Computer;
        }
        #endregion
            
        #region CreatePopupMenu
        private void CreatePopupMenu()
        {
            DevComponents.DotNetBar.ButtonItem item;

            m_PopupFromCode = new DevComponents.DotNetBar.ButtonItem();

            // Create items
            item = new DevComponents.DotNetBar.ButtonItem("bCut");
            item.Text = "Cu&t";
            // To remember: cannot use the ImageIndex for items that we create from code
            item.Image = imageList1.Images[0];
            m_PopupFromCode.SubItems.Add(item);

            item = new DevComponents.DotNetBar.ButtonItem("bCopy");
            item.Text = "&Copy";
            item.Image = imageList1.Images[1];
            m_PopupFromCode.SubItems.Add(item);

            item = new DevComponents.DotNetBar.ButtonItem("bPaste");
            item.Text = "&Paste";
            item.Image = imageList1.Images[2];
            m_PopupFromCode.SubItems.Add(item);

            item = new DevComponents.DotNetBar.ButtonItem("bOpenFile");
            item.Text = "&Open File";
            item.Enabled = false;
            item.BeginGroup = true;
            m_PopupFromCode.SubItems.Add(item);

            item = new DevComponents.DotNetBar.ButtonItem("bInsertBreakpoint");
            item.Text = "Insert B&reakpoint";
            item.BeginGroup = true;
            m_PopupFromCode.SubItems.Add(item);

            item = new DevComponents.DotNetBar.ButtonItem("bNewBreakpoint");
            item.Text = "New &Breakpoint...";
            m_PopupFromCode.SubItems.Add(item);

            item = new DevComponents.DotNetBar.ButtonItem("bRunToCursor");
            item.Text = "&Run To Cursor";
            item.BeginGroup = true;
            m_PopupFromCode.SubItems.Add(item);

            item = new DevComponents.DotNetBar.ButtonItem("bAddTask");
            item.Text = "Add Task List S&hortcut";
            item.BeginGroup = true;
            m_PopupFromCode.SubItems.Add(item);

            // Setup side-bar, make sure that image that is used fits, or exceeds the height
            // Side-bar will be displayed only for popup menus
            DevComponents.DotNetBar.SideBarImage si = new DevComponents.DotNetBar.SideBarImage();
            si.Picture = Image.FromFile("devco.jpg"); //new Bitmap(typeof(WindowsApplication1.Form1), "devco.jpg");
            // If image exceeds the size of the popup menu this specifies the image alignment
            si.Alignment = DevComponents.DotNetBar.eAlignment.Bottom;
            // If there is no image specified gradient can be used
            si.GradientColor1 = Color.Orange;
            si.GradientColor2 = Color.Black;
            m_PopupFromCode.PopUpSideBar = si;


            // notifyIcon1.ContextMenuStrip
        }
        #endregion        
    }
}
