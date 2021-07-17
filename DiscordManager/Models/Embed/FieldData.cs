using DCM.Extensions;

namespace DCM.Models.Embed
{
    public class FieldData
    {
        public const int FIELD_HEADING_MAX_LENGTH = 256;
        public const int FIELD_TEXT_MAX_LENGTH = 1024;

        private string _heading;
        public string Heading
        {
            get => _heading;
            set => _heading = value?.Truncate(FIELD_HEADING_MAX_LENGTH)
                                    .NullWhenEmptyOrWhitespace()
                                    .ExceptionWhenNull(nameof(Heading));
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => _text = value?.Truncate(FIELD_TEXT_MAX_LENGTH)
                                 .NullWhenEmptyOrWhitespace()
                                 .ExceptionWhenNull(nameof(Text));
        }

        public bool IsInline { get; set; }
    }
}
