using System.Runtime.Serialization;

namespace Domain.Models
{
    [DataContract]
    public class IncrementRequest
    {
        [DataMember(Order = 1)]
        public int Inc { get; set; }
    }
}
