using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SucceedSoft.Gobang
{
    /// <summary> 
    /// Computer类主要是电脑人工智能方面
    /// </summary>
    public class Computer
    {
        /// <summary>
        /// 电脑下棋的位置 X(7电脑先下时第一步下在中心)
        /// </summary>
        private int ChessX;
        /// <summary>
        /// 电脑下棋的位置 Y(7电脑先下时第一步下在中心)
        /// </summary>
        private int ChessY;
        /// <summary>
        /// 记录所有最佳位置堆栈
        /// </summary>
        private Stack mStarckBest = new Stack();
        /// <summary>
        /// 记录所有距中心最近的位置堆栈State
        /// </summary>
        private Stack mStarckShort = new Stack();
        /// <summary>
        /// 记录最低层节点的索引
        /// </summary>
        private Stack mStarckBottom = new Stack();
        /// <summary>
        /// 棋盘状态深度 默认值 1
        /// </summary>
        private int iDepth = 1;        
        /// <summary>
        /// 电脑下棋的位置 X
        /// </summary>
        public int X
        {
            get { return ChessX; }
        }
        /// <summary>
        /// 电脑下棋的位置 Y
        /// </summary>
        public int Y
        {
            get { return ChessY; }
        }
        /// <summary>
        /// 棋盘状态结构体
        /// </summary>
        public struct ChessState
        {
            /// <summary>
            /// 15,15棋盘格局
            /// </summary>
            public int[,] QP;
            /// <summary>
            /// 当前棋盘状态结最大权值
            /// </summary>
            public double QZ;
            /// <summary>
            /// 父节点的下标(-1为无父节点)
            /// </summary>
            public int Parent;
            /// <summary>
            /// 儿女节点下标
            /// </summary>
            public int[] Child;
            /// <summary>
            /// 最优节点（评估函数值最大或最小）的儿女节点下标
            /// </summary>
            //public int bestChild;
            /// <summary>
            /// 节点下标X
            /// </summary>
            public string X;
            /// <summary>
            /// 节点下标Y
            /// </summary>
            public string Y;
            /// <summary>
            /// 节点深度
            /// </summary>
            public string Depth;
            
            public int Xmin;
            public int Ymin;
            public int Xmax;
            public int Ymax;            
        }
        /// <summary>
        /// 棋盘状态索引 默认值 -1
        /// </summary>
        private int iLastIndex = -1;
        /// <summary>
        /// 棋盘状态数组　大小为50000
        /// </summary>
        public ChessState[] cs;//5+25+125+625=780;//4步 

        //----------------自写的方法,查找当前最佳下棋位置 开始-----------------------------        
        private int PCStep1Start = 0;
        private int PCStep1End = 0;                
        public void Search(int[,] ArrchessBoard, int iParent, int Xmin, int Ymin, int Xmax, int Ymax)
        {                
            PCStep1Start = 0;                              
            //以下考虑在下面每一个点下棋则继续下去               
            for (int X1 = Xmin; X1 <= Xmax; X1++)
            {
                for (int Y1 = Ymin; Y1 <= Ymax; Y1++)
                {
                    if (ArrchessBoard[X1, Y1] != 0)
                    {//只考虑还没有下棋的位置,以有棋的位置则不处理
                        continue;
                    }                        
                    iLastIndex++;//从 0 开始                       
                    cs[iLastIndex].QP = new int[15, 15];
                    for (int X = Xmin; X < Xmax; X++)
                    {
                        for (int Y = Ymin; Y < Ymax; Y++)
                        {
                            cs[iLastIndex].QP[X, Y] = ArrchessBoard[X, Y];
                        }
                    }
                    //从父节点复制棋盘后,加入当前一个权值最高的点   mflag=true 电脑方           黑子1                        
                    cs[iLastIndex].QP[X1, Y1] = Const.FirstPlayer == Player.Computer ? 1 : 2;
                    cs[iLastIndex].Parent = iParent;
                    cs[iLastIndex].X = X1.ToString();
                    cs[iLastIndex].Y = Y1.ToString();  
                                          
                    int Xmin2 = Xmin; int Ymin2 = Ymin; int Xmax2 = Xmax; int Ymax2 = Ymax;
                    #region 更新虚构棋盘大小
                    Xmax2 = X1 + Const.ChessBoardCellCount > Xmax2 ? X1 + Const.ChessBoardCellCount : Xmax2;
                    Xmax2 = Xmax2 > 14 ? 14 : Xmax2;

                    Xmin2 = X1 - Const.ChessBoardCellCount < Xmin2 ? X1 - Const.ChessBoardCellCount : Xmin2;
                    Xmin2 = Xmin2 < 0 ? 0 : Xmin2;

                    Ymax2 = Y1 + Const.ChessBoardCellCount > Ymax2 ? Y1 + Const.ChessBoardCellCount : Ymax2;
                    Ymax2 = Ymax2 > 14 ? 14 : Ymax2;

                    Ymin2 = Y1 - Const.ChessBoardCellCount < Ymin2 ? Y1 - Const.ChessBoardCellCount : Ymin2;
                    Ymin2 = Ymin2 < 0 ? 0 : Ymin2;                    
                    #endregion
                    cs[iLastIndex].Xmin = Xmin2;
                    cs[iLastIndex].Ymin = Ymin2;
                    cs[iLastIndex].Xmax = Xmax2;
                    cs[iLastIndex].Ymax = Ymax2;
                    mStarckBottom.Push(iLastIndex);                        
                }//for
            }//for
            PCStep1End = iLastIndex;
            
            GC.Collect();
        }

        #region 老方法

        #region 按权值电脑找出最好的下棋点的坐标(X,Y) public void SearchBestPoint(int[,] arrchessboard)
        /// <summary>
        /// 老方法：按权值电脑找出最好的下棋点的坐标(X,Y)
        /// </summary>
        /// <param name="arrchessboard">整个棋盘数组</param>
        /// <param name="iParent">父节点的下标</param>
        /// <param name="xmin"></param>
        /// <param name="ymin"></param>
        /// <param name="xmax"></param>
        /// <param name="ymax"></param>
        public void SearchBestPoint(int[,] arrchessboard, int iParent, int xmin, int ymin, int xmax, int ymax)
        {//OK
            if (Const.iPlayedChessCount == 0)
            {
                ChessX = 7;
                ChessY = 7;
                return;
            }
            iDepth = 1;
            cs = new ChessState[80000];//请空历史值,不会在同一位置下棋 //8000000

            mStarckBottom.Clear();
            iLastIndex = -1;

            Search(arrchessboard, iParent, xmin, ymin, xmax, ymax);

            double iQZ = 0;
            double iQZTemp = 0;
            double[,] allQZ = new double[15, 15];
            int theX, theY;
            for (int i = PCStep1Start; i < PCStep1End; i++)
            {
                theX = Convert.ToInt32(cs[i].X);
                theY = Convert.ToInt32(cs[i].Y);
                iQZTemp = ReckonPointQZ(theX, theY, cs[i].QP);
                if (iQZTemp >= iQZ)
                {
                    iQZ = iQZTemp;
                    allQZ[theX, theY] = iQZTemp;
                }
            }

            SetBestPointXY(allQZ);

            allQZ = null;
            cs = null;
            GC.Collect();

        }//public
        #endregion

        #region  计算点(m,n)的权植,计算后恢复点(x,y)为无棋状态  private int ReckonPointQZ(int m, int n, int[,] arrchessboard)
            /// <summary>
        /// 计算点(m,n)的权植,计算后恢复点(x,y)为无棋状态        
        /// </summary>
        /// <param name="m">位置 X</param>
        /// <param name="n">位置 Y</param>
        /// <param name="arrchessboard">位置(M,N)的权值</param>
        /// <returns>点(m,n)的权植</returns>
        private double ReckonPointQZ(int m, int n, int[,] arrchessboard)
        {
            double qz = 0;
            //如果该位置下我方的子
            if (Const.FirstPlayer == Player.Computer)
                arrchessboard[m, n] = 1;//我方黑子
            else
                arrchessboard[m, n] = 2;//我方白子
            //表示获取m,n点正东正西方向（X轴的方向）的连子个数；
            qz += ChessRule.TheEastNorthWestSouth(m, n, arrchessboard, true);
            qz += ChessRule.TheEastWest(m, n, arrchessboard, true);
            qz += ChessRule.TheSouthNorth(m, n, arrchessboard, true);
            qz += ChessRule.TheWestNorthEastSouth(m, n, arrchessboard, true);

            //如果该位置下对方的子
            if (Const.FirstPlayer == Player.Computer)
                arrchessboard[m, n] = 2;//对方白子			
            else
                arrchessboard[m, n] = 1;//对方黑子            
            qz += ChessRule.TheEastNorthWestSouth(m, n, arrchessboard, false);
            qz += ChessRule.TheEastWest(m, n, arrchessboard, false);
            qz += ChessRule.TheSouthNorth(m, n, arrchessboard, false);
            qz += ChessRule.TheWestNorthEastSouth(m, n, arrchessboard, false);
            arrchessboard[m, n] = 0;//0表示此处无子//数组是引用传递，探测完后恢复到默认值
            return qz;
        }
        #endregion

        #region 遍历整个数组,找出权值最大点,设置电脑下棋的位置(x,y) private void SetBestPointXY(double[,] qz)
        /// <summary>
        /// 遍历整个权值数组,找出权值最大点,设置电脑下棋的位置(x,y)
        /// </summary>
        /// <param name="qz">权值数组</param>
        private void SetBestPointXY(double[,] qz)
        {
            double max = 0;
            mStarckBest.Clear();//清空,否则会在有子的地方下子
            for (int m = 0; m < 15; m++)
            {
                for (int n = 0; n < 15; n++)
                {
                    if (qz[m, n] > max)
                    {
                        mStarckBest.Clear();
                        mStarckBest.Push(m.ToString() + "," + n.ToString());
                        max = qz[m, n]; // x = m;// y = n;
                    }
                    else if (qz[m, n] == max)
                    {//相等时可以记录,让电脑随机选择
                        mStarckBest.Push(m.ToString() + "," + n.ToString());
                    }                    
                }
            }
            #region 在权值相等的点中筛选出距中心(7,7)最近的点至mStarckShort  在mStarckShort随机选择一条
            object[] best = mStarckBest.ToArray();
            int theShortPath = 98;//98的设置依据:勾股定理 0=<x=<14 , 0=<y=<14 -> (x-7)(x-7)+(y-7)(y-7) =< 98
            int theCurrePath = 0;
            string[] strTemp;
            for (int k = 0; k < best.Length; k++)
            {
                strTemp = best[k].ToString().Split(new char[] { ',' });
                theCurrePath = (int.Parse(strTemp[0]) - 7) * (int.Parse(strTemp[0]) - 7) + (int.Parse(strTemp[1]) - 7) * (int.Parse(strTemp[1]) - 7);
                if (theShortPath > theCurrePath)
                {
                    theShortPath = theCurrePath;
                    mStarckShort.Clear();
                    mStarckShort.Push(strTemp[0] + "," + strTemp[1]);
                }
                else if (theShortPath == theCurrePath)
                {
                    mStarckShort.Push(strTemp[0] + "," + strTemp[1]);
                }
                else
                { }
            }

            //在mStarckShort随机选择一条
            System.Random r = new Random();
            int ran = r.Next(mStarckShort.Count);
            object[] shortpath = mStarckShort.ToArray();
            string[] last = shortpath[ran].ToString().Split(new char[] { ',' });//随机选择
            this.ChessX = int.Parse(last[0]);//设置电脑下棋的位置
            this.ChessY = int.Parse(last[1]);//设置电脑下棋的位置
            #endregion
        }
        #endregion

        #endregion

        #region 新方法

        #region 1 按图案,电脑找出最好的下棋点的坐标(X,Y)
        /// <summary>
        /// 新方法：按图案,电脑找出最好的下棋点的坐标(X,Y)，用属性X,Y记录位置
        /// </summary>
        /// <param name="ArrchessBoard">整个棋盘数组</param>
        /// <param name="iParent">父节点的下标</param>
        /// <param name="Xmin"></param>
        /// <param name="Ymin"></param>
        /// <param name="Xmax"></param>
        /// <param name="Ymax"></param>
        public void SearchBestPointStr(int[,] ArrchessBoard, int iParent, int Xmin, int Ymin, int Xmax, int Ymax)
        {//OK
            if (Const.iPlayedChessCount == 0)
            {
                ChessX = 7;
                ChessY = 7;
                return;
            }
            iDepth = 1;
            cs = new ChessState[80000];//请空历史值,不会在同一位置下棋 //8000000
            mStarckBottom.Clear();
            iLastIndex = -1;
            Search(ArrchessBoard, iParent, Xmin, Ymin, Xmax, Ymax);
            string[,] allSTR = new string[15, 15];
            int theX, theY;
            for (int i = PCStep1Start; i < PCStep1End; i++)
            {
                theX = Convert.ToInt32(cs[i].X);
                theY = Convert.ToInt32(cs[i].Y);                
                allSTR[theX, theY] = ReckonPointState(theX, theY, cs[i].QP);               
            }
            SetBestPointXYStr(allSTR);
            allSTR = null;
            cs = null;
            GC.Collect();
        }//public
        #endregion

        #region 2 返回点(m,n)形成双方的八方图案
        /// <summary>
        /// 返回点(m,n)形成双方的八方图案
        /// </summary>
        /// <param name="m">位置 X</param>
        /// <param name="n">位置 Y</param>
        /// <param name="arrchessboard">位置(M,N)的权值</param>
        /// <returns>点(m,n)的权植</returns>
        private string ReckonPointState(int m, int n, int[,] arrchessboard)
        {
            string strState = string.Empty;
            //如果该位置下我方的子
            if (Const.FirstPlayer == Player.Computer)
                arrchessboard[m, n] = 1;//我方黑子
            else
                arrchessboard[m, n] = 2;//我方白子
            //表示获取m,n点正东正西方向（X轴的方向）的连子个数；
            strState += ChessRule.strTheEastNorthWestSouthStr(m, n, arrchessboard) + ",";
            strState += ChessRule.strTheEastWestStr(m, n, arrchessboard) + ",";
            strState += ChessRule.strTheSouthNorthStr(m, n, arrchessboard) + ",";
            strState += ChessRule.strTheWestNorthEastSouthStr(m, n, arrchessboard) + ",";
            //如果该位置下对方的子
            if (Const.FirstPlayer == Player.Computer)
                arrchessboard[m, n] = 2;//对方白子			
            else
                arrchessboard[m, n] = 1;//对方黑子   
         
            strState += ChessRule.strTheEastNorthWestSouthStr(m, n, arrchessboard) + ",";
            strState += ChessRule.strTheEastWestStr(m, n, arrchessboard) + ",";
            strState += ChessRule.strTheSouthNorthStr(m, n, arrchessboard) + ",";
            strState += ChessRule.strTheWestNorthEastSouthStr(m, n, arrchessboard);
            arrchessboard[m, n] = 0;//0表示此处无子//数组是引用传递，探测完后恢复到默认值
            return strState;
        }
        #endregion

        #region 3 遍历整个数组,找出最好图案,设置电脑下棋的位置(x,y)
        /// <summary>
        /// 遍历整个图案数组,找出图案最好点,设置电脑下棋的位置(x,y)
        /// </summary>
        /// <param name="qz">权值数组</param>
        private void SetBestPointXYStr(string[,] Str)
        {//mflag = true,1,黑子 mflag=false时人在下棋
           
            mStarckBest.Clear();//清空,否则会在有子的地方下子
            for (int X = 0; X < 15; X++)
            {
                for (int Y = 0; Y < 15; Y++)
                {
                    //判断中包含的图案  自己的还是对手的
                    string strTempStr = Str[X, Y];
                    if (strTempStr == null)
                    {
                        continue;
                    }
                    else if (strTempStr.IndexOf(Const.S1_FiveLink) > -1 && Const.FirstPlayer == Player.Computer)
                    {
                        this.ChessX = X;//设置电脑下棋的位置
                        this.ChessY = Y;//设置电脑下棋的位置
                        return;
                    }                    
                    //if (Str[m, n] > max)
                    //{
                    //    mStarckBest.Clear();
                    //    mStarckBest.Push(m.ToString() + "," + n.ToString());
                    //    max = Str[m, n]; // x = m;// y = n;
                    //}
                    //else if (Str[m, n] == max)
                    //{//相等时可以记录,让电脑随机选择
                    //    mStarckBest.Push(m.ToString() + "," + n.ToString());
                    //}

                }//for
            }//for

            #region 在图案相等的点中筛选出距中心(7,7)最近的点至mStarckShort  在mStarckShort随机选择一条
            object[] best = mStarckBest.ToArray();
            int theShortPath = 98;//98的设置依据:勾股定理 0=<x=<14 , 0=<y=<14 -> (x-7)(x-7)+(y-7)(y-7) =< 98
            int theCurrePath = 0;
            string[] strTemp;
            for (int k = 0; k < best.Length; k++)
            {
                strTemp = best[k].ToString().Split(new char[] { ',' });
                theCurrePath = (int.Parse(strTemp[0]) - 7) * (int.Parse(strTemp[0]) - 7) + (int.Parse(strTemp[1]) - 7) * (int.Parse(strTemp[1]) - 7);
                if (theShortPath > theCurrePath)
                {
                    theShortPath = theCurrePath;
                    mStarckShort.Clear();
                    mStarckShort.Push(strTemp[0] + "," + strTemp[1]);
                }
                else if (theShortPath == theCurrePath)
                {
                    mStarckShort.Push(strTemp[0] + "," + strTemp[1]);
                }
                else
                { }
            }

            //在mStarckShort随机选择一条
            System.Random r = new Random();
            int ran = r.Next(mStarckShort.Count);
            object[] shortpath = mStarckShort.ToArray();
            string[] last = shortpath[ran].ToString().Split(new char[] { ',' });//随机选择
            this.ChessX = int.Parse(last[0]);//设置电脑下棋的位置
            this.ChessY = int.Parse(last[1]);//设置电脑下棋的位置
            #endregion
        }
        #endregion

        #endregion
    }
}
