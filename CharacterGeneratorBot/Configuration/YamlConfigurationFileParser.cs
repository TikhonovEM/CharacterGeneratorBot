using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace CharacterGeneratorBot.Configuration
{
    internal class YamlConfigurationFileParser
    {
        private readonly IDictionary<string, string> _data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private readonly Stack<string> _context = new();
        private string _currentPath;

        public IDictionary<string, string> Parse(Stream input)
        {
            _data.Clear();
            _context.Clear();

            var yaml = new YamlStream();
            yaml.Load(new StreamReader(input));

            // Examine the stream and fetch the top level node
            var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

            // The document node is a mapping node
            VisitYamlMappingNode(mapping);

            return _data;
        }

        // Implementation details elided for brevity
        private void VisitYamlMappingNode(YamlMappingNode node) { }

        private void VisitYamlMappingNode(YamlScalarNode yamlKey, YamlMappingNode yamlValue) { }

        private void VisitYamlNodePair(KeyValuePair<YamlNode, YamlNode> yamlNodePair) { }

        private void VisitYamlSequenceNode(YamlScalarNode yamlKey, YamlSequenceNode yamlValue) { }

        private void VisitYamlSequenceNode(YamlSequenceNode node) { }

        private void EnterContext(string context) { }

        private void ExitContext() { }

        // Final 'leaf' call for each tree which records the setting's value 
        private void VisitYamlScalarNode(YamlScalarNode yamlKey, YamlScalarNode yamlValue)
        {
            EnterContext(yamlKey.Value);
            var currentKey = _currentPath;

            if (_data.ContainsKey(currentKey))
            {
                throw new FormatException($"Key is duplicated - {currentKey}");
            }

            _data[currentKey] = yamlValue.Value;
            ExitContext();
        }
    }
}
