using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.portal.db.Model
{
    public class DataField
    {
        private string _id;
        private string _sobject;
        private string _stable;
        private string _sfieldas;
        private string _sfield;
        private string _stitle;
        private string _sdatatype;
        private string _lsys;
        private string _isxml;
        private string _iskey;
        private string _isrefkey;

        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }

        public string SOBJECT {
            set { _sobject = value; }
            get { return _sobject; }
        }
        public string STABLE
        {
            set { _stable = value; }
            get { return _stable; }
        }
        public string SFIELDAS
        {
            set { _sfieldas = value; }
            get { return _sfieldas; }
        }
        public string SFIELD
        {
            set { _sfield = value; }
            get { return _sfield; }
        }
        public string STITLE
        {
            set { _stitle = value; }
            get { return _stitle; }
        }
        public string SDATATYPE
        {
            set { _sdatatype = value; }
            get { return _sdatatype; }
        }
        public string LSYS
        {
            set { _lsys = value; }
            get { return _lsys; }
        }
        public string SXML
        {
            set { _isxml = value; }
            get { return _isxml; }
        }
        public string SKEY
        {
            set { _iskey = value; }
            get { return _iskey; }
        }
        public string SREFKEY
        {
            set { _isrefkey = value; }
            get { return _isrefkey; }
        }
    }
}
