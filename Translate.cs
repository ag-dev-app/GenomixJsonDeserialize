using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Translation.V2;


namespace GenomixJsonDeserialize
{
    public class Translate
    {
        public string TranslateText(string txt)
        {
            TranslationClient client = TranslationClient.CreateFromApiKey("AIzaSyBcA2JsKYHygSvNFL1_FwrcG1dRasqm7eg");
            var response = client.TranslateText(
                text: txt,
                targetLanguage: "fr",  // Français
                sourceLanguage: "en");  // English
            return response.TranslatedText;
        }
    }
}
