using System.Collections.Generic;

namespace VoterBot.Entities
{
    public class Channel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }

    }

    public static class DefaultList
    {
        public static List<Channel> Channels { get; set; } = new List<Channel>()
        {
            new Channel
            {
                Id = 10,
                Name = "Channel: e-gov.uz 🔑",
                Link = "eGovUz "
            },
            new Channel
            {
                Id = 10,
                Name = "Channel: my.gov.uz 🔑",
                Link = "MyGovUz"
            }
        };
    }


}
