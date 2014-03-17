using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.portal.db.Model
{
    public class BaseModel
    {
        private string _created;
        private string _updated;
        private string _createBy;
        private string _updateBy;

        public string Created
        {
            set { _created = value; }
            get { return _created; }
        }

        public string CreateBy
        {
            set { _createBy = value; }
            get { return _createBy; }
        }

        public string Updated
        {
            set { _updated = value; }
            get { return _updated; }
        }

        public string UpdateBy
        {
            set { _updateBy = value; }
            get { return _updateBy; }
        }
    }
}
