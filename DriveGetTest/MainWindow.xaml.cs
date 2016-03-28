using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DriveGetTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WebRequestGetExample ex = new WebRequestGetExample();


            // this is a text file containing directory info
            string googleDriveShareLink = "https://drive.google.com/file/d/0B1ta5lLNy8oHSzZwcmpoblZlYms/view?usp=sharing";


            // // get the directory info from Drive
            List<FileMetadata> contents = ex.getContents(googleDriveShareLink);


            // display all contents of table of contents file (txt file containin directory info with paths to files)
            string fileContents = "";
            foreach(FileMetadata current in contents)
            {
                fileContents += current.toString();
            }
            textBox1.Text = fileContents;


            // get the newest project file
            FileMetadata newestFile = getNewestFileMetadata(contents);
            textBox2.Text = newestFile.toString();


            // get contents of the newest file
            textBox3.Text = ex.readFile(newestFile.path);
        }


        public FileMetadata getNewestFileMetadata(List<FileMetadata> contents)
        {
            FileMetadata newestMetadata = null;

            if (contents.Count > 0)
            {
                newestMetadata = contents.First();

                foreach (FileMetadata current in contents)
                {
                    if (current.id > newestMetadata.id)
                    {
                        newestMetadata = current;
                    }
                }
            }

            return newestMetadata;
        }
    }
}
