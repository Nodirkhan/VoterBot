using System.Collections.Generic;

namespace VoterBot.Entities
{
    public class Major
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
