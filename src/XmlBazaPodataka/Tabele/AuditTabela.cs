using System;
using System.Runtime.Serialization;

namespace XmlBazaPodataka.Tabele
{
    public enum MessageType { [EnumMember] Info, [EnumMember] Warning, [EnumMember] Error };

    [DataContract]
    public class AuditTabela
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime Timestamp { get; set; }

        [DataMember]
        public MessageType Message_Type { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
