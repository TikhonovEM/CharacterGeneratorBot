using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using System.Net.Http.Json;
using System.Text.Json;

namespace CharacterGeneratorBot
{
    public class Bot
    {
        private ITelegramBotClient _botClient;

        // Это объект с настройками работы бота. Здесь мы будем указывать, какие типы Update мы будем получать, Timeout бота и так далее.
        private ReceiverOptions _receiverOptions;

        public Bot()
        {
            var token = Configuration.ConfigurationProvider.Configuration.GetSection("BotToken").Value;
            _botClient = new TelegramBotClient(token);
            _receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new[]
                {
                    UpdateType.Message
                },
                ThrowPendingUpdates = true,
            };
        }

        public async Task Start()
        {
            using var cts = new CancellationTokenSource();

            // UpdateHander - обработчик приходящих Update`ов
            // ErrorHandler - обработчик ошибок, связанных с Bot API
            _botClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token);

            var me = await _botClient.GetMeAsync();
            Console.WriteLine($"{me.FirstName} запущен!");

            await Task.Delay(-1); // Устанавливаем бесконечную задержку, чтобы наш бот работал постоянно
        }

        private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                        {
                            var message = update.Message!;

                            var user = message.From!;

                            Console.WriteLine($"{user.FirstName} ({user.Id}) написал сообщение: {message.Text}");

                            // Chat - содержит всю информацию о чате
                            var chat = message.Chat;
                            var answerMessage = await MorkBorg.CharacterGenerator.GenerateAsMarkdownText(cancellationToken);
                            
                            await botClient.SendTextMessageAsync(chat.Id, answerMessage, parseMode: ParseMode.Markdown, cancellationToken: cancellationToken);

                            return;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
        {
            var ErrorMessage = error switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => error.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
