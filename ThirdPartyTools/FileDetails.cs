
using System;
using System.IO;

namespace ThirdPartyTools
{
    public class FileDetails : IFileDetails, IAuthUser
    {
        private readonly Random _random = new Random();

        public event EventHandler<bool> AuthorisedAllFileData;

        public bool IsAuthorised { get; set; }
        public UserDetail UserDetail { get; set; }

        public string Version(string filePath)
        {
            try
            {

                Path.GetFullPath(filePath);
                OnAuthorisedAllFileData();
                return string.Format($"{GetRandom(5)}.{GetRandom(8)}.{GetRandom(22)}");
            }
            catch (ArgumentException ax) when (ax.ParamName == nameof(filePath))
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        // Making the non-detrerministic random number generator more testable.
        public int GetRandom(int v)
        {
           return _random.Next(v);
        }

        private void OnAuthorisedAllFileData()
        {
            AuthorisedAllFileData?.Invoke(this, this.IsAuthorised);
        }

        public int Size(string filePath)
        {
            return GetRandom(100000) + 100000;
        }

        public FileDetails()
        {}

        

    }
}
