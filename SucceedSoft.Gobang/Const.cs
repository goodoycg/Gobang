using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SucceedSoft.Gobang
{
    public class Const
    {//2147483647 >= int32 >= -2147483648

        //20100711094749.JPG Ȩֵ���㲻׼

        public const string Value0 = "0";//����
        public const string Value1 = "1";//����
        public const string Value2 = "2";//����

        #region ����
        public static string SystemTitle = "����������";
        /// <summary>
        /// ��Ϸ�����
        /// </summary>
        public static GameResult GameOverResult = GameResult.None;
        /// <summary>
        /// ���ؽ��
        /// </summary>
        public static CurrChessBoardState Result = CurrChessBoardState.None;
        /// <summary>
        /// ����Ԥ�ⲽ��
        /// </summary>
        public static double dForecastStepCount = 1;
        /// <summary>
        /// ���̸��ӵĴ�С
        /// </summary>
        public const int ChessCellSize = 25;
        /// <summary>
        /// ��߱߿���
        /// </summary>
        public const int LeftBoardWidth = 3;
        /// <summary>
        /// �ϱ߱������߶�
        /// </summary>
        public const int TopTitleHeight = 158;
        /// <summary>
        /// �ұ����ӽ���
        /// </summary>
        public const int RightChessLimit = 370;
        /// <summary>
        /// �±����ӽ���
        /// </summary>
        public const int BottomChessLimit = 520;
        /// <summary>
        /// ����λ�õ���X
        /// </summary>
        public const int ExcursionX = 0;
        /// <summary>
        /// ����λ�õ���Y
        /// </summary>
        public const int ExcursionY = 0;
        /// <summary>
        /// ���Ӳ����ַ�λ�õ���X(һλ��)
        /// </summary>
        public const int ChessStepX = 7;
        /// <summary>
        /// ���Ӳ����ַ�λ�õ���X(��λ��)
        /// </summary>
        public const int ChessStepX2 = 4;  
        /// <summary>
        /// ���Ӳ����ַ�λ�õ���Y
        /// </summary>
        public const int ChessStepY = 5;
        /// <summary>
        /// +�ŵ�λ�õ���X
        /// </summary>
        public const int PlusExcursionX = 1;
        /// <summary>
        /// +�ŵ�λ�õ���Y
        /// </summary>
        public const int PlusExcursionY = 0;
        /// <summary>
        /// ��Ϸ�Ƿ����
        /// </summary>
        public static bool bGameOver = false;
        /// <summary>
        /// ��ǰ��Ϸ���˻��ǵ�����˼������
        /// </summary>
        public static Player bCurrPlayer = Player.Person;
        /// <summary>
        /// ������������
        /// </summary>
        public static int iPlayedChessCount = 0;           
        /// <summary>
        /// �������
        /// </summary>
        public static Player FirstPlayer = Player.Person;
        /// <summary>
        /// ����������Χ ChessBoardCellCount ��֮�ڵ���С����(ǰ2��3������������Χ4����С����)
        /// </summary>
        public static int ChessBoardCellCount = 2;        
        /// <summary>
        /// Χס���̵����ӵ���С�������϶���X
        /// </summary>
        public static int Xmin = 5;
        /// <summary>
        /// Χס���̵����ӵ���С�������¶���X
        /// </summary>
        public static int Xmax = 9;
        /// <summary>
        /// Χס���̵����ӵ���С�������϶���Y
        /// </summary>
        public static int Ymin = 5;
        /// <summary>
        /// Χס���̵����ӵ���С�������¶���Y
        /// </summary>
        public static int Ymax = 9;

        #endregion

        #region Ȩֵ
        /// <summary>
        /// �����ѷ�Ȩֵ    10000000 7��0
        /// </summary>
        public static double iFiveLineMy        = 10000000;
        /// <summary>
        /// �����Է�Ȩֵ    1000000 6��0
        /// </summary>
        public static double iFiveLineOt        = 1000000;
        /// <summary>
        /// �����ѷ�Ȩֵ    100000 5��0
        /// </summary>
        public static double iFourLiveMy        = 100000;
        /// <summary>
        /// ���ĶԷ�Ȩֵ    10000 4��0
        /// </summary>
        public static double iFourLiveOt        = 10000;
        /// <summary>
        /// �����ѷ�Ȩֵ    1000 3��0
        /// </summary>
        public static double iFourRushMy        = 1000;
        /// <summary>
        /// ���ĶԷ�Ȩֵ    750
        /// </summary>
        public static double iFourRushOt        = 750;
        //-----------------------Ҫ���أ�2 * �����Է�Ȩֵ > �����ѷ�Ȩֵ (��������Ҫ��Ҫ��һ������) 550 * 2 > 1000 ----------
        
        /// <summary>
        /// �����ѷ�Ȩֵ    700
        /// </summary>
        public static double iThreeLineMy       = 700;
        /// <summary>
        /// �����Է�Ȩֵ    650
        /// </summary>
        public static double iThreeLineOt       = 650;
        /// <summary>
        /// �����ѷ�Ȩֵ    600
        /// </summary>
        public static double iThreeJumpMy       = 600;
        /// <summary>
        /// �����Է�Ȩֵ    550
        /// </summary>
        public static double iThreeJumpOt       = 550;
        /// <summary>
        /// �����ѷ�Ȩֵ    500
        /// </summary>
        public static double iThreeDieMy        = 500;
        /// <summary>
        /// �����Է�Ȩֵ    480
        /// </summary>
        public static double iThreeDieOt        = 480;
        //-----------------�ѷ�,�Է�Ȩֵ��ͬ,����,����,������ȨֵҲ��ͬ------------------------------------------------------------------------------

        //B----------------Ҫ���أ�2 * �������Է�Ȩֵ > �����ѷ�Ȩֵ (�������Ҫ��Ҫ��һ������) 280 * 2 > 500 -----------------------------------------------------------------------------------
        /// <summary>
        /// ����A�ѷ�Ȩֵ        460
        /// </summary>
        public static double iTwoLineAMy  = 460;
        /// <summary>
        /// ����A�Է�Ȩֵ        440
        /// </summary>
        public static double iTwoLineAOt = 440;
        /// <summary>
        /// ����B�ѷ�Ȩֵ        420
        /// </summary>
        public static double iTwoLineBMy = 420;
        /// <summary>
        /// ����B�Է�Ȩֵ        400
        /// </summary>
        public static double iTwoLineBOt = 400;
        /// <summary>
        /// ����C�ѷ�Ȩֵ        380
        /// </summary>
        public static double iTwoLineCMy = 380;
        /// <summary>
        /// ����C�Է�Ȩֵ        360
        /// </summary>
        public static double iTwoLineCOt = 360;
        /// <summary>
        /// �����ѷ�Ȩֵ        340
        /// </summary>
        public static double iTwoJumpMy = 340;
        /// <summary>
        /// �����Է�Ȩֵ        320
        /// </summary>
        public static double iTwoJumpOt = 320;
        /// <summary>
        /// �������ѷ�Ȩֵ        300
        /// </summary>
        public static double iTwoBigJumpMy = 300;
        /// <summary>
        /// �������Է�Ȩֵ        280
        /// </summary>
        public static double iTwoBigJumpOt= 280;
        //B----------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// һȨֵ    1
        /// </summary>
        public static double iOne               = 1;
        /// <summary>
        /// ��Ȩֵ    0
        /// </summary>
        public static double iZero              = 0;

        #endregion        
       
        #region ��������
        /// <summary>
        /// ������������
        /// </summary>
        public const string ComputerPlaying = "������������";  
        /// <summary>
        /// �����������
        /// </summary>
        public const string PersonPlaying = "�����������";
        #endregion

        #region ��ͼ
        /// <summary>
        /// 1����
        /// </summary>
        public const string S1_FiveLink = "11111";
        /// <summary>
        /// 2����
        /// </summary>
        public const string S2_FiveLink = "22222";
        /// <summary>
        /// 1����
        /// </summary>
        public const string S1_FourLive = "011110";
        /// <summary>
        /// 2����
        /// </summary>
        public const string S2_FourLive = "022220";
        /// <summary>
        /// 1����A
        /// </summary>
        public const string S1_FourRushA = "01111";
        /// <summary>
        /// 1����B
        /// </summary>
        public const string S1_FourRushB = "11110";
        /// <summary>
        /// 1����C
        /// </summary>
        public const string S1_FourRushC = "10111";
        /// <summary>
        /// 1����D
        /// </summary>
        public const string S1_FourRushD = "11101";
        /// <summary>
        /// 1����E
        /// </summary>
        public const string S1_FourRushE = "11011";
        /// <summary>
        /// 2����A
        /// </summary>
        public const string S2_FourRushA = "02222";
        /// <summary>
        /// 2����B
        /// </summary>
        public const string S2_FourRushB = "22220";
        /// <summary>
        /// 2����C
        /// </summary>
        public const string S2_FourRushC = "20222";
        /// <summary>
        /// 2����D
        /// </summary>
        public const string S2_FourRushD = "22202";
        /// <summary>
        /// 2����E
        /// </summary>
        public const string S2_FourRushE = "22022";
        /// <summary>
        /// 1����A
        /// </summary>                
        public const string S1_ThreeLineA = "001110";
        /// <summary>
        /// 1����B
        /// </summary>  
        public const string S1_ThreeLineB = "011100";
        /// <summary>
        /// 2����A
        /// </summary>                
        public const string S2_ThreeLineA = "002220";
        /// <summary>
        /// 2����B
        /// </summary>  
        public const string S2_ThreeLineB = "022200";
        /// <summary>
        /// 1����A
        /// </summary>  
        public const string S1_ThreeJumpA = "010110";
        /// <summary>
        /// 1����B
        /// </summary>  
        public const string S1_ThreeJumpB = "011010";
        /// <summary>
        /// 2����A
        /// </summary>  
        public const string S2_ThreeJumpA = "020220";
        /// <summary>
        /// 2����B
        /// </summary>  
        public const string S2_ThreeJumpB = "022020";
        /// <summary>
        /// 1����A
        /// </summary>
        public const string S1_ThreeDieA = "211100";
        /// <summary>
        /// 1����B
        /// </summary>
        public const string S1_ThreeDieB = "001112";
        /// <summary>
        /// 1����C
        /// </summary>
        public const string S1_ThreeDieC = "211100";
        /// <summary>
        /// 1����D
        /// </summary>
        public const string S1_ThreeDieD = "201110";
        /// <summary>
        /// 1����E
        /// </summary>
        public const string S1_ThreeDieE = "011102";
        /// <summary>
        /// 1����F
        /// </summary>
        public const string S1_ThreeDieF = "210110";
        /// <summary>
        /// 1����G
        /// </summary>
        public const string S1_ThreeDieG = "011012";
        /// <summary>
        /// 1����H
        /// </summary>
        public const string S1_ThreeDieH = "211010";
        /// <summary>
        /// 1����I
        /// </summary>
        public const string S1_ThreeDieI = "010112";
        /// <summary>
        /// 1����J
        /// </summary>
        public const string S1_ThreeDieJ = "2011102";
        /// <summary>
        /// 2����A
        /// </summary>
        public const string S2_ThreeDieA = "122200";
        /// <summary>
        /// 2����B
        /// </summary>
        public const string S2_ThreeDieB = "002221";
        /// <summary>
        /// 2����C
        /// </summary>
        public const string S2_ThreeDieC = "122200";
        /// <summary>
        /// 2����D
        /// </summary>
        public const string S2_ThreeDieD = "102220";
        /// <summary>
        /// 2����E
        /// </summary>
        public const string S2_ThreeDieE = "022201";
        /// <summary>
        /// 2����F
        /// </summary>
        public const string S2_ThreeDieF = "120220";
        /// <summary>
        /// 2����G
        /// </summary>
        public const string S2_ThreeDieG = "022021";
        /// <summary>
        /// 2����H
        /// </summary>
        public const string S2_ThreeDieH = "122020";
        /// <summary>
        /// 2����I
        /// </summary>
        public const string S2_ThreeDieI = "020221";
        /// <summary>
        /// 2����J
        /// </summary>
        public const string S2_ThreeDieJ = "1022201";


        /// <summary>
        /// 1����Aa
        /// </summary>
        public const string S1_TwoLineAa = "000110";
        /// <summary>
        /// 1����Ab
        /// </summary>
        public const string S1_TwoLineAb = "011000";               
        /// <summary>
        /// 1����Ba
        /// </summary>
        public const string S1_TwoLineBa = "010100";
        /// <summary>
        /// 1����Bb
        /// </summary>
        public const string S1_TwoLineBb = "001010";
        /// <summary>
        /// 1����C
        /// </summary>
        public const string S1_TwoLineC = "010010";
        /// <summary>
        /// 2����Aa
        /// </summary>
        public const string S2_TwoLineAa = "000220";
        /// <summary>
        /// 2����Ab
        /// </summary>
        public const string S2_TwoLineAb = "022000";
        /// <summary>
        /// 2����Ba
        /// </summary>
        public const string S2_TwoLineBa = "020200";
        /// <summary>
        /// 2����Bb
        /// </summary>
        public const string S2_TwoLineBb = "002020";
        /// <summary>
        /// 2����C
        /// </summary>
        public const string S2_TwoLineC = "020020";
        /// <summary>
        /// 1����A
        /// </summary>
        public const string S1_TwoJumpA = "001010";
        /// <summary>
        /// 1����B
        /// </summary>
        public const string S1_TwoJumpB = "010100";
        /// <summary>
        /// 2����A
        /// </summary>
        public const string S2_TwoJumpA = "002020";
        /// <summary>
        /// 2����B
        /// </summary>
        public const string S2_TwoJumpB = "020200";
        /// <summary>
        /// 1������
        /// </summary>
        public const string S1_TwoBigJump = "001100";
        /// <summary>
        /// 2������
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
        /// ƽ��
        /// </summary>
        Nogfall

    }
    public enum Player
    {
        /// <summary>
        /// ����
        /// </summary>
        Computer,
        /// <summary>
        /// ��
        /// </summary>
        Person
    }
    /// <summary>
    /// ��ǰҪ�µ������ɫ���ؽ����0���ѷ�ʤ��  1��ƽ��   2���������壩
    /// </summary>
    public enum ChessColor
    {
        White,
        Black
    }
    /// <summary>
    /// ��ǰ���̵�״̬,�ѷ�ʤ��,ƽ��,��������
    /// </summary>
    public enum CurrChessBoardState
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// �ѷ�ʤ��
        /// </summary>
        GameOver,
        /// <summary>
        /// ƽ��
        /// </summary>
        Dogfall,
        /// <summary>
        /// ��������
        /// </summary>
        ContinuePlayChess
    }
}
