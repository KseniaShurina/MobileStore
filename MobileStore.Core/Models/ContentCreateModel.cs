using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileStore.Core.Models
{
    public class ContentCreateModel
    {
        private string ContentType { get; } = null!;

        public string Name { get; }

        public Stream Data { get; }

        public ContentCreateModel(string contentType, string name, Stream data)
        {
            ContentType = contentType;
            Name = name;
            Data = data;
        }
    }
}
