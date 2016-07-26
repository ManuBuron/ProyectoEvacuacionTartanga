using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections;

[XmlRoot("MensajesError")]
public class MensajesRiesgos
{
	[XmlArray("Riesgo"),XmlArrayItem("Error")]
	public List<ClaseMensajesRiesgo> errors = new List<ClaseMensajesRiesgo>();
}

