using System.Xml.Serialization;

namespace RightTest.FinancesBL.DTOs;

[XmlRoot("ValCurs")]
public class ExternalCurrencyDto
{
    public ExternalCurrencyDto() { }

    [XmlAttribute("Date")]
    public string Date { get; set; }

    [XmlElement("Valute")]
    public List<ValuteDto> Valutes { get; set; } = [];
}
