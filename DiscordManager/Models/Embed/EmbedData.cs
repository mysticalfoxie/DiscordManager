using DCM.Exceptions;
using DCM.Extensions;
using Discord;

namespace DCM.Models.Embed
{
    public class EmbedData
    {
        public const int SUB_HEADING_MAX_LENGTH = 256;
        public const int TEXT_MAX_LENGTH = 2048;
        public const int MAX_FIELD_COUNT = 25;
        public const int GLOBAL_MAX_CHARACTERS = 6000;

        private string _subHeading;
        public string SubHeading
        {
            get => _subHeading;
            set
            {
                if ((value?.Length ?? 0) > SUB_HEADING_MAX_LENGTH)
                    throw new EmbedLimitException("sub-heading", SUB_HEADING_MAX_LENGTH);
                else
                    _subHeading = value;
            }
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => _text = value?.Truncate(TEXT_MAX_LENGTH).NullWhenEmptyOrWhitespace();
        }

        private FieldData[] _fields;
        public FieldData[] Fields
        {
            get => _fields;
            set => _fields = value?.Truncate(MAX_FIELD_COUNT);
        }

        public HeaderData Header { get; set; }
        public FooterData Footer { get; set; }

        private string _thumbnailUrl;
        public string ThumbnailUrl
        {
            get => _thumbnailUrl;
            set => _thumbnailUrl = value?.NullWhenEmptyOrWhitespace();
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get => _imageUrl;
            set => _imageUrl = value?.NullWhenEmptyOrWhitespace();
        }

        public Color? BorderColor { get; set; }
    }

}
