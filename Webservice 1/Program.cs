using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections;
namespace ConsoleApplicatio2
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Driver> _responsedataforUser = new List<Driver>();

            string Origin = "24.887563,67.136250";

            string[] dest = new string[4] { "24.902516,67.075482", "24.886853, 67.124866", "24.879641, 67.108108", "24.890449, 67.070418" };
           

            for (int i = 0; i < dest.Length; i++)
            {
                GetDrivingDistanceInMiles(Origin, dest[i], _responsedataforUser);
            }



            int count = 0;
            foreach (var item in _responsedataforUser)
            {
                Driver d = item;
                Console.WriteLine(count + " = Distance  " + d.distance + " Duration " + d.duration);
                count++;

            }

            Console.ReadKey();


        }


        public static void GetDrivingDistanceInMiles(string origin, string destination, List<Driver> Responses)
        {

            string url = @"http://maps.googleapis.com/maps/api/distancematrix/xml?origins=" +
              origin + "&destinations=" + destination +
              "&mode=driving&sensor=false&language=en-EN&units=imperial";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();


            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(responsereader);


            if (xmldoc.GetElementsByTagName("status")[0].ChildNodes[0].InnerText == "OK")
            {
                XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                XmlNodeList duration = xmldoc.GetElementsByTagName("duration");

                string Duration = duration[0].ChildNodes[1].InnerText.ToString();
                string _distance = distance[0].ChildNodes[1].InnerText.ToString();

                Responses.Add(new Driver(Duration, _distance));
                
            }

           
        }

    }

    class Driver
    {
        public string duration;
        public string distance;
        public Driver(string du, string dis)
        {
            this.distance = dis;
            this.duration = du;
        }
    }



}
