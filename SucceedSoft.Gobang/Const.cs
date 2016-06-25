using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SucceedSoft.Gobang
{
    public class Const
    {//2147483647 >= int32 >= -2147483648

        //20100711094749.JPG 权值计算不准

        public const string Value0 = "0";//无棋
        public const string Value1 = "1";//有棋
        public const string Value2 = "2";//有棋

        #region 常量
        public static string SystemTitle = "快乐五子棋";
        /// <summary>
        /// 游戏最后结果
        /// </summary>
        public static GameResult GameOverResult = GameResult.None;
        /// <summary>
        /// 返回结果
        /// </summary>
        public static CurrChessBoardState Result = CurrChessBoardState.None;
        /// <summary>
        /// 电脑预测步数
        /// </summary>
        public static double dForecastStepCount = 1;
        /// <summary>
        /// 棋盘格子的大小
        /// </summary>
        public const int ChessCellSize = 25;
        /// <summary>
        /// 左边边框宽度
        /// </summary>
        public const int LeftBoardWidth = 3;
        /// <summary>
        /// 上边标题栏高度
        /// </summary>
        public const int TopTitleHeight = 158;
        /// <summary>
        /// 右边落子界限
        /// </summary>
        public const int RightChessLimit = 370;
        /// <summary>
        /// 下边落子界限
        /// </summary>
        public const int BottomChessLimit = 520;
        /// <summary>
        /// 棋子位置调整X
        /// </summary>
        public const int ExcursionX = 0;
        /// <summary>
        /// 棋子位置调整Y
        /// </summary>
        public const int ExcursionY = 0;
        /// <summary>
        /// 棋子步骤字符位置调整X(一位数)
        /// </summary>
        public const int ChessStepX = 7;
        /// <summary>
        /// 棋子步骤字符位置调整X(二位数)
        /// </summary>
        public const int ChessStepX2 = 4;  
        /// <summary>
        /// 棋子步骤字符位置调整Y
        /// </summary>
        public const int ChessStepY = 5;
        /// <summary>
        /// +号的位置调整X
        /// </summary>
        public const int PlusExcursionX = 1;
        /// <summary>
        /// +号的位置调整Y
        /// </summary>
        public const int PlusExcursionY = 0;
        /// <summary>
        /// 游戏是否结束
        /// </summary>
        public static bool bGameOver = false;
        /// <summary>
        /// 当前游戏是人还是电脑在思索下棋
        /// </summary>
        public static Player bCurrPlayer = Player.Person;
        /// <summary>
        /// 棋盘上棋子数
        /// </summary>
        public static int iPlayedChessCount = 0;           
        /// <summary>
        /// 先手玩家
        /// </summary>
        public static Player FirstPlayer = Player.Person;
        /// <summary>
        /// 已有棋子周围 ChessBoardCellCount 格之内的最小棋盘(前2、3步已有棋子周围4格最小棋盘)
        /// </summary>
        public static int ChessBoardCellCount = 2;        
        /// <summary>
        /// 围住棋盘的棋子的最小矩形左上顶点X
        /// </summary>
        public static int Xmin = 5;
        /// <summary>
        /// 围住棋盘的棋子的最小矩形右下顶点X
        /// </summary>
        public static int Xmax = 9;
        /// <summary>
        /// 围住棋盘的棋子的最小矩形左上顶点Y
        /// </summary>
        public static int Ymin = 5;
        /// <summary>
        /// 围住棋盘的棋子的最小矩形右下顶点Y
        /// </summary>
        public static int Ymax = 9;

        #endregion

        #region 权值
        /// <summary>
        /// 五连已方权值    10000000 7个0
        /// </summary>
        public static double iFiveLineMy        = 10000000;
        /// <summary>
        /// 五连对方权值    1000000 6个0
        /// </summary>
        public static double iFiveLineOt        = 1000000;
        /// <summary>
        /// 活四已方权值    100000 5个0
        /// </summary>
        public static double iFourLiveMy        = 100000;
        /// <summary>
        /// 活四对方权值    10000 4个0
        /// </summary>
        public static double iFourLiveOt        = 10000;
        /// <summary>
        /// 冲四已方权值    1000 3个0
        /// </summary>
        public static double iFourRushMy        = 1000;
        /// <summary>
        /// 冲四对方权值    750
        /// </summary>
        public static double iFourRushOt        = 750;
        //-----------------------要遵守：2 * 跳三对方权值 > 冲四已方权值 (二个活三要重要于一个冲四) 550 * 2 > 1000 ----------
        
        /// <summary>
        /// 连三已方权值    700
        /// </summary>
        public static double iThreeLineMy       = 700;
        /// <summary>
        /// 连三对方权值    650
        /// </summary>
        public static double iThreeLineOt       = 650;
        /// <summary>
        /// 跳三已方权值    600
        /// </summary>
        public static double iThreeJumpMy       = 600;
        /// <summary>
        /// 跳三对方权值    550
        /// </summary>
        public static double iThreeJumpOt       = 550;
        /// <summary>
        /// 死三已方权值    500
        /// </summary>
        public static double iThreeDieMy        = 500;
        /// <summary>
        /// 死三对方权值    480
        /// </summary>
        public static double iThreeDieOt        = 480;
        //-----------------已方,对方权值不同,连二,跳二,大跳二权值也不同------------------------------------------------------------------------------

        //B----------------要遵守：2 * 大跳二对方权值 > 死三已方权值 (二个活二要重要于一个死三) 280 * 2 > 500 -----------------------------------------------------------------------------------
        /// <summary>
        /// 连二A已方权值        460
        /// </summary>
        public static double iTwoLineAMy  = 460;
        /// <summary>
        /// 连二A对方权值        440
        /// </summary>
        public static double iTwoLineAOt = 440;
        /// <summary>
        /// 连二B已方权值        420
        /// </summary>
        public static double iTwoLineBMy = 420;
        /// <summary>
        /// 连二B对方权值        400
        /// </summary>
        public static double iTwoLineBOt = 400;
        /// <summary>
        /// 连二C已方权值        380
        /// </summary>
        public static double iTwoLineCMy = 380;
        /// <summary>
        /// 连二C对方权值        360
        /// </summary>
        public static double iTwoLineCOt = 360;
        /// <summary>
        /// 跳二已方权值        340
        /// </summary>
        public static double iTwoJumpMy = 340;
        /// <summary>
        /// 跳二对方权值        320
        /// </summary>
        public static double iTwoJumpOt = 320;
        /// <summary>
        /// 大跳二已方权值        300
        /// </summary>
        public static double iTwoBigJumpMy = 300;
        /// <summary>
        /// 大跳二对方权值        280
        /// </summary>
        public static double iTwoBigJumpOt= 280;
        //B----------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 一权值    1
        /// </summary>
        public static double iOne               = 1;
        /// <summary>
        /// 零权值    0
        /// </summary>
        public static double iZero              = 0;

        #endregion        
       
        #region 正在下棋
        /// <summary>
        /// 电脑正在下棋
        /// </summary>
        public const string ComputerPlaying = "电脑正在下棋";  
        /// <summary>
        /// 玩家正在下棋
        /// </summary>
        public const string PersonPlaying = "玩家正在下棋";
        #endregion

        #region 连图
        /// <summary>
        /// 1五连
        /// </summary>
        public const string S1_FiveLink = "11111";
        /// <summary>
        /// 2五连
        /// </summary>
        public const string S2_FiveLink = "22222";
        /// <summary>
        /// 1活四
        /// </summary>
        public const string S1_FourLive = "011110";
        /// <summary>
        /// 2活四
        /// </summary>
        public const string S2_FourLive = "022220";
        /// <summary>
        /// 1冲四A
        /// </summary>
        public const string S1_FourRushA = "01111";
        /// <summary>
        /// 1冲四B
        /// </summary>
        public const string S1_FourRushB = "11110";
        /// <summary>
        /// 1冲四C
        /// </summary>
        public const string S1_FourRushC = "10111";
        /// <summary>
        /// 1冲四D
        /// </summary>
        public const string S1_FourRushD = "11101";
        /// <summary>
        /// 1冲四E
        /// </summary>
        public const string S1_FourRushE = "11011";
        /// <summary>
        /// 2冲四A
        /// </summary>
        public const string S2_FourRushA = "02222";
        /// <summary>
        /// 2冲四B
        /// </summary>
        public const string S2_FourRushB = "22220";
        /// <summary>
        /// 2冲四C
        /// </summary>
        public const string S2_FourRushC = "20222";
        /// <summary>
        /// 2冲四D
        /// </summary>
        public const string S2_FourRushD = "22202";
        /// <summary>
        /// 2冲四E
        /// </summary>
        public const string S2_FourRushE = "22022";
        /// <summary>
        /// 1连三A
        /// </summary>                
        public const string S1_ThreeLineA = "001110";
        /// <summary>
        /// 1连三B
        /// </summary>  
        public const string S1_ThreeLineB = "011100";
        /// <summary>
        /// 2连三A
        /// </summary>                
        public const string S2_ThreeLineA = "002220";
        /// <summary>
        /// 2连三B
        /// </summary>  
        public const string S2_ThreeLineB = "022200";
        /// <summary>
        /// 1跳三A
        /// </summary>  
        public const string S1_ThreeJumpA = "010110";
        /// <summary>
        /// 1跳三B
        /// </summary>  
        public const string S1_ThreeJumpB = "011010";
        /// <summary>
        /// 2跳三A
        /// </summary>  
        public const string S2_ThreeJumpA = "020220";
        /// <summary>
        /// 2跳三B
        /// </summary>  
        public const string S2_ThreeJumpB = "022020";
        /// <summary>
        /// 1死三A
        /// </summary>
        public const string S1_ThreeDieA = "211100";
        /// <summary>
        /// 1死三B
        /// </summary>
        public const string S1_ThreeDieB = "001112";
        /// <summary>
        /// 1死三C
        /// </summary>
        public const string S1_ThreeDieC = "211100";
        /// <summary>
        /// 1死三D
        /// </summary>
        public const string S1_ThreeDieD = "201110";
        /// <summary>
        /// 1死三E
        /// </summary>
        public const string S1_ThreeDieE = "011102";
        /// <summary>
        /// 1死三F
        /// </summary>
        public const string S1_ThreeDieF = "210110";
        /// <summary>
        /// 1死三G
        /// </summary>
        public const string S1_ThreeDieG = "011012";
        /// <summary>
        /// 1死三H
        /// </summary>
        public const string S1_ThreeDieH = "211010";
        /// <summary>
        /// 1死三I
        /// </summary>
        public const string S1_ThreeDieI = "010112";
        /// <summary>
        /// 1死三J
        /// </summary>
        public const string S1_ThreeDieJ = "2011102";
        /// <summary>
        /// 2死三A
        /// </summary>
        public const string S2_ThreeDieA = "122200";
        /// <summary>
        /// 2死三B
        /// </summary>
        public const string S2_ThreeDieB = "002221";
        /// <summary>
        /// 2死三C
        /// </summary>
        public const string S2_ThreeDieC = "122200";
        /// <summary>
        /// 2死三D
        /// </summary>
        public const string S2_ThreeDieD = "102220";
        /// <summary>
        /// 2死三E
        /// </summary>
        public const string S2_ThreeDieE = "022201";
        /// <summary>
        /// 2死三F
        /// </summary>
        public const string S2_ThreeDieF = "120220";
        /// <summary>
        /// 2死三G
        /// </summary>
        public const string S2_ThreeDieG = "022021";
        /// <summary>
        /// 2死三H
        /// </summary>
        public const string S2_ThreeDieH = "122020";
        /// <summary>
        /// 2死三I
        /// </summary>
        public const string S2_ThreeDieI = "020221";
        /// <summary>
        /// 2死三J
        /// </summary>
        public const string S2_ThreeDieJ = "1022201";


        /// <summary>
        /// 1连二Aa
        /// </summary>
        public const string S1_TwoLineAa = "000110";
        /// <summary>
        /// 1连二Ab
        /// </summary>
        public const string S1_TwoLineAb = "011000";               
        /// <summary>
        /// 1连二Ba
        /// </summary>
        public const string S1_TwoLineBa = "010100";
        /// <summary>
        /// 1连二Bb
        /// </summary>
        public const string S1_TwoLineBb = "001010";
        /// <summary>
        /// 1连二C
        /// </summary>
        public const string S1_TwoLineC = "010010";
        /// <summary>
        /// 2连二Aa
        /// </summary>
        public const string S2_TwoLineAa = "000220";
        /// <summary>
        /// 2连二Ab
        /// </summary>
        public const string S2_TwoLineAb = "022000";
        /// <summary>
        /// 2连二Ba
        /// </summary>
        public const string S2_TwoLineBa = "020200";
        /// <summary>
        /// 2连二Bb
        /// </summary>
        public const string S2_TwoLineBb = "002020";
        /// <summary>
        /// 2连二C
        /// </summary>
        public const string S2_TwoLineC = "020020";
        /// <summary>
        /// 1跳二A
        /// </summary>
        public const string S1_TwoJumpA = "001010";
        /// <summary>
        /// 1跳二B
        /// </summary>
        public const string S1_TwoJumpB = "010100";
        /// <summary>
        /// 2跳二A
        /// </summary>
        public const string S2_TwoJumpA = "002020";
        /// <summary>
        /// 2跳二B
        /// </summary>
        public const string S2_TwoJumpB = "020200";
        /// <summary>
        /// 1大跳二
        /// </summary>
        public const string S1_TwoBigJump = "001100";
        /// <summary>
        /// 2大跳二
        /// </summary>
        public const string S2_TwoBigJump = "002200";        
        #endregion

        

        public static string Runpath()
        {
            string m_strSavePath = Application.StartupPath;
            if (!m_strSavePath.EndsWith("\\") && !m_strSavePath.EndsWith("/"))
            {
                m_strSavePath += "\\";
            }
            return m_strSavePath;
        }
    }
    public enum GameResult
    {
        None,
        BlackVictory,
        WhiteVictory, 
        /// <summary>
        /// 平局
        /// </summary>
        Nogfall

    }
    public enum Player
    {
        /// <summary>
        /// 电脑
        /// </summary>
        Computer,
        /// <summary>
        /// 人
        /// </summary>
        Person
    }
    /// <summary>
    /// 当前要下的棋的颜色返回结果（0：已分胜负  1：平局   2：继续下棋）
    /// </summary>
    public enum ChessColor
    {
        White,
        Black
    }
    /// <summary>
    /// 当前棋盘的状态,已分胜负,平局,继续下棋
    /// </summary>
    public enum CurrChessBoardState
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 已分胜负
        /// </summary>
        GameOver,
        /// <summary>
        /// 平局
        /// </summary>
        Dogfall,
        /// <summary>
        /// 继续下棋
        /// </summary>
        ContinuePlayChess
    }
}
