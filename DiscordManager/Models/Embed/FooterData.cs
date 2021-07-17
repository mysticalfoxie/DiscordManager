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

        private DateTime? _timeStamp;
        public DateTime? Timestamp
        {
            get => _timeStamp;
            set => _timeStamp = value?.NullWhenDefault();
        }

        private string _iconUrl;
        public string IconUrl
        {
            get => _iconUrl;
            set => _iconUrl = value?.NullWhenEmptyOrWhitespace();
        }
    }
}
