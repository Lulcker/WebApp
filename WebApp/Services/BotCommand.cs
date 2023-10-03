using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WebApp.Models;
using WebApp.Repositories;

namespace WebApp.Services
{
    public class BotCommand : IBotCommand
    {
        private readonly ApplicationContext _db;
        private readonly IAdminCommandRepository _command;
        private TelegramBotClient bot = Bot.GetTelegramBot();
        public BotCommand(ApplicationContext db, IAdminCommandRepository command)
        {
            _db = db;
            _command = command;
        }

        public async Task StartCommand(Update update)
        {
            var chatId = update.Message.Chat.Id;
            var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton("Новые посты"),
                new KeyboardButton("Запросы на обновления")
            });
            replyKeyboardMarkup.ResizeKeyboard = true;

            await bot.SendTextMessageAsync(chatId, "Выберите действие", replyMarkup: replyKeyboardMarkup);
        }

        public async Task NewPostCommand(Update update)
        {
            var chatId = update.Message.Chat.Id;

            if (_db.Posts.Any(p => !p.Enabled))
            {
                var post = await _db.Posts.FirstAsync(p => !p.Enabled);

                string path = Directory.GetCurrentDirectory() + "\\wwwroot" + post.PathToImage[1..];
                await using Stream stream = System.IO.File.OpenRead(path);
                Message message = await bot.SendPhotoAsync(
                    chatId: chatId,
                    photo: InputFile.FromStream(stream: stream),
                    caption: $"Название поста:\n{post.Title}\n\nОписание:\n{post.Description}\n\nСодержание:\n{post.Content}\n\nАвтор: {post.Author}"
                    );

                var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton("Одобрить"),
                    new KeyboardButton("Отклонить"),
                    new KeyboardButton("Назад")
                });
                replyKeyboardMarkup.ResizeKeyboard = true;

                await bot.SendTextMessageAsync(chatId, "Выберите действие:", replyMarkup: replyKeyboardMarkup);
            }
            else
            {
                await bot.SendTextMessageAsync(chatId, "Новых постов нет");
            }
        }

        public async Task AcceptCommand(Update update)
        {
            var chatId = update.Message.Chat.Id;

            var post = await _db.Posts.FirstAsync(p => !p.Enabled);
            await _command.AcceptPostAsync(post.Id);
            await bot.SendTextMessageAsync(chatId, "Пост опубликован");

            if (_db.Posts.Any(p => !p.Enabled))
            {
                post = await _db.Posts.FirstAsync(p => !p.Enabled);

                string path = Directory.GetCurrentDirectory() + "\\wwwroot" + post.PathToImage[1..];
                await using Stream stream = System.IO.File.OpenRead(path);
                Message message = await bot.SendPhotoAsync(
                    chatId: chatId,
                    photo: InputFile.FromStream(stream: stream),
                    caption: $"Название поста:\n{post.Title}\n\nОписание:\n{post.Description}\n\nСодержание:\n{post.Content}\n\nАвтор: {post.Author}"
                    );
            }
            else
            {
                var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton("Назад")
                });
                replyKeyboardMarkup.ResizeKeyboard = true;

                await bot.SendTextMessageAsync(chatId, "Новых постов больше нет", replyMarkup: replyKeyboardMarkup);
            }
        }

        public async Task CancelCommand(Update update)
        {
            var chatId = update.Message.Chat.Id;

            var post = await _db.Posts.FirstAsync(p => !p.Enabled);
            await _command.CancelPostAsync(post.Id);
            await bot.SendTextMessageAsync(chatId, "Пост не опубликован");

            if (_db.Posts.Any(p => !p.Enabled))
            {
                post = await _db.Posts.FirstAsync(p => !p.Enabled);

                string path = Directory.GetCurrentDirectory() + "\\wwwroot" + post.PathToImage[1..];
                await using Stream stream = System.IO.File.OpenRead(path);
                Message message = await bot.SendPhotoAsync(
                    chatId: chatId,
                    photo: InputFile.FromStream(stream: stream),
                    caption: $"Название поста:\n{post.Title}\n\nОписание:\n{post.Description}\n\nСодержание:\n{post.Content}\n\nАвтор: {post.Author}"
                    );
            }
            else
            {
                var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton("Назад")
                });
                replyKeyboardMarkup.ResizeKeyboard = true;

                await bot.SendTextMessageAsync(chatId, "Новых постов больше нет", replyMarkup: replyKeyboardMarkup);
            }
        }

        public async Task UpdatePostCommand(Update update)
        {
            var chatId = update.Message.Chat.Id;

            if (_db.UpdatePosts.Any())
            {
                var post = await _db.UpdatePosts.FirstAsync();

                string path = Directory.GetCurrentDirectory() + "\\wwwroot" + post.PathToImage[1..];
                await using Stream stream = System.IO.File.OpenRead(path);

                Message message = await bot.SendPhotoAsync(
                    chatId: chatId,
                    photo: InputFile.FromStream(stream: stream),
                    caption: $"Название поста:\n{post.Title}\n\nОписание:\n{post.Description}\n\nСодержание:\n{post.Content}\n\nАвтор: {post.Author}"
                    );

                var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton("Обновить"),
                    new KeyboardButton("Не обновлять"),
                    new KeyboardButton("Назад")
                });
                replyKeyboardMarkup.ResizeKeyboard = true;

                await bot.SendTextMessageAsync(chatId, "Выберите действие:", replyMarkup: replyKeyboardMarkup);
            }
            else
            {
                await bot.SendTextMessageAsync(chatId, "Постов на обновление нет");
            }
        }

        public async Task AcceptUpdateCommand(Update update)
        {
            var chatId = update.Message.Chat.Id;

            var post = await _db.UpdatePosts.FirstAsync();
            await _command.AcceptUpdatePostAsync(post.Id);
            await bot.SendTextMessageAsync(chatId, "Пост обновлён");

            if (_db.UpdatePosts.Any())
            {
                post = await _db.UpdatePosts.FirstAsync();

                string path = Directory.GetCurrentDirectory() + "\\wwwroot" + post.PathToImage[1..];
                await using Stream stream = System.IO.File.OpenRead(path);

                Message message = await bot.SendPhotoAsync(
                    chatId: chatId,
                    photo: InputFile.FromStream(stream: stream),
                    caption: $"Название поста:\n{post.Title}\n\nОписание:\n{post.Description}\n\nСодержание:\n{post.Content}\n\nАвтор: {post.Author}"
                    );
            }
            else
            {
                var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton("Назад")
                });
                replyKeyboardMarkup.ResizeKeyboard = true;

                await bot.SendTextMessageAsync(chatId, "Постов на обновление нет", replyMarkup: replyKeyboardMarkup);
            }
        }

        public async Task CancelUpdateCommand(Update update)
        {
            var chatId = update.Message.Chat.Id;

            var post = await _db.UpdatePosts.FirstAsync();
            await _command.CancelUpdatePostAsync(post.Id);
            await bot.SendTextMessageAsync(chatId, "Пост не обновлён");

            if (_db.UpdatePosts.Any())
            {
                post = await _db.UpdatePosts.FirstAsync();

                string path = Directory.GetCurrentDirectory() + "\\wwwroot" + post.PathToImage[1..];
                await using Stream stream = System.IO.File.OpenRead(path);

                Message message = await bot.SendPhotoAsync(
                    chatId: chatId,
                    photo: InputFile.FromStream(stream: stream),
                    caption: $"Название поста:\n{post.Title}\n\nОписание:\n{post.Description}\n\nСодержание:\n{post.Content}\n\nАвтор: {post.Author}"
                    );
            }
            else
            {
                var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton("Назад")
                });
                replyKeyboardMarkup.ResizeKeyboard = true;

                await bot.SendTextMessageAsync(chatId, "Постов на обновление нет", replyMarkup: replyKeyboardMarkup);
            }
        }

        public async Task Back(Update update)
        {
            var chatId = update.Message.Chat.Id;

            var replyKeyboardMarkups = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton("Новые посты"),
                new KeyboardButton("Запросы на обновления")
            });
            replyKeyboardMarkups.ResizeKeyboard = true;

            await bot.SendTextMessageAsync(chatId, "Выберите действие", replyMarkup: replyKeyboardMarkups);
        }
    }
}
