using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class API : MonoBehaviour {

	string sparql;
	string url = "http://www.ontobee.org/sparql?query=";
	WWW www;
	string response = System.IO.File.ReadAllText("response.txt");

	void Start () {
		/*
		sparql = WWW.EscapeURL("prefix rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>\nprefix owl: <http://www.w3.org/2002/07/owl#>\nSELECT distinct ?p ?o ?predicate_label ?object_label\nWHERE {\n   <http://purl.obolibrary.org/obo/FLOPO_0001906> ?p ?o .\n   OPTIONAL { ?p rdfs:label ?predicate_label . }\n   OPTIONAL { ?o rdfs:label ?object_label . }\n}");
		url = url + sparql;
		print(url);
		var headers = new Dictionary<string, string>();
		WWWForm form = new WWWForm();
		form.
		//headers.Add("Content-Type", "application/sparql-results+json");
		headers.Add("Accept", "application/sparql-results+json");
		headers.Add("Content-Type", "application/sparql-results+json");
		www = new WWW(url, null, headers);
		StartCoroutine(WaitForRequest(www));
		*/

		MappingContainer mc = new MappingContainer();
		mc.loadFromFile(@"Assets/xml_sample.rdf");
	}

	void Update () {
		
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.text);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}
	}
}
