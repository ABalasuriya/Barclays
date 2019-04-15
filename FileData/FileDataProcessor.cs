using System;
using System.Collections.Generic;
using System.Linq;
using ThirdPartyTools;

namespace FileData
{ 
    public class FileDataProcessor
    {
        #region private properties

        private readonly IList<string> AllVersionArguements = new List<string>() { @"-v", @"--v", @"/v", @"--version" };
        private readonly IList<string> AllSizeguements = new List<string>() { @"-s", @"--s", @"/s", @"--size" };
        
        #endregion

        #region public properties

        public string[] Args { get; }
        public string CommandArguement { get;}

        public string FileLocation { get; }
        protected IFileDetails FileDetailsTool { get; }
        public bool HasError { get; private set; }

        public bool CanGetFileSizeData { get; private set; }

        #endregion

        #region methods 
        public string GetFileData()
        {
            this.HasError = false;
            try
            {
                if (!(FileDetailsTool as IAuthUser).IsAuthorised)
                {
                    return GetFileDataThirdPartuAPI();
                }
                else
                {
                    return $"{GetFileDataThirdPartuAPI()}, FilePath for authorised users : {this.FileLocation}";
                } 

            }
            catch (Exception)
            {
                // Could log the error.
                this.HasError = true;
                throw;
            }
            
        }

        private string GetFileDataThirdPartuAPI()
        {
            if (AllVersionArguements.FirstOrDefault(x => x.Equals(CommandArguement)) != null)
            {
                return $"File Version number: {FileDetailsTool.Version(this.FileLocation)}";
            }
            else if (AllSizeguements.Contains(CommandArguement))
            {
                return $"File size: {FileDetailsTool.Size(this.FileLocation).ToString()}";
            }
            else
                throw new InvalidOperationException("Command not valid!");
        }

        #endregion

        #region constructors

        private FileDataProcessor(string[] args)
        { }


        public FileDataProcessor(string[] args, IFileDetails fileDetails = default)
        {
            try
            {
                // check for empty values in array args.

                if (string.IsNullOrEmpty(args[0]))
                {
                    throw new ArgumentNullException(nameof(CommandArguement), FileDataConstants.commandEmptyErrorMessage);
                }
                else if (string.IsNullOrEmpty(args[1]))
                {
                    throw new ArgumentNullException(nameof(FileLocation), FileDataConstants.FileLocationEmptyErrorMessage);
                }

                Args = args;
                CommandArguement = Args[0];
                FileLocation = Args[1];
                FileDetailsTool = fileDetails;

                //(FileDetailsTool as IAuthUser).AuthorisedAllFileData += (sender, e) =>
                //{
                //    this.CanGetFileSizeData = e ? true : false;
                //};

                (FileDetailsTool as IAuthUser).AuthorisedAllFileData += FileDataProcessor_AuthorisedAllFileData;

            }
            catch (NullReferenceException)
            {
                // might want to do some error logging here.

                throw;   // Preserve stack trace.
            }
            catch (Exception)
            {
                // might want to do some error logging here.

                throw;   // Preserve stack trace.
            }
            
        }

        private void FileDataProcessor_AuthorisedAllFileData(object sender, bool e)
        {
            this.CanGetFileSizeData = (e ? true : false);
        }

        #endregion

    }
}