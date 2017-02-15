using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace GoogleReCaptchaV2Validator
{
    [DataContract]
    public class ReCaptchaResponse
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }

        
        public DateTime? ChallengeTimeStamp { get; set; }

        [DataMember(Name = "challenge_ts")]
        private string ChallengeTimeStampSerialized { get; set; }

        [DataMember(Name = "hostname")]
        public string Hostname { get; set; }

        [DataMember(Name = "error-codes")]
        public List<string> ErrorCodes { get; set; }

        [OnDeserialized]
        void OnDeserializing(StreamingContext context)
        {
            if (string.IsNullOrEmpty(ChallengeTimeStampSerialized))
            {
                ChallengeTimeStamp = null;
            }
            else
            {
                ChallengeTimeStamp = DateTime.ParseExact(ChallengeTimeStampSerialized, "yyyy-MM-ddTHH:mm:ssZ",
                    CultureInfo.InvariantCulture);
            }
        }

    }
}
