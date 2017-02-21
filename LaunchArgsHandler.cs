using System;

using NDesk.Options;

using P3D.Legacy.Shared.Data;

namespace P3D.Legacy.Shared
{
    public class LaunchArgsHandler
    {
        public static LaunchArgsHandler ParseArgs(params string[] args)
        {
            var options = new LaunchArgsHandler();
            options.OptionSet.Parse(args);
            return options;
        }
        public static string CreateArgs(GameJoltYaml gameJoltYaml, bool forceGraphics)
        {
            return $"-gj={GetGJArg(gameJoltYaml)} {(forceGraphics ? "-forcegraphics" : "")}";
        }

        private static string GetGJArg(GameJoltYaml gameJoltYaml)
        {
            var key = new byte[16];
            new Random().NextBytes(key);
            var data = GameJoltYaml.Serialize(gameJoltYaml);

            return $"{new StringEncoding().EncodeRaw(key)}|{new StringEncryption(key).Encrypt(data)}";
        }
        private static GameJoltYaml ParseGJArg(string arg)
        {
            var split = arg.Split('|');
            var key = new StringEncoding().DecodeRaw(split[0]);
            var data = new StringEncryption(key).Decrypt(split[1]);

            return GameJoltYaml.Deserialize(data);
        }

        public GameJoltYaml GameJoltYaml { get; set; }
        public bool ForceGraphics { get; set; }

        private OptionSet OptionSet { get; }

        public LaunchArgsHandler()
        {
            OptionSet = new OptionSet()
            {
                {"gj=", v => GameJoltYaml = ParseGJArg(v)},
                {"forcegraphics", v => ForceGraphics = true},
            };
        }
    }
}
