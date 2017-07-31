using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CodeFirst.Entities
{
    public class ConnectProperty
    {
        [DataMember]
        [Key]
        public Guid Id { get; set; }
        [DataMember]
        public string ConnectionString { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public bool IsUsing { get; set; }
    }
}
