using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoHzk
{
    public enum TextAlignEnum 
    {
        Left = 0,
        Top = 0,
        Middle = 1,
        Right = 2,
        Center = 4,
        Bottom = 8
    }
    internal enum hzkResult
    {
        invalid = 0,
        crlf = 1,
        asc = 2,
        hzk = 3
    }
    internal class MeasureResult
    {
        private const int _maxTextLength = 4096;
        private const int _maxTextureSize = 2048;

        internal Hzk MyHzk;
        internal List<int> LineStartIndex;
        internal List<int> LineWidth;
        private int CurrentX, CurrentY;
        internal List<Vector2> CharPosition;
        internal List<hzkResult> CharType;//true=Hzk, false=Asc
        internal List<byte[]> CharData;
        private int RowStride, ColumnStride;
        /// <summary>
        /// -_-|||
        /// </summary>
        /// <param name="H"></param>
        /// <param name="Length"></param>
        internal MeasureResult(Hzk H, int Length,int RS,int CS)
        {
            if (Length > _maxTextLength)
                throw new ArgumentException("Text is too long.", "Text.Length");
            MyHzk = H;
            LineStartIndex = new List<int>();
            LineWidth = new List<int>();
            LineStartIndex.Add(0);
            CharPosition = new List<Vector2>(Length);
            CharType = new List<hzkResult>(Length);
            CharData = new List<byte[]>(Length);
            CurrentX = 0;
            CurrentY = 0;
            RowStride = RS;
            ColumnStride = CS;
        }//End Sub New

        internal void AddHzk( byte[] Data)
        {
            CharPosition.Add(new Vector2(CurrentX, CurrentY));
            CurrentX += MyHzk.MyHzkSize + ColumnStride;
            CharType.Add(hzkResult.hzk);
            CharData.Add(Data);
       }
        internal void AddAsc(byte[] Data)
        {
            CharPosition.Add(new Vector2(CurrentX, CurrentY));
            CurrentX += MyHzk.MyAscSize + ColumnStride;
            CharType.Add(hzkResult.asc);
            CharData.Add(Data);
        }

        internal void AddCrlf()
        {
            LineWidth.Add(Math.Max(0, CurrentX - ColumnStride));
            CurrentX = 0;
            CurrentY += MyHzk.MyHzkSize + RowStride;
            LineStartIndex.Add(CharData.Count);
        }
        internal Texture2D Render(GraphicsDevice Device,TextAlignEnum Align)
        {
            LineWidth.Add(Math.Max(0, CurrentX - ColumnStride));
            int tmpw, tmph;
            int tmpx = 0, tmpl = 0;
            tmpw = Math.Max(LineWidth.Max(), 1);
            tmph = CurrentY + MyHzk.MyHzkSize;
            if (tmpw > _maxTextureSize || tmph > _maxTextureSize)
                throw new ArgumentException("Texture size too big, maybe there were too much Characters.", "Text");
            uint[] tmpData = new uint[tmpw * tmph];
            for(int t=0;t<CharData.Count; t++)
            {
                if (tmpl<LineStartIndex .Count && t == LineStartIndex[tmpl])
                { 
                    switch (Align & (TextAlignEnum.Middle | TextAlignEnum.Right))
                    {
                        case TextAlignEnum.Middle:
                            tmpx = (tmpw - LineWidth[tmpl]) / 2;
                            break;
                        case TextAlignEnum.Right:
                            tmpx = (tmpw - LineWidth[tmpl]);
                            break;
                        default:
                            tmpx = 0;
                            break;
                    }
                    tmpl++;
                }
                if (CharType[t]== hzkResult.asc)
                {
                    for (int y=0; y<MyHzk.MyHzkSize; y++)
                        for (int tt = 0; tt < MyHzk.MyAscStride; tt++)
                            for (int x = 0; x < 8; x++)
                                tmpData[(y+(int) CharPosition[t].Y) * tmpw + tt * 8 + x + (int)CharPosition[t].X+tmpx] = 
                                    ((CharData[t][y *MyHzk.MyAscStride + tt] & (128 >> x)) > 0 ?
                                        Color.White.PackedValue : Color.Transparent.PackedValue);
                }
                else if (CharType[t] == hzkResult.hzk)
                {
                    for (int y = 0; y < MyHzk.MyHzkSize; y++)
                        for (int tt = 0; tt < MyHzk.MyHzkStride; tt++)
                            for (int x = 0; x < 8; x++)
                                tmpData[(y + (int)CharPosition[t].Y) * tmpw + tt * 8 + x + (int)CharPosition[t].X+tmpx] =
                                    ((CharData[t][y * MyHzk.MyHzkStride + tt] & (128 >> x)) > 0 ?
                                        Color.White.PackedValue : Color.Transparent.PackedValue);
                }//End If Asc/Hzk
            }//Next Char
            Texture2D result = new Texture2D(Device, tmpw, tmph);
            result.SetData<uint>(tmpData);
            return result;
        }//End Function
    }//End Class
    
    /// <summary>
    /// -_-|||
    /// </summary>
    internal class MeasureBoxResult
    {
        private const int _maxTextLength = 4096;
        private const int _maxTextureSize = 2048;

        internal Hzk MyHzk;
        internal Point Box;
        internal List<int> LineStartIndex;
        internal List<int> LineWidth;
        private int CurrentX, CurrentY;
        internal List<Vector2> CharPosition;
        internal List<hzkResult> CharType;//true=Hzk, false=Asc
        internal List<byte[]> CharData;
        private int RowStride, ColumnStride;
        /// <summary>
        /// -_-|||
        /// </summary>
        /// <param name="H"></param>
        /// <param name="Length"></param>
        internal MeasureBoxResult(Hzk H,Point Bound, int Length, int RS, int CS)
        {
            if (Length > _maxTextLength)
                throw new ArgumentException("Text is too long.", "Text.Length");
            if(Bound.X>_maxTextureSize || Bound.Y>_maxTextureSize)
                throw new ArgumentException("Texture size too big.", "BoxSize");
            MyHzk = H;
            if (Bound.X < MyHzk.MyHzkSize || Bound.Y < MyHzk.MyHzkSize)
                throw new ArgumentException("Texture size too small.", "BoxSize");
            Box = Bound;
            LineStartIndex = new List<int>();
            LineWidth = new List<int>();
            LineStartIndex.Add(0);
            CharPosition = new List<Vector2>(Length);
            CharType = new List<hzkResult>(Length);
            CharData = new List<byte[]>(Length);
            CurrentX = 0;
            CurrentY = 0;
            RowStride = RS;
            ColumnStride = CS;
        }//End Sub New

        internal void AddHzk(byte[] Data)
        {
            if (CurrentX + MyHzk.MyHzkSize > Box.X)
                AddCrlf();
            CharPosition.Add(new Vector2(CurrentX, CurrentY));
            CurrentX += MyHzk.MyHzkSize + ColumnStride;
            CharType.Add(hzkResult.hzk);
            CharData.Add(Data);
        }
        internal void AddAsc(byte[] Data)
        {
            if (CurrentX + MyHzk.MyAscSize > Box.X)
                AddCrlf();
            CharPosition.Add(new Vector2(CurrentX, CurrentY));
            CurrentX += MyHzk.MyAscSize + ColumnStride;
            CharType.Add(hzkResult.asc);
            CharData.Add(Data);
        }
        internal void AddCrlf()
        {
            LineWidth.Add(Math.Max(0, CurrentX - ColumnStride));
            CurrentX = 0;
            CurrentY += MyHzk.MyHzkSize + RowStride;
            LineStartIndex.Add(CharData.Count);
        }
        internal Texture2D Render(GraphicsDevice Device, TextAlignEnum Align)
        {
            LineWidth.Add(Math.Max(0, CurrentX - ColumnStride));
            int tmpw, tmph;
            int tmpx = 0, tmpy = 0, tmpl = 0;
            tmpw = Box.X;
            tmph = CurrentY + MyHzk.MyHzkSize;
            switch (Align & (TextAlignEnum.Center | TextAlignEnum.Bottom))
            {
                case TextAlignEnum.Center:
                    tmpy = (Box.Y - tmph) / 2;
                    break;
                case TextAlignEnum.Bottom:
                    tmpy = Box.Y - tmph;
                    break;
                default:
                    tmpy = 0;
                    break;
            }
            
            if (tmpw > _maxTextureSize || tmph > _maxTextureSize)
                throw new ArgumentException("Texture size too big, maybe there were too much Characters.", "Text");
            uint[] tmpData = new uint[Box.X * Box.Y];
            for (int t = 0; t < CharData.Count; t++)
            {
                if (tmpl < LineStartIndex.Count && t == LineStartIndex[tmpl])
                {
                    switch (Align & (TextAlignEnum.Middle | TextAlignEnum.Right))
                    {
                        case TextAlignEnum.Middle:
                            tmpx = (tmpw - LineWidth[tmpl]) / 2;
                            break;
                        case TextAlignEnum.Right:
                            tmpx = (tmpw - LineWidth[tmpl]);
                            break;
                        default:
                            tmpx = 0;
                            break;
                    }
                    tmpl++;
                }
                if (CharType[t] == hzkResult.asc)
                {
                    if(tmpy + (int)CharPosition[t].Y>= 0 &&
                        tmpy + (int)CharPosition[t].Y+MyHzk.MyHzkSize<=Box.Y)
                    for (int y = 0; y < MyHzk.MyHzkSize; y++)
                        for (int tt = 0; tt < MyHzk.MyAscStride; tt++)
                            for (int x = 0; x < 8; x++)
                                tmpData[(tmpy + y + (int)CharPosition[t].Y) * tmpw + tt * 8 + x + (int)CharPosition[t].X + tmpx] =
                                    ((CharData[t][y * MyHzk.MyAscStride + tt] & (128 >> x)) > 0 ?
                                        Color.White.PackedValue : Color.Transparent.PackedValue);
                }
                else if (CharType[t] == hzkResult.hzk)
                {
                    if (tmpy + (int)CharPosition[t].Y >= 0 &&
                        tmpy + (int)CharPosition[t].Y + MyHzk.MyHzkSize <= Box.Y)
                        for (int y = 0; y < MyHzk.MyHzkSize; y++)
                        for (int tt = 0; tt < MyHzk.MyHzkStride; tt++)
                            for (int x = 0; x < 8; x++)
                                tmpData[(tmpy+y + (int)CharPosition[t].Y) * tmpw + tt * 8 + x + (int)CharPosition[t].X + tmpx] =
                                    ((CharData[t][y * MyHzk.MyHzkStride + tt] & (128 >> x)) > 0 ?
                                        Color.White.PackedValue : Color.Transparent.PackedValue);
                }//End If Asc/Hzk
            }//Next Char
            Texture2D result = new Texture2D(Device, Box.X, Box.Y);
            result.SetData<uint>(tmpData);
            return result;
        }//End Function
    }//End Class

}//End Namespace
