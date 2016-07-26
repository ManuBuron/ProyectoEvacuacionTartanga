using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("DialogosCollection")]
public class PersonasContainer 
{
	[XmlArray("personas"),XmlArrayItem("mensajeP")]
	public List<EstructuraPersona> personas = new List<EstructuraPersona>();
}
	
[XmlRoot("DialogosCollection")]
public class PersonasDialogosContainer 
{
	[XmlArray("personasDialogo"),XmlArrayItem("mensajePD")]
	public List<EstructuraPersonaDialogo> personasD = new List<EstructuraPersonaDialogo>();
}

[XmlRoot("DialogosCollection")]
public class ObjetosContainer 
{
	[XmlArray("objeto"),XmlArrayItem("mensajeOb")]
	public List<EstructuraObjeto> objetos = new List<EstructuraObjeto>();
}

