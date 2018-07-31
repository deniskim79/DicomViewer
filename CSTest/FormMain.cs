using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSTest
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            myView.OpenDicom();
        }

        private void buttonRotateCW_Click(object sender, EventArgs e)
        {
            if (myView.IsImageOpen())
                myView.RotateFilp(CSCustomDisplay.CustomView.eRotateFlipDirection.eRotateCW, true);
        }

        private void buttonRotateCCW_Click(object sender, EventArgs e)
        {
            if (myView.IsImageOpen())
                myView.RotateFilp(CSCustomDisplay.CustomView.eRotateFlipDirection.eRotateCCW, true);
        }

        private void buttonRotate180_Click(object sender, EventArgs e)
        {
            if (myView.IsImageOpen())
                myView.RotateFilp(CSCustomDisplay.CustomView.eRotateFlipDirection.eRotate180, true);
        }

        private void buttonFlipTB_Click(object sender, EventArgs e)
        {
            if (myView.IsImageOpen())
                myView.RotateFilp(CSCustomDisplay.CustomView.eRotateFlipDirection.eFlipTB, true);
        }

        private void buttonFlipLR_Click(object sender, EventArgs e)
        {
            if (myView.IsImageOpen())
                myView.RotateFilp(CSCustomDisplay.CustomView.eRotateFlipDirection.eFlipLR, true);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (myView.IsImageOpen())
                myView.RotateFilp(CSCustomDisplay.CustomView.eRotateFlipDirection.eReset, true);
        }

        private void buttonHeader_Click(object sender, EventArgs e)
        {
            if (myView.DcmFile == null)
                return;

            TagViewer allHeader = new TagViewer();
            allHeader.DcmFile = myView.DcmFile;

            allHeader.ShowDialog(this);
        }


        private Manager mgr = new Manager();

        private void buttonTest_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = true;
            openFile.DefaultExt = "dcm";
            openFile.Filter = "Dicom File(*.dcm; *.dic;)|*.dcm;*.dic;|AllFiles(*.*)|*.*";
            openFile.ShowDialog();
            if (openFile.FileNames != null && openFile.FileNames.Length > 0)
            {
                foreach(var dcmPath in openFile.FileNames)
                {
                    var dcmFile = Dicom.DicomFile.Open(dcmPath);
                    Dicom.DicomFileMetaInformation mi = dcmFile.FileMetaInfo;
                    Dicom.DicomDataset ds = dcmFile.Dataset;

                    var studyUID = ds.Get(Dicom.DicomTag.StudyInstanceUID, "");

                    Exam exam = mgr.GetExam(studyUID);
                    if (exam == null)
                    {
                        exam = new Exam(studyUID);

                        exam.TransferSyntax = mi.TransferSyntax;

                        exam.StudyDate = ds.Get(Dicom.DicomTag.StudyDate, "");
                        exam.StudyTime = ds.Get(Dicom.DicomTag.StudyTime, "");

                        mgr.Add(exam);
                    }
                    
                    var sopInstanceUID = ds.Get(Dicom.DicomTag.SOPInstanceUID, "");
                    var seriesUID = ds.Get(Dicom.DicomTag.SeriesInstanceUID, "");
                    var seriesNumber = ds.Get(Dicom.DicomTag.SeriesNumber, "");
                    var instanceNumber = ds.Get(Dicom.DicomTag.InstanceNumber, "");

                    var series = exam.GetSeries(seriesUID, seriesNumber);
                    if(series == null)
                    {
                        series = new ExamSeries();

                        series.SeriesUID = seriesUID;
                        series.SeriesNumber = seriesNumber;

                        series.SeriesDate = ds.Get(Dicom.DicomTag.SeriesDate, "");
                        series.SeriesTime = ds.Get(Dicom.DicomTag.SeriesTime, "");

                        exam.Add(series);
                    }

                    var file = series.GetFileByUID(sopInstanceUID);
                    if (file == null)
                    {
                        file = new ExamSeriesFile();
                        file.FilePath = dcmPath;
                        file.SopInstanceUID = sopInstanceUID;
                        file.SeriesNumber = seriesNumber;
                        file.InstanceNumber = instanceNumber;

                        series.Add(file);
                    }
                }

                mgr.Sort();
            }
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (myView.DcmFile != null)
                myView.Reset(CSCustomDisplay.CustomView.ResetType.resetZoom);
        }
    }
}
