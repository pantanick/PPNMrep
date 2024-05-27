using System;
using System.Collections.Generic;
using static System.Console;

public static class main{
	static vector harmonic_osci(double x, vector y){
		vector z = new vector(y.size);
		z[0]=y[1];
		z[1]=-y[0];
		return z;
	}
	static vector osci_friction(double x, vector y){
		double b=0.25;
		double c=5.0;
		vector z = new vector(y.size);
		z[0]=y[1];
		z[1]=-b*y[1]-c*System.Math.Sin(y[0]);
		return z;
	}//oscillation
	static void Main(){
		vector ystart = new vector(1.0,0.0);
		var result_harmonic = ode.driver(harmonic_osci, (0.0, 10.0), ystart);
		var (xlist_harmonic, ylist_harmonic)= result_harmonic;
		using (var writer = new System.IO.StreamWriter("out_harmonic.txt")){
			for (int i = 0; i < xlist_harmonic.Count; i++){
				writer.Write($"{xlist_harmonic[i]} ");
				ylist_harmonic[i].print("", "{0:g10} ", writer);
			}
		}
		var result_friction = ode.driver(osci_friction, (0.0, 10.0), ystart);
		var (xlist_friction,ylist_friction)=result_friction;
		for(int i=0; i<xlist_friction.Count;i++){
			Console.Write($"{xlist_friction[i]}	");
			ylist_friction[i].print();
		}
	
	}//Main

}//class main
