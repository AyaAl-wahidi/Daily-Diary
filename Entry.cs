using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryManager
{
    public class Entry
    {
        private string date;
        private string content;

        public string Date { 
            set { date = value; }
            get { return date; }
        }

        public string Content
        {
            set { content = value; }
            get { return content; }
        }
    }
}
