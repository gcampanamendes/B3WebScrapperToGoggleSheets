using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace B3WebScrapperToGoggleSheets.Classes
{
    public class GoogleCloudServiceAccountInfo
    {
        private string jsonData;
        private string filePath;

        public string Type;
        public string ProjectId;
        public string PrivateKeyId;
        public string ClientEmail;
        public string ClientId;
        public string AuthenticationUrl;
        public string TokenUrl;
        public string UniverseDomain;

        public GoogleCloudServiceAccountInfo()
        {
        }
        public GoogleCloudServiceAccountInfo(string credentialFilePath)
        {
            filePath = credentialFilePath;
            Update();
        }

        public void Update(string credentialFilePath)
        {
            filePath = credentialFilePath;
            Update();
        }

        public void Update()
        {
            if (filePath != string.Empty)
            {
                jsonData = File.ReadAllText(filePath);

                dynamic jsonObject = JsonConvert.DeserializeObject(jsonData);

                Type = jsonObject?.type;
                ProjectId = jsonObject?.project_id;
                PrivateKeyId = jsonObject?.private_key_id;
                ClientEmail = jsonObject?.client_email;
                ClientId = jsonObject?.client_id;
                AuthenticationUrl = jsonObject?.auth_uri;
                TokenUrl = jsonObject?.token_uri;
                UniverseDomain = jsonObject?.universe_domain;
            }
            else
            {
                throw new ArgumentNullException(nameof(filePath));
            }
        }

        public string ConvertToJson()
        {
            return JsonConvert.SerializeObject(jsonData, Formatting.Indented);
        }
    }
}