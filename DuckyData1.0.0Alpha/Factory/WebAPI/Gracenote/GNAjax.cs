using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace DuckyData1._0._0Alpha.Factory.WebAPI.Gracenote
{
    public class GNAjax
    {
        public string gracenoteUri { get; set; }

        public GNAjax() {
            this.gracenoteUri ="https://c415878569.web.cddbp.net/webapi/xml/1.0/";
        }

        public string call(string requestXml) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.gracenoteUri);
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes,0,bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if(response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
            return "";
        }

    }
}