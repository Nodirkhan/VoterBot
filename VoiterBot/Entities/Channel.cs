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
                Name = "Birinchi kanal",
                Link = "t.me//Nodir_khan"
            },
            new Channel
            {
                Id = 20,
                Name = "Ikkinchi kanal",
                Link = "t.me//TestSinovBotUchun"
            }
        };
    }


}
