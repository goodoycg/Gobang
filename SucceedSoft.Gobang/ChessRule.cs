using System;
using System.Collections.Generic;
using System.Text;
namespace SucceedSoft.Gobang
{
    /// <summary>
    /// ChessRule�������ж���Ӯ�Ĺ�����
    /// </summary>
    public class ChessRule
    {
        #region �ж�����λ���Ƿ�����
        /// <summary>
        /// �ж�����λ���Ƿ�����,true���壬false����
        /// </summary>
        /// <param name="m">����λ�� X</param>
        /// <param name="n">����λ�� Y</param>
        /// <param name="arrchessboard"></param>
        /// <returns></returns>
        public static bool IsExistChess(int m, int n, int[,] arrchessboard)
        {
            return arrchessboard[m, n] > 0;            
        }
        #endregion

        #region ��ǰ���̵�״̬,�γ������ѷ�ʤ��
        /// <summary>
        /// ��ǰ���̵�״̬,�γ������ѷ�ʤ��
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="arrchessboard"></param>
        /// <returns></returns>
        public static CurrChessBoardState Result(int m, int n, int[,] arrchessboard)
        {//(m,n)���Ѿ�������m,n���ĸ�����������������������������������������������ϣ����϶���
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
     

        // ���ģ�������ĳһ�����߻���������ͬɫ4�Ӳ�����ؽ������������ڴ�4�������ӳ����ϸ���һ�����ӵĽ�*�����4�ӽ���������
        // ���ģ��������ġ���ģ�����һ�������γ����������Ҵ��������Ŀ����Եľ��档
        // ָ�������������������͡���������
        // ������������ĳһ�����߻���������ͬɫ�������������ڴ����������ӳ�������һ��������һ������һ���������������ӵĽ����������ӽ���������
        // �������м�����һ�����ӽ�*����������������ӳ��߾�������һ�����ӵĽ�����������������       

        #region  �����Ƿ�����
        /// <summary>
        /// ������λ�ö�����ʱΪƽ��
        /// </summary>
        /// <param name="arrchessboard"></param>
        /// <returns>ƽ�֣�true</returns>
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

        //��east��south��west��north

        #region / double 45�ȶ������ϼ��m��n������Ȩֵ public static double TheEastNorthWestSouth(int m, int n, int[,] arrchessboard,bool bsmyself)
        /// <summary>
        ///  / �������ϼ��m��n������Ȩֵ
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

        #region / string 45�ȶ������ϼ��(m��n)�����ͼ�� public static string strTheEastNorthWestSouth(int m, int n, int[,] arrchessboard)
        /// <summary>
        ///  / �������ϼ��(m��n)�����ͼ��
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

        #region \ double 135���������ϼ��m��n������Ȩֵ public static double TheWestNorthEastSouth(int m, int n, int[,] arrchessboard,bool bsmyself)
        /// <summary>
        ///  \ �������ϼ��m��n������Ȩֵ
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

        #region \ string 135���������ϼ��(m��n)�����ͼ�� public static string strTheWestNorthEastSouth(int m, int n, int[,] arrchessboard)
        /// <summary>
        ///  \ �������ϼ��(m��n)�����ͼ��
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

        #region | double �����������m��n������Ȩֵ public static double TheSouthNorth(int m, int n, int[,] arrchessboard,bool bsmyself)
        /// <summary>
        ///  | �����������m��n������Ȩֵ
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="arrchessboard"></param>
        /// <returns></returns>
        public static double TheSouthNorth(int m, int n, int[,] arrchessboard, bool bsmyself)
        {
            double numqz = 0;//Ȩֵ�ۼ�
            numqz += CheckString(strTheSouthNorth(m, n, arrchessboard), arrchessboard[m, n].ToString(), bsmyself);
           // OA.Common.LogInfo.OutLogInfo("| " + " [" + m.ToString() + "," + n.ToString() + "]=" + numqz.ToString() + " " + bsmyself.ToString());
            return numqz;
        }
        #endregion

        #region | string �����������(m��n)�����ͼ�� public static string strTheSouthNorth(int m, int n, int[,] arrchessboard)
        /// <summary>
        ///  | �����������(m��n)�����ͼ��
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

        #region - double �����������m��n������Ȩֵ public static double TheEastWest(int m, int n, int[,] arrchessboard,bool bsmyself)
        /// <summary>
        ///  - �����������m��n������Ȩֵ
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="arrchessboard"></param>
        /// <returns></returns>
        public static double TheEastWest(int m, int n, int[,] arrchessboard, bool bsmyself)
        {
            double numqz = 0;//Ȩֵ�ۼ� 
            numqz += CheckString(strTheEastWest(m, n, arrchessboard), arrchessboard[m, n].ToString(),bsmyself);
           // OA.Common.LogInfo.OutLogInfo("- " + " [" + m.ToString() + "," + n.ToString() + "]=" + numqz.ToString() + " " + bsmyself.ToString());
            return numqz;
        }
        #endregion

        #region - string �����������(m��n)�����ͼ�� public static string strTheEastWest(int m, int n, int[,] arrchessboard)
        /// <summary>
        ///  - �����������(m��n)�����ͼ��
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

        
        #region  ����Ȩֵ
        /// <summary>
        /// ����Ȩֵ
        /// </summary>
        /// <param name="strWaitCheck">һ�����ַ���ɵ�ͼ��</param>
        /// <param name="strChessFlag">Ҫ���ǳ������������ 1����2</param>
        /// <param name="bmyself">true��ʾ���Լ��ڴ˴�����,false�ǿ��ǶԷ��ڴ˴�����</param>
        /// <returns></returns>
        private static double CheckString(string strWaitCheck, string strChessFlag, bool bmyself)
        { 
            //
            if (strChessFlag == "1")
            {////ͬʱ�ж��ʱ,Ҫ�ۼ�
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
            {//ͬʱ�ж��ʱ,Ҫ�ۼ�
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

        #region  ��������Ҫ��ͼ��
        /// <summary>
        /// ��������Ҫ��ͼ��
        /// </summary>
        /// <param name="strWaitCheck">һ�����ַ���ɵ�ͼ��</param>
        /// <param name="strChessFlag">Ҫ���ǳ������������ 1����2</param>
        /// <param name="bmyself">true��ʾ���Լ��ڴ˴�����,false�ǿ��ǶԷ��ڴ˴�����</param>
        /// <returns></returns>
        private static string CheckStringStr(string strWaitCheck, string strChessFlag)
        {
            string strTemp = string.Empty;
            if (strChessFlag == Const.Value1)
            {
                #region ͬʱ�ж��ʱ,Ҫ�ۼ�
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
                #region ͬʱ�ж��ʱ,Ҫ�ۼ�
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

        #region / string 45�ȶ������ϼ��(m��n)�����ͼ�� public static string strTheEastNorthWestSouth(int m, int n, int[,] arrchessboard)
        /// <summary>
        ///  / �������ϼ��(m��n)�����ͼ��
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

        #region \ string 135���������ϼ��(m��n)�����ͼ�� public static string strTheWestNorthEastSouth(int m, int n, int[,] arrchessboard)
        /// <summary>
        ///  \ �������ϼ��(m��n)�����ͼ��
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

        #region | string �����������(m��n)�����ͼ�� public static string strTheSouthNorth(int m, int n, int[,] arrchessboard)
        /// <summary>
        ///  | �����������(m��n)�����ͼ��
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

        #region - string �����������(m��n)�����ͼ�� public static string strTheEastWest(int m, int n, int[,] arrchessboard)
        /// <summary>
        ///  - �����������(m��n)�����ͼ��
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