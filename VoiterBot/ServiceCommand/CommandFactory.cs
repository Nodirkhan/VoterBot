using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Security.Cryptography;
using VoterBot.Commands;
using VoterBot.Interface;

namespace VoterBot.ServiceCommand
{
    public class CommandFactory
    {
        private static Dictionary<string, Command> _commands { get; set; }
        public CommandFactory()
        {
            _commands = _commands ?? Register();
        }
        public static Command GetCommand(string key) =>
            _commands.ContainsKey(key) ? _commands[key] : null;

        public static Dictionary<string, Command> Register()
        {
            _commands = new Dictionary<string, Command>();
            _commands.Add(CommandWords.START, new StartCommand());
            _commands.Add(CommandWords.SHOW_LANGUAGES, new ShowLanguageCommand());
            _commands.Add(CommandWords.SHOW_CHANNELS, new ShowChannelsCommand());
            _commands.Add(CommandWords.UZ, new ModifiedLanguageCommand());
            _commands.Add(CommandWords.RU, new ModifiedLanguageCommand());
            _commands.Add(CommandWords.ENG, new ModifiedLanguageCommand());
            _commands.Add(CommandWords.VERIFY, new VerifySubscriberCommand());
            _commands.Add(CommandWords.FULLNAME, new FullNameCommand());
            _commands.Add(CommandWords.CONTACT, new SendContactCommand());
            _commands.Add(CommandWords.MODIFY_CONTACT, new ModifyConactCommand());
            _commands.Add(CommandWords.ACCEPTED_COMMENT, new CommentCommand());
            _commands.Add(CommandWords.KEYBOARDBUTTON, new SendKeyBoardButton());
            _commands.Add(CommandWords.CHANGE_LANGUAGE, new ShowLanguageCommand());
            _commands.Add(CommandWords.PERSONAL_NUMBER, new PersonalNumberCommand());

            return _commands;
        }

        public static class CommandWords
        {
            public const string START = "/start";
            public const string SHOW_LANGUAGES = "Tilni tanlang";
            public const string SHOW_CHANNELS = "show_channels";
            public const string UZ = "Uz";
            public const string RU = "Ru";
            public const string ENG = "Eng";
            public const string VERIFY = "Verify";
            public const string FULLNAME = "FullName";
            public const string CONTACT = "Contact";
            public const string MODIFY_CONTACT = "Modify_Contact";
            public const string ACCEPTED_COMMENT = "Accepted_Comment";
            public const string KEYBOARDBUTTON = "KeyboardButton";
            public const string CHANGE_LANGUAGE = "Tilni o'zgartirish";
            public const string PERSONAL_NUMBER = "Shaxsiy raqam";
            public const string DELETE_MESSAGE = "Delete_Message";
        }
    }
}
