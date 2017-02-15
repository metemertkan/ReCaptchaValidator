using System;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Serialization.Json;
using System.Text;

namespace GoogleReCaptchaV2Validator
{
    public class ReCaptchaValidator
    {
        private readonly string _privateKey;
        private readonly string _googleVerificationEndpoint;


        public ReCaptchaValidator(string privateKey)
        {
            if (string.IsNullOrEmpty(privateKey))
                throw new ArgumentNullException(nameof(privateKey));

            _privateKey = privateKey;
            _googleVerificationEndpoint = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
        }

        public ReCaptchaResponse Validate(string response, string remoteIp)
        {
            if (string.IsNullOrEmpty(response))
                throw new ArgumentNullException(nameof(response));

            var endPointUrl = string.Format(_googleVerificationEndpoint, _privateKey, response);
            if (!string.IsNullOrEmpty(remoteIp))
            {
                endPointUrl = string.Concat(endPointUrl, "&remoteip=", remoteIp);
            }

            using (var client = new WebClient())
            {

                var verifyResult = client.DownloadString(endPointUrl);
                var deserializer = new DataContractJsonSerializer(typeof(ReCaptchaResponse));
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(verifyResult)))
                {
                    return deserializer.ReadObject(ms) as ReCaptchaResponse;
                }
            }
        }

        public ReCaptchaResponse Validate(string response)
        {
            return Validate(response, null);
        }

    }

}
