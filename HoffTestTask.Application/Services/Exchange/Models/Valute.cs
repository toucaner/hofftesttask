using System.Xml.Serialization;

namespace HoffTestTask.Features.Services.Exchange.Models;

public class Valute
{
    [XmlAttribute("ID")]
    public string Code { get; set; }
    [XmlElement("Value")]
    public string Value { get; set; }
}