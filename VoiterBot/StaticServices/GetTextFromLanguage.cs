using System.Collections.Generic;
using VoterBot.Entities;
using VoterBot.Enums;

namespace VoterBot.StaticServices
{
    public static class GetTextFromLanguage
    {
        public static string GetText(Language language, BotResponseText data)
        {
            return language switch
            {
                Language.Uz => data.Uz,
                Language.Ru => data.Ru,
                Language.Eng => data.Eng,
                _ => data.Uz
            };
        }

        public static List<string> GetKeyboardText(Language language, List<BotResponseText> KeyBoards)
        {
            List<string> textKeyboards = new List<string>();
            foreach (var keyBoard in KeyBoards)
            {
                textKeyboards.Add(GetTextFromLanguage.GetText(language, keyBoard));
            }
            return textKeyboards;
        }
    }
}
