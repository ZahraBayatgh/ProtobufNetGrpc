using System;
using System.Runtime.Serialization;

namespace Domain.Models
{
    [DataContract]
    public class DecrementRequest
    {
        [Obsolete("Not allowed to use this for instantiation")]
        public DecrementRequest()
        {

        }
        public DecrementRequest(int dec)
        {
            Dec = dec;
        }

        [DataMember(Order = 1)]
        public int Dec { get; set; }
    }
}
