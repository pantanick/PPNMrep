using System;
using static System.Math;

public static class integration{
	public static double integrate(
		Func<double,double> f, double a, double b,double δ=0.001, double ε=0.001, double f2=double.NaN, double f3=double.NaN
		){
	double h=b-a;
	if(double.IsNaN(f2)){
		f2=f(a+2*h/6); f3=f(a+4*h/6);
	}
	double f1=f(a+h/6), f4=f(a+5*h/6);
	double Q = (2*f1+f2+f3+2*f4)/6*(b-a); // higher order rule
	double q = (  f1+f2+f3+  f4)/4*(b-a); // lower order rule
	double err = System.Math.Abs(Q-q);
	if (err <= δ + ε * System.Math.Abs(Q)){
		return Q;
	}
	else {
		return  integrate(f, a, (a + b) / 2, δ / System.Math.Sqrt(2), ε, f1, f2) +
			integrate(f, (a + b) / 2, b, δ / System.Math.Sqrt(2), ε, f3, f4);
	}

	}//integrate

	public static double erf(double z){
		if(z<0){
			return -erf(-z);
		}
		else if(0<=z && z<=1){
			Func<double,double> erfunc1 = x => System.Math.Exp(-x*x);
			double erfint1=integrate(erfunc1,0,z);
			double erfres1= 2.00/(System.Math.Sqrt(System.Math.PI))*erfint1;
		       	return erfres1 ; 
		}
		else {
			Func<double, double> erfunc2 = t => Exp(-Pow(z + (1 - t) / t, 2)) / Pow(t,2);
			double erfint2=integrate(erfunc2,0,1);
			double erfres2= 1-(2.00/System.Math.Sqrt(System.Math.PI))*erfint2;
			return erfres2;
		}
	}//erf
}//class integration
