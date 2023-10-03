using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using WebApp.Services;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("/")]
    public class BotController : ControllerBase
    {
        private readonly IBotCommand _botCommand;

        public BotController(IBotCommand botCommand)
        {
            _botCommand = botCommand;
        }

        [HttpPost]
        public async Task Post(Update update)
        {
            switch (update.Message.Text)
            {
                case "/start":
                    await _botCommand.StartCommand(update);
                    break;

                case "Новые посты":
                    await _botCommand.NewPostCommand(update);
                    break;

                case "Одобрить":
                    await _botCommand.AcceptCommand(update);
                    break;

                case "Отклонить":
                    await _botCommand.CancelCommand(update);
                    break;

                case "Запросы на обновления":
                    await _botCommand.UpdatePostCommand(update);
                    break;

                case "Обновить":
                    await _botCommand.AcceptUpdateCommand(update);
                    break;

                case "Не обновлять":
                    await _botCommand.CancelUpdateCommand(update);
                    break;

                case "Назад":
                    await _botCommand.Back(update);
                    break;

                default:
                    break;
            }
        }
    }
}
