using System.Runtime.Serialization;

namespace Domain.Models
{
    [DataContract]
    public class IncrementResponce
    {
        [DataMember(Order = 1)]
        public int Result { get; set; }
    }
}
