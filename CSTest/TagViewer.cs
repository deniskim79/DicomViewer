using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSTest
{
    public partial class TagViewer : Form
    {
        public Dicom.DicomFile DcmFile = null;

        List<string> allTextLine = new List<string>();
        List<TagData> allTags = new List<TagData>();
        List<TagData> displayTags = new List<TagData>();

        public TagViewer()
        {
            InitializeComponent();
        }

        private void AllHeaderView_Load(object sender, EventArgs e)
        {
            if (DcmFile == null)
                throw new Exception("Dicom file is invalid!");
                        
            FindAllTags();

            FillListView();
        }

        private void FillListView()
        {
            listViewAllHeader.BeginUpdate();

            listViewAllHeader.Items.Clear();

            int i, nCount = displayTags.Count;
            for (i = 0; i < nCount; i++)
            {
                var tag = displayTags[i];
                var item = new ListViewItem(tag.strTagName);
                
                item.SubItems.Add(tag.strVR);
                item.SubItems.Add(tag.strDataLength);
                item.SubItems.Add(tag.strDataValue);

                listViewAllHeader.Items.Add(item);
            }

            listViewAllHeader.EndUpdate();
        }

        private void FindAllTags()
        {
            allTags.Clear();
            displayTags.Clear();
            allTextLine.Clear();

            var mi = DcmFile.FileMetaInfo;
            var ds = DcmFile.Dataset;

            var dw = new DumpWalker();

            new Dicom.DicomDatasetWalker(DcmFile.FileMetaInfo).Walk(dw);
            new Dicom.DicomDatasetWalker(DcmFile.Dataset).Walk(dw);

            allTags.AddRange(dw.allTags);
            displayTags.AddRange(dw.allTags);

            int i, nCount = allTags.Count;
            for (i = 0; i < nCount; i++)
                allTextLine.Add(allTags[i].ToString());
        }


        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if(textBoxSearch.Text != "")
            {
                var prev = this.Cursor;

                this.Cursor = Cursors.WaitCursor;

                SearchResult(textBoxSearch.Text);

                this.Cursor = prev;
            }
            else
            {
                displayTags.Clear();
                displayTags.AddRange(allTags);
            }

            FillListView();
        }

        private void SearchResult(string search)
        {
            displayTags.Clear();

            int i, nCount = allTextLine.Count;
            for (i = 0; i < nCount; i++)
            {
                if(allTextLine[i].IndexOf(search) >= 0)
                    displayTags.Add(allTags[i]);
            }
        }
    }

    class TagData
    {
        public string strTagName = "";
        public string strVR = "";
        public string strDataLength = "";
        public string strDataValue = "";

        public override string ToString()
        {
            var strFull = string.Format("{0}{1}{2}{3}", strTagName, strVR, strDataLength, strDataValue).ToLower();
            return strFull;
        }
    }

    class DumpWalker : Dicom.IDicomDatasetWalker
    {
        private int _level = 0;

        public List<TagData> allTags = new List<TagData>();

        public DumpWalker()
        {
            Level = 0;
        }

        public int Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
                Indent = String.Empty;
                for (int i = 0; i < _level; i++) Indent += "    ";
            }
        }

        private string Indent { get; set; }

        private void AddItem(string tag, string vr, string length, string value)
        {
            TagData data = new TagData
            {
                strTagName = tag,
                strVR = vr,
                strDataLength = length,
                strDataValue = value
            };

            allTags.Add(data);
        }

        public void OnBeginWalk()
        {
        }

        public bool OnElement(Dicom.DicomElement element)
        {
            var tag = String.Format(
                "{0}{1}  {2}",
                Indent,
                element.Tag.ToString().ToUpper(),
                element.Tag.DictionaryEntry.Name);

            string value = "<large value not displayed>";
            if (element.Length <= 2048) value = String.Join("\\", element.Get<string[]>());

            if (element.ValueRepresentation == Dicom.DicomVR.UI && element.Count > 0)
            {
                var uid = element.Get<Dicom.DicomUID>(0);
                var name = uid.Name;
                if (name != "Unknown") value = String.Format("{0} ({1})", value, name);
            }

            AddItem(tag, element.ValueRepresentation.Code, element.Length.ToString(), value);
            return true;
        }

#if !NET35
        public Task<bool> OnElementAsync(Dicom.DicomElement element)
        {
            return Task.FromResult(this.OnElement(element));
        }
#endif

        public bool OnBeginSequence(Dicom.DicomSequence sequence)
        {
            var tag = String.Format(
                "{0}{1}  {2}",
                Indent,
                sequence.Tag.ToString().ToUpper(),
                sequence.Tag.DictionaryEntry.Name);

            AddItem(tag, "SQ", String.Empty, String.Empty);

            Level++;
            return true;
        }

        public bool OnBeginSequenceItem(Dicom.DicomDataset dataset)
        {
            var tag = String.Format("{0}Sequence Item:", Indent);

            AddItem(tag, String.Empty, String.Empty, String.Empty);

            Level++;
            return true;
        }

        public bool OnEndSequenceItem()
        {
            Level--;
            return true;
        }

        public bool OnEndSequence()
        {
            Level--;
            return true;
        }

        public bool OnBeginFragment(Dicom.DicomFragmentSequence fragment)
        {
            var tag = String.Format(
                "{0}{1}  {2}",
                Indent,
                fragment.Tag.ToString().ToUpper(),
                fragment.Tag.DictionaryEntry.Name);

            AddItem(tag, fragment.ValueRepresentation.Code, String.Empty, String.Empty);

            Level++;
            return true;
        }

        public bool OnFragmentItem(Dicom.IO.Buffer.IByteBuffer item)
        {
            var tag = String.Format("{0}Fragment", Indent);

            AddItem(tag, String.Empty, item.Size.ToString(), String.Empty);
            return true;
        }

#if !NET35
        public Task<bool> OnFragmentItemAsync(Dicom.IO.Buffer.IByteBuffer item)
        {
            return Task.FromResult(this.OnFragmentItem(item));
        }
#endif

        public bool OnEndFragment()
        {
            Level--;
            return true;
        }

        public void OnEndWalk()
        {
        }
    }
}
