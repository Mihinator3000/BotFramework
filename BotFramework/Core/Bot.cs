﻿using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using Tef.BotFramework.Abstractions;
using Tef.BotFramework.Common;
using Tef.BotFramework.Core.CommandControllers;
using Tef.BotFramework.Tools.Loggers;

namespace Tef.BotFramework.Core
{
    public class Bot : IDisposable
    {
        private readonly CommandHandler _commandHandler;
        private readonly IBotApiProvider _botProvider;
        private readonly ICommandParser _commandParser;

        private char _prefix = '\0';
        private bool _caseSensitive = true;

        public Bot(IBotApiProvider botProvider)
        {
            _botProvider = botProvider;

            _commandParser = new CommandParser();
            _commandHandler = new CommandHandler();
        }

        public void Start()
        {
            _botProvider.OnMessage += ApiProviderOnMessage;
        }

        public Bot AddDefaultLogger()
        {
            LoggerHolder.Instance.Verbose("Initialized");
            return this;
        }

        public Bot AddLogger(ILogger logger)
        {
            LoggerHolder.Init(logger);
            LoggerHolder.Instance.Verbose("Initialized");
            return this;
        }

        public Bot SetPrefix(char prefix)
        {
            _prefix = prefix;
            return this;
        }

        public Bot WithoutCaseSensitiveCommands()
        {
            _caseSensitive = false;
            _commandHandler.WithoutCaseSensitiveCommands();
            return this;
        }

        public Bot AddCommand(IBotCommand command)
        {
            _commandHandler.RegisterCommand(command);
            return this;
        }

        public Bot AddCommands(IEnumerable<IBotCommand> commands)
        {
            foreach (var command in commands)
                _commandHandler.RegisterCommand(command);
            return this;
        }

        private void ApiProviderOnMessage(object sender, BotEventArgs e)
        {
            try
            {
                var commandWithArgs = _commandParser.ParseCommand(e);
                var commandName = commandWithArgs.CommandName;

                if (commandName.FirstOrDefault() != _prefix && _prefix != '\0')
                    return;

                if (commandName.FirstOrDefault() == _prefix)
                    commandName = commandName.Remove(0, 1);

                if (!_caseSensitive)
                    commandName = commandName.ToLower();

                commandWithArgs =
                    new CommandArgumentContainer(commandName, commandWithArgs.Sender, commandWithArgs.Arguments);

                var commandTaskResult = _commandHandler.IsCommandCorrect(commandWithArgs);
                LoggerHolder.Instance.Verbose(commandTaskResult.ToString());

                if (!commandTaskResult.IsSuccess)
                    return;

                var commandExecuteResult = _commandHandler.ExecuteCommand(commandWithArgs);
                if (!commandExecuteResult.IsSuccess)
                    LoggerHolder.Instance.Warning(commandExecuteResult.ToString());

                var writeMessageResult =
                    _botProvider.WriteMessage(new BotEventArgs(commandExecuteResult.ToString(), commandWithArgs.Sender.GroupId, commandWithArgs.Sender.UserSenderId, commandWithArgs.Sender.Username));

                LoggerHolder.Instance.Verbose(writeMessageResult.ToString());
            }
            catch (Exception error)
            {
                LoggerHolder.Instance.Error(error.Message);
                _botProvider.Restart();
            }
        }

        public void Dispose()
        {
            _botProvider.OnMessage -= ApiProviderOnMessage;
        }
    }
}