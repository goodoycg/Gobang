using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using System.Media;
namespace SucceedSoft.Gobang
{
    /// <summary>
    /// Chessboard类用来绘制棋盘，控制下棋等操作
    /// </summary>
    public class ChessBoard
    {
        private Image imgBlackStone;
        private Image imgWhiteStone;

        private Font f20 = new Font("黑体", 20f,FontStyle.Bold);
        private Font fStep10 = new Font("宋体", 10f);
        private Font fResult = new Font("黑体", 20f, FontStyle.Bold);

        private SolidBrush sbStep = new SolidBrush(Color.Black);        
        private SolidBrush sb = new SolidBrush(Color.White);
        private SolidBrush sbPlus = new SolidBrush(Color.Red);
        
        public delegate void GameOver(object sender, GameOverEventArgs e);
        public event GameOver OnGameOver;

        public delegate void PlayerChange(object sender, PlayerChangeEventArgs e);
        public event PlayerChange OnPlayerChange; 
        
        /// <summary>
        /// arrchessboard为棋盘情况数组，
        /// arrchessboard[i,j]= 0 无子
        /// arrchessboard[i,j]= 1 黑子
        /// arrchessboard[i,j]= 2 白子
        /// </summary>
        public int[,] arrchessboard = new int[15, 15];
        /// <summary>
        /// 记录步骤
        /// </summary>
        private int[,] ArrayStep = new int[15, 15];        
        //棋子对象
        private Font f;       
        //private Stone stone;
        private SoundPlayer sndPing = new SoundPlayer("Sound\\Move.wav");
        //电脑对象
        private Computer ComputerPlayer;
        /// <summary>
        /// 当前要走的棋的颜色
        /// </summary>
        public ChessColor CurrChessColor = ChessColor.Black;
        /// <summary>
        /// 历史记录堆栈
        /// </summary>
        private Stack mStarckHistory = new Stack();        
        /// <summary>
        /// 历史记录属性
        /// </summary>
        public Stack StarckHistory
        {
            get { return mStarckHistory; }
        }        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="g">绘制的对象</param>
        public ChessBoard(Graphics m_Graphics)
        {            
            f = new Font("宋体", 30f, FontStyle.Bold);
            Initialization();
            ResourceManager rm = new ResourceManager("SucceedSoft.Gobang.My", Assembly.GetExecutingAssembly());
            imgBlackStone = ((System.Drawing.Image)(rm.GetObject("BlackChess.gif")));            
            imgWhiteStone = ((System.Drawing.Image)(rm.GetObject("WhiteChess.gif")));
            ComputerPlayer = new Computer();
        }
        
        /// <summary>
        /// 绘制棋子
        /// </summary>
        public void DrawChess(bool displaystep, Graphics m_Graphics)
        {           
            int lm = -1, ln = -1; 
            if (mStarckHistory.Count > 0)
            {//分解上一落子点
                string lstr = mStarckHistory.Peek().ToString().Replace("● ", string.Empty).Replace("○ ", string.Empty);
                string[] arr = lstr.Split(',');
                lm = Convert.ToInt32(arr[0]);
                ln = Convert.ToInt32(arr[1]);
            }            
            for (int i = 0; i < 15; i++)
            {//绘制步骤
                for (int j = 0; j < 15; j++)
                {
                    if (arrchessboard[i, j] == 1) DrawStone(m_Graphics,i, j, true, lm, ln);
                    if (arrchessboard[i, j] == 2) DrawStone(m_Graphics,i, j, false, lm, ln);
                    DrawStep(m_Graphics, i, j, ArrayStep[i, j], arrchessboard[i, j], displaystep);
                }
            }
            //"1黑棋胜利！"  "2白棋胜利！" "3平局！"Const.GameOverResult
            if (Const.GameOverResult == GameResult.BlackVictory)
            {                
                sb = new SolidBrush(Color.Black);
                if ((Const.FirstPlayer == Player.Computer) == (CurrChessColor == ChessColor.Black))
                {//电脑输了
                    m_Graphics.DrawString("黑棋[玩家]胜利", fResult, sb, Const.LeftBoardWidth + Const.ChessCellSize, Const.TopTitleHeight + 10);
                }
                else
                {
                    m_Graphics.DrawString("黑棋[电脑]胜利", fResult, sb, Const.LeftBoardWidth + Const.ChessCellSize, Const.TopTitleHeight + 10);
                }                
            }
            else if (Const.GameOverResult == GameResult.WhiteVictory)
            {
                sb = new SolidBrush(Color.White);
                if ((Const.FirstPlayer == Player.Computer) == (CurrChessColor == ChessColor.Black))
                {//电脑输了
                    m_Graphics.DrawString("白棋[玩家]胜利", fResult, sb, Const.LeftBoardWidth + Const.ChessCellSize, Const.TopTitleHeight + 10);
                }
                else
                {
                    m_Graphics.DrawString("白棋[电脑]胜利", fResult, sb, Const.LeftBoardWidth + Const.ChessCellSize, Const.TopTitleHeight + 10);
                }               
            }
            else if (Const.GameOverResult == GameResult.Nogfall)
            {
                sb = new SolidBrush(Color.Red);
                m_Graphics.DrawString("平局", fResult, sb, Const.LeftBoardWidth + Const.ChessCellSize * 3, Const.TopTitleHeight + 10);                
            } 
        }
        /// <summary>
        /// 当前棋子(m,n)落下,是已分胜负还是继续对局 (确定位置后,人和电脑是一样的下棋)
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        private void PlayChess(int m, int n, bool displaystep, Graphics mGraphics)
        {            
            if (Const.bGameOver)
            {
                return;
            }
            int lm = -1, ln = -1;		//上一落子点            
            if ((CurrChessColor == ChessColor.Black))
            {//记录下棋情况和历史记录
                arrchessboard[m, n] = 1;
                mStarckHistory.Push("● " + m.ToString() + "," + n.ToString());
            }
            else
            {
                arrchessboard[m, n] = 2;
                mStarckHistory.Push("○ " + m.ToString() + "," + n.ToString());
            }
            //先记录再分析最后下棋位置
            if (mStarckHistory.Count > 0)
            {//分解最后下棋位置
                string lstr = mStarckHistory.Peek().ToString().Replace("● ", string.Empty).Replace("○ ", string.Empty);
                string[] arr = lstr.Split(',');
                lm = Convert.ToInt32(arr[0]);
                ln = Convert.ToInt32(arr[1]);
            }
            //判断结果返回结果（0：胜利  1：平局   2：继续）
            Const.iPlayedChessCount++;
            ArrayStep[m, n] = Const.iPlayedChessCount;
            
            #region 更新虚构棋盘大小
            Const.Xmax = m + Const.ChessBoardCellCount > Const.Xmax ? m + Const.ChessBoardCellCount : Const.Xmax;
            Const.Xmax = Const.Xmax > 14 ? 14 : Const.Xmax;
           
            Const.Xmin = m - Const.ChessBoardCellCount < Const.Xmin ? m - Const.ChessBoardCellCount : Const.Xmin;
            Const.Xmin = Const.Xmin < 0 ? 0 : Const.Xmin;
           
            Const.Ymax = n + Const.ChessBoardCellCount > Const.Ymax ? n + Const.ChessBoardCellCount : Const.Ymax;
            Const.Ymax = Const.Ymax > 14 ? 14 : Const.Ymax;
            
            Const.Ymin = n - Const.ChessBoardCellCount < Const.Ymin ? n - Const.ChessBoardCellCount : Const.Ymin;
            Const.Ymin = Const.Ymin < 0 ? 0 : Const.Ymin;
            #endregion
            //"1黑棋胜利！"  "2白棋胜利！" "3平局！"
            Const.Result = ChessRule.Result(m, n, arrchessboard);
            if (Const.Result == CurrChessBoardState.ContinuePlayChess)
            {
                if (CurrChessColor == ChessColor.Black)
                    CurrChessColor = ChessColor.White;
                else
                    CurrChessColor = ChessColor.Black;                              
            }
            else
            {
                switch (Const.Result)
                {
                    case CurrChessBoardState.GameOver:
                        if ((CurrChessColor == ChessColor.Black))
                        {
                            Const.GameOverResult = GameResult.BlackVictory;
                        }
                        else
                        {
                            Const.GameOverResult = GameResult.WhiteVictory;
                        }

                        if (CurrChessColor == ChessColor.Black)
                            CurrChessColor = ChessColor.White;
                        else
                            CurrChessColor = ChessColor.Black;

                        Const.Result = CurrChessBoardState.None;
                        break;
                    case CurrChessBoardState.Dogfall:
                        Const.GameOverResult = GameResult.Nogfall;

                        if (CurrChessColor == ChessColor.Black)
                            CurrChessColor = ChessColor.White;
                        else
                            CurrChessColor = ChessColor.Black;

                        Const.Result = CurrChessBoardState.None;
                        break;
                }
                GameOverEventArgs game = new GameOverEventArgs();
                game.Result = Const.Result;
                game.ChessColorFlag = CurrChessColor;
                this.OnGameOver(null, game);
                //重新开始！
                new SoundPlayer(Const.Runpath() + "Sound\\GameOver.wav").Play();
                Const.bGameOver = true;  
            }

        }
        /// <summary>
        /// 人下棋
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ManPlayChess(int m, int n, bool displaystep,Graphics mGraphics)
        {                 
                if (!ChessRule.IsExistChess(m, n, arrchessboard))
                {
                    PlayChess(m, n, displaystep, mGraphics);
                }
                if (Const.bGameOver)
                    return;
        }
        /// <summary>
        /// 电脑开始下棋
        /// </summary>
        public void ComputerPlayChess(bool DisplayStep, Graphics mGraphics)
        {
            Const.bCurrPlayer = Player.Computer;
            PlayerChangeEventArgs thePlayer = new PlayerChangeEventArgs();
            thePlayer.CurrPlayer = Const.bCurrPlayer;
            this.OnPlayerChange(null, thePlayer);

            System.Windows.Forms.ProgressBar.CheckForIllegalCrossThreadCalls = false;
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(NewThreadComputerPlayChess));
            thread.Priority = System.Threading.ThreadPriority.Normal;
            thread.IsBackground = true;
            thread.Start();
            //NewThreadComputerPlayChess(DisplayStep, mGraphics);
            m_mmDisplayStep = DisplayStep;
            m_mmGraphics = mGraphics;            
        }
        private bool m_mmDisplayStep;
        private Graphics m_mmGraphics;
        /// <summary>
        /// 电脑开新进程进行搜索
        /// </summary>
        private void NewThreadComputerPlayChess()
        {
            int i = 2;
            if (i == 2)
            {//老方法
                ComputerPlayer.SearchBestPoint(arrchessboard, -1, Const.Xmin, Const.Ymin, Const.Xmax, Const.Ymax);
            }
            else
            {//新方法，在老方法的基础上判断是否形成特殊的局，如 五连，活四，冲四，双连三,在此基础上再判断权值
                ComputerPlayer.SearchBestPointStr(arrchessboard, -1, Const.Xmin, Const.Ymin, Const.Xmax, Const.Ymax);                
            }
            PlayChess(ComputerPlayer.X, ComputerPlayer.Y, m_mmDisplayStep, m_mmGraphics);

            if (Const.bGameOver)
                return;

            Const.bCurrPlayer = Player.Person;
            PlayerChangeEventArgs thePlayer = new PlayerChangeEventArgs();
            thePlayer.CurrPlayer = Const.bCurrPlayer;
            this.OnPlayerChange(null, thePlayer);
        }

        #region 开始下棋,初始载入玩家（true是电脑,false是人）
        /// <summary>
        /// 开始下棋,初始载入玩家
        /// </summary>
        /// <param name="playfirstflag">先手玩家</param>
        /// <param name="displaystep">是否显示步骤</param>
        public void StartPlay(Player FirstPlay, bool displaystep, Graphics m_Graphics)
        { // 2
            Const.FirstPlayer = FirstPlay;
            if (!Const.bGameOver)
            {
                Initialization();
               //computer = new Computer(Const.bFirstPlayerFlag);
                if (Const.FirstPlayer == Player.Computer)
                {
                    ComputerPlayChess(displaystep, m_Graphics);
                }
            }
        }
        #endregion

        #region 悔棋
        /// <summary>
        /// 悔棋 
        /// </summary>
        public void BackChess(bool displaystep)
        {
            int m = -1, n = -1;
            string lstr = string.Empty;
            string[] arr;
            
            lstr = mStarckHistory.Pop().ToString().Replace("● ", String.Empty).Replace("○ ", 
                String.Empty);
            arr = lstr.Split(',');
            m = Convert.ToInt32(arr[0]);
            n = Convert.ToInt32(arr[1]);
            arrchessboard[m, n] = 0;//数组重置 此处无棋 0
            
            Const.iPlayedChessCount--;               
        
            lstr = mStarckHistory.Pop().ToString().Replace("● ", String.Empty).Replace("○ ", 
                String.Empty);
            arr = lstr.Split(',');
            m = Convert.ToInt32(arr[0]);
            n = Convert.ToInt32(arr[1]);
            arrchessboard[m, n] = 0;//数组重置  此处无棋 0                
            Const.iPlayedChessCount--;            
        }
        #endregion

        #region 初始化,当前走黑棋,清空棋盘和历史记录,构造棋子对象
        /// <summary>
        /// 初始化,当前走黑棋,清空棋盘和历史记录,构造棋子对象
        /// </summary>
        public void Initialization()
        {
            CurrChessColor = ChessColor.Black;
            //C#中数组成员默认值为0,null
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 15; j++)
                {
                    arrchessboard[i, j] = 0;
                    ArrayStep[i, j] = 0;
                }
            mStarckHistory.Clear();
        }
        #endregion        

        #region 绘制棋子
        /// <summary>
        /// 绘制棋子
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="flag">判断是黑棋(true)还是白棋(false)</param>
        /// <param name="lm"></param>
        /// <param name="ln"></param>
        private void DrawStone(Graphics m_Graphics,int m, int n, bool flag, int lm, int ln)
        {
            if (m_Graphics == null)
                return;
            if (flag)
            {
                m_Graphics.DrawImage(
                    imgBlackStone,
                    m * Const.ChessCellSize + Const.ExcursionX + Const.LeftBoardWidth,
                    n * Const.ChessCellSize + Const.ExcursionY + Const.TopTitleHeight, 
                    imgBlackStone.Width, 
                    imgBlackStone.Height);
            }
            else
            {
                m_Graphics.DrawImage(imgWhiteStone,
                    m * Const.ChessCellSize + Const.ExcursionX + Const.LeftBoardWidth,
                    n * Const.ChessCellSize + Const.ExcursionY + Const.TopTitleHeight, 
                    imgWhiteStone.Width, imgWhiteStone.Height);
            }
            m_Graphics.DrawString("+", f20, sbPlus,
                   lm * Const.ChessCellSize + Const.ExcursionX + Const.LeftBoardWidth + Const.PlusExcursionX,
                   ln * Const.ChessCellSize + Const.ExcursionY + Const.TopTitleHeight + Const.PlusExcursionY);
        }
        #endregion

        #region 绘制步骤

        /// <summary>
        /// 绘制步骤
        /// </summary>
        /// <param name="m">棋盘位置X</param>
        /// <param name="n">棋盘位置Y</param>
        /// <param name="mnv">第stepCount步</param>
        /// <param name="chesscolor">判断是黑棋还是白棋,以便确定字体颜色</param>
        private void DrawStep(Graphics m_Graphics, int m, int n, int stepCount, int chessColor, bool displaystep)
        {

            if (displaystep && stepCount != Const.iPlayedChessCount)
            {
                if (chessColor == 1)
                    sbStep.Color = Color.White;
                else
                    sbStep.Color = Color.Black;//chessColor == 2

                if (stepCount > 9)
                {                                       
                    
                    m_Graphics.DrawString(stepCount.ToString(),
                       fStep10, sbStep,
                       m * Const.ChessCellSize + Const.ChessStepX2 + Const.LeftBoardWidth,
                       n * Const.ChessCellSize + Const.ChessStepY + Const.TopTitleHeight);
                }
                else if(stepCount > 0)
                {
                    m_Graphics.DrawString(stepCount.ToString(),
                         fStep10,
                         sbStep, m * Const.ChessCellSize + Const.ChessStepX + Const.LeftBoardWidth,
                         n * Const.ChessCellSize + Const.ChessStepY + Const.TopTitleHeight);
                    
                }
                else
                {}

            }
        }
        #endregion
    }

    public class GameOverEventArgs : System.EventArgs
    {
        private CurrChessBoardState m_iResult = 0;
        public CurrChessBoardState Result
	    {
            set { this.m_iResult = value; }
            get { return this.m_iResult; }
	    }
        private ChessColor m_bChessFlag = ChessColor.White;
        public ChessColor ChessColorFlag
        {
            set { this.m_bChessFlag = value; }
            get { return this.m_bChessFlag; }
        }        
    }
    public class PlayerChangeEventArgs : System.EventArgs
    {
        private  Player m_Player = Player.Computer;
        public Player CurrPlayer
        {
            set { this.m_Player = value; }
            get { return this.m_Player; }
        }       
    }
}