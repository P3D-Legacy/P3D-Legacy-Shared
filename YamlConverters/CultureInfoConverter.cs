using System;
using System.Globalization;
using System.Linq;

using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Shared.YamlConverters
{
    public class CultureInfoConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type) => type == typeof(CultureInfo);

        internal static CultureInfo GetCultureInfo(string ietfLanguageTag) => CultureInfo.GetCultures(CultureTypes.AllCultures).FirstOrDefault(info => info.IetfLanguageTag == ietfLanguageTag);
        public object ReadYaml(IParser parser, Type type)
        {
            var value = ((Scalar) parser.Current).Value;
            parser.MoveNext();
            return GetCultureInfo(value);
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var cultureInfo = (CultureInfo) value;
            emitter.Emit(new Scalar(null, null, cultureInfo.IetfLanguageTag, ScalarStyle.Plain, true, false));
        }
    }
}