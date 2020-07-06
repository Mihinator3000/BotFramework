﻿using System;
using Telegram.Bot;

namespace BotFramework.Telegram
{
    public class TelegramLogger
    {
        private readonly TelegramBotClient _telegramApi;

        public TelegramLogger(TelegramBotClient telegramApi)
        {
            _telegramApi = telegramApi;
        }

        public void Login()
        {
            var botClient = new TelegramBotClient(TelegramSettings.Key, TelegramSettings.Proxy);
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}
