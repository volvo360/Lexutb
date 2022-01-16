using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class ExchangeRates
    {
        public static JObject GetData;

        public static void getExchangeRates()
        {
            try
            {
                String URLString = "https://freecurrencyapi.net/api/v2/latest?apikey=57bd8600-74ef-11ec-999c-bfa540553179&base_currency=SEK";
                using (var webClient = new System.Net.WebClient())
                {
                    var json = webClient.DownloadString(URLString);
                    GetData = JObject.Parse(json);
                }
            }
            catch (Exception)
            {
                return;
            }

            //Is only left as a reference for the future

            /*string JPY = (string)GetData["data"]["JPY"];
            string CNY = (string)GetData["data"]["CNY"];
            string CHF = (string)GetData["data"]["CHF"];
            string CAD = (string)GetData["data"]["CAD"];
            string MXN = (string)GetData["data"]["MXN"];
            string INR = (string)GetData["data"]["INR"];
            string BRL = (string)GetData["data"]["BRL"];
            string RUB = (string)GetData["data"]["RUB"];
            string KRW = (string)GetData["data"]["KRW"];
            string IDR = (string)GetData["data"]["IDR"];
            string TRY = (string)GetData["data"]["TRY"];
            string SAR = (string)GetData["data"]["SAR"];
            string SEK = (string)GetData["data"]["SEK"];
            string NGN = (string)GetData["data"]["NGN"];
            string PLN = (string)GetData["data"]["PLN"];
            string ARS = (string)GetData["data"]["ARS"];
            string NOK = (string)GetData["data"]["NOK"];
            string TWD = (string)GetData["data"]["TWD"];
            string IRR = (string)GetData["data"]["IRR"];
            string AED = (string)GetData["data"]["AED"];
            string COP = (string)GetData["data"]["COP"];
            string THB = (string)GetData["data"]["THB"];
            string ZAR = (string)GetData["data"]["ZAR"];
            string DKK = (string)GetData["data"]["DKK"];
            string MYR = (string)GetData["data"]["MYR"];
            string SGD = (string)GetData["data"]["SGD"];
            string ILS = (string)GetData["data"]["ILS"];
            string HKD = (string)GetData["data"]["HKD"];
            string EGP = (string)GetData["data"]["EGP"];
            string PHP = (string)GetData["data"]["PHP"];
            string CLP = (string)GetData["data"]["CLP"];
            string PKR = (string)GetData["data"]["PKR"];
            string IQD = (string)GetData["data"]["IQD"];
            string DZD = (string)GetData["data"]["DZD"];
            string KZT = (string)GetData["data"]["KZT"];
            string QAR = (string)GetData["data"]["QAR"];
            string CZK = (string)GetData["data"]["CZK"];
            string PEN = (string)GetData["data"]["PEN"];
            string RON = (string)GetData["data"]["RON"];
            string VND = (string)GetData["data"]["VND"];
            string BDT = (string)GetData["data"]["BDT"];
            string HUF = (string)GetData["data"]["HUF"];
            string UAH = (string)GetData["data"]["UAH"];
            string AOA = (string)GetData["data"]["AOA"];
            string MAD = (string)GetData["data"]["MAD"];
            string OMR = (string)GetData["data"]["OMR"];
            string CUC = (string)GetData["data"]["CUC"];
            string BYR = (string)GetData["data"]["BYR"];
            string AZN = (string)GetData["data"]["AZN"];
            string LKR = (string)GetData["data"]["LKR"];
            string SDG = (string)GetData["data"]["SDG"];
            string SYP = (string)GetData["data"]["SYP"];
            string MMK = (string)GetData["data"]["MMK"];
            string DOP = (string)GetData["data"]["DOP"];
            string UZS = (string)GetData["data"]["UZS"];
            string KES = (string)GetData["data"]["KES"];
            string GTQ = (string)GetData["data"]["GTQ"];
            string URY = (string)GetData["data"]["URY"];
            string HRV = (string)GetData["data"]["HRV"];
            string MOP = (string)GetData["data"]["MOP"];
            string ETB = (string)GetData["data"]["ETB"];
            string CRC = (string)GetData["data"]["CRC"];
            string TZS = (string)GetData["data"]["TZS"];
            string TMT = (string)GetData["data"]["TMT"];
            string TND = (string)GetData["data"]["TND"];
            string PAB = (string)GetData["data"]["PAB"];
            string LBP = (string)GetData["data"]["LBP"];
            string RSD = (string)GetData["data"]["RSD"];
            string LYD = (string)GetData["data"]["LYD"];
            string GHS = (string)GetData["data"]["GHS"];
            string YER = (string)GetData["data"]["YER"];
            string BOB = (string)GetData["data"]["BOB"];
            string BHD = (string)GetData["data"]["BHD"];
            string CDF = (string)GetData["data"]["CDF"];
            string PYG = (string)GetData["data"]["PYG"];
            string UGX = (string)GetData["data"]["UGX"];
            string SVC = (string)GetData["data"]["SVC"];
            string TTD = (string)GetData["data"]["TTD"];
            string AFN = (string)GetData["data"]["AFN"];
            string NPR = (string)GetData["data"]["NPR"];
            string HNL = (string)GetData["data"]["HNL"];
            string BIH = (string)GetData["data"]["BIH"];
            string BND = (string)GetData["data"]["BND"];
            string ISK = (string)GetData["data"]["NOK"];
            string KHR = (string)GetData["data"]["ISK"];
            string GEL = (string)GetData["data"]["GEL"];
            string MZN = (string)GetData["data"]["MZN"];
            string BWP = (string)GetData["data"]["BWP"];
            string PGK = (string)GetData["data"]["PGK"];
            string JMD = (string)GetData["data"]["JMD"];
            string XAF = (string)GetData["data"]["XAF"];
            string NAD = (string)GetData["data"]["NAD"];
            string ALL = (string)GetData["data"]["ALL"];
            string SSP = (string)GetData["data"]["SSP"];
            string MUR = (string)GetData["data"]["MUR"];
            string MNT = (string)GetData["data"]["MNT"];
            string NIO = (string)GetData["data"]["NIO"];
            string LAK = (string)GetData["data"]["LAK"];
            string MKD = (string)GetData["data"]["MKD"];
            string MGA = (string)GetData["data"]["MGA"];
            string XPF = (string)GetData["data"]["XPF"];
            string TJS = (string)GetData["data"]["TJS"];
            string HTG = (string)GetData["data"]["HTG"];
            string BSD = (string)GetData["data"]["BSD"];
            string MDL = (string)GetData["data"]["MDL"];
            string RWF = (string)GetData["data"]["RWF"];
            string KGS = (string)GetData["data"]["KGS"];
            string GNF = (string)GetData["data"]["GNF"];
            string SRD = (string)GetData["data"]["SRD"];
            string SLL = (string)GetData["data"]["SLL"];
            string XOF = (string)GetData["data"]["XOF"];
            string MWK = (string)GetData["data"]["MWK"];
            string FJD = (string)GetData["data"]["FJD"];
            string ERN = (string)GetData["data"]["ERN"];
            string SZL = (string)GetData["data"]["SZL"];
            string GYD = (string)GetData["data"]["GYD"];
            string BIF = (string)GetData["data"]["BIF"];
            string KYD = (string)GetData["data"]["KYD"];
            string MVR = (string)GetData["data"]["MVR"];
            string LSL = (string)GetData["data"]["LSL"];
            string LRD = (string)GetData["data"]["LRD"];
            string CVE = (string)GetData["data"]["CVE"];
            string DJF = (string)GetData["data"]["DJF"];
            string SCR = (string)GetData["data"]["SCR"];
            string SOS = (string)GetData["data"]["SOS"];
            string GMD = (string)GetData["data"]["GMD"];
            string KMF = (string)GetData["data"]["KMF"];
            string STD = (string)GetData["data"]["STD"];
            string XRP = (string)GetData["data"]["XRP"];
            string AUD = (string)GetData["data"]["AUD"];
            string BGN = (string)GetData["data"]["BGN"];
            string BTC = (string)GetData["data"]["BTC"];
            string JOD = (string)GetData["data"]["JOD"];
            string GBP = (string)GetData["data"]["GBP"];
            string ETH = (string)GetData["data"]["ETH"];
            string EUR = (string)GetData["data"]["EUR"];
            string LTC = (string)GetData["data"]["LTC"];
            string NZD = (string)GetData["data"]["NZD"];*/
        }
    }
}
