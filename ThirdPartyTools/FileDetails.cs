
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
                return string.Format($"{_random.Next(5)}.{_random.Next(8)}.{_random.Next(22)}");
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

        private void OnAuthorisedAllFileData()
        {
            AuthorisedAllFileData?.Invoke(this, this.IsAuthorised);
        }

        public int Size(string filePath)
        {
            return _random.Next(100000) + 100000;
        }

        public FileDetails()
        {

        }
    }
}
