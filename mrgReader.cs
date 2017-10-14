using Accord.Imaging;
using Accord.Math;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace mrgReader
{   

    public struct MRGfile
    {
        public int width;
        public int height;

        public List<int> zigzagY;
        public List<int> zigzagU;
        public List<int> zigzagV;

        public int quality;

        public MRGfile(List<int> Y, List<int> U, List<int> V, int Quality, int Width, int Height)
        {
            this.zigzagY = Y;
            this.zigzagU = U;
            this.zigzagV = V;
            this.quality = Quality;
            this.width = Width;
            this.height = Height;
        }
    }


    struct Yuv
    {
        public double Yf;      //Conversion from RGB
        public double Uf;      //Conversion from RGB
        public double Vf;      //Conversion from RGB

        public int Ynorm;           //normalized values
        public int Unorm;           //normalized values
        public int Vnorm;           //normalized values

        public double Ydct;      //DCT values
        public double Udct;      //DCT values
        public double Vdct;      //DCT values

        public int Yquant;        //After quantization
        public int Uquant;        //After quantization
        public int Vquant;        //After quantization

    }






    struct myYcBc
    {
        public int width;
        public int height;

        public Yuv[,] ycbcrArr;

        public List<int> zigzagY;
        public List<int> zigzagU;
        public List<int> zigzagV;

        public myYcBc(int width, int height)
        {
            //Y = new byte[width, height];
            //Cb = new byte[width, height];
            //Cr = new byte[width, height];

            ycbcrArr = new Yuv[width, height];
            zigzagY = new List<int>();
            zigzagU = new List<int>();
            zigzagV = new List<int>();

            this.width = width;
            this.height = height;
        }
    }





    public struct Coords
    {
        public int xvalue;
        public int yvalue;

        public Coords(int x, int y) : this()
        {
            this.xvalue = x;
            this.yvalue = y;
        }
    }





    public partial class mrgReader : Form
    {
        public int[,] QuantMatrix = new int[8, 8] { 
                                          { 16,11,10,16,24,40,51,61 },
                                          { 12,12,14,19,26,58,60,55 },
                                          { 14,13,16,24,40,57,69,56 },
                                          { 14,17,22,29,51,87,80,62 },
                                          { 18,22,37,56,68,109,103,77 },
                                          { 24,35,55,64,81,104,113,92 },
                                          { 49,64,78,87,103,121,120,101 },
                                          { 72,92,95,98,112,100,103,99 },
                                        };

        public mrgReader()
        {
            InitializeComponent();
            for (int rows = 0; rows < 8; rows++)
            Matrixviewer.Rows.Add();
        }


        public bool ThumbnailCallback()
        {
            return false;
        }


        private void OpenMrgFile_btn_Click(object sender, EventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "MRG Files(*.MRG)|*.MRG|All files (*.*)|*.*";
            openFileDialog1.Title = "Select an MRG File";

            // Show the Dialog.



            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (FileStream originalFileStream = (FileStream)openFileDialog1.OpenFile())
                {
                    using (FileStream decompressedFileStream = File.Create("temp"))
                    {
                        using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(decompressedFileStream);
                        }
                    }
                }
            }

            int Quality = 0;
            int width = 0;
            int height = 0;

            string line;

            StreamReader file = new StreamReader("temp");

            if ((line = file.ReadLine()) != null)
            {
                int x = 0;
                int.TryParse(line, out x);
                Quality = x;
            }

            if ((line = file.ReadLine()) != null)
            {
                int x = 0;
                int.TryParse(line, out x);
                width = x;
            }
            if ((line = file.ReadLine()) != null)
            {
                int x = 0;
                int.TryParse(line, out x);
                height = x;
            }

            List<int> zigzagY = new List<int>();
            List<int> zigzagU = new List<int>();
            List<int> zigzagV = new List<int>();

            for (int i = 0; i < (width * height); i++)
            {
                if ((line = file.ReadLine()) != null)
                {
                    int x = 0;
                    int.TryParse(line, out x);
                    zigzagY.Add(x);
                }
            }

            for (int i = 0; i < (width * height); i++)
            {
                if ((line = file.ReadLine()) != null)
                {
                    int x = 0;
                    int.TryParse(line, out x);
                    zigzagU.Add(x);
                }
            }

            for (int i = 0; i < (width * height); i++)
            {
                if ((line = file.ReadLine()) != null)
                {
                    int x = 0;
                    int.TryParse(line, out x);
                    zigzagV.Add(x);
                }
            }


            Quality_found.Text = Quality.ToString();
            width_found.Text = width.ToString();
            height_found.Text = height.ToString();

            //Create mrgfile object to work with

            MRGfile mrgfile = new MRGfile(zigzagY, zigzagU, zigzagV, Quality, width, height);

            QualityMatrixAlter(Quality);


            //////////
            /////////////// convert ZigZag format to array 
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            myYcBc MyOwnYcbc = new myYcBc(width, height);


            int dezigcount = 0;

            for (int y = 0; y < mrgfile.height; y += 8)
            {
                for (int x = 0; x < mrgfile.width; x += 8)
                {            

                    for (int index = 0; index < 64; index++)
                    {                       
                        Coords Targetcoords;

                        Targetcoords = ZZStraverseValue(index);

                        MyOwnYcbc.ycbcrArr[Targetcoords.xvalue + x, Targetcoords.yvalue + y].Yquant = mrgfile.zigzagY[dezigcount];
                        MyOwnYcbc.ycbcrArr[Targetcoords.xvalue + x, Targetcoords.yvalue + y].Uquant = mrgfile.zigzagU[dezigcount];
                        MyOwnYcbc.ycbcrArr[Targetcoords.xvalue + x, Targetcoords.yvalue + y].Vquant = mrgfile.zigzagV[dezigcount];

                        dezigcount++;                        
                    }
                }
            }


            //////////
            /////////////// Reverse Quantization
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            for (int y = 0; y < MyOwnYcbc.height; y += 8)
            {
                for (int x = 0; x < MyOwnYcbc.width; x += 8)
                {
                    for (int ity = y; ity < y + 8; ity++)
                    {
                        for (int itx = x; itx < x + 8; itx++)
                        {
                            if (itx < MyOwnYcbc.width && ity < MyOwnYcbc.height)
                            {
                                MyOwnYcbc.ycbcrArr[itx, ity].Ydct = MyOwnYcbc.ycbcrArr[itx, ity].Yquant  * QuantMatrix[itx - x, ity - y];
                                MyOwnYcbc.ycbcrArr[itx, ity].Udct = MyOwnYcbc.ycbcrArr[itx, ity].Uquant  * QuantMatrix[itx - x, ity - y];
                                MyOwnYcbc.ycbcrArr[itx, ity].Vdct = MyOwnYcbc.ycbcrArr[itx, ity].Vquant  * QuantMatrix[itx - x, ity - y];
                            }
                        }
                    }
                }
            }


            //////////
            /////////////// Inverse DCT
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////



            double[,] YtempIDCT = new double[8, 8];
            double[,] UtempIDCT = new double[8, 8];
            double[,] VtempIDCT = new double[8, 8];


            for (int y = 0; y < MyOwnYcbc.height; y += 8)
            {
                for (int x = 0; x < MyOwnYcbc.width; x += 8)
                {
                    if (x <= MyOwnYcbc.width - 8 && y <= MyOwnYcbc.height - 8) //safeguard against out of bounds error
                    {

                        for (int ity = y; ity < y + 8; ity++)
                        {
                            for (int itx = x; itx < x + 8; itx++)
                            {
                                YtempIDCT[ity - y, itx - x] = MyOwnYcbc.ycbcrArr[itx, ity].Ydct;
                                UtempIDCT[ity - y, itx - x] = MyOwnYcbc.ycbcrArr[itx, ity].Udct;
                                VtempIDCT[ity - y, itx - x] = MyOwnYcbc.ycbcrArr[itx, ity].Vdct;
                            }
                        }

                        CosineTransform.IDCT(YtempIDCT);
                        CosineTransform.IDCT(UtempIDCT);
                        CosineTransform.IDCT(VtempIDCT);

                        for (int ity = y; ity < y + 8; ity++)
                        {
                            for (int itx = x; itx < x + 8; itx++)
                            {
                                MyOwnYcbc.ycbcrArr[itx, ity].Ynorm = (int)YtempIDCT[ity - y, itx - x];
                                MyOwnYcbc.ycbcrArr[itx, ity].Unorm = (int)UtempIDCT[ity - y, itx - x];
                                MyOwnYcbc.ycbcrArr[itx, ity].Vnorm = (int)VtempIDCT[ity - y, itx - x];
                            }
                        }
                    }
                }
            }


            //////////
            /////////////// DEnormalize from -128 <-> 127 range to 0 <-> 255
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            for (int y = 0; y < MyOwnYcbc.height; y++)
            {
                for (int x = 0; x < MyOwnYcbc.width; x++)
                {
                    Yuv ycColorspaceEdit = MyOwnYcbc.ycbcrArr[x, y];

                    ycColorspaceEdit.Yf =   (ycColorspaceEdit.Ynorm) + 128;

                    if (y % 2 == 0 && x % 2 == 0)
                    {
                        ycColorspaceEdit.Uf = (ycColorspaceEdit.Unorm) + 128;
                        ycColorspaceEdit.Vf = (ycColorspaceEdit.Vnorm) + 128;
                    }
                    else
                    {
                        ycColorspaceEdit.Uf = (ycColorspaceEdit.Unorm);
                        ycColorspaceEdit.Vf = (ycColorspaceEdit.Vnorm);
                    }

                    MyOwnYcbc.ycbcrArr[x, y] = ycColorspaceEdit;
                }
            }


            //////////
            /////////////// Convert from YUV to RGB and assign to bitmap image
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Bitmap Finalbitmap = new Bitmap(MyOwnYcbc.width,MyOwnYcbc.height);

            for (int y = 0; y < MyOwnYcbc.height; y++)
            {
                for (int x = 0; x < MyOwnYcbc.width; x++)
                {                    
                    YCbCr ycrcb = new YCbCr();
                    RGB rgbcolor = new RGB();
                    
                    ycrcb.Y =  (float)(MyOwnYcbc.ycbcrArr[x, y].Yf /255);
                    ycrcb.Cr = (float)(MyOwnYcbc.ycbcrArr[x, y].Uf /255);
                    ycrcb.Cb = (float)(MyOwnYcbc.ycbcrArr[x, y].Vf /255);

                   //rgbcolor.Red = (byte)(ycrcb.Cr * 255);
                   //rgbcolor.Blue = (byte)(ycrcb.Cr * 255);
                   //rgbcolor.Green = (byte)(ycrcb.Cr * 255);
                    YCbCr.ToRGB(ycrcb, rgbcolor);

                    Finalbitmap.SetPixel(x, y, rgbcolor.Color);                    
                }
            }


            System.Drawing.Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);


            DecompressedResultBox.Image = Finalbitmap.GetThumbnailImage(DecompressedResultBox.Width, DecompressedResultBox.Height, myCallback, IntPtr.Zero);






















            Coords testcoords;

            testcoords.xvalue = 920;
            testcoords.yvalue = 0;

            for (int y = testcoords.yvalue; y < testcoords.yvalue + 8; y++)
            {
                for (int x = testcoords.xvalue; x < testcoords.xvalue + 8; x++)
                {
                    Matrixviewer.Rows[y - testcoords.yvalue].Cells[x - testcoords.xvalue].Value = MyOwnYcbc.ycbcrArr[x, y].Yquant;
                    //Matrixviewer.Rows[x].Cells[y].Value = QuantMatrix[x, y];
                    //Matrixviewer.Rows[x].Cells[y].Value = testoutputmatrix[x, y];
                }
            }


            //for (int y = 0; y < 8; y++)
            //{
            //    for (int x = 0; x <  8; x++)
            //    {
            //        Matrixviewer.Rows[x].Cells[y].Value = QuantMatrix[x, y];
            //        //Matrixviewer.Rows[x].Cells[y].Value = testoutputmatrix[x, y];
            //    }
            //}











        }       


        public Coords ZZStraverseValue(int index)
        {
            Coords[] ZigZagLookupTable = new Coords[64]{
             new Coords(0,0),new Coords(1,0),new Coords(0,1),new Coords(0,2),new Coords(1,1),new Coords(2,0),new Coords(3,0),new Coords(2,1), 
             new Coords(1,2),new Coords(0,3),new Coords(0,4),new Coords(1,3),new Coords(2,2),new Coords(3,1),new Coords(4,0),new Coords(5,0), 
             new Coords(4,1),new Coords(3,2),new Coords(2,3),new Coords(1,4),new Coords(0,5),new Coords(0,6),new Coords(1,5),new Coords(2,4), 
             new Coords(3,3),new Coords(4,2),new Coords(5,1),new Coords(6,0),new Coords(7,0),new Coords(6,1),new Coords(5,2),new Coords(4,3), 
             new Coords(3,4),new Coords(2,5),new Coords(1,6),new Coords(0,7),new Coords(1,7),new Coords(2,6),new Coords(3,5),new Coords(4,4), 
             new Coords(5,3),new Coords(6,2),new Coords(7,1),new Coords(7,2),new Coords(6,3),new Coords(5,4),new Coords(4,5),new Coords(3,6), 
             new Coords(2,7),new Coords(3,7),new Coords(4,6),new Coords(5,5),new Coords(6,4),new Coords(7,3),new Coords(7,4),new Coords(6,5), 
             new Coords(5,6),new Coords(4,7),new Coords(5,7),new Coords(6,6),new Coords(7,5),new Coords(7,6),new Coords(6,7),new Coords(7,7) 
            };

            return ZigZagLookupTable[index];
        }

        public void QualityMatrixAlter(int QuantizationQuality)
        {
            if (QuantizationQuality != 50)
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        if (QuantizationQuality > 50)
                        {
                            QuantMatrix[x, y] = (int)(QuantMatrix[x, y] * ((float)(100 - QuantizationQuality) / 50f));
                            if (QuantMatrix[x, y] > 255)
                            {
                                QuantMatrix[x, y] = 255;
                            }
                            if (QuantMatrix[x, y] == 0)
                            {
                                QuantMatrix[x, y] = 1;
                            }
                        }
                        else if (QuantizationQuality < 50)
                        {
                            QuantMatrix[x, y] = QuantMatrix[x, y] * (50 / QuantizationQuality);
                            if (QuantMatrix[x, y] > 255)
                            {
                                QuantMatrix[x, y] = 255;
                            }
                            else if (QuantMatrix[x, y] == 0)
                            {
                                QuantMatrix[x, y] = 1;
                            }
                        }
                    }
                }
            }
        }


    }
}
