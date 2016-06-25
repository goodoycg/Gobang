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
    /// Chessboard�������������̣���������Ȳ���
    /// </summary>
    public class ChessBoard
    {
        private Image imgBlackStone;
        private Image imgWhiteStone;

        private Font f20 = new Font("����", 20f,FontStyle.Bold);
        private Font fStep10 = new Font("����", 10f);
        private Font fResult = new Font("����", 20f, FontStyle.Bold);

        private SolidBrush sbStep = new SolidBrush(Color.Black);        
        private SolidBrush sb = new SolidBrush(Color.White);
        private SolidBrush sbPlus = new SolidBrush(Color.Red);
        
        public delegate void GameOver(object sender, GameOverEventArgs e);
        public event GameOver OnGameOver;

        public delegate void PlayerChange(object sender, PlayerChangeEventArgs e);
        public event PlayerChange OnPlayerChange; 
        
        /// <summary>
        /// arrchessboardΪ����������飬
        /// arrchessboard[i,j]= 0 ����
        /// arrchessboard[i,j]= 1 ����
        /// arrchessboard[i,j]= 2 ����
        /// </summary>
        public int[,] arrchessboard = new int[15, 15];
        /// <summary>
        /// ��¼����
        /// </summary>
        private int[,] ArrayStep = new int[15, 15];        
        //���Ӷ���
        private Font f;       
        //private Stone stone;
        private SoundPlayer sndPing = new SoundPlayer("Sound\\Move.wav");
        //���Զ���
        private Computer ComputerPlayer;
        /// <summary>
        /// ��ǰҪ�ߵ������ɫ
        /// </summary>
        public ChessColor CurrChessColor = ChessColor.Black;
        /// <summary>
        /// ��ʷ��¼��ջ
        /// </summary>
        private Stack mStarckHistory = new Stack();        
        /// <summary>
        /// ��ʷ��¼����
        /// </summary>
        public Stack StarckHistory
        {
            get { return mStarckHistory; }
        }        
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="g">���ƵĶ���</param>
        public ChessBoard(Graphics m_Graphics)
        {            
            f = new Font("����", 30f, FontStyle.Bold);
            Initialization();
            ResourceManager rm = new ResourceManager("SucceedSoft.Gobang.My", Assembly.GetExecutingAssembly());
            imgBlackStone = ((System.Drawing.Image)(rm.GetObject("BlackChess.gif")));            
            imgWhiteStone = ((System.Drawing.Image)(rm.GetObject("WhiteChess.gif")));
            ComputerPlayer = new Computer();
        }
        
        /// <summary>
        /// ��������
        /// </summary>
        public void DrawChess(bool displaystep, Graphics m_Graphics)
        {           
            int lm = -1, ln = -1; 
            if (mStarckHistory.Count > 0)
            {//�ֽ���һ���ӵ�
                string lstr = mStarckHistory.Peek().ToString().Replace("�� ", string.Empty).Replace("�� ", string.Empty);
                string[] arr = lstr.Split(',');
                lm = Convert.ToInt32(arr[0]);
                ln = Convert.ToInt32(arr[1]);
            }            
            for (int i = 0; i < 15; i++)
            {//���Ʋ���
                for (int j = 0; j < 15; j++)
                {
                    if (arrchessboard[i, j] == 1) DrawStone(m_Graphics,i, j, true, lm, ln);
                    if (arrchessboard[i, j] == 2) DrawStone(m_Graphics,i, j, false, lm, ln);
                    DrawStep(m_Graphics, i, j, ArrayStep[i, j], arrchessboard[i, j], displaystep);
                }
            }
            //"1����ʤ����"  "2����ʤ����" "3ƽ�֣�"Const.GameOverResult
            if (Const.GameOverResult == GameResult.BlackVictory)
            {                
                sb = new SolidBrush(Color.Black);
                if ((Const.FirstPlayer == Player.Computer) == (CurrChessColor == ChessColor.Black))
                {//��������
                    m_Graphics.DrawString("����[���]ʤ��", fResult, sb, Const.LeftBoardWidth + Const.ChessCellSize, Const.TopTitleHeight + 10);
                }
                else
                {
                    m_Graphics.DrawString("����[����]ʤ��", fResult, sb, Const.LeftBoardWidth + Const.ChessCellSize, Const.TopTitleHeight + 10);
                }                
            }
            else if (Const.GameOverResult == GameResult.WhiteVictory)
            {
                sb = new SolidBrush(Color.White);
                if ((Const.FirstPlayer == Player.Computer) == (CurrChessColor == ChessColor.Black))
                {//��������
                    m_Graphics.DrawString("����[���]ʤ��", fResult, sb, Const.LeftBoardWidth + Const.ChessCellSize, Const.TopTitleHeight + 10);
                }
                else
                {
                    m_Graphics.DrawString("����[����]ʤ��", fResult, sb, Const.LeftBoardWidth + Const.ChessCellSize, Const.TopTitleHeight + 10);
                }               
            }
            else if (Const.GameOverResult == GameResult.Nogfall)
            {
                sb = new SolidBrush(Color.Red);
                m_Graphics.DrawString("ƽ��", fResult, sb, Const.LeftBoardWidth + Const.ChessCellSize * 3, Const.TopTitleHeight + 10);                
            } 
        }
        /// <summary>
        /// ��ǰ����(m,n)����,���ѷ�ʤ�����Ǽ����Ծ� (ȷ��λ�ú�,�˺͵�����һ��������)
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        private void PlayChess(int m, int n, bool displaystep, Graphics mGraphics)
        {            
            if (Const.bGameOver)
            {
                return;
            }
            int lm = -1, ln = -1;		//��һ���ӵ�            
            if ((CurrChessColor == ChessColor.Black))
            {//��¼�����������ʷ��¼
                arrchessboard[m, n] = 1;
                mStarckHistory.Push("�� " + m.ToString() + "," + n.ToString());
            }
            else
            {
                arrchessboard[m, n] = 2;
                mStarckHistory.Push("�� " + m.ToString() + "," + n.ToString());
            }
            //�ȼ�¼�ٷ����������λ��
            if (mStarckHistory.Count > 0)
            {//�ֽ��������λ��
                string lstr = mStarckHistory.Peek().ToString().Replace("�� ", string.Empty).Replace("�� ", string.Empty);
                string[] arr = lstr.Split(',');
                lm = Convert.ToInt32(arr[0]);
                ln = Convert.ToInt32(arr[1]);
            }
            //�жϽ�����ؽ����0��ʤ��  1��ƽ��   2��������
            Const.iPlayedChessCount++;
            ArrayStep[m, n] = Const.iPlayedChessCount;
            
            #region �����鹹���̴�С
            Const.Xmax = m + Const.ChessBoardCellCount > Const.Xmax ? m + Const.ChessBoardCellCount : Const.Xmax;
            Const.Xmax = Const.Xmax > 14 ? 14 : Const.Xmax;
           
            Const.Xmin = m - Const.ChessBoardCellCount < Const.Xmin ? m - Const.ChessBoardCellCount : Const.Xmin;
            Const.Xmin = Const.Xmin < 0 ? 0 : Const.Xmin;
           
            Const.Ymax = n + Const.ChessBoardCellCount > Const.Ymax ? n + Const.ChessBoardCellCount : Const.Ymax;
            Const.Ymax = Const.Ymax > 14 ? 14 : Const.Ymax;
            
            Const.Ymin = n - Const.ChessBoardCellCount < Const.Ymin ? n - Const.ChessBoardCellCount : Const.Ymin;
            Const.Ymin = Const.Ymin < 0 ? 0 : Const.Ymin;
            #endregion
            //"1����ʤ����"  "2����ʤ����" "3ƽ�֣�"
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
                //���¿�ʼ��
                new SoundPlayer(Const.Runpath() + "Sound\\GameOver.wav").Play();
                Const.bGameOver = true;  
            }

        }
        /// <summary>
        /// ������
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
        /// ���Կ�ʼ����
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
        /// ���Կ��½��̽�������
        /// </summary>
        private void NewThreadComputerPlayChess()
        {
            int i = 2;
            if (i == 2)
            {//�Ϸ���
                ComputerPlayer.SearchBestPoint(arrchessboard, -1, Const.Xmin, Const.Ymin, Const.Xmax, Const.Ymax);
            }
            else
            {//�·��������Ϸ����Ļ������ж��Ƿ��γ�����ľ֣��� ���������ģ����ģ�˫����,�ڴ˻��������ж�Ȩֵ
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

        #region ��ʼ����,��ʼ������ң�true�ǵ���,false���ˣ�
        /// <summary>
        /// ��ʼ����,��ʼ�������
        /// </summary>
        /// <param name="playfirstflag">�������</param>
        /// <param name="displaystep">�Ƿ���ʾ����</param>
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

        #region ����
        /// <summary>
        /// ���� 
        /// </summary>
        public void BackChess(bool displaystep)
        {
            int m = -1, n = -1;
            string lstr = string.Empty;
            string[] arr;
            
            lstr = mStarckHistory.Pop().ToString().Replace("�� ", String.Empty).Replace("�� ", 
                String.Empty);
            arr = lstr.Split(',');
            m = Convert.ToInt32(arr[0]);
            n = Convert.ToInt32(arr[1]);
            arrchessboard[m, n] = 0;//�������� �˴����� 0
            
            Const.iPlayedChessCount--;               
        
            lstr = mStarckHistory.Pop().ToString().Replace("�� ", String.Empty).Replace("�� ", 
                String.Empty);
            arr = lstr.Split(',');
            m = Convert.ToInt32(arr[0]);
            n = Convert.ToInt32(arr[1]);
            arrchessboard[m, n] = 0;//��������  �˴����� 0                
            Const.iPlayedChessCount--;            
        }
        #endregion

        #region ��ʼ��,��ǰ�ߺ���,������̺���ʷ��¼,�������Ӷ���
        /// <summary>
        /// ��ʼ��,��ǰ�ߺ���,������̺���ʷ��¼,�������Ӷ���
        /// </summary>
        public void Initialization()
        {
            CurrChessColor = ChessColor.Black;
            //C#�������ԱĬ��ֵΪ0,null
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 15; j++)
                {
                    arrchessboard[i, j] = 0;
                    ArrayStep[i, j] = 0;
                }
            mStarckHistory.Clear();
        }
        #endregion        

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="flag">�ж��Ǻ���(true)���ǰ���(false)</param>
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

        #region ���Ʋ���

        /// <summary>
        /// ���Ʋ���
        /// </summary>
        /// <param name="m">����λ��X</param>
        /// <param name="n">����λ��Y</param>
        /// <param name="mnv">��stepCount��</param>
        /// <param name="chesscolor">�ж��Ǻ��廹�ǰ���,�Ա�ȷ��������ɫ</param>
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