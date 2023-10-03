using Telegram.Bot;

namespace WebApp.Services
{
    public class Bot
    {
        private static TelegramBotClient client { get; set; }

        public static TelegramBotClient GetTelegramBot()
        {
            if (client != null)
            {
                return client;
            }
            client = new TelegramBotClient("6520070877:AAGw5neu_DPdsz6X0eHO8h4kBn5vWhBqB7Q");
            return client;
        }
    }
}
