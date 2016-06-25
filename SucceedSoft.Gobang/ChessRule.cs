using System;
using System.Collections.Generic;
using System.Text;
namespace SucceedSoft.Gobang
{
    /// <summary>
    /// ChessRule类用来判断输赢的规则方面
    /// </summary>
    public class ChessRule
    {
        #region 判断下棋位置是否有子
        /// <summary>
        /// 判断下棋位置是否有子,true有棋，false无棋
        /// </summary>
        /// <param name="m">下棋位置 X</param>
        /// <param name="n">下棋位置 Y</param>
        /// <param name="arrchessboard"></param>
        /// <returns></returns>
        public static bool IsExistChess(int m, int n, int[,] arrchessboard)
        {
            return arrchessboard[m, n] > 0;            
        }
        #endregion

        #region 当前棋盘的状态,形成五连已分胜负
        /// <summary>
        /// 当前棋盘的状态,形成五连已分胜负
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="arrchessboard"></param>
        /// <returns></returns>
        public static CurrChessBoardState Result(int m, int n, int[,] arrchessboard)
        {//(m,n)点已经下棋了m,n点四个方向的连子数，依次正东正西，正南正北方，西北东南，西南东北
            string strTemp = string.Empty;            
            //A
            strTemp = ChessRule.strTheEastWest(m, n, arrchessboard);
            if (arrchessboard[m,n] == 1 && strTemp.IndexOf(Const.S1_FiveLink) > -1)
                return CurrChessBoardState.GameOver;
            else if (arrchessboard[m, n] == 2 && strTemp.IndexOf(Const.S2_FiveLink) > -1)
                return CurrChessBoardState.GameOver;
            //B
            strTemp = ChessRule.strTheSouthNorth(m, n, arrchessboard);
             if (arrchessboard[m,n] == 1 && strTemp.IndexOf(Const.S1_FiveLink) > -1)
                 return CurrChessBoardState.GameOver;
            else if (arrchessboard[m, n] == 2 && strTemp.IndexOf(Const.S2_FiveLink) > -1)
                 return CurrChessBoardState.GameOver; 
            //C
            strTemp = ChessRule.strTheEastNorthWestSouth(m, n, arrchessboard);
             if (arrchessboard[m,n] == 1 && strTemp.IndexOf(Const.S1_FiveLink) > -1)
                 return CurrChessBoardState.GameOver;
            else if (arrchessboard[m, n] == 2 && strTemp.IndexOf(Const.S2_FiveLink) > -1)
                 return CurrChessBoardState.GameOver;
            //D
            strTemp = ChessRule.strTheWestNorthEastSouth(m, n, arrchessboard); 
             if (arrchessboard[m,n] == 1 && strTemp.IndexOf(Const.S1_FiveLink) > -1)
                 return CurrChessBoardState.GameOver;
            else if (arrchessboard[m, n] == 2 && strTemp.IndexOf(Const.S2_FiveLink) > -1)
                 return CurrChessBoardState.GameOver;
            
            return CheckTie(arrchessboard);
        }
        #endregion
     

        // 活四：在棋盘某一条阳线或阴线上有同色4子不间隔地紧紧相连，且在此4子两端延长线上各有一个无子的交*点与此4子紧密相连。
        // 冲四：除“活四”外的，再下一着棋便可形成五连，并且存在五连的可能性的局面。
        // 指活三，包括“连三”和“跳三”。
        // 连三：在棋盘某一条阳线或阴线上有同色三子相连，且在此三子两端延长线上有一端至少有一个，另一端至少有两个无子的交叉点与此三子紧密相连。
        // 跳三：中间仅间隔一个无子交*点的连三，但两端延长线均至少有一个无子的交叉点与此三子相连。       

        #region  棋盘是否下满
        /// <summary>
        /// 当所有位置都有子时为平局
        /// </summary>
        /// <param name="arrchessboard"></param>
        /// <returns>平局：true</returns>
        private static CurrChessBoardState CheckTie(int[,] arrchessboard)
        {            
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (arrchessboard[i, j] == 0)
                        return CurrChessBoardState.ContinuePlayChess;
                }
            }
            return CurrChessBoardState.Dogfall;
        }

        #endregion

        //东east南south西west北north

        #region / double 45度东北西南检测m，n点棋子权值 public static double TheEastNorthWestSouth(int m, int n, int[,] arrchessboard,bool bsmyself)
        /// <summary>
        ///  / 东北西南检测m，n点棋子权值
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="arrchessboard"></param>
        /// <returns></returns>
        public static double TheEastNorthWestSouth(int m, int n, int[,] arrchessboard, bool bsmyself)
        {
            double numqz = 0;
            numqz += CheckString(strTheEastNorthWestSouth(m, n, arrchessboard), arrchessboard[m, n].ToString(), bsmyself);
            return numqz;
        }        
        #endregion

        #region / string 45度东北西南检测(m，n)点最长的图案 public static string strTheEastNorthWestSouth(int m, int n, int[,] arrchessboard)
        /// <summary>
        ///  / 东北西南检测(m，n)点最长的图案
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="arrchessboard"></param>
        /// <returns></returns>
        public static string strTheEastNorthWestSouth(int m, int n, int[,] arrchessboard)
        {                 

            string strChessRecord = string.Empty;
            int i = 5;
            int x1, x2, y1, y2;
            x1 = m; x2 = m; y1 = n; y2 = n;
            while (x1 < 14 && y1 > 0 & i > 1)
            {
                x1++;
                y1--;
                i--;
            }

            int k = 12;
            while (x2 > 0 && y2 < 14 & k <= 15)
            {
                x2--;
                y2++;
                k++;
            }
            //OA.Common.LogInfo.OutLogInfo("m=" + m.ToString() + "--" + "n=" + n.ToString() + "--" + "x1=" + x1.ToString() + "--" + "y1=" + y1.ToString() + "--" + "x2=" + x2.ToString() + "--" + "y2=" + y2.ToString());
            for (int a = x1; a >= x2; a--)
            {
                //OA.Common.LogInfo.OutLogInfo("TheEastNorthWestSouth--a =" + Convert.ToString(a).ToString() + ",y1=" + Convert.ToString(y1).ToString());
                strChessRecord += arrchessboard[a, y1].ToString();
                y1++;
            }           

            return strChessRecord;

        }
        #endregion

        #region \ double 135度西北东南检测m，n点棋子权值 public static double TheWestNorthEastSouth(int m, int n, int[,] arrchessboard,bool bsmyself)
        /// <summary>
        ///  \ 西北东南检测m，n点棋子权值
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="arrchessboard"></param>
        /// <returns></returns>
        public static double TheWestNorthEastSouth(int m, int n, int[,] arrchessboard, bool bsmyself)
        {
            double numqz = 0;
            numqz += CheckString(strTheWestNorthEastSouth(m, n, arrchessboard), arrchessboard[m, n].ToString(), bsmyself);
            return numqz;
        }
        #endregion

        #region \ string 135度西北东南检测(m，n)点最长的图案 public static string strTheWestNorthEastSouth(int m, int n, int[,] arrchessboard)
        /// <summary>
        ///  \ 西北东南检测(m，n)点最长的图案
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="arrchessboard"></param>
        /// <returns></returns>
        public static string strTheWestNorthEastSouth(int m, int n, int[,] arrchessboard)
        {       

            string strChessRecord = string.Empty;
            int i = 5;
            int x1, x2, y1, y2;
            x1 = m; x2 = m; y1 = n; y2 = n;
            while (x1 > 0 && y1 > 0 & i > 1)
            {
                x1--;
                y1--;
                i--;
            }

            int k = 12;
            while (x2 < 14 && y2 < 14 & k <= 15)
            {
                x2++;
                y2++;
                k++;
            }

            for (int a = x1; a <= x2; a++)
            {
                //OA.Common.LogInfo.OutLogInfo("TheWestNorthEastSouth--a =" + Convert.ToString(a ).ToString() + ",y1 =" + Convert.ToString(y1).ToString());
                strChessRecord += arrchessboard[a, y1].ToString();
                y1++;
            }

            return strChessRecord;

        }
        #endregion

        #region | double 正南正北检测m，n点棋子权值 public static double TheSouthNorth(int m, int n, int[,] arrchessboard,bool bsmyself)
        /// <summary>
        ///  | 正南正北检测m，n点棋子权值
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="arrchessboard"></param>
        /// <returns></returns>
        public static double TheSouthNorth(int m, int n, int[,] arrchessboard, bool bsmyself)
        {
            double numqz = 0;//权值累计
            numqz += CheckString(strTheSouthNorth(m, n, arrchessboard), arrchessboard[m, n].ToString(), bsmyself);
           // OA.Common.LogInfo.OutLogInfo("| " + " [" + m.ToString() + "," + n.ToString() + "]=" + numqz.ToString() + " " + bsmyself.ToString());
            return numqz;
        }
        #endregion

        #region | string 正南正北检测(m，n)点最长的图案 public static string strTheSouthNorth(int m, int n, int[,] arrchessboard)
        /// <summary>
        ///  | 正南正北检测(m，n)点最长的图案
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="arrchessboard"></param>
        /// <returns></returns>
        public static string strTheSouthNorth(int m, int n, int[,] arrchessboard)
        {      

            string strChessRecord = string.Empty;
            if (n < 4)
            {//0,1,2,3 
                for (int a = 0; a <= n + 4; a++)
                {
                    // OA.Common.LogInfo.OutLogInfo("A_TheSouthNorth--a=" + Convert.ToString(a ).ToString() + ",m=" + Convert.ToString(m).ToString());
                    strChessRecord += arrchessboard[m, a].ToString();
                }
            }
            else if (n > 10)
            {//11,12,13,14
                for (int a = 14; a >= n - 4; a--)
                {
                    // OA.Common.LogInfo.OutLogInfo("B_TheSouthNorth--a=" + Convert.ToString(a).ToString() + ",m=" + Convert.ToString(m).ToString());
                    strChessRecord += arrchessboard[m, a].ToString();
                }
            }
            else
            {//4,5,6,7,8,9,10
                for (int a = n - 4; a <= n + 4; a++)
                {
                    // OA.Common.LogInfo.OutLogInfo("C_TheSouthNorth--a =" + Convert.ToString(a).ToString() + ",m=" + Convert.ToString(m).ToString());
                    strChessRecord += arrchessboard[m, a].ToString();
                }                
            }
            return strChessRecord;

        }
        #endregion

        #region - double 正东正西检测m，n点棋子权值 public static double TheEastWest(int m, int n, int[,] arrchessboard,bool bsmyself)
        /// <summary>
        ///  - 正东正西检测m，n点棋子权值
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="arrchessboard"></param>
        /// <returns></returns>
        public static double TheEastWest(int m, int n, int[,] arrchessboard, bool bsmyself)
        {
            double numqz = 0;//权值累计 
            numqz += CheckString(strTheEastWest(m, n, arrchessboard), arrchessboard[m, n].ToString(),bsmyself);
           // OA.Common.LogInfo.OutLogInfo("- " + " [" + m.ToString() + "," + n.ToString() + "]=" + numqz.ToString() + " " + bsmyself.ToString());
            return numqz;
        }
        #endregion

        #region - string 正东正西检测(m，n)点最长的图案 public static string strTheEastWest(int m, int n, int[,] arrchessboard)
        /// <summary>
        ///  - 正东正西检测(m，n)点最长的图案
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="arrchessboard"></param>
        /// <returns></returns>
        public static string strTheEastWest(int m, int n, int[,] arrchessboard)
        {
            string strChessRecord = string.Empty;
            if (m < 4)
            {//0,1,2,3 
                for (int a = 0; a <= m + 4; a++)
                {
                    //OA.Common.LogInfo.OutLogInfo("A_TheEastWest--a=" + Convert.ToString(a).ToString() + ",n=" + Convert.ToString(n).ToString());
                    strChessRecord += arrchessboard[a, n].ToString();
                }
            }
            else if (m > 10)
            {//11,12,13,14
                for (int a = 14; a >= m - 4; a--)
                {
                    //OA.Common.LogInfo.OutLogInfo("B_TheEastWest--a =" + Convert.ToString(a ).ToString() + ",n=" + Convert.ToString(n).ToString());
                    strChessRecord += arrchessboard[a, n].ToString();
                }
            }
            else
            {////4,5,6,7,8,9,10
                for (int a = m - 4; a <= m + 4; a++)
                {
                    //OA.Common.LogInfo.OutLogInfo("C_TheEastWest--a - 1=" + Convert.ToString(a - 1).ToString() + ",n=" + Convert.ToString(n).ToString());
                    strChessRecord += arrchessboard[a, n].ToString();
                }
            }
            return strChessRecord;
        }
        #endregion

        
        #region  计算权值
        /// <summary>
        /// 计算权值
        /// </summary>
        /// <param name="strWaitCheck">一个由字符组成的图案</param>
        /// <param name="strChessFlag">要考虑成形情况的棋子 1或者2</param>
        /// <param name="bmyself">true表示是自己在此处下棋,false是考虑对方在此处下棋</param>
        /// <returns></returns>
        private static double CheckString(string strWaitCheck, string strChessFlag, bool bmyself)
        { 
            //
            if (strChessFlag == "1")
            {////同时有多个时,要累计
                if (strWaitCheck.IndexOf(Const.S1_FiveLink) > -1)
                {
                    return bmyself ? Const.iFiveLineMy : Const.iFiveLineOt;
                }
                else if(strWaitCheck.IndexOf(Const.S1_FourLive) > -1)
                {
                    return bmyself ? Const.iFourLiveMy:Const.iFourLiveOt;
                }
                else if (strWaitCheck.IndexOf(Const.S1_FourRushA) > -1 || strWaitCheck.IndexOf(Const.S1_FourRushB) > -1 || strWaitCheck.IndexOf(Const.S1_FourRushC) > -1 || strWaitCheck.IndexOf(Const.S1_FourRushD) > -1 || strWaitCheck.IndexOf(Const.S1_FourRushE) > -1 )
                {
                    return bmyself ? Const.iFourRushMy : Const.iFourRushOt;
                }               
                else if (strWaitCheck.IndexOf(Const.S1_ThreeLineA) > -1 || strWaitCheck.IndexOf(Const.S1_ThreeLineB) > -1 )
                {
                    return bmyself ? Const.iThreeLineMy : Const.iThreeLineOt;
                }
                else if (strWaitCheck.IndexOf(Const.S1_ThreeJumpA) > -1 || strWaitCheck.IndexOf(Const.S1_ThreeJumpB) > -1 )
                {
                    return bmyself ? Const.iThreeJumpMy : Const.iThreeJumpOt;
                }               
                else if (strWaitCheck.IndexOf(Const.S1_ThreeDieA) > -1 || strWaitCheck.IndexOf(Const.S1_ThreeDieB) > -1 || strWaitCheck.IndexOf(Const.S1_ThreeDieC) > -1 || strWaitCheck.IndexOf(Const.S1_ThreeDieD) > -1 || strWaitCheck.IndexOf(Const.S1_ThreeDieE) > -1 || strWaitCheck.IndexOf(Const.S1_ThreeDieF) > -1 || strWaitCheck.IndexOf(Const.S1_ThreeDieG) > -1 || strWaitCheck.IndexOf(Const.S1_ThreeDieH) > -1 || strWaitCheck.IndexOf(Const.S1_ThreeDieI) > -1 || strWaitCheck.IndexOf(Const.S1_ThreeDieJ) > -1)
                {
                    return bmyself ? Const.iThreeDieMy:Const.iThreeDieOt;
                }
                else if (strWaitCheck.IndexOf(Const.S1_TwoLineAa) > -1 || strWaitCheck.IndexOf(Const.S1_TwoLineAb) > -1)
                {
                    return bmyself ? Const.iTwoLineAMy : Const.iTwoLineAOt;
                }
                else if (strWaitCheck.IndexOf(Const.S1_TwoLineBa) > -1 || strWaitCheck.IndexOf(Const.S1_TwoLineBb) > -1)
                {
                    return bmyself ? Const.iTwoLineBMy : Const.iTwoLineBOt;
                }
                else if (strWaitCheck.IndexOf(Const.S1_TwoLineC) > -1)
                {
                    return bmyself ? Const.iTwoLineCMy : Const.iTwoLineCOt;
                }
                else if (strWaitCheck.IndexOf(Const.S1_TwoJumpA) > -1 || strWaitCheck.IndexOf(Const.S1_TwoJumpB) > -1 )
                {
                    return bmyself ? Const.iTwoJumpMy : Const.iTwoJumpOt;
                }
                else if (strWaitCheck.IndexOf(Const.S1_TwoBigJump) > -1)
                {
                    return bmyself ? Const.iTwoBigJumpMy : Const.iTwoBigJumpOt;
                }
                else
                {
                    return Const.iZero;
                }
            }
            else if (strChessFlag == "2")
            {//同时有多个时,要累计
               // OA.Common.LogInfo.OutLogInfo("Test 2");
                if (strWaitCheck.IndexOf(Const.S2_FiveLink) > -1)
                {
                    return bmyself ? Const.iFiveLineMy : Const.iFiveLineOt;
                }
                else if (strWaitCheck.IndexOf(Const.S2_FourLive) > -1)
                {
                    return bmyself ? Const.iFourLiveMy : Const.iFourLiveOt;
                }
                else if (strWaitCheck.IndexOf(Const.S2_FourRushA) > -1 || strWaitCheck.IndexOf(Const.S2_FourRushB) > -1 || strWaitCheck.IndexOf(Const.S2_FourRushC) > -1 || strWaitCheck.IndexOf(Const.S2_FourRushD) > -1 || strWaitCheck.IndexOf(Const.S2_FourRushE) > -1 )
                {
                    return bmyself ? Const.iFourRushMy : Const.iFourRushOt;
                }
                else if (strWaitCheck.IndexOf(Const.S2_ThreeLineA) > -1 || strWaitCheck.IndexOf(Const.S2_ThreeLineB) > -1)
                {
                    return bmyself ? Const.iThreeLineMy : Const.iThreeLineOt;
                }
                else if (strWaitCheck.IndexOf(Const.S2_ThreeJumpA) > -1 || strWaitCheck.IndexOf(Const.S2_ThreeJumpB) > -1)
                {
                    return bmyself ? Const.iThreeJumpMy : Const.iThreeJumpOt;
                }                 
                else if (strWaitCheck.IndexOf(Const.S2_ThreeDieA) > -1 || strWaitCheck.IndexOf(Const.S2_ThreeDieB) > -1 || strWaitCheck.IndexOf(Const.S2_ThreeDieC) > -1 || strWaitCheck.IndexOf(Const.S2_ThreeDieD) > -1 || strWaitCheck.IndexOf(Const.S2_ThreeDieE) > -1 || strWaitCheck.IndexOf(Const.S2_ThreeDieF) > -1 || strWaitCheck.IndexOf(Const.S2_ThreeDieG) > -1 || strWaitCheck.IndexOf(Const.S2_ThreeDieH) > -1 || strWaitCheck.IndexOf(Const.S2_ThreeDieI) > -1 || strWaitCheck.IndexOf(Const.S2_ThreeDieJ) > -1)
                {
                    return bmyself ? Const.iThreeDieMy : Const.iThreeDieOt;
                }
                else if (strWaitCheck.IndexOf(Const.S2_TwoLineAa) > -1 || strWaitCheck.IndexOf(Const.S2_TwoLineAb) > -1)
                {
                    return bmyself ? Const.iTwoLineAMy : Const.iTwoLineAOt;
                }
                else if (strWaitCheck.IndexOf(Const.S2_TwoLineBa) > -1 || strWaitCheck.IndexOf(Const.S2_TwoLineBb) > -1)
                {
                    return bmyself ? Const.iTwoLineBMy : Const.iTwoLineBOt;
                }
                else if (strWaitCheck.IndexOf(Const.S2_TwoLineC) > -1 )
                {
                    return bmyself ? Const.iTwoLineCMy : Const.iTwoLineCOt;
                }
                else if (strWaitCheck.IndexOf(Const.S2_TwoJumpA) > -1 || strWaitCheck.IndexOf(Const.S2_TwoJumpB) > -1)
                {
                    return bmyself ? Const.iTwoJumpMy : Const.iTwoJumpOt;
                }
                else if (strWaitCheck.IndexOf(Const.S2_TwoBigJump) > -1)
                {
                    return bmyself ? Const.iTwoBigJumpMy : Const.iTwoBigJumpOt;
                }
                else
                {
                    return Const.iZero;
                }
            }
            else
            { return Const.iZero; }//if
        }
        #endregion

        #region New

        #region  包含最重要的图案
        /// <summary>
        /// 包含最重要的图案
        /// </summary>
        /// <param name="strWaitCheck">一个由字符组成的图案</param>
        /// <param name="strChessFlag">要考虑成形情况的棋子 1或者2</param>
        /// <param name="bmyself">true表示是自己在此处下棋,false是考虑对方在此处下棋</param>
        /// <returns></returns>
        private static string CheckStringStr(string strWaitCheck, string strChessFlag)
        {
            string strTemp = string.Empty;
            if (strChessFlag == Const.Value1)
            {
                #region 同时有多个时,要累计
                if (strWaitCheck.IndexOf(Const.S1_FiveLink) > -1)
                {
                    return Const.S1_FiveLink;
                }
                else if (strWaitCheck.IndexOf(Const.S1_FourLive) > -1)
                {
                    return Const.S1_FourLive;
                }
                else if (strWaitCheck.IndexOf(Const.S1_FourRushA) > -1 || strWaitCheck.IndexOf(Const.S1_FourRushB) > -1 ||
                    strWaitCheck.IndexOf(Const.S1_FourRushC) > -1 || strWaitCheck.IndexOf(Const.S1_FourRushD) > -1 ||
                    strWaitCheck.IndexOf(Const.S1_FourRushE) > -1)
                {
                    if (strWaitCheck.IndexOf(Const.S1_FourRushA) > -1)
                        strTemp = Const.S1_FourRushA + ",";
                    if (strWaitCheck.IndexOf(Const.S1_FourRushB) > -1)
                        strTemp += Const.S1_FourRushB + ",";
                    if (strWaitCheck.IndexOf(Const.S1_FourRushC) > -1)
                        strTemp += Const.S1_FourRushC + ",";
                    if (strWaitCheck.IndexOf(Const.S1_FourRushD) > -1)
                        strTemp += Const.S1_FourRushD + ",";
                    if (strWaitCheck.IndexOf(Const.S1_FourRushE) > -1)
                        strTemp += Const.S1_FourRushE + ",";
                    strTemp = strTemp.Substring(0, strTemp.Length - 1);
                    return strTemp;
                }
                else if (strWaitCheck.IndexOf(Const.S1_ThreeLineA) > -1 || strWaitCheck.IndexOf(Const.S1_ThreeLineB) > -1)
                {
                    if (strWaitCheck.IndexOf(Const.S1_ThreeLineA) > -1)
                        strTemp = Const.S1_ThreeLineA + ",";
                    if (strWaitCheck.IndexOf(Const.S1_ThreeLineB) > -1)
                        strTemp += Const.S1_ThreeLineB + ",";
                    strTemp = strTemp.Substring(0, strTemp.Length - 1);
                    return strTemp;
                }
                else if (strWaitCheck.IndexOf(Const.S1_ThreeJumpA) > -1 || strWaitCheck.IndexOf(Const.S1_ThreeJumpB) > -1)
                {
                    if (strWaitCheck.IndexOf(Const.S1_ThreeJumpA) > -1)
                        strTemp = Const.S1_ThreeJumpA + ",";
                    if (strWaitCheck.IndexOf(Const.S1_ThreeJumpB) > -1)
                        strTemp += Const.S1_ThreeJumpB + ",";
                    strTemp = strTemp.Substring(0, strTemp.Length - 1);
                    return strTemp;
                }
                else if (strWaitCheck.IndexOf(Const.S1_ThreeDieA) > -1 || strWaitCheck.IndexOf(Const.S1_ThreeDieB) > -1 ||
                    strWaitCheck.IndexOf(Const.S1_ThreeDieC) > -1 || strWaitCheck.IndexOf(Const.S1_ThreeDieD) > -1 ||
                    strWaitCheck.IndexOf(Const.S1_ThreeDieE) > -1 || strWaitCheck.IndexOf(Const.S1_ThreeDieF) > -1 ||
                    strWaitCheck.IndexOf(Const.S1_ThreeDieG) > -1 || strWaitCheck.IndexOf(Const.S1_ThreeDieH) > -1 ||
                    strWaitCheck.IndexOf(Const.S1_ThreeDieI) > -1 || strWaitCheck.IndexOf(Const.S1_ThreeDieJ) > -1)
                {
                    if (strWaitCheck.IndexOf(Const.S1_ThreeDieA) > -1)
                        strTemp = Const.S1_ThreeDieA + ",";
                    if (strWaitCheck.IndexOf(Const.S1_ThreeDieB) > -1)
                        strTemp += Const.S1_ThreeDieB + ",";
                    if (strWaitCheck.IndexOf(Const.S1_ThreeDieC) > -1)
                        strTemp += Const.S1_ThreeDieC + ",";
                    if (strWaitCheck.IndexOf(Const.S1_ThreeDieD) > -1)
                        strTemp += Const.S1_ThreeDieD + ",";
                    if (strWaitCheck.IndexOf(Const.S1_ThreeDieE) > -1)
                        strTemp += Const.S1_ThreeDieE + ",";
                    if (strWaitCheck.IndexOf(Const.S1_ThreeDieF) > -1)
                        strTemp += Const.S1_ThreeDieF + ",";
                    if (strWaitCheck.IndexOf(Const.S1_ThreeDieG) > -1)
                        strTemp += Const.S1_ThreeDieG + ",";
                    if (strWaitCheck.IndexOf(Const.S1_ThreeDieH) > -1)
                        strTemp += Const.S1_ThreeDieH + ",";
                    if (strWaitCheck.IndexOf(Const.S1_ThreeDieI) > -1)
                        strTemp += Const.S1_ThreeDieI + ",";
                    if (strWaitCheck.IndexOf(Const.S1_ThreeDieJ) > -1)
                        strTemp += Const.S1_ThreeDieJ + ",";
                    strTemp = strTemp.Substring(0, strTemp.Length - 1);
                    return strTemp;
                }
                else if (strWaitCheck.IndexOf(Const.S1_TwoLineAa) > -1 || strWaitCheck.IndexOf(Const.S1_TwoLineAb) > -1)
                {
                    if (strWaitCheck.IndexOf(Const.S1_TwoLineAa) > -1)
                        strTemp = Const.S1_TwoLineAa + ",";
                    if (strWaitCheck.IndexOf(Const.S1_TwoLineAb) > -1)
                        strTemp += Const.S1_TwoLineAb + ",";
                    strTemp = strTemp.Substring(0, strTemp.Length - 1);
                    return strTemp;
                }
                else if (strWaitCheck.IndexOf(Const.S1_TwoLineBa) > -1 || strWaitCheck.IndexOf(Const.S1_TwoLineBb) > -1)
                {
                    if (strWaitCheck.IndexOf(Const.S1_TwoLineBa) > -1)
                        strTemp = Const.S1_TwoLineBa + ",";
                    if (strWaitCheck.IndexOf(Const.S1_TwoLineBb) > -1)
                        strTemp += Const.S1_TwoLineBb + ",";
                    strTemp = strTemp.Substring(0, strTemp.Length - 1);
                    return strTemp;
                }
                else if (strWaitCheck.IndexOf(Const.S1_TwoLineC) > -1)
                {
                    return Const.S1_TwoLineC;
                }
                else if (strWaitCheck.IndexOf(Const.S1_TwoJumpA) > -1 || strWaitCheck.IndexOf(Const.S1_TwoJumpB) > -1)
                {
                    if (strWaitCheck.IndexOf(Const.S1_TwoJumpA) > -1)
                        strTemp = Const.S1_TwoJumpA + ",";
                    if (strWaitCheck.IndexOf(Const.S1_TwoJumpB) > -1)
                        strTemp += Const.S1_TwoJumpB + ",";

                    strTemp = strTemp.Substring(0, strTemp.Length - 1);
                    return strTemp;
                }
                else if (strWaitCheck.IndexOf(Const.S1_TwoBigJump) > -1)
                {
                    return Const.S1_TwoBigJump;
                }
                else
                {
                    return string.Empty;
                }

                #endregion
            }
            else if (strChessFlag == Const.Value2)
            {
                #region 同时有多个时,要累计
                if (strWaitCheck.IndexOf(Const.S2_FiveLink) > -1)
                {
                    return Const.S2_FiveLink;
                }
                else if (strWaitCheck.IndexOf(Const.S2_FourLive) > -1)
                {
                    return Const.S2_FourLive;
                }
                else if (strWaitCheck.IndexOf(Const.S2_FourRushA) > -1 || strWaitCheck.IndexOf(Const.S2_FourRushB) > -1 ||
                    strWaitCheck.IndexOf(Const.S2_FourRushC) > -1 || strWaitCheck.IndexOf(Const.S2_FourRushD) > -1 ||
                    strWaitCheck.IndexOf(Const.S2_FourRushE) > -1)
                {
                    if (strWaitCheck.IndexOf(Const.S2_FourRushA) > -1)
                        strTemp = Const.S2_FourRushA + ",";
                    if (strWaitCheck.IndexOf(Const.S2_FourRushB) > -1)
                        strTemp += Const.S2_FourRushB + ",";
                    if (strWaitCheck.IndexOf(Const.S2_FourRushC) > -1)
                        strTemp += Const.S2_FourRushC + ",";
                    if (strWaitCheck.IndexOf(Const.S2_FourRushD) > -1)
                        strTemp += Const.S2_FourRushD + ",";
                    if (strWaitCheck.IndexOf(Const.S2_FourRushE) > -1)
                        strTemp += Const.S2_FourRushE + ",";
                    strTemp = strTemp.Substring(0, strTemp.Length - 1);
                    return strTemp;
                }
                else if (strWaitCheck.IndexOf(Const.S2_ThreeLineA) > -1 || strWaitCheck.IndexOf(Const.S2_ThreeLineB) > -1)
                {
                    if (strWaitCheck.IndexOf(Const.S2_ThreeLineA) > -1)
                        strTemp = Const.S2_ThreeLineA + ",";
                    if (strWaitCheck.IndexOf(Const.S2_ThreeLineB) > -1)
                        strTemp += Const.S2_ThreeLineB + ",";
                    strTemp = strTemp.Substring(0, strTemp.Length - 1);
                    return strTemp;
                }
                else if (strWaitCheck.IndexOf(Const.S2_ThreeJumpA) > -1 || strWaitCheck.IndexOf(Const.S2_ThreeJumpB) > -1)
                {
                    if (strWaitCheck.IndexOf(Const.S2_ThreeJumpA) > -1)
                        strTemp = Const.S2_ThreeJumpA + ",";
                    if (strWaitCheck.IndexOf(Const.S2_ThreeJumpB) > -1)
                        strTemp += Const.S2_ThreeJumpB + ",";
                    strTemp = strTemp.Substring(0, strTemp.Length - 1);
                    return strTemp;
                }
                else if (strWaitCheck.IndexOf(Const.S2_ThreeDieA) > -1 || strWaitCheck.IndexOf(Const.S2_ThreeDieB) > -1 ||
                    strWaitCheck.IndexOf(Const.S2_ThreeDieC) > -1 || strWaitCheck.IndexOf(Const.S2_ThreeDieD) > -1 ||
                    strWaitCheck.IndexOf(Const.S2_ThreeDieE) > -1 || strWaitCheck.IndexOf(Const.S2_ThreeDieF) > -1 ||
                    strWaitCheck.IndexOf(Const.S2_ThreeDieG) > -1 || strWaitCheck.IndexOf(Const.S2_ThreeDieH) > -1 ||
                    strWaitCheck.IndexOf(Const.S2_ThreeDieI) > -1 || strWaitCheck.IndexOf(Const.S2_ThreeDieJ) > -1)
                {
                    if (strWaitCheck.IndexOf(Const.S2_ThreeDieA) > -1)
                        strTemp = Const.S2_ThreeDieA + ",";
                    if (strWaitCheck.IndexOf(Const.S2_ThreeDieB) > -1)
                        strTemp += Const.S2_ThreeDieB + ",";
                    if (strWaitCheck.IndexOf(Const.S2_ThreeDieC) > -1)
                        strTemp += Const.S2_ThreeDieC + ",";
                    if (strWaitCheck.IndexOf(Const.S2_ThreeDieD) > -1)
                        strTemp += Const.S2_ThreeDieD + ",";
                    if (strWaitCheck.IndexOf(Const.S2_ThreeDieE) > -1)
                        strTemp += Const.S2_ThreeDieE + ",";
                    if (strWaitCheck.IndexOf(Const.S2_ThreeDieF) > -1)
                        strTemp += Const.S2_ThreeDieF + ",";
                    if (strWaitCheck.IndexOf(Const.S2_ThreeDieG) > -1)
                        strTemp += Const.S2_ThreeDieG + ",";
                    if (strWaitCheck.IndexOf(Const.S2_ThreeDieH) > -1)
                        strTemp += Const.S2_ThreeDieH + ",";
                    if (strWaitCheck.IndexOf(Const.S2_ThreeDieI) > -1)
                        strTemp += Const.S2_ThreeDieI + ",";
                    if (strWaitCheck.IndexOf(Const.S2_ThreeDieJ) > -1)
                        strTemp += Const.S2_ThreeDieJ + ",";
                    strTemp = strTemp.Substring(0, strTemp.Length - 1);
                    return strTemp;
                }
                else if (strWaitCheck.IndexOf(Const.S2_TwoLineAa) > -1 || strWaitCheck.IndexOf(Const.S2_TwoLineAb) > -1)
                {
                    if (strWaitCheck.IndexOf(Const.S2_TwoLineAa) > -1)
                        strTemp = Const.S2_TwoLineAa + ",";
                    if (strWaitCheck.IndexOf(Const.S2_TwoLineAb) > -1)
                        strTemp += Const.S2_TwoLineAb + ",";
                    strTemp = strTemp.Substring(0, strTemp.Length - 1);
                    return strTemp;
                }
                else if (strWaitCheck.IndexOf(Const.S2_TwoLineBa) > -1 || strWaitCheck.IndexOf(Const.S2_TwoLineBb) > -1)
                {
                    if (strWaitCheck.IndexOf(Const.S2_TwoLineBa) > -1)
                        strTemp = Const.S2_TwoLineBa + ",";
                    if (strWaitCheck.IndexOf(Const.S2_TwoLineBb) > -1)
                        strTemp += Const.S2_TwoLineBb + ",";
                    strTemp = strTemp.Substring(0, strTemp.Length - 1);
                    return strTemp;
                }
                else if (strWaitCheck.IndexOf(Const.S2_TwoLineC) > -1)
                {
                    return Const.S2_TwoLineC;
                }
                else if (strWaitCheck.IndexOf(Const.S2_TwoJumpA) > -1 || strWaitCheck.IndexOf(Const.S2_TwoJumpB) > -1)
                {
                    if (strWaitCheck.IndexOf(Const.S2_TwoJumpA) > -1)
                        strTemp = Const.S2_TwoJumpA + ",";
                    if (strWaitCheck.IndexOf(Const.S2_TwoJumpB) > -1)
                        strTemp += Const.S2_TwoJumpB + ",";
                    strTemp = strTemp.Substring(0, strTemp.Length - 1);
                    return strTemp;
                }
                else if (strWaitCheck.IndexOf(Const.S2_TwoBigJump) > -1)
                {
                    return Const.S2_TwoBigJump;
                }
                else
                {
                    return string.Empty;
                }
                #endregion
            }
            else
            { return string.Empty; }//if
        }
        #endregion       

        #region / string 45度东北西南检测(m，n)点最长的图案 public static string strTheEastNorthWestSouth(int m, int n, int[,] arrchessboard)
        /// <summary>
        ///  / 东北西南检测(m，n)点最长的图案
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="arrchessboard"></param>
        /// <returns></returns>
        public static string strTheEastNorthWestSouthStr(int m, int n, int[,] arrchessboard)
        {
            string strChessRecord = string.Empty;
            int i = 5;
            int x1, x2, y1, y2;
            x1 = m; x2 = m; y1 = n; y2 = n;
            while (x1 < 14 && y1 > 0 & i > 1)
            {
                x1++;
                y1--;
                i--;
            }

            int k = 12;
            while (x2 > 0 && y2 < 14 & k <= 15)
            {
                x2--;
                y2++;
                k++;
            }
            
            for (int a = x1; a >= x2; a--)
            {
                strChessRecord += arrchessboard[a, y1].ToString();
                y1++;
            }

            return CheckStringStr(strChessRecord, arrchessboard[m,n].ToString());
        }
        #endregion        

        #region \ string 135度西北东南检测(m，n)点最长的图案 public static string strTheWestNorthEastSouth(int m, int n, int[,] arrchessboard)
        /// <summary>
        ///  \ 西北东南检测(m，n)点最长的图案
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="arrchessboard"></param>
        /// <returns></returns>
        public static string strTheWestNorthEastSouthStr(int m, int n, int[,] arrchessboard)
        {
            string strChessRecord = string.Empty;
            int i = 5;
            int x1, x2, y1, y2;
            x1 = m; x2 = m; y1 = n; y2 = n;
            while (x1 > 0 && y1 > 0 & i > 1)
            {
                x1--;
                y1--;
                i--;
            }

            int k = 12;
            while (x2 < 14 && y2 < 14 & k <= 15)
            {
                x2++;
                y2++;
                k++;
            }

            for (int a = x1; a <= x2; a++)
            {
                strChessRecord += arrchessboard[a, y1].ToString();
                y1++;
            }

            return CheckStringStr(strChessRecord, arrchessboard[m, n].ToString());

        }
        #endregion        

        #region | string 正南正北检测(m，n)点最长的图案 public static string strTheSouthNorth(int m, int n, int[,] arrchessboard)
        /// <summary>
        ///  | 正南正北检测(m，n)点最长的图案
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="arrchessboard"></param>
        /// <returns></returns>
        public static string strTheSouthNorthStr(int m, int n, int[,] arrchessboard)
        {
            string strChessRecord = string.Empty;
            if (n < 4)
            {//0,1,2,3 
                for (int a = 0; a <= n + 4; a++)
                {
                    strChessRecord += arrchessboard[m, a].ToString();
                }
            }
            else if (n > 10)
            {//11,12,13,14
                for (int a = 14; a >= n - 4; a--)
                {
                    strChessRecord += arrchessboard[m, a].ToString();
                }
            }
            else
            {//4,5,6,7,8,9,10
                for (int a = n - 4; a <= n + 4; a++)
                {
                    strChessRecord += arrchessboard[m, a].ToString();
                }
            }
            return CheckStringStr(strChessRecord, arrchessboard[m, n].ToString());

        }
        #endregion        

        #region - string 正东正西检测(m，n)点最长的图案 public static string strTheEastWest(int m, int n, int[,] arrchessboard)
        /// <summary>
        ///  - 正东正西检测(m，n)点最长的图案
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="arrchessboard"></param>
        /// <returns></returns>
        public static string strTheEastWestStr(int m, int n, int[,] arrchessboard)
        {
            string strChessRecord = string.Empty;
            if (m < 4)
            {//0,1,2,3 
                for (int a = 0; a <= m + 4; a++)
                {
                    strChessRecord += arrchessboard[a, n].ToString();
                }
            }
            else if (m > 10)
            {//11,12,13,14
                for (int a = 14; a >= m - 4; a--)
                {
                    strChessRecord += arrchessboard[a, n].ToString();
                }
            }
            else
            {////4,5,6,7,8,9,10
                for (int a = m - 4; a <= m + 4; a++)
                {
                    strChessRecord += arrchessboard[a, n].ToString();
                }
            }
            return CheckStringStr(strChessRecord, arrchessboard[m, n].ToString());
        }
        #endregion
        #endregion

    }
}