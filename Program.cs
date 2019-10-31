using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Task04
{
    class Program
    {
        public static String GetData(String linkedURL)
        {
            return HTMLInteraction.GetHTMLbyURL(linkedURL);
        }

        private static Task ProcessURL(String url)
        {
            return Task.Run(() => { Console.WriteLine("URL: {0}, Symbols count: {1}", url, GetData(url).Length); }
            );
        }

        public static async Task Main(string[] args)
        {
            String initialURL = args[0];
            List<String> urls = HTMLInteraction.getURLs(initialURL);
            try
            {
                List<Task> tasks = new List<Task>();
                foreach (String url in urls)
                {
                    tasks.Add(ProcessURL(url));
                }

                await Task.WhenAll(tasks.ToArray());
            }
            catch (WebException)
            {
                Console.WriteLine("URL is not valid or connection problem happened");
                return;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("URL unsupported");
                return;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("No response");
            }
        }
    }
}