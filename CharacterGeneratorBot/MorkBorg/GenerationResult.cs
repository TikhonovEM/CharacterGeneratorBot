using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterGeneratorBot.MorkBorg
{
    public class GenerationResult
    {
        public bool IsSuccess { get; set; }

        public string? ErrorMessage { get; set; }

        public Character? Character { get; set; }
    }
}
