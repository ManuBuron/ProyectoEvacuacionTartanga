using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections;

[XmlRoot("MensajesMenu")]
public class MensajeIdiomaContainer{
	[XmlArray("Menu"),XmlArrayItem("Mensaje")]
	public List<ClaseMensajeIdioma> mensaje = new List<ClaseMensajeIdioma>();
}
