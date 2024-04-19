using System;
using static System.Console;
using static System.Math;

class main{
	public static bool approx
	(double a, double b, double acc=1e-9, double eps=1e-9){
		if (Abs(b-a) <= acc) return true;
		if (Abs(b-a) <= Max(Abs(a),Abs(b))*eps) return true;
		return false;
	}

	static int Main(string[] args){

		int i=1;
		while (i+1 > i) 
		{
			i++;
		}
		WriteLine($"Part 1 : Maximum/minimum representable integers");
		Write($"My max int = {i} (calculated) , given by computer = {int.MaxValue}\n");
		i=0;
		while (i-1<i)
		{
			i--;
		}
		Write($"My min int = {i} (calculated) , given by computer = {int.MinValue}\n \n");
		double x=1;
		while (1+x !=1)
		{
		x /=2;
		}
		x *=2;
		float y=1F;
		while ((float)(1F+y) !=1F)
		{
		y/=2F;
		}
		y *=2F;
		WriteLine($"Part 2 : Machine epsilon");
		Write($"Double machine e = {x} (calculated) , given by computer = {Pow(2,-52)}\n");
		Write($"Float machine e = {y} (calculated) , given by computer = {Pow(2,-23)}\n \n");
		double epsilon = Pow(2,-52);
		double tiny=epsilon/2;
		double a=1+tiny+tiny;
		double b=tiny+tiny+1;
		WriteLine($"Part 3 : Comparing a and b");
		Write($"a==b ? {a==b}\n");
		Write($"a>b ? {a>b}\n");
		Write($"a<b ? {a<b}\n");
		Write($"\nDue to the limitations of floating-point precision, a is slightly less than b.\n"+
		"Because of the way they are calculated, b is slightly more accurate than a because adding\n"+
		"a small number to a relatively large one can result in fewer precision issues compared to adding"+
		"\na small number to 1. In the second case, the small number might get lost in the representation"+
		" of 1.\n \n");
		double d1=+0.1+0.1+0.1+0.1+0.1+0.1+0.1+0.1;
		double d2=8*0.1;
		WriteLine($"Part 4 : Comparing d1 and d2");
		WriteLine($"d1={d1:e15}");
		WriteLine($"d2={d2:e15}");
		WriteLine($"d1==d2 ? {d1==d2}");
		WriteLine($"\nComparing doubles: task");
		WriteLine($"d1={d1:e15}");
		WriteLine($"d2={d2:e15}");
		WriteLine($"d1~d2 ? {approx(d1,d2,2*epsilon)}");


	return 0;
	}
}
