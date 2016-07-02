using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace DuckyData1._0._0Alpha.Factory.WebAPI.Gracenote
{
    public class GNQueryBuilder
    {
        public string CLIENT { set; get; }
        public string USER { set; get; }


        public GNQueryBuilder() {
            this.CLIENT = "415878569-69A575A91B033483D8617EC72A4A9AF4";
            this.USER = "37813720161300473-A960B080450AAB53C5FF99EC77DF3D38";
        }

        public string ALBUM_SEARCH(string artist, string album_title, string track_title) {

            XDocument document = new XDocument(
                new XDeclaration("1.0","utf-8","yes"),

                new XElement("QUERIES",
                    new XElement("AUTH",
                        new XElement("CLIENT",this.CLIENT),
                        new XElement("USER",this.USER)
                        ),
                        new XElement("LANG","eng"),
                        new XElement("QUERY",new XAttribute("CMD","ALBUM_SEARCH"),
                            new XElement("TEXT",new XAttribute("TYPE","ARTIST"),artist),
                            new XElement("TEXT",new XAttribute("TYPE","ALBUM_TITLE"),album_title),
                            new XElement("TEXT",new XAttribute("TYPE","TRACK_TITLE"),track_title),
                            new XElement("OPTION",
                                new XElement("PARAMETER","SELECT_EXTENDED"),
                                new XElement("VALUE","COVER,REVIEW,ARTIST_BIOGRAPHY,ARTIST_IMAGE,ARTIST_OET,MOOD,TEMPO")
                                )
                    ))
                );
            string xml = document.ToString();
            return xml;
        }
    }
}