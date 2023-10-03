using Telegram.Bot.Types;

namespace WebApp.Services
{
    public interface IBotCommand
    {
        Task StartCommand(Update update);

        Task NewPostCommand(Update update);

        Task UpdatePostCommand(Update update);

        Task AcceptCommand(Update update);

        Task CancelCommand(Update update);

        Task AcceptUpdateCommand(Update update);

        Task CancelUpdateCommand(Update update);

        Task Back(Update update);
    }
}
