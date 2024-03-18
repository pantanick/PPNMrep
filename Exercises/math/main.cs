using System;
using static System.Console;
using static System.Math;
static class main{
	static double x=1.0;
	static double Sin(double x) {return 0;}
	static string hello = $"hello, x={x}\n";
	static double times2(double y){
		double x=7;
		WriteLine(x);
		return y*2;
		}
	static int Main(){
		Write($"{hello}x={x} Sin(x)={System.Math.Sin(x)}\n");
		double prod=1.0;
		for(int x=1;x<10;x+=1) {
			Write($"fgamma({x})={sfuns.fgamma(x)}  {x-1}!={prod} \n");
			prod*=x; 
			}			
	return 0;
	}
}
