namespace CarouselCardsBot
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

    [Serializable]
    public class CarouselCardsDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var reply = context.MakeMessage();

            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments = GetCardsAttachments();

            await context.PostAsync(reply);

            context.Wait(this.MessageReceivedAsync);
        }

        private static IList<Attachment> GetCardsAttachments()
        {
            return new List<Attachment>()
            {
                GetHeroCard(
                    "Universidad César Vallejo",
                    //"Offload the heavy lifting of data center management",
                    "Los sistemas informáticos e internet han transformado nuestra vida cotidiana y la sociedad en general.",
                    "Store and help protect your data. Get durable, highly available data storage across the globe and pay only for what you use.",
                    //new CardImage(url: "https://docs.microsoft.com/en-us/aspnet/aspnet/overview/developing-apps-with-windows-azure/building-real-world-cloud-apps-with-windows-azure/data-storage-options/_static/image5.png"),
                    new CardImage(url: "https://www.ucv.edu.pe/assets/imgs/logo.png"),
                    //new CardAction(ActionTypes.OpenUrl, "Learn more", value: "https://azure.microsoft.com/en-us/services/storage/")),
                    new CardAction(ActionTypes.OpenUrl, "Leer más", value: "https://www.ucv.edu.pe/pregrado/ingenieria-de-sistemas")),
                GetThumbnailCard(
                    //"DocumentDB",
                    "Trilce UCV",
                    //"Blazing fast, planet-scale NoSQL",
                    "Plataforma Campus del estudiante",
                    //"NoSQL service for highly available, globally distributed apps—take full advantage of SQL and JavaScript over document and key-value data without the hassles of on-premises or virtual machine-based cloud database options.",
                    "Service for highly availab le, globally distributed apps—take full advantage of SQL and JavaScript over document and key-value data without the hassles of on-premises or virtual machine-based cloud database options.",
                    //new CardImage(url: "https://docs.microsoft.com/en-us/azure/documentdb/media/documentdb-introduction/json-database-resources1.png"),
                    new CardImage(url: "data:image/png;base64,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"),
                    //new CardAction(ActionTypes.OpenUrl, "Learn more", value: "https://azure.microsoft.com/en-us/services/documentdb/")),
                    new CardAction(ActionTypes.OpenUrl, "Leer más", value: "https://trilce.ucv.edu.pe/default.aspx")),
                    
                //GetHeroCard(
                //    "Azure Functions",
                //    "Process events with a serverless code architecture",
                //    "An event-based serverless compute experience to accelerate your development. It can scale based on demand and you pay only for the resources you consume.",
                //    new CardImage(url: "https://msdnshared.blob.core.windows.net/media/2016/09/fsharp-functions2.png"),
                //    new CardAction(ActionTypes.OpenUrl, "Learn more", value: "https://azure.microsoft.com/en-us/services/functions/")),
                //GetThumbnailCard(
                //    "Cognitive Services",
                //    "Build powerful intelligence into your applications to enable natural and contextual interactions",
                //    "Enable natural and contextual interaction with tools that augment users' experiences using the power of machine-based intelligence. Tap into an ever-growing collection of powerful artificial intelligence algorithms for vision, speech, language, and knowledge.",
                //    new CardImage(url: "https://msdnshared.blob.core.windows.net/media/2017/03/Azure-Cognitive-Services-e1489079006258.png"),
                //    new CardAction(ActionTypes.OpenUrl, "Learn more", value: "https://azure.microsoft.com/en-us/services/cognitive-services/")),
            };
        }

        private static Attachment GetHeroCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction)
        {
            var heroCard = new HeroCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage>() { cardImage },
                Buttons = new List<CardAction>() { cardAction },
            };

            return heroCard.ToAttachment();
        }

        private static Attachment GetThumbnailCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction)
        {
            var heroCard = new ThumbnailCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage>() { cardImage },
                Buttons = new List<CardAction>() { cardAction },
            };

            return heroCard.ToAttachment();
        }
    }
}
