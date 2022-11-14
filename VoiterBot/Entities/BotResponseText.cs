using System.ComponentModel.DataAnnotations;
using VoterBot.Enums;

namespace VoterBot.Entities
{
    public class BotResponseText
    {
        [Key]
        public int Id { get; set; } 

        public string Uz { get; set; }

        public string Ru { get; set; }

        public string Eng { get; set; }

        public ResponseTextType Type { get; set; }
    }
}
