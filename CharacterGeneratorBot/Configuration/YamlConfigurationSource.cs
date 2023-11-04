using Microsoft.Extensions.Configuration;
using NetEscapades.Configuration.Yaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterGeneratorBot.Configuration
{
    internal class YamlConfigurationSource : FileConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            FileProvider = FileProvider ?? builder.GetFileProvider();
            return new YamlConfigurationProvider(this);
        }
    }
}
