using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveGetTest
{
    public class FileMetadata
    {
        public int id;
        public string path;

        public FileMetadata() { }

        public FileMetadata(int id, string path)
        {
            this.id = id;
            this.path = path;
        }

        public string toString()
        {
            return id + ":" + path;
        }
    }
}
