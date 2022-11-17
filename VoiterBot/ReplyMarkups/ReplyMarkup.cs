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
                    InlineKeyboardButton.WithUrl(channel.Name, "https://t.me//" + channel.Link)
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

        public static IReplyMarkup SendKeyBoardButton(List<string> texts)
        {
            var rows = new List<KeyboardButton[]>();
            var cols = new List<KeyboardButton>();
            foreach (var text in texts)
            {
                if (cols.Count == 2)
                {
                    rows.Add(cols.ToArray());
                    cols.Clear();
                    cols.Add(text);
                }
                else
                {
                    cols.Add(new KeyboardButton(text));
                }
            }
            if (cols.Count > 0)
            {
                rows.Add(cols.ToArray());
            }
            return new ReplyKeyboardMarkup(rows)
            {
                ResizeKeyboard = true
            };
        }
    }
}
