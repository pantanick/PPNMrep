using System;
using static System.Console;
using static System.Math;

static class main {
	static void Main(){
		WriteLine($"sqrt(-1)= {cmath.sqrt(new complex(-1,0))}, i={cmath.I}");
		WriteLine($"sqrt(-1)=i ? {cmath.sqrt(new complex(-1,0)).approx(cmath.I)}\n");
		WriteLine($"sqrt(i)= {cmath.sqrt(cmath.I)}, from euler's formula: sqrt(i)=1/Sqrt(2)+i/Sqrt(2)");
		WriteLine($"sqrt(i)=1/Sqrt(2)+i/Sqrt(2) ? {cmath.sqrt(cmath.I).approx(new complex(Sqrt(0.5),Sqrt(0.5)))}\n");
		WriteLine($"ln(i) = {cmath.log(cmath.I)} ,  from euler's formula: ln(i) = i*π/2");
		WriteLine($"ln(i)=i*π/2 ? {cmath.log(cmath.I).approx(cmath.I*PI/2)}\n");
		WriteLine($"i^i = {cmath.pow(cmath.I,cmath.I)} ,  from euler's formula: i^i = e^(-π/2)");
		WriteLine($"i^i = e^(-π/2) ? {cmath.pow(cmath.I,cmath.I).approx(cmath.exp(-PI/2))}\n");
		WriteLine($"e^iπ = {cmath.exp(cmath.I*PI)}\n");
		WriteLine($"sin(iπ) = {cmath.sin(cmath.I*PI)}\n");
		WriteLine($"e^i = {cmath.exp(cmath.I)}\n");

	}//Main
}//main
