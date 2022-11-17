using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VoterBot.Enums;

namespace VoterBot.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public long UserId { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public Language Language { get; set; }

        public int VoterNumber { get; set; }

        public string ContactNumber { get; set; }

        public bool IsSubscriber { get; set; } = false;

        public int? MajorId { get; set; }
        public Major Major { get; set; }

    }
}
