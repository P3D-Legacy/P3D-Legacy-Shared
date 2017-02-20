using System;

using P3D.Legacy.Shared.Data;

namespace P3D.Legacy.Shared
{
    public static class ArgHandler
    {
        public static string GetLaunchArg(GameJoltYaml gameJoltYaml)
        {
            var key = new byte[16];
            new Random().NextBytes(key);
            var data = GameJoltYaml.Serialize(gameJoltYaml);

            return $"{new StringEncoding().EncryptRaw(key)}|{new StringEncryption(key).Encrypt(data)}";
        }

        public static GameJoltYaml ParseLaunchArg(string arg)
        {
            var split = arg.Split('|');
            var key = new StringEncoding().DecryptRaw(split[0]);
            var data = new StringEncryption(key).Decrypt(split[1]);

            return GameJoltYaml.Deserialize(data);
        }
    }
}
