using System;
using System.IO;
using static System.Console;
using static System.Math;
using System.Collections.Generic;

class main {
    static void Main() {
        var pointstream = new System.IO.StreamWriter("out_sinpoints.txt");
	int n=10;
	int m=2;
        vector x = new vector(n);
        vector y = new vector(n);
	matrix A = new matrix(n,m);
        for (int i = 0; i < n; i++) {
            x[i] = i;
            y[i] = System.Math.Sin(i);
	    A[i,0]=x[i];
	    A[i,1]=y[i];
	    pointstream.WriteLine($"{x[i]} {y[i]}");

        }
	pointstream.Close();
	System.Console.WriteLine($"\n Matrix A (x sinx)= ");
	A.print();
	System.Console.WriteLine($"\n Vector x = ");
	x.print();
	System.Console.WriteLine($"\n Vector y (sinx)= ");
	y.print();
        QuadraticSpline spline = new QuadraticSpline(x, y);
//test
	for(double i=0;i<n;i++){
		System.Console.WriteLine($"\n Spline value at x={i} : {spline.interpolate(i)} ");

	}
        for (double z = 0; z < n - 1; z += 0.1) {
		Error.WriteLine($"{z}	{spline.interpolate(z)}	{spline.derivative(z)}	{spline.integral(z)}");
        }
   	}//Main
}//main

public class QuadraticSpline {
    private vector x, y, b, c;

    public QuadraticSpline(vector xs, vector ys) {
        x = xs;
        y = ys;
        int n = x.size;
        b = new vector(n - 1);
        c = new vector(n - 1);
        vector dx = new vector(n - 1);
        vector p = new vector(n - 1);
        for (int i = 0; i < n - 1; i++){
            dx[i] = x[i + 1] - x[i];
            p[i] = (y[i + 1] - y[i]) / dx[i];
        }
	//forward elimination
	for(int i=0;i<n-2;i++){
		c[i+1]=(p[i+1]- p[i]- c[i]*dx[i])/dx[i+1];
	}
	c[n-2]/=2;
	//back substitution
	for(int i=n-3; i>=0;i--){
		c[i]=(p[i+1] - p[i] - c[i+1]*dx[i+1])/dx[i];
		b[i] = p[i] - c[i]*dx[i];
	}
	b[n-2]=p[n-2]-c[n-2]*dx[n-2];
    }//QuadraticSpline

    public double interpolate(double z) {
        int i = binsearch(x, z);
        double h = z - x[i];
        return y[i] + h * (b[i] + h * c[i]);
    }//interpolate

    public double derivative(double z) {
        int i = binsearch(x, z);
        double h = z - x[i];
        return b[i] + 2 * h * c[i];
    }//derivative

    public double integral(double z) {
        int i = binsearch(x, z);
        double sum = 0;
        for (int j = 0; j < i; j++) {
            double h = x[j + 1] - x[j];
            sum += y[j] * h + 0.5 * b[j] * h * h + (1.0 / 3.0) * c[j] * h * h * h;
        }
        double h_last = z - x[i];
        sum += y[i] * h_last + 0.5 * b[i] * h_last * h_last + (1.0 / 3.0) * c[i] * h_last * h_last * h_last;
        return sum;
    }//integral

    private int binsearch(vector x, double z){
        int i = 0, j = x.size - 1;
        while (j - i > 1) {
            int mid = (i + j) / 2;
            if (z > x[mid]) i = mid; else j = mid;
        }
        return i;
    }//binsearch
}//class QuadraticSpline
