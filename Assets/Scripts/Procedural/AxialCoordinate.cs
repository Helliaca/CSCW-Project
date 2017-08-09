using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxialCoordinate {
	/*
	 * X: "left to right"
	 * Y: "top left to bottom right"
	 * Z: "bottom left to top right
	 */ 
	public enum DIR {X, Y, Z, _X, _Y, _Z}; //Underscore represents "minus"/negative
	public int r, q;
	public float y; //Height from ground. (World coordinate y)

	public AxialCoordinate(int r, int q, float y) {
		this.q = q;
		this.r = r;
		this.y = y;
	}

	public AxialCoordinate next(DIR direction) {
		switch(direction) {
		case DIR.X : {return new AxialCoordinate(r+1, q, y);}
		case DIR.Y : {return new AxialCoordinate(r, q+1, y);}
		case DIR.Z : {return new AxialCoordinate(r+1, q-1, y);}
		case DIR._X : {return new AxialCoordinate(r-1, q, y);}
		case DIR._Y : {return new AxialCoordinate(r, q-1, y);}
		case DIR._Z : {return new AxialCoordinate(r-1, q+1, y);}
		}
		return null;
	}

	public Vector2 toWorld(float height) {
		float width = Mathf.Sqrt(3)/2 * height;
		return new Vector2(r*width+q*width/2, q*height*0.75f);
	}
}