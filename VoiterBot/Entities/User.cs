using System.Collections.Generic;
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

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoterNumber { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public Language Language { get; set; }


        public string ContactNumber { get; set; }

        public bool IsSubscriber { get; set; } = false;

        public List<string> Comments{ get; set; }

        public User()
        {
            Comments = new List<string>();
        }

    }
}
