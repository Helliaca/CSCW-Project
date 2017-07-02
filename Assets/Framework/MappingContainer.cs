using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class MappingContainer {
	public List<Mapping> data;

	public void loadFromFile(string path) {
		this.data = new List<Mapping>();
		string text = File.ReadAllText(path);

		List<string> cells = new List<string>();

		string start_tag = "<Cell>";
		string end_tag = "</Cell>";
		int currentIndex = 0;
		while(currentIndex<text.Length) {
			int startIndex = text.IndexOf(start_tag, currentIndex) + start_tag.Length;
			int endIndex = text.IndexOf(end_tag, currentIndex);
			if(startIndex<0 || endIndex<0) break;
			cells.Add(text.Substring(startIndex, endIndex - startIndex));
			currentIndex = endIndex + end_tag.Length;
		}

		/*
		 * REGEX: <entity1(.?*)resource="(.?*)"/>
		 * REGEX: <entity2(.?*)resource="(.?*)"/>
		 * MEASURE: <measure(.?*)>(.?*)</measure>
		 * RELEATION: <relation(.?*)>(.?*)</relation>
		*/
		Regex reg1 = new Regex(@"<entity1(.*?)resource=""(.*?)""/>");
		Regex reg2 = new Regex(@"<entity2(.*?)resource=""(.*?)""/>");
		Regex reg3 = new Regex(@"<measure(.*?)>(.*?)</measure>");
		Regex reg4 = new Regex(@"<relation(.*?)>(.*?)</relation>");
		foreach(string cell in cells) {
			Mapping map = new Mapping();
			map.entity1 = reg1.Match(cell).Groups[2].ToString();
			map.entity2 = reg2.Match(cell).Groups[2].ToString();
			map.measure = float.Parse(reg3.Match(cell).Groups[2].ToString());
			map.relation = reg4.Match(cell).Groups[2].ToString();
			data.Add(map);
		}
		Debug.Log(data.Count);
	}
}
