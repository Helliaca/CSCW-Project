using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PriorityQueue {

	private int _length;
	private Mapping[] queue;
	private MappingContainer _source;
	private int sourceIndex;

	public MappingContainer source {
		get {return _source;}
		set {
			sourceIndex = 0;
			_source = value;
		}
	}
	public int length {
		get {return _length;}
		set {_length = value;}
	}

	public Mapping getSample() {
		sort();
		int x = UnityEngine.Random.Range(0,4);
		if(x==0) return queue[queue.Length/2];
		else if(x==1) return queue[queue.Length-1];
		return queue[0];
	}
		
	void Initialize (MappingContainer source) {
		this.source = source;
		sourceIndex = 0;
		rePopulateQueue();
	}

	void sort() {
		Array.Sort(queue, delegate(Mapping x, Mapping y) { return x.priority.CompareTo(y.priority); });
	}

	void rePopulateQueue() {
		queue = new Mapping[length];
		for(int i=0; i<length; i++) {
			queue[i] = getNewMapping();
		}
	}

	Mapping getNewMapping() {
		if(sourceIndex>=source.data.Count) {
			Globals.DevConsole.print("Not enough Mappings loaded. Recycling.");
			sourceIndex = 0;
			return getNewMapping();
		}
		return source.data[sourceIndex];
	}
}
