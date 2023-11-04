using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterGeneratorBot.Configuration
{
    internal class YamlConfigurationProvider : FileConfigurationProvider
    {
        public YamlConfigurationProvider(YamlConfigurationSource source) : base(source) { }

        public override void Load(Stream stream)
        {
            var parser = new YamlConfigurationFileParser();

            Data = parser.Parse(stream);
        }
    }
}
