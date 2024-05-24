using System;
using System.IO;
using static System.Console;
using static System.Math;
using System.Collections.Generic;

public class main{
	public static void Main(){
		int n=10;
		int m=2;
		vector x = new vector (n);
		vector y = new vector (n);
		matrix A = new matrix(n,m);
		var pointstream = new System.IO.StreamWriter("out_cospoints.txt");
		for(int i=0; i<n; i++){
			x[i]=i;
			y[i]=System.Math.Cos(i);
			A[i,0]=x[i];
			A[i,1]=y[i];
			pointstream.WriteLine($"{x[i]}	{y[i]}");
		}
		pointstream.Close();
		System.Console.WriteLine($"\nMatrix A (x cosx)= ");
		A.print();
		System.Console.WriteLine($"\nVector x = ");
		x.print();
		System.Console.WriteLine($"\nVector y (cosx) =");
		y.print();
		//Checking if the spline works
		double ztest=5.5;
		double result=linterp(x,y,ztest);
		System.Console.WriteLine($"Spline value at {ztest} : {result}");
		System.Console.WriteLine($"Theoretical cos value at {ztest} : {System.Math.Cos(ztest)}");
		double result_int=linterpInt(x,y,ztest);
		System.Console.WriteLine($"Integral value from x[0]=0 to {ztest} : {result_int}");
		//Extracting plotdata. Resolution: *10
		vector xplot = new vector ((n-1)*10+1);
		vector yplot = new vector ((n-1)*10+1);
		vector linInt = new vector ((n-1)*10+1);
		xplot[0]=x[0];
		yplot[0]=y[0];
		for(int i=1; i<(n-1)*10+1; i++){
			xplot[i]=i*0.1;
		        yplot[i]=linterp(x,y,xplot[i]);
			linInt[i]=linterpInt(x,y,xplot[i]);
		}
		//Writing data for plot - out_linplotdata.txt
		for(int i=0; i<(n-1)*10+1; i++){
			Error.WriteLine($"{xplot[i]}	{yplot[i]}	{linInt[i]}");
		}
	}//Main

public static double linterp(double[] x, double[] y, double z){
	        int i=binsearch(x,z);
		double dx=x[i+1]-x[i]; if(!(dx>0)) throw new Exception("uups...");
		double dy=y[i+1]-y[i];
		return y[i]+dy/dx*(z-x[i]);
        }//linterp

public static int binsearch(double[] x, double z){ 
		if( z<x[0] || z>x[x.Length-1] ) throw new Exception($"binsearch: bad z. z={z} , x.Length={x.Length}, x[0]={x[0]}");
		int i=0, j=x.Length-1;
		while(j-i>1){
			int mid=(i+j)/2;
			if(z>x[mid]) i=mid; else j=mid;
		}
		return i;
	}//binsearch


public static double linterpInt(double[] x, double[] y, double z){
		int i = binsearch(x, z);
		double res = 0;
		for(int j = 0; j < i; j++){
			res += (x[j+1]-x[j])*(y[j+1]+y[j])/2;
		}
		res += (z-x[i])*(linterp(x,y,z) + y[i])/2;
		return res;
	}//linterpInt


}//main
