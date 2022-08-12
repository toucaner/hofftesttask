using System.Xml.Serialization;

namespace HoffTestTask.Features.Services.Exchange.Models;

[XmlRoot(ElementName = "ValCurs")]
public class CbrResponse
{
    [XmlElement("Valute")] 
    public Valute[] ExchangeRates { get; set; }
}