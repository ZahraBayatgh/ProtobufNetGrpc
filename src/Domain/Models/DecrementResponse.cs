using System;
using System.Runtime.Serialization;

namespace Domain.Models
{
    [DataContract]
    public class DecrementResponse
    {
        [Obsolete("Not allowed to use this for instantiation")]
        public DecrementResponse()
        {

        }
        public DecrementResponse(int result)
        {
            Result = result;
        }

        [DataMember(Order = 1)]
        public int Result { get; set; }
    }
}
