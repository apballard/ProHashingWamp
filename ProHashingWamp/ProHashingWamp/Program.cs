using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WampSharp.V2;
using WampSharp.V2.Client;
using WampSharp.V2.Fluent;
using WampSharp.V2.Rpc;

namespace ProHashingWamp
{
    public interface IArgumentsService
    {
        [WampProcedure("f_all_balance_updates")]
        JObject f_all_balance_updates(string apiKey);

        [WampProcedure("f_all_profitability_updates")]
        JObject f_all_profitability_updates();

        [WampProcedure("f_all_miner_updates")]
        JArray f_all_miner_updates(string apiKey);
    }

    public class myItem
    {
        public string key;
        public string value;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var apiKey = "PUT_YOUR_API_KEY_HERE";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WampChannelFactory factory =
                new WampChannelFactory();

            const string serverAddress = "wss://live.prohashing.com:443/ws";
            
            IWampClientAuthenticator myAuthenticator = new WampCraClientAuthenticator(authenticationId: "web", secret: "web");

            IWampChannel channel =
                factory.ConnectToRealm("mining")
                    .WebSocketTransport(serverAddress)
                    .SetSecurityOptions(o =>
                    {
                        o.EnabledSslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
                        //o.Certificates.Add(new X509Certificate2(@"server_cert.pem"));
                        o.AllowNameMismatchCertificate = true;
                        o.AllowUnstrustedCertificate = true;
                    })
                    .JsonSerialization()
                    .Authenticator(myAuthenticator)
                    .Build();

            ServicePointManager.ServerCertificateValidationCallback = (s, crt, chain, policy) => true;

            channel.Open().Wait(5000);

            Console.WriteLine("Connection Complete ");

            //IWampRealmProxy realmProxy = channel.RealmProxy;   //use this for a subscription in conjunction with commented out below
            IArgumentsService proxy = channel.RealmProxy.Services.GetCalleeProxy<IArgumentsService>();

            Console.WriteLine("Channel Proxy: " + channel.RealmProxy.Name);

            JObject myResults = proxy.f_all_balance_updates(apiKey);
            var path = Directory.GetCurrentDirectory();
            path += "\\UserBalance.json";
            File.WriteAllText(path, myResults.ToString());


            myResults = proxy.f_all_profitability_updates();
            path = Directory.GetCurrentDirectory();
            path += "\\Profitability.json";
            File.WriteAllText(path, myResults.ToString());


            var myResults2 = proxy.f_all_miner_updates(apiKey);


            path = Directory.GetCurrentDirectory();
            path += "\\SessionDetails.json";
            File.WriteAllText(path, myResults2.ToString());

            //----------------------------------------
            //int received = 0;
            //IDisposable subscription = null;



            //subscription =
            //    realmProxy.Services.GetSubject("balance_updates_"+apiKey)
            //        .Subscribe(x =>
            //        {
            //            Console.WriteLine("Got Event: " + x);

            //            received++;

            //            if (received > 5)
            //            {
            //                Console.WriteLine("Closing ..");
            //                subscription.Dispose();
            //            }
            //        });

            //Console.ReadLine();
            ////----------------------------------------
        }
    }
}
