using System.Xml.Serialization;

namespace RightTest.FinancesBL.DTOs;

public class ValuteDto
{
    public ValuteDto() { }

    [XmlAttribute("ID")]
    public string Id { get; set; }

    public int NumCode { get; set; }

    public string CharCode { get; set; }

    public int Nominal { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }

    public string VunitRate { get; set; }
}
