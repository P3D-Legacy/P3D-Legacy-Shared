using System;
using System.Globalization;

using P3D.Legacy.Shared.Extensions;

using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Shared.YamlConverters
{
    public class CultureInfoConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type) => type == typeof(CultureInfo);

        public object ReadYaml(IParser parser, Type type)
        {
            var value = ((Scalar) parser.Current).Value;
            parser.MoveNext();
            return CultureInfoExtensions.TryGetCultureInfo(value, out var culture) ? culture : null;
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var cultureInfo = (CultureInfo) value;
            emitter.Emit(new Scalar(null, null, cultureInfo.Name, ScalarStyle.Plain, true, false));
        }
    }
}