using DCM.Extensions;

namespace DCM.Models.Embed
{
    public class HeaderData
    {
        public const int HEADING_MAX_LENGTH = 256;

        private string _heading;
        public string Heading
        {
            get => _heading;
            set => _heading = value?.Truncate(HEADING_MAX_LENGTH)
                                    .NullWhenEmptyOrWhitespace()
                                    .ExceptionWhenNull(nameof(Heading));
        }

        private string _iconUrl;
        public string IconUrl
        {
            get => _iconUrl;
            set => _iconUrl = value?.NullWhenEmptyOrWhitespace();
        }

        private string _url;
        public string Url
        {
            get => _url;
            set => _url = value?.NullWhenEmptyOrWhitespace();
        }
    }
}
