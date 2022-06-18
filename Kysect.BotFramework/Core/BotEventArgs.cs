﻿using System;
using System.Collections.Generic;
using System.Linq;
using Kysect.BotFramework.Core.BotMedia;
using Kysect.BotFramework.Core.BotMessages;
using Kysect.BotFramework.Core.Contexts;

namespace Kysect.BotFramework.Core
{
    public class BotEventArgs : EventArgs
    {
        public IBotMessage Message { get; }
        public SenderInfo SenderInfo { get; }

        public IServiceProvider ServiceProvider { get; }

        public BotEventArgs(IBotMessage message, SenderInfo senderInfo, IServiceProvider serviceProvider)
        {
            Message = message;
            SenderInfo = senderInfo;
            ServiceProvider = serviceProvider;
        }

        public string FindCommandName()
        {
            if (Message.Text is null)
            {
                return string.Empty;
            }

            return Message.Text.Split().FirstOrDefault();
        }

        public List<string> GetCommandArguments() => Message.Text.Split().Skip(1).ToList();

        public List<IBotMediaFile> GetMediaFiles()
        {
            if (Message is BotSingleMediaMessage singleMediaMessage)
            {
                return new List<IBotMediaFile> {singleMediaMessage.MediaFile};
            }

            if (Message is BotMultipleMediaMessage multipleMediaMessage)
            {
                return multipleMediaMessage.MediaFiles;
            }

            return new List<IBotMediaFile>();
        }
    }
}