using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SucceedSoft.Gobang
{
    /// <summary> 
    /// Computer����Ҫ�ǵ����˹����ܷ���
    /// </summary>
    public class Computer
    {
        /// <summary>
        /// ���������λ�� X(7��������ʱ��һ����������)
        /// </summary>
        private int ChessX;
        /// <summary>
        /// ���������λ�� Y(7��������ʱ��һ����������)
        /// </summary>
        private int ChessY;
        /// <summary>
        /// ��¼�������λ�ö�ջ
        /// </summary>
        private Stack mStarckBest = new Stack();
        /// <summary>
        /// ��¼���о����������λ�ö�ջState
        /// </summary>
        private Stack mStarckShort = new Stack();
        /// <summary>
        /// ��¼��Ͳ�ڵ������
        /// </summary>
        private Stack mStarckBottom = new Stack();
        /// <summary>
        /// ����״̬��� Ĭ��ֵ 1
        /// </summary>
        private int iDepth = 1;        
        /// <summary>
        /// ���������λ�� X
        /// </summary>
        public int X
        {
            get { return ChessX; }
        }
        /// <summary>
        /// ���������λ�� Y
        /// </summary>
        public int Y
        {
            get { return ChessY; }
        }
        /// <summary>
        /// ����״̬�ṹ��
        /// </summary>
        public struct ChessState
        {
            /// <summary>
            /// 15,15���̸��
            /// </summary>
            public int[,] QP;
            /// <summary>
            /// ��ǰ����״̬�����Ȩֵ
            /// </summary>
            public double QZ;
            /// <summary>
            /// ���ڵ���±�(-1Ϊ�޸��ڵ�)
            /// </summary>
            public int Parent;
            /// <summary>
            /// ��Ů�ڵ��±�
            /// </summary>
            public int[] Child;
            /// <summary>
            /// ���Žڵ㣨��������ֵ������С���Ķ�Ů�ڵ��±�
            /// </summary>
            //public int bestChild;
            /// <summary>
            /// �ڵ��±�X
            /// </summary>
            public string X;
            /// <summary>
            /// �ڵ��±�Y
            /// </summary>
            public string Y;
            /// <summary>
            /// �ڵ����
            /// </summary>
            public string Depth;
            
            public int Xmin;
            public int Ymin;
            public int Xmax;
            public int Ymax;            
        }
        /// <summary>
        /// ����״̬���� Ĭ��ֵ -1
        /// </summary>
        private int iLastIndex = -1;
        /// <summary>
        /// ����״̬���顡��СΪ50000
        /// </summary>
        public ChessState[] cs;//5+25+125+625=780;//4�� 

        //----------------��д�ķ���,���ҵ�ǰ�������λ�� ��ʼ-----------------------------        
        private int PCStep1Start = 0;
        private int PCStep1End = 0;                
        public void Search(int[,] ArrchessBoard, int iParent, int Xmin, int Ymin, int Xmax, int Ymax)
        {                
            PCStep1Start = 0;                              
            //���¿���������ÿһ���������������ȥ               
            for (int X1 = Xmin; X1 <= Xmax; X1++)
            {
                for (int Y1 = Ymin; Y1 <= Ymax; Y1++)
                {
                    if (ArrchessBoard[X1, Y1] != 0)
                    {//ֻ���ǻ�û�������λ��,�������λ���򲻴���
                        continue;
                    }                        
                    iLastIndex++;//�� 0 ��ʼ                       
                    cs[iLastIndex].QP = new int[15, 15];
                    for (int X = Xmin; X < Xmax; X++)
                    {
                        for (int Y = Ymin; Y < Ymax; Y++)
                        {
                            cs[iLastIndex].QP[X, Y] = ArrchessBoard[X, Y];
                        }
                    }
                    //�Ӹ��ڵ㸴�����̺�,���뵱ǰһ��Ȩֵ��ߵĵ�   mflag=true ���Է�           ����1                        
                    cs[iLastIndex].QP[X1, Y1] = Const.FirstPlayer == Player.Computer ? 1 : 2;
                    cs[iLastIndex].Parent = iParent;
                    cs[iLastIndex].X = X1.ToString();
                    cs[iLastIndex].Y = Y1.ToString();  
                                          
                    int Xmin2 = Xmin; int Ymin2 = Ymin; int Xmax2 = Xmax; int Ymax2 = Ymax;
                    #region �����鹹���̴�С
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

        #region �Ϸ���

        #region ��Ȩֵ�����ҳ���õ�����������(X,Y) public void SearchBestPoint(int[,] arrchessboard)
        /// <summary>
        /// �Ϸ�������Ȩֵ�����ҳ���õ�����������(X,Y)
        /// </summary>
        /// <param name="arrchessboard">������������</param>
        /// <param name="iParent">���ڵ���±�</param>
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
            cs = new ChessState[80000];//�����ʷֵ,������ͬһλ������ //8000000

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

        #region  �����(m,n)��Ȩֲ,�����ָ���(x,y)Ϊ����״̬  private int ReckonPointQZ(int m, int n, int[,] arrchessboard)
            /// <summary>
        /// �����(m,n)��Ȩֲ,�����ָ���(x,y)Ϊ����״̬        
        /// </summary>
        /// <param name="m">λ�� X</param>
        /// <param name="n">λ�� Y</param>
        /// <param name="arrchessboard">λ��(M,N)��Ȩֵ</param>
        /// <returns>��(m,n)��Ȩֲ</returns>
        private double ReckonPointQZ(int m, int n, int[,] arrchessboard)
        {
            double qz = 0;
            //�����λ�����ҷ�����
            if (Const.FirstPlayer == Player.Computer)
                arrchessboard[m, n] = 1;//�ҷ�����
            else
                arrchessboard[m, n] = 2;//�ҷ�����
            //��ʾ��ȡm,n��������������X��ķ��򣩵����Ӹ�����
            qz += ChessRule.TheEastNorthWestSouth(m, n, arrchessboard, true);
            qz += ChessRule.TheEastWest(m, n, arrchessboard, true);
            qz += ChessRule.TheSouthNorth(m, n, arrchessboard, true);
            qz += ChessRule.TheWestNorthEastSouth(m, n, arrchessboard, true);

            //�����λ���¶Է�����
            if (Const.FirstPlayer == Player.Computer)
                arrchessboard[m, n] = 2;//�Է�����			
            else
                arrchessboard[m, n] = 1;//�Է�����            
            qz += ChessRule.TheEastNorthWestSouth(m, n, arrchessboard, false);
            qz += ChessRule.TheEastWest(m, n, arrchessboard, false);
            qz += ChessRule.TheSouthNorth(m, n, arrchessboard, false);
            qz += ChessRule.TheWestNorthEastSouth(m, n, arrchessboard, false);
            arrchessboard[m, n] = 0;//0��ʾ�˴�����//���������ô��ݣ�̽�����ָ���Ĭ��ֵ
            return qz;
        }
        #endregion

        #region ������������,�ҳ�Ȩֵ����,���õ��������λ��(x,y) private void SetBestPointXY(double[,] qz)
        /// <summary>
        /// ��������Ȩֵ����,�ҳ�Ȩֵ����,���õ��������λ��(x,y)
        /// </summary>
        /// <param name="qz">Ȩֵ����</param>
        private void SetBestPointXY(double[,] qz)
        {
            double max = 0;
            mStarckBest.Clear();//���,����������ӵĵط�����
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
                    {//���ʱ���Լ�¼,�õ������ѡ��
                        mStarckBest.Push(m.ToString() + "," + n.ToString());
                    }                    
                }
            }
            #region ��Ȩֵ��ȵĵ���ɸѡ��������(7,7)����ĵ���mStarckShort  ��mStarckShort���ѡ��һ��
            object[] best = mStarckBest.ToArray();
            int theShortPath = 98;//98����������:���ɶ��� 0=<x=<14 , 0=<y=<14 -> (x-7)(x-7)+(y-7)(y-7) =< 98
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

            //��mStarckShort���ѡ��һ��
            System.Random r = new Random();
            int ran = r.Next(mStarckShort.Count);
            object[] shortpath = mStarckShort.ToArray();
            string[] last = shortpath[ran].ToString().Split(new char[] { ',' });//���ѡ��
            this.ChessX = int.Parse(last[0]);//���õ��������λ��
            this.ChessY = int.Parse(last[1]);//���õ��������λ��
            #endregion
        }
        #endregion

        #endregion

        #region �·���

        #region 1 ��ͼ��,�����ҳ���õ�����������(X,Y)
        /// <summary>
        /// �·�������ͼ��,�����ҳ���õ�����������(X,Y)��������X,Y��¼λ��
        /// </summary>
        /// <param name="ArrchessBoard">������������</param>
        /// <param name="iParent">���ڵ���±�</param>
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
            cs = new ChessState[80000];//�����ʷֵ,������ͬһλ������ //8000000
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

        #region 2 ���ص�(m,n)�γ�˫���İ˷�ͼ��
        /// <summary>
        /// ���ص�(m,n)�γ�˫���İ˷�ͼ��
        /// </summary>
        /// <param name="m">λ�� X</param>
        /// <param name="n">λ�� Y</param>
        /// <param name="arrchessboard">λ��(M,N)��Ȩֵ</param>
        /// <returns>��(m,n)��Ȩֲ</returns>
        private string ReckonPointState(int m, int n, int[,] arrchessboard)
        {
            string strState = string.Empty;
            //�����λ�����ҷ�����
            if (Const.FirstPlayer == Player.Computer)
                arrchessboard[m, n] = 1;//�ҷ�����
            else
                arrchessboard[m, n] = 2;//�ҷ�����
            //��ʾ��ȡm,n��������������X��ķ��򣩵����Ӹ�����
            strState += ChessRule.strTheEastNorthWestSouthStr(m, n, arrchessboard) + ",";
            strState += ChessRule.strTheEastWestStr(m, n, arrchessboard) + ",";
            strState += ChessRule.strTheSouthNorthStr(m, n, arrchessboard) + ",";
            strState += ChessRule.strTheWestNorthEastSouthStr(m, n, arrchessboard) + ",";
            //�����λ���¶Է�����
            if (Const.FirstPlayer == Player.Computer)
                arrchessboard[m, n] = 2;//�Է�����			
            else
                arrchessboard[m, n] = 1;//�Է�����   
         
            strState += ChessRule.strTheEastNorthWestSouthStr(m, n, arrchessboard) + ",";
            strState += ChessRule.strTheEastWestStr(m, n, arrchessboard) + ",";
            strState += ChessRule.strTheSouthNorthStr(m, n, arrchessboard) + ",";
            strState += ChessRule.strTheWestNorthEastSouthStr(m, n, arrchessboard);
            arrchessboard[m, n] = 0;//0��ʾ�˴�����//���������ô��ݣ�̽�����ָ���Ĭ��ֵ
            return strState;
        }
        #endregion

        #region 3 ������������,�ҳ����ͼ��,���õ��������λ��(x,y)
        /// <summary>
        /// ��������ͼ������,�ҳ�ͼ����õ�,���õ��������λ��(x,y)
        /// </summary>
        /// <param name="qz">Ȩֵ����</param>
        private void SetBestPointXYStr(string[,] Str)
        {//mflag = true,1,���� mflag=falseʱ��������
           
            mStarckBest.Clear();//���,����������ӵĵط�����
            for (int X = 0; X < 15; X++)
            {
                for (int Y = 0; Y < 15; Y++)
                {
                    //�ж��а�����ͼ��  �Լ��Ļ��Ƕ��ֵ�
                    string strTempStr = Str[X, Y];
                    if (strTempStr == null)
                    {
                        continue;
                    }
                    else if (strTempStr.IndexOf(Const.S1_FiveLink) > -1 && Const.FirstPlayer == Player.Computer)
                    {
                        this.ChessX = X;//���õ��������λ��
                        this.ChessY = Y;//���õ��������λ��
                        return;
                    }                    
                    //if (Str[m, n] > max)
                    //{
                    //    mStarckBest.Clear();
                    //    mStarckBest.Push(m.ToString() + "," + n.ToString());
                    //    max = Str[m, n]; // x = m;// y = n;
                    //}
                    //else if (Str[m, n] == max)
                    //{//���ʱ���Լ�¼,�õ������ѡ��
                    //    mStarckBest.Push(m.ToString() + "," + n.ToString());
                    //}

                }//for
            }//for

            #region ��ͼ����ȵĵ���ɸѡ��������(7,7)����ĵ���mStarckShort  ��mStarckShort���ѡ��һ��
            object[] best = mStarckBest.ToArray();
            int theShortPath = 98;//98����������:���ɶ��� 0=<x=<14 , 0=<y=<14 -> (x-7)(x-7)+(y-7)(y-7) =< 98
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

            //��mStarckShort���ѡ��һ��
            System.Random r = new Random();
            int ran = r.Next(mStarckShort.Count);
            object[] shortpath = mStarckShort.ToArray();
            string[] last = shortpath[ran].ToString().Split(new char[] { ',' });//���ѡ��
            this.ChessX = int.Parse(last[0]);//���õ��������λ��
            this.ChessY = int.Parse(last[1]);//���õ��������λ��
            #endregion
        }
        #endregion

        #endregion
    }
}
