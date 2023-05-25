using Domain.Models;
using Newtonsoft.Json;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Tbot
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            var botClient = new TelegramBotClient ("6056337872:AAGd79_wr1UNiviSLNXeUjAF4z2cj22uspg");
            using CancellationTokenSource cts = new();
            ReceiverOptions recieverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };
            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: recieverOptions,
                cancellationToken: cts.Token
                );
            var me = await botClient.GetMeAsync();
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            cts.Cancel();
            HttpClient client = new HttpClient();
            var result = await client.GetAsync("https://localhost:7107/api/Product");
            Console.WriteLine(result);

            var test = await result.Content.ReadAsStringAsync();
            Console.WriteLine(test);
            Product[] products = JsonConvert.DeserializeObject<Product[]>(test);

            foreach (var product in products)
            {
                Console.WriteLine(product.Productid + " " + product.Category + " " + product.Price);
            }

        }
        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {


            if (update.Message is not { } message)
                return;
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;
            Console.WriteLine($"Recived a '{message.Text}' message in chat {chatId}");
            Message sentMessage = await botClient.SendTextMessageAsync(chatId: chatId, text: "you said:\n" + messageText, cancellationToken: cancellationToken);
            if (message.Text == "Проверка")
            {
                await botClient.SendTextMessageAsync(chatId: chatId, text: "Проверка", cancellationToken: cancellationToken);
            };

            if (message.Text == "Стикер")
            {
                string sticker_fielid = "CAACAgIAAxkBAAEhqolkb9HCwNHy8U-NCSZlg3rZcijwkQACGQ8AAkg8AAFIT1LGAAG4c7cHLwQ";
                await botClient.SendStickerAsync(chatId: chatId,
                    sticker: sticker_fielid,
            cancellationToken: cancellationToken);
            }
            if (message.Text == "Видео")
            {
                string video1 = "https://rr18---sn-n8v7kn76.googlevideo.com/videoplayback?expire=1685068024&ei=mMRvZOyMGZeG1gLR-bvQBg&ip=212.102.39.71&id=o-AM_kn3gJislMXPOgXDKuHmHME6Rw-k3NF1XsXhN10U2n&itag=135&aitags=133%2C134%2C135%2C160%2C242%2C243%2C244%2C278&source=youtube&requiressl=yes&spc=qEK7BzHhvlRlC7psA7twnungN85WOTMFUtdJHP4tWA&vprv=1&svpuc=1&mime=video%2Fmp4&ns=JTEhRh46pWuZ51P24gV7_YwN&gir=yes&clen=579608&dur=4.680&lmt=1680131770715115&keepalive=yes&fexp=24007246,51000014,51000022&c=WEB&txp=5316224&n=eMrRgxKWAQMbMg&sparams=expire%2Cei%2Cip%2Cid%2Caitags%2Csource%2Crequiressl%2Cspc%2Cvprv%2Csvpuc%2Cmime%2Cns%2Cgir%2Cclen%2Cdur%2Clmt&sig=AOq0QJ8wRgIhAJXSy78k05kJI6DULB8yXUhHb-GD8rz5qBkytar5x4uQAiEAhbvt1BLsxJIvXothGWBg_A5kILXcTmbevNhasjYnI9w%3D&rm=sn-n02xgoxufvg3-2gbs7d,sn-2gbey7l&req_id=bcc56fbdb6f9a3ee&cmsv=e&redirect_counter=2&cms_redirect=yes&ipbypass=yes&mh=OG&mip=45.143.23.41&mm=29&mn=sn-n8v7kn76&ms=rdu&mt=1685051595&mv=m&mvi=18&pl=22&lsparams=ipbypass,mh,mip,mm,mn,ms,mv,mvi,pl&lsig=AG3C_xAwRAIgfKSsqChqHbPsreE7eih7E5zRzOBckhLX_BQVVUJ5810CIF9BQs9eYEbx4AJZgqvFllxZY2bvKTg9hxTy6MQCQ3UR";
               await botClient.SendVideoAsync(chatId: chatId,
                   parseMode: ParseMode.Html,
                   supportsStreaming: true,
               video: video1,
               cancellationToken: cancellationToken);
            }
            


        }
        static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                   => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            };
            Console.WriteLine(ErrorMessage);
                return Task.CompletedTask;
        }
    }
}
