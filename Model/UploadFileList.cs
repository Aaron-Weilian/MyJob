using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace com.portal.db.Model
{
    public class UploadFileList
    {
        public UploadFileList() { }

        #region Model

        private string _uploadfilelistid;
        private string _filename;
        private string _filepath;
        private byte[] _filestream;
        private string _status;
        private string _messagetype;
        private string _confirmDate;
        private string _uploadby;

        public string UploadFileListID {

            set { _uploadfilelistid = value; }
            get { return _uploadfilelistid; }
            
        }

        public string FileName
        {

            set { _filename = value; }
            get { return _filename; }

        }

        public string FilePath
        {

            set { _filepath = value; }
            get { return _filepath; }

        }

        public byte[] FileStream
        {

            set { _filestream = value; }
            get { return _filestream; }

        }

        public string Status
        {

            set { _status = value; }
            get { return _status; }

        }

        public string MessageType
        {

            set { _messagetype = value; }
            get { return _messagetype; }

        }

        public string ConfirmDate
        {

            set { _confirmDate = value; }
            get { return _confirmDate; }

        }

        public string UploadBy
        {

            set { _uploadby = value; }
            get { return _uploadby; }

        }

        #endregion Model
    }
}
