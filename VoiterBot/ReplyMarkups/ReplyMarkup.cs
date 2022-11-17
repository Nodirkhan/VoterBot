using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Threading.Channels;
using Telegram.Bot.Types.ReplyMarkups;
using VoterBot.Entities;

namespace VoterBot.ReplyMarkups
{
    public static class ReplyMarkup
    {
        public static IReplyMarkup OptionsOfLanguage()
        {
            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text:"Uz 🇺🇿",callbackData: "Uz"),
                    InlineKeyboardButton.WithCallbackData(text:"Ru 🇷🇺",callbackData: "Ru"),
                    InlineKeyboardButton.WithCallbackData(text:"Eng 🇺🇸",callbackData: "Eng")
                }
            });
        }

        public static IReplyMarkup OptionsOfChannel(string checkerbutton)
        {
            var rows = new List<InlineKeyboardButton[]>();

            foreach(var channel in DefaultList.Channels)
            {
                rows.Add(new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithUrl(channel.Name, "t.me//" + channel.Link)
                });
            }

            rows.Add(new InlineKeyboardButton[]
            {
                InlineKeyboardButton.WithCallbackData(checkerbutton, "VERIFY")
            });

            return new InlineKeyboardMarkup(rows);
        }

        public static IReplyMarkup SendContact()
        {
            return new ReplyKeyboardMarkup(new KeyboardButton[]
            {
                KeyboardButton.WithRequestContact("Contact")
            });
        }
    }
}
