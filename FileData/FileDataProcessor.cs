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
        protected FileDetails FileDetialsTool { get; }

        #endregion

        #region methods 
        public string GetFileData()
        {
            try
            {

                if (AllVersionArguements.FirstOrDefault(x => x.Equals(CommandArguement)) != null)
                {
                    return $"File Version number: {FileDetialsTool.Version(this.FileLocation)}";
                }
                else if (AllSizeguements.Contains(CommandArguement))
                {
                    return $"File size: {FileDetialsTool.Size(this.FileLocation).ToString()}";
                }
                else
                    throw new InvalidOperationException("Command not valid!");
            }
            catch (Exception)
            {
                // Could log the error.

                throw;
            }
            
        }

        #endregion

        #region constructors

        private FileDataProcessor(string[] args)
        { }


        public FileDataProcessor(string[] args, FileDetails fileDetails = default)
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
                FileDetialsTool = fileDetails;

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
        #endregion

    }
}