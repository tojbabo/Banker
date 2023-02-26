using Banker.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banker.DATA
{
    public class MASTER
    {
        #region 볼거 없음
        private static MASTER _instance;
        public static MASTER instance
        {
            get
            {
                if (_instance == null) _instance = new MASTER();
                return _instance;
            }
        }
        private MASTER () { }
        #endregion

        public MetaData metadata;

        #region about vm
        private USAGE _usage;
        public USAGE usage { get => _usage; }

        private MONTH _month;
        public MONTH month { get => _month; }

        private META _meta;
        public META META { get => _meta; }
        #endregion

        public void Init()
        {
            metadata = new MetaData();
            _Init_VM();
        }
        private void _Init_VM()
        {
            _usage = new USAGE();
            _month = new MONTH();
            _meta = new META();

        }
        public void Done()
        {
            metadata.Close();
        }
    }
}
