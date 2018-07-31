using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace CSCustomDisplay
{
    public partial class CustomView : UserControl
    {
        private Dicom.DicomFile m_dcmFile = null;
        public Dicom.DicomFile DcmFile
        {
            get { return m_dcmFile; }
        }

        private Dicom.Imaging.DicomImage m_dcmImage = null;
        
        private Point m_pointSave = new Point(0, 0);
        private bool m_bCapture = false;

        private string[] m_strTag = new string[(int)UseTags.eTagName.eAllTagCount];

        private SolidBrush BrushBlack = new SolidBrush(Color.Black);
        private SolidBrush BrushWhite = new SolidBrush(Color.White);

        private int _Rotate = 0;
        private bool _FlipX = false;
        private bool _FlipY = false;

        private double DefaultWidth = int.MinValue;
        private double DefaultCenter = int.MinValue;

        private double ZoomRatio = -1.0;
        private Point PanPosition = new Point(0, 0);

        public enum eRotateFlipDirection
        {
            eReset,
            eRotateCW,
            eRotateCCW,
            eFlipLR,
            eFlipTB,
            eRotate180
        }

        private enum eRFText
        {
            RotateNoneFlipNone,
            Rotate90FlipNone,
            Rotate180FlipNone,
            Rotate270FlipNone,
            RotateNoneFlipX,
            Rotate90FlipX,
            Rotate180FlipX,
            Rotate270FlipX
        }

        public CustomView()
        {
            InitializeComponent();

            ResizeRedraw = true;
            DoubleBuffered = true;
        }

        static public bool IsDicomSignature(string filePath)
        {
            using (var file = new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read)))
            {
                int location = 128;
                int readLen = 4;
                file.BaseStream.Seek(location, SeekOrigin.Begin);

                byte[] buf = new byte[readLen];
                file.BaseStream.Position = location;
                int count = file.Read(buf, 0, readLen);
                string s = System.Text.ASCIIEncoding.ASCII.GetString(buf);
                if (s == "DICM")
                    return true;

                return false;
            }
        }

        public bool IsOpen()
        {
            return m_dcmFile != null;
        }

        public bool IsImageOpen()
        {
            return m_dcmImage != null;
        }

        public bool OpenDicom()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = false;
            openFile.DefaultExt = "dcm";
            openFile.Filter = "Dicom File(*.dcm; *.dic;)|*.dcm;*.dic;|AllFiles(*.*)|*.*";
            openFile.ShowDialog();
            if (openFile.FileName.Length > 0)
            {
                if(openFile.FilterIndex == 0 || IsDicomSignature(openFile.FileName) == true)
                {
                    return ReadDicom(openFile.FileName);
                }
            }

            return false;
        }

        public string GetTag(UseTags.eTagName tagName)
        {
            int nIndex = (int)tagName;
            if (nIndex < 0 || nIndex >= (int)UseTags.eTagName.eAllTagCount)
                throw new Exception("Tag Index out of range");

            if(m_dcmFile == null)
                throw new Exception("Dicom file is not open");

            return m_strTag[(int)tagName];
        }

        private void FillTag()
        {
            if (m_dcmFile == null)
                throw new Exception("Dicom file is not open");

            Dicom.DicomFileMetaInformation mi = m_dcmFile.FileMetaInfo;
            Dicom.DicomDataset ds = m_dcmFile.Dataset;
            
            for (int i=0; i<(int)UseTags.eTagName.eAllTagCount; i++)
            {
                var tagName = UseTags.UseDcmTag[i];
                if (tagName.Group < 0x0008)
                    m_strTag[i] = mi.Get(tagName, "");
                else
                    m_strTag[i] = ds.Get(tagName, "");
            }
        }

        public bool ReadDicom(string dcmPath)
        {
            try
            {
                if (m_dcmFile != null)
                {
                    m_dcmImage = null;
                    m_dcmFile = null;
                }

                m_dcmFile = Dicom.DicomFile.Open(dcmPath);
                if (m_dcmFile == null)
                {
                    _FlipX = _FlipY = false;
                    _Rotate = 0;

                    Refresh();
                    return false;
                }

                FillTag();

                m_dcmImage = new Dicom.Imaging.DicomImage(m_dcmFile.Dataset);

                if (m_dcmImage == null)
                {
                    Refresh();
                    return false;
                }

                DefaultWidth = m_dcmImage.WindowWidth;
                DefaultCenter = m_dcmImage.WindowCenter;

                _FlipX = _FlipY = false;
                _Rotate = 0;
                ZoomRatio = 0;
                PanPosition.X = PanPosition.Y = 0;

                Refresh();

                return true;
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                MessageBox.Show(e.Message);
            }

            return false;
        }

        private void CustomView_MouseDown(object sender, MouseEventArgs e)
        {
            if(m_bCapture == false)
            {
                SetCapture(this.Handle);
                m_bCapture = true;

                m_pointSave = new Point(e.X, e.Y);
            }
        }

        private void CustomView_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_bCapture == true)
                ReleaseCapture();
            m_pointSave = new Point(0, 0);
            m_bCapture = false;
            Refresh();
        }

        private void CustomView_MouseMove(object sender, MouseEventArgs e)
        {
            if(m_bCapture == true && m_dcmImage != null)
            {
                int nx = e.X - m_pointSave.X;
                int ny = e.Y - m_pointSave.Y;

                var bCtrl = ModifierKeys.HasFlag(Keys.Control);
                var bAlt = ModifierKeys.HasFlag(Keys.LMenu);
                var bShift = ModifierKeys.HasFlag(Keys.Shift);

                float fRatio = 1.0f;

                if (bCtrl && !bShift && !bAlt)
                    fRatio = 10.0f;
                else if (bCtrl && bShift && !bAlt)
                    fRatio = 100.0f;

                var bUpdate = false;
                if (e.Button == MouseButtons.Right)
                {
                    m_dcmImage.WindowWidth += (fRatio * nx);
                    m_dcmImage.WindowCenter -= (fRatio * ny);

                    if (m_dcmImage.WindowWidth < 1)
                        m_dcmImage.WindowWidth = 1;

                    bUpdate = true;
                }
                else if(e.Button == MouseButtons.Left)
                {
                    bUpdate = true;
                    if (bCtrl && !bShift && !bAlt)
                    {
                        // Zoom
                        var zTemp = ZoomRatio;
                        zTemp += nx;

                        if(zTemp > 500 || zTemp < 10.0)
                            bUpdate = false;
                        else
                            ZoomRatio = zTemp;
                            
                    }
                    else if (!bCtrl && bShift && !bAlt)
                    {
                        // Pan
                        PanPosition.X += nx;
                        PanPosition.Y += ny;
                    }
                    else
                        bUpdate = false;
                }

                if(bUpdate)
                    Refresh();

                m_pointSave.X = e.X;
                m_pointSave.Y = e.Y;
            }
        }

        private int MulDiv(int number, int numerator, int denominator)
        {
            return (int)(((long)number * numerator + (denominator >> 1)) / denominator);
        }

        private Rectangle GetDrawRect(Size wndSize, Size imgSize1, bool useRFValue)
        {
            int nx = 0, ny = 0, nw = 0, nh = 0;

            int nImgWidth = imgSize1.Width;
            int nImgHeight = imgSize1.Height;

            // Fit to dst
            nh = wndSize.Height;
            nw = MulDiv(nh, nImgWidth, nImgHeight);

            if (nw > wndSize.Width)
            {
                nw = wndSize.Width;
                nh = MulDiv(nw, nImgHeight, nImgWidth);
            }

            if(ZoomRatio < 1.0)
                ZoomRatio = ((nw * 1.0) / nImgWidth * 100);

            nw = MulDiv(nImgWidth, (int)ZoomRatio, 100);
            nh = MulDiv(nImgHeight, (int)ZoomRatio, 100);

            nx = (wndSize.Width - nw) / 2;
            ny = (wndSize.Height - nh) / 2;
                        
            Rectangle rc = new Rectangle(PanPosition.X + nx, PanPosition.Y + ny, nw, nh);
            return rc;
        }

        public void RotateFilp(eRotateFlipDirection direction, bool refresh)
        {
            if (m_dcmImage == null)
                return;

            if(direction == eRotateFlipDirection.eReset)
            {
                m_dcmImage.WindowWidth = DefaultWidth;
                m_dcmImage.WindowCenter = DefaultCenter;

                ZoomRatio = -1.0;
                PanPosition.X = PanPosition.Y = 0;

                _FlipX = _FlipY = false;
                _Rotate = 0;
            }
            else
            {
                switch (direction)
                {
                    case eRotateFlipDirection.eRotateCW:
                        switch (_Rotate)
                        {
                            case 0: _Rotate = 90; break;
                            case 90: _Rotate = 180; break;
                            case 180: _Rotate = 270; break;
                            case 270: _Rotate = 0; break;
                        }
                        break;
                    case eRotateFlipDirection.eRotateCCW:
                        switch (_Rotate)
                        {
                            case 0: _Rotate = 270; break;
                            case 90: _Rotate = 0; break;
                            case 180: _Rotate = 90; break;
                            case 270: _Rotate = 180; break;
                        }
                        break;
                    case eRotateFlipDirection.eFlipLR:
                        _FlipX = !_FlipX;
                        break;
                    case eRotateFlipDirection.eFlipTB:
                        _FlipY = !_FlipY;
                        break;
                    case eRotateFlipDirection.eRotate180:
                        switch(_Rotate)
                        {
                            case 0: _Rotate = 180; break;
                            case 90: _Rotate = 270; break;
                            case 180: _Rotate = 0; break;
                            case 270: _Rotate = 90; break;
                        }
                        break;
                    default:
                        break;
                }
            }
            
            Refresh();
        }

        public enum ResetType
        {
            resetAll,
            resetZoom,
            resetPan,
            resetRotateFlip
        }

        public void Reset(ResetType reset)
        {
            switch(reset)
            {
                case ResetType.resetAll:
                    ZoomRatio = -1;
                    PanPosition.X = PanPosition.Y = 0;
                    _FlipX = false;
                    _FlipY = false;
                    _Rotate = 0;
                    break;
                case ResetType.resetZoom:
                    ZoomRatio = -1;
                    break;
                case ResetType.resetPan:
                    PanPosition.X = PanPosition.Y = 0;
                    break;
                case ResetType.resetRotateFlip:
                    _FlipX = false;
                    _FlipY = false;
                    _Rotate = 0;
                    break;
            }
        }

        private void CustomView_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (m_dcmImage != null)
            {
                // Fill Background
                g.FillRectangle(BrushBlack, ClientRectangle);

                using (var renderImage = m_dcmImage.RenderImage())
                {
                    renderImage.Render(0, _FlipX, _FlipY, _Rotate);
                    using (var bmpImage = renderImage.As<Bitmap>())
                    {
                        Rectangle rcDraw = GetDrawRect(ClientSize, bmpImage.Size, false);

                        if (m_bCapture)
                        {
                            // Quickly draw with mouse click
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
                            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                        }
                        else
                        {
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.AssumeLinear;
                        }

                        g.DrawImage(bmpImage, rcDraw);
                    }
                }
            }
            
            if(m_dcmFile != null)
            {
                float fGap = 1.0f;

                string tagValue = "";
                float fFontHeight = GetFontHeight();
                using (Font font = new Font("Consolas", fFontHeight))
                {

                    tagValue = string.Format("[{0}] {1}",
                        GetTag(UseTags.eTagName.eModality),
                        GetTag(UseTags.eTagName.ePatientName));

                    SizeF msSize = g.MeasureString(tagValue, font);
                    PointF drawPos = new PointF();
                    drawPos.X = fGap;
                    drawPos.Y = fGap;
                    DrawShadowString(g, tagValue, drawPos, font);

                    tagValue = string.Format("{0} - {1}",
                        GetTag(UseTags.eTagName.eSeriesDate),
                        GetTag(UseTags.eTagName.eSeriesTime));
                    msSize = g.MeasureString(tagValue, font);
                    drawPos.Y += fGap + msSize.Height;
                    DrawShadowString(g, tagValue, drawPos, font);

                    tagValue = GetTag(UseTags.eTagName.eStudyDescription);
                    msSize = g.MeasureString(tagValue, font);
                    drawPos.Y += fGap + msSize.Height;
                    DrawShadowString(g, tagValue, drawPos, font);

                    // Draw Window Width/Center
                    if (m_dcmImage != null)
                        tagValue = string.Format("W:{0} C:{1}", m_dcmImage.WindowWidth, m_dcmImage.WindowCenter);
                    else
                        tagValue = "W:n/a C:n/a";
                    msSize = g.MeasureString(tagValue, font);
                    drawPos.X = ClientSize.Width - msSize.Width - fGap;
                    drawPos.Y = ClientSize.Height - msSize.Height - fGap;
                    DrawShadowString(g, tagValue, drawPos, font);

                    // Draw Image Size
                    if (m_dcmImage != null)
                        tagValue = string.Format("{0} X {1}", m_dcmImage.Width, m_dcmImage.Height);
                    else
                        tagValue = "col X row";
                    msSize = g.MeasureString(tagValue, font);
                    drawPos.X = ClientSize.Width - msSize.Width - fGap;
                    drawPos.Y -= (fGap + msSize.Height);
                    DrawShadowString(g, tagValue, drawPos, font);
                   
                    tagValue = string.Format("PAN : {0}, {1}", PanPosition.X, PanPosition.Y);
                    msSize = g.MeasureString(tagValue, font);
                    drawPos.X = ClientSize.Width - msSize.Width - fGap;
                    drawPos.Y -= (fGap + msSize.Height);
                    DrawShadowString(g, tagValue, drawPos, font);


                    tagValue = string.Format("ZOOM : {0}%", (int)ZoomRatio);
                    msSize = g.MeasureString(tagValue, font);
                    drawPos.X = ClientSize.Width - msSize.Width - fGap;
                    drawPos.Y -= (fGap + msSize.Height);
                    DrawShadowString(g, tagValue, drawPos, font);
                }
            }
        }

        private void DrawShadowString(Graphics g, string drawString, PointF pos, Font font)
        {
            var drawPos = pos;
            drawPos.X += 1.0f;
            drawPos.Y += 1.0f;
            g.DrawString(drawString, font, BrushBlack, drawPos);

            drawPos.X += 1.0f;
            drawPos.Y += 1.0f;
            g.DrawString(drawString, font, BrushBlack, drawPos);

            g.DrawString(drawString, font, BrushWhite, pos);
        }

        private int GetFontHeight()
        {
            int lfHeight = 10;
            int nDisplayLine = 40;

            if (ClientSize.Width > ClientSize.Height)
                lfHeight = ClientSize.Height / nDisplayLine;
            else
                lfHeight = ClientSize.Width / nDisplayLine;

            if (lfHeight < 10)
                lfHeight = 10;
            else if (lfHeight > 49)
                lfHeight = (int)(lfHeight * 0.98);

            return lfHeight;
        }
        
        [DllImport("user32")]
        private static extern IntPtr SetCapture(IntPtr hWnd);

        [DllImport("user32")]
        private static extern bool ReleaseCapture();
    }
}
