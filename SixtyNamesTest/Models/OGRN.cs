using SixtyNamesTest.Models.Implementation;
using System;

namespace SixtyNamesTest.Models
{
    public class OGRN : IOGRN
    {
        #region Properties

        public string OGRNText { get; private set; }

        #endregion

        #region Methods

        public static OGRN Generate()
        {
            var random = new Random();

            return new OGRN
            {
                OGRNText = random.Next(1, 100000).ToString() + random.Next(1, 10000000).ToString()
            };
        }

        #endregion

    }
}
