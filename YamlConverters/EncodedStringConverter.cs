using System;

using P3D.Legacy.Shared.Data;

using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

using static P3D.Legacy.Shared.Data.EncodedString;

namespace P3D.Legacy.Shared.YamlConverters
{
    public class EncodedStringConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type) => type == typeof(EncodedString);

        public object ReadYaml(IParser parser, Type type)
        {
            var value = ((Scalar) parser.Current).Value;
            parser.MoveNext();
            return FromEncodedData(value);
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            emitter.Emit(new Scalar(null, null, GetEncryptedData((EncodedString) value), ScalarStyle.Plain, true, false));
        }
    }
}