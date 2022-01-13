using System;
using System.Linq;
using System.Text;

namespace Hexviewer
{
    public class Hexviewer
    {
        private string _replacement = ".";
        private Encoding _encoder;
        private int _bytesPerLine = 32;
        private byte[] _file;

        public Hexviewer(byte[] file)
        {
            _file = file;
            _encoder = Encoding.GetEncoding("UTF-8", new EncoderReplacementFallback(_replacement),
                new DecoderReplacementFallback(_replacement));
        }

        public void SetFile(byte[] file)
        {
            _file = file;
        }

        public void SetReplacementString(string replacement)
        {
            _replacement = replacement;
            _encoder = Encoding.GetEncoding("UTF-8", new EncoderReplacementFallback(_replacement),
                new DecoderReplacementFallback(_replacement));
        }

        public void SetBytesPerLine(int bytesPerLine)
        {
            if (bytesPerLine <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bytesPerLine), "Number of bytes per line need to be positive!");
            }
            _bytesPerLine = bytesPerLine;
        }

        public string Build()
        {
            var builder = new StringBuilder();

            var index = 0;
            var maxIndexLength = (int)Math.Ceiling(Math.Log(_file.Length, 16));
            while (index < _file.Length)
            {
                var line = _file.Skip(index).Take(_bytesPerLine).ToArray();

                var bytesToHex = string.Join(" ", BitConverter.ToString(line).Split('-'));
                var bytesToAscii = _encoder.GetString(line);

                var lineBuilder = new StringBuilder();

                lineBuilder.Append(index.ToString($"X{maxIndexLength}"));
                lineBuilder.Append(":   ");
                lineBuilder.Append(bytesToHex);

                var difference = _bytesPerLine * 2 + _bytesPerLine - 1 - bytesToHex.Length;
                while (difference > 0)
                {
                    lineBuilder.Append(" ");
                    difference--;
                }

                lineBuilder.Append("   |   ");
                lineBuilder.Append(bytesToAscii.Replace(Environment.NewLine, " "));

                builder.AppendLine(lineBuilder.ToString());

                index += _bytesPerLine;
            }

            return builder.ToString();
        }
    }
}