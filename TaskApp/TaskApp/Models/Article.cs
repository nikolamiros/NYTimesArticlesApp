using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TaskApp.Models
{
    public class Article
    {
        [DataContract]
        public class Headline
        {
            [DataMember(Name = "main")]
            public string Main { get; set; }
        }

        [DataContract]
        public class Document
        {
            [DataMember(Name = "web_url")]
            public string WebUrl { get; set; }

            [DataMember(Name = "lead_paragraph")]
            public string LeadParagraph { get; set; }

            [DataMember(Name = "headline")]
            public Headline Headline { get; set; }

            [DataMember(Name = "pub_date")]
            public DateTime PublishDate { get; set; }
        }

        [DataContract]
        public class Response
        {
            [DataMember(Name = "docs")]
            public IList<Document> Documents { get; set; }
        }

        [DataContract]
        public class Example
        {
            [DataMember(Name = "status")]
            public string Status { get; set; }

            [DataMember(Name = "response")]
            public Response Response { get; set; }
        }

    }
}
