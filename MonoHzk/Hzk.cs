using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MonoHzk
{
    public class Hzk
    {

        private const byte _ascCodeStart = 32;
        private const byte _zoneCodeStart = 161;
        private const byte _bitCodeStart = 161;
        private const byte _zoneStride = 94;
        private const char _return = '\n';//'\n' or '\r\n' are recognized.
        private const int _minHzkSize = 8;
        private const int _maxHzkSize = 64;

        /// <summary>
        /// Hzk Character Width N Height
        /// </summary>
        internal int MyHzkSize;

        /// <summary>
        /// Asc Letter Width.
        /// </summary>
        internal int MyAscSize;

        /// <summary>
        /// How many bytes per line for a Hzk Character.
        /// </summary>
        internal int MyHzkStride;

        /// <summary>
        /// How many bytes per line for a Asc Letter.
        /// </summary>
        internal int MyAscStride;

        /// <summary>
        /// Hzk Data.
        /// </summary>
        private byte[] MyHzkData;
        /// <summary>
        /// Asc Data.
        /// </summary>
        private byte[] MyAscData;

        public Hzk(byte[] HzkData, byte[] AscData, int Size)
        {
                try
            {
                if (Size > _maxHzkSize || Size < _minHzkSize)
                    throw new ArgumentException("Invalid Hzk Font Size.", "Size");

                MyHzkSize = Size;
                MyHzkStride = (int)(Math.Ceiling(((double)Size / 8.0)));
                MyAscSize = (int)(Math.Ceiling(((double)Size / 2.0)));
                MyAscStride = (int)(Math.Ceiling(((double)MyAscSize / 8.0)));
                MyHzkData = HzkData;
                MyAscData = AscData;
            }
            catch (Exception ex)
            {
                throw ex;
                // Just throw everything away.
            }
        }

        public Hzk(string HzkFn, string AscFn, int Size)
        {
            try
            {
                if (Size > _maxHzkSize || Size < _minHzkSize)
                    throw new ArgumentException("Invalid Hzk Font Size.", "Size");
                MyHzkSize = Size;
                MyHzkStride = (int)(Math.Ceiling(((double)Size / 8.0)));
                MyAscSize = (int)(Math.Ceiling(((double)Size / 2.0)));
                MyAscStride = (int)(Math.Ceiling(((double)MyAscSize / 8.0)));
                MyHzkData = File.ReadAllBytes(HzkFn);
                MyAscData = File.ReadAllBytes(AscFn);
            }
            catch (Exception ex)
            {
                throw ex;
                // Just throw everything away.
            }
        }
        #region delegates

        /// <summary>
        /// ^o^
        /// </summary>
        /// <param name="hzkData"></param>
        private delegate void hzkDataCallback(byte[] hzkData);
        /// <summary>
        /// >_<
        /// </summary>
        private delegate void hzkNoDataCallback();

        /// <summary>
        /// ^o^
        /// </summary>
        /// <param name="UnicodeChar"></param>
        /// <param name="OnNull"></param>
        /// <param name="OnAsc"></param>
        /// <param name="OnHzk"></param>
        private void accessHzk(char UnicodeChar,  hzkDataCallback OnAsc,hzkDataCallback OnHzk,hzkNoDataCallback OnNull, hzkNoDataCallback OnCrlf)
        { 
            byte[] tmpAsc = Encoding.Default.GetBytes(new char[] { UnicodeChar });
            //This Lib allows asc.bin contains character after asc127, although most asc.bin doesnt.
            if (tmpAsc.Length == 1)
            {
                if (tmpAsc[0] == _return)
                {
                    OnCrlf();
                    return;
                }
                else if (tmpAsc[0] >= _ascCodeStart)
                {
                    int tmpSeek = (tmpAsc[0]-_ascCodeStart) * MyAscStride * MyHzkSize;
                    if (((tmpSeek < MyAscData.Length) && (tmpSeek >= 0)))
                    {
                        byte[] tmpData = new byte[MyAscStride * MyHzkSize];
                        Array.Copy(MyAscData, tmpSeek, tmpData, 0, tmpData.Length);
                        OnAsc(tmpData);
                        return;
                    }
                }
            }

            else if (tmpAsc.Length == 2)
            {
                //Zone Code of a Character
                int tmpZ = (tmpAsc[0] - _zoneCodeStart);
                //Bit Code of a Character
                int tmpB = (tmpAsc[1] - _bitCodeStart);

                int tmpSeek = (((_zoneStride * tmpZ) + tmpB) * (MyHzkStride * MyHzkSize));
                if (((tmpSeek < MyHzkData.Length) && (tmpSeek >= 0)))
                {
                    byte[] tmpData = new byte[MyHzkStride * MyHzkSize];
                    Array.Copy(MyHzkData, tmpSeek, tmpData, 0, tmpData.Length);
                    OnHzk(tmpData);
                    return;
                }
            }//End If Asc or Hzk
            OnNull();
            return;
        }
        #endregion

        /// <summary>
        /// bit tmpData of a Chinese character.
        /// </summary>
        /// <param name="UnicodeChar"></param>
        /// <returns></returns>
        public byte[] HzData(char UnicodeChar)
        {
            byte[] result;
            accessHzk(UnicodeChar,
                (b) => { result = b; },
                (b) => { result = b; },
                () => { result = null; },
                () => { result = null; }
                );
            return null;
        }//End Function

        /// <summary>
        /// Graph for a character~
        /// </summary>
        /// <returns></returns
        public string HzGraph(char UnicodeChar, char TrueChar = '.', char FalseChar = ' ')
        {
            StringBuilder sb;
            string result = "";
            accessHzk(UnicodeChar,
                (b) =>
                {
                    sb = new StringBuilder((MyHzkSize * (MyAscSize + 2)));
                    for (int y = 0; y < MyHzkSize; y++)
                    {
                        for (int t = 0; t < MyAscStride; t++)
                            for (int x = 0; x < 8; x++)
                                sb.Append((b[y * MyAscStride + t] & (128 >> x)) > 0 ? TrueChar : FalseChar);
                        sb.AppendLine();
                    }

                    result = sb.ToString();
                },//On Asc
                (b) =>
                {
                    sb = new StringBuilder((MyHzkSize * (MyHzkSize + 2)));
                    for (int y = 0; y < MyHzkSize; y++)
                    {
                        for (int t = 0; t < MyHzkStride; t++)
                            for (int x = 0; x < 8; x++)
                                sb.Append((b[y * MyHzkStride + t] & (128 >> x)) > 0 ? TrueChar : FalseChar);
                        sb.AppendLine();
                    }
                    result = sb.ToString();
                },//On Hzk
                () =>result = "",//On Null
                () =>result = ""//On Crlf
                );//End Call
            return result;
        }//End Function
        /// <summary>
        /// Returns Texture2D for a character.
        /// </summary>
        /// <param name="Device"></param>
        /// <param name="UnicodeChar"></param>
        /// <returns></returns>
        public Texture2D HzTexture(GraphicsDevice Device, char UnicodeChar)
        {
            uint[] tdata;
            Texture2D result = null;

            accessHzk(UnicodeChar,
                (b) =>
                {
                    tdata = new uint[MyAscSize * MyHzkSize];
                    result = new Texture2D(Device, MyAscSize, MyHzkSize);
                    for (int y = 0; y < MyHzkSize; y++)
                    {
                        for (int t = 0; t < MyAscStride; t++)
                            for (int x = 0; x < 8; x++)
                                tdata[y * MyAscSize + t * 8 + x] = ((b[y * MyAscStride + t] & (128 >> x)) > 0 ?
                                        Color.White.PackedValue : Color.Transparent.PackedValue);
                    }
                    result.SetData<uint>(tdata);
                },//On Asc
                (b) =>
                {
                    tdata = new uint[MyHzkSize * MyHzkSize];
                    result = new Texture2D(Device, MyHzkSize, MyHzkSize);

                    for (int y = 0; y < MyHzkSize; y++)
                    {
                        for (int t = 0; t < MyHzkStride; t++)
                            for (int x = 0; x < 8; x++)
                                tdata[y * MyHzkSize + t * 8 + x] = ((b[y * MyHzkStride + t] & (128 >> x)) > 0 ?
                                        Color.White.PackedValue : Color.Transparent.PackedValue);
                    }
                    result.SetData<uint>(tdata);
                },//On Hzk
                () => result = new Texture2D(Device, MyAscSize, MyHzkSize),//On Nul
                () => result = new Texture2D(Device, MyAscSize, MyHzkSize)//On Crlf
                );
            return result;
        }//End Function
        /// <summary>
        /// Draw text to a texture2d.
        /// </summary>
        /// <param name="Device"></param>
        /// <param name="Text"></param>
        /// <param name="Align"></param>
        /// <param name="RowStride"></param>
        /// <param name="ColumnStride"></param>
        /// <returns></returns>
        public Texture2D DrawText(GraphicsDevice Device, string Text,
            TextAlignEnum Align = TextAlignEnum.Left, int RowStride = 0, int ColumnStride = 0)
        {
            Texture2D result;
            MeasureResult tmpMR = new MeasureResult(this, Text.Length, RowStride, ColumnStride);
            for (int t = 0; t < Text.Length; t++)
            {
                accessHzk(Text[t],
                    (d) =>
                    {
                        tmpMR.AddAsc(d);
                    },
                    (d) =>
                    {
                        tmpMR.AddHzk(d);
                    },
                    ()=> {; },
                    () =>
                    {
                        tmpMR.AddCrlf();
                    });
            }//Next Char
            result = tmpMR.Render(Device, Align) ;
            return result;
        }//End Function
        public Texture2D DrawTextBox(GraphicsDevice Device, string Text, Point BoundingSize,
            TextAlignEnum Align = TextAlignEnum.Left, int RowStride = 0, int ColumnStride = 0)
        {
            Texture2D result;
            MeasureBoxResult tmpMR = new MeasureBoxResult(this,BoundingSize, 
                Text.Length, RowStride, ColumnStride);
            for (int t = 0; t < Text.Length; t++)
            {
                accessHzk(Text[t],
                    (d) =>
                    {
                        tmpMR.AddAsc(d);
                    },
                    (d) =>
                    {
                        tmpMR.AddHzk(d);
                    },
                    ()=> {; },
                    () =>
                    {
                        tmpMR.AddCrlf();
                    });
            }//Next Char
            result = tmpMR.Render(Device, Align);
            return result;
        }
    }//End Class
}//End Namespace
