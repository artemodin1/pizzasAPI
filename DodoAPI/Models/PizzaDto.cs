using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DodoAPI.Models
{
    [DataContract]
    public class PizzaDto
    {
        [DataMember(Name = "Title")]
        public string Title { get; set; }
        [DataMember(Name = "Price")]
        public ushort? Price { get; set; }
        [DataMember(Name = "Picture")]
        public string Picture { get; set; }
        [DataMember(Name = "Description")]
        public string Description { get; set; }
        [DataMember(Name = "Active")]
        public bool? Active { get; set; }
        [DataMember(Name = "New")]
        public bool? New { get; set; }
        [DataMember(Name = "Ingredients")]
        public List<string> Ingredients { get; set; }
        [DataMember(Name = "Dough")]
        public string Dough { get; set; }
        [DataMember(Name = "Additionally")]
        public string Additionally { get; set; }
    }
}
