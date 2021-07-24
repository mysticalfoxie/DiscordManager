using DCM.Extensions;
using System;

namespace DCM.Models.Embed
{
    public class FooterData
    {
        public const int TEXT_MAX_LENGTH = 2048;

        private string _text;
        public string Text
        {
            get => _text;
            set => _text = value?.Truncate(TEXT_MAX_LENGTH)
                                 .NullWhenEmptyOrWhitespace()
                                 .ExceptionWhenNull(nameof(Text));
        }

        // 2021-07-24T21:18:21.205Z
        private string _timeStamp; 
        public string Timestamp
        {
            get => _timeStamp;
            set => _timeStamp = value?.NullWhenEmptyOrWhitespace();
        }

        private string _iconUrl;
        public string IconUrl
        {
            get => _iconUrl;
            set => _iconUrl = value?.NullWhenEmptyOrWhitespace();
        }
    }
}
