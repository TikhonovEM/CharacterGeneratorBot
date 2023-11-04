using CharacterGeneratorBot;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddYamlFile("config.yml", optional: false);

CharacterGeneratorBot.Configuration.ConfigurationProvider.Configuration = builder.Build();

var bot = new Bot();
await bot.Start();