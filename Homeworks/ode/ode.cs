using System;
using System.Collections.Generic;
using static System.Math;

public static class ode{
	public static (vector,vector) rkstep12(Func<double,vector,vector> f, double x, vector y, double h){
		vector k0=f(x,y);//lower (euler)
		vector k1=f(x+h/2,y+k0*h/2);//higher (midpoint)
		vector yh=y+k1*h;
		vector dy=(k1-k0)*h;//error estimate
		return (yh,dy);
	}//rkstep12

	public static (List<double>,List<vector>) driver(
		Func<double,vector,vector>f,
		(double,double) interval,
		vector ystart,
		double h=0.125,
		double abs_acc=0.01,
		double rel_acc=0.01
		){
		var(a,b)=interval;
		double x=a;
		vector y=ystart.copy();
		var xlist=new List<double>();
		var ylist=new List<vector>();
		xlist.Add(x);
		ylist.Add(y);
		do{
			if (x>=b) return (xlist,ylist); //done
			if (x+h>b) h=b-x; //last step should end at b
			var (yh,dy) = rkstep12(f,x,y,h);
			double tol = (abs_acc+rel_acc*yh.norm()) * Sqrt(h/(b-a));
			double err = dy.norm();
			if(err<=tol){ // accept step
				x+=h; 
				y=yh;
				xlist.Add(x);
				ylist.Add(y);
			}
			h *= Min( Pow(tol/err,0.25)*0.95 , 2); // readjust stepsize
			}while(true);
	}//driver
}//class ode
