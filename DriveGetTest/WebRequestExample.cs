namespace DriveGetTest
{
    using System;
    using System.IO;
    using System.Net;
    using System.Collections.Generic;

    public class WebRequestGetExample
    {
        char[] lineDelimeter = { '\n' };
        char[] rowDelimeter = { ',' };


        /* Constructor */
        public WebRequestGetExample() { }


        /**
        * Read the contents of a file as an entire string from a WebRequest
        */
        public string readFile(string path)
        {
            WebRequest request = WebRequest.Create(path);
            return fetchStringFromRequest(request);
        }


        /**
        * Read the contents of a file as an entire string from a WebRequest
        */
        private string fetchStringFromRequest(WebRequest request)
        {
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;

            // Get the response.
            WebResponse response = request.GetResponse();

            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content.
            string responseFromServer = reader.ReadToEnd();

            // Display the content.
            Console.WriteLine(responseFromServer);

            // Clean up the streams and the response.
            reader.Close();
            response.Close();

            return responseFromServer;
        }


        /**
        * Gets the "special" directory contents from the contents file
        */
        public List<FileMetadata> getContents(string googleDriveShareLink)
        {
            string googleDriveDownloadLink = getDownloadLinkFromShareLink(googleDriveShareLink);

            WebRequest request = WebRequest.Create(googleDriveDownloadLink); //"https://drive.google.com/uc?export=download&id=0B1ta5lLNy8oHSzZwcmpoblZlYms");

            string rawFileContents = fetchStringFromRequest(request);

            return getContentsFromRaw(rawFileContents);
        }


        /**
        * converts a Google Drive share link to a download link
        * https://drive.google.com/file/d/0B1ta5lLNy8oHSzZwcmpoblZlYms/view?usp=sharing -> https://drive.google.com/uc?export=download&id=0B1ta5lLNy8oHSzZwcmpoblZlYms"
        */
        public string getDownloadLinkFromShareLink(string googleDriveShareLink)
        {
            Console.WriteLine("Converting \""+ googleDriveShareLink + "\" to a download link.");
            const string emptyGoogleDriveDownloadLink = "https://drive.google.com/uc?export=download&id=";
            string fileID = "";


            // get the file ID from the Google Share link
            var split = googleDriveShareLink.Split('/');
            fileID = split[5];
            Console.WriteLine("File ID found: " + fileID);


            // create the Google Download link using the file ID from the share link
            string googleDriveDownloadLink = emptyGoogleDriveDownloadLink + fileID;


            Console.WriteLine("Converted Google Drive share link \"" + googleDriveShareLink + "\" to a Google Drive download link \"" + googleDriveDownloadLink + "\"");
            return googleDriveDownloadLink;
        }


        /**
        * Pulls the "file contents" from the contents file
        */
        private List<FileMetadata> getContentsFromRaw(string rawContents)
        {
            List<FileMetadata> contents = new List<FileMetadata>();


            // parse each line of the file
            var lineSplit = rawContents.Split(lineDelimeter);


            // parse each line into peices
            foreach (string line in lineSplit)
            {
                var row = line.Split(rowDelimeter);

                // special contents file format
                int id = Int32.Parse(row[0]);
                string path = row[1];
         
                FileMetadata fileDetail = new FileMetadata(id, path);
                contents.Add(fileDetail);
            }

            return contents;
        }
        
    }
}