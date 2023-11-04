using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterGeneratorBot.MorkBorg
{
    public class CharacterGenerationParams
    {
        public bool WithNegativeTrait { get; set; } = true;
        public bool WithBrokenBody { get; set; } = true;
        public bool WithBadHabit { get; set; } = true;
        public bool WithDarkPast { get; set; } = true;

        private static readonly CharacterGenerationParams instance = new();
        public static CharacterGenerationParams Instance { get => instance; }
    }
    
}
