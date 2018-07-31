using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSTest
{
    class ExamSeriesFile
    {
        public string SopInstanceUID = "";
        public string FilePath = "";
        public string SeriesNumber = "";
        public string SeriesUID = "";
        public string InstanceNumber = "";
        
        /// <summary>
        /// 문자열로 정렬시 1, 10, 2, 21, ...로 정렬 되는 문제로 인해 정수로 정렬하려고 추가함
        /// </summary>
        public int nSeriesNumber
        {
            get { return int.Parse(SeriesNumber); }
        }
        public int nInstanceNumber
        {
            get { return int.Parse(InstanceNumber); }
        }



        public void TraceFileInfo()
        {
            Trace.WriteLine(string.Format("{0}, S:{1}, I:{2}", FilePath, SeriesNumber, InstanceNumber));
        }
    }

    class ExamSeries
    {
        private List<ExamSeriesFile> _listFile = new List<ExamSeriesFile>();

        private string _seriesUID = "";

        public string SeriesUID
        {
            get { return _seriesUID; }
            set { _seriesUID = value; }
        }

        private string _seriesNumber = "";
        public string SeriesNumber
        {
            get { return _seriesNumber; }
            set { _seriesNumber = value; }
        }

        public int nSeriesNumber
        {
            get { return int.Parse(_seriesNumber); }
        }

        private string _seriesDate = "";
        public string SeriesDate
        {
            get { return _seriesDate; }
            set { _seriesDate = value; }
        }

        private string _seriesTime = "";
        public string SeriesTime
        {
            get { return _seriesTime; }
            set { _seriesTime = value; }
        }

        public int GetFileCount()
        {
            return _listFile.Count;
        }

        public ExamSeriesFile GetFileAt(int nIndex)
        {
            if (nIndex < 0 || nIndex >= _listFile.Count)
                throw new Exception("Out of range");

            return _listFile[nIndex];
        }

        public ExamSeriesFile GetFileByNumber(int instanceNumber)
        {
            var item = _listFile.Find(x => x.nInstanceNumber == instanceNumber);
            return item;
        }

        public ExamSeriesFile GetFileByUID(string instanceUID)
        {
            var item = _listFile.Find(x => x.SopInstanceUID == instanceUID);
            return item;
        }

        public int Add(ExamSeriesFile file)
        {
            var item = GetFileByUID(file.SopInstanceUID);
            if(item == null)
            {
                _listFile.Add(file);
                return (_listFile.Count - 1);
            }

            return -1;
        }

        public void SortFile()
        {
            _listFile = _listFile.OrderBy(file => file.nSeriesNumber).ThenBy(file => file.nInstanceNumber).ToList();
        }
    }

    
    class Exam
    {
        private List<ExamSeries> _listSeries = null;

        public List<ExamSeries> ListSeries
        {
            get { return _listSeries; }
        }

        private string _studyUID = "";
        public string StudyUID
        {
            get { return _studyUID; }
            set { _studyUID = value; }
        }

        private string _studyDate = "";
        public string StudyDate
        {
            get { return _studyDate; }
            set { _studyDate = value; }
        }

        private string _studyTime = "";
        public string StudyTime
        {
            get { return _studyTime; }
            set { _studyTime = value; }
        }

        private Dicom.DicomTransferSyntax _transferSyntax;
        public Dicom.DicomTransferSyntax TransferSyntax
        {
            get { return _transferSyntax; }
            set { _transferSyntax = value; }
        }

        public Exam(string studyUID)
        {
            _studyUID = studyUID;
            _listSeries = new List<ExamSeries>();
        }

        public int GetSeriesCount()
        {
            return _listSeries.Count;
        }

        public ExamSeries GetSeriesAt(int nIndex)
        {
            if (nIndex < 0 || nIndex >= _listSeries.Count)
                throw new Exception("Out of range");

            return _listSeries[nIndex];
        }

        public ExamSeries GetSeries(string seriesUID, string seriesNumber)
        {
            var item = _listSeries.Find(x => x.SeriesUID == seriesUID && x.SeriesNumber == seriesNumber);
            return item;
        }

        public ExamSeries GetSeriesByUID(string seriesUID)
        {
            var item = _listSeries.Find(x => x.SeriesUID == seriesUID);
            return item;
        }

        public ExamSeries GetSeriesByNumber(int seriesNumber)
        {
            var item = _listSeries.Find(x => x.nSeriesNumber == seriesNumber);
            return item;
        }

        public ExamSeriesFile GetFileByUID(string sopInstanceUID)
        {
            foreach(var series in _listSeries)
            {
                var file = series.GetFileByUID(sopInstanceUID);
                if (file != null)
                    return file;
            }

            return null;
        }

        public bool Add(ExamSeriesFile file)
        {
            var item = GetFileByUID(file.SopInstanceUID);
            if (item == null)
            {
                var series = GetSeries(file.SeriesUID, file.SeriesNumber);

                if(series == null)
                {
                    series = new ExamSeries();
                    series.SeriesUID = file.SeriesUID;
                    series.SeriesNumber = file.SeriesNumber;

                    _listSeries.Add(series);
                }

                series.Add(file);

                return true;
            }

            return false;
        }

        public bool Add(ExamSeries series)
        {
            var item = GetSeries(series.SeriesUID, series.SeriesNumber);
            if (item == null)
            {
                _listSeries.Add(series);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Sort()
        {
            _listSeries = _listSeries.OrderBy(series => series.nSeriesNumber).ToList();

            foreach(var item in _listSeries)
            {
                item.SortFile();
            }
        }
    }

    class Manager
    {
        private List<Exam> _listExam = null;

        public Manager()
        {
            _listExam = new List<Exam>();
        }

        public void Sort()
        {
            foreach (var item in _listExam)
            {
                item.Sort();
            }
        }

        public bool Add(Exam exam)
        {
            var item = _listExam.Find(x => x.StudyUID == exam.StudyUID);
            if (item == null)
            {
                _listExam.Add(exam);
                return true;
            }

            return false;
        }

        public int GetCount()
        {
            return _listExam.Count;
        }

        public Exam GetExam(string studyUID)
        {
            foreach (var exam in _listExam)
            {
                if (exam.StudyUID == studyUID)
                    return exam;
            }

            return null;
        }

        public Exam GetExam(int index)
        {
            if (index < 0 || index >= _listExam.Count)
                return null;

            return _listExam[index];
        }
    }
}
