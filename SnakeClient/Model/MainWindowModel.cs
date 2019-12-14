using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;

namespace Snake
{
    class MainWindowModel
    {
        public void PostDirection(RequestBody requestBody, string serverAdress)
        {
            byte[] requestByteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requestBody));
            WebRequest request = WebRequest.Create(serverAdress + "/direction");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = requestByteArray.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(requestByteArray, 0, requestByteArray.Length);
            requestStream.Close();
        }

        public ResponseBody GetState(string serverAdress)
        {
            StreamReader streamReader =
                new StreamReader(WebRequest.Create(serverAdress + "/gameboard").GetResponse().GetResponseStream());
            ResponseBody responseBody =
                JsonConvert.DeserializeObject<ResponseBody>(streamReader.ReadToEnd());
            streamReader.Close();
            return responseBody;
        }
    }
}
