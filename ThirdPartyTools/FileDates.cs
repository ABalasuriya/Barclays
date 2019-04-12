using System;

namespace ThirdPartyTools
{
    public class FileDates
    {
        private readonly string _filePath;

        public FileDates(string filePath)
        {
            _filePath = filePath;
        }

        public DateTimeOffset Created
        {
            get
            {
                return DateTimeOffset.UtcNow;
            }
        }

        public DateTimeOffset Modified
        {
            get
            {
                return DateTimeOffset.UtcNow;
            }
        }
    }
}
