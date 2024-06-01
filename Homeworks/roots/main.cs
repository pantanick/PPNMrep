using System;
public class main {
	public static Func<vector,vector>rosenbrock_grad = v =>
		{
			double x=v[0];
			double y=v[1];
			return new vector(2 * (x - 1) + 400 * x * (x * x - y),200 * (y - x * x));
		};
	public static Func<vector,vector>himmelblau_grad = v =>
		{
			double x=v[0];
			double y=v[1];
			return new vector(4 * x * (x * x + y - 11)+2 * (x + y * y-7),2*(x*x+y-11)+4*y*(x+y*y-7));
		};
	public static void Main(){
		vector vstart=new vector (0.1,0.1);
		vector[] vstart2list=new vector[]
		{
			new vector (2.0,1.5),
			new vector(-3.0,2.5),
			new vector(-3.0,-3.5),
			new vector (2.5,-2.0)
		};
		Console.WriteLine($"Rosenbrock's valley function: f(x,y)=(1-x)^2 + 100(y-x^2)^2");
		Console.WriteLine($"Gradient = (2(x-1)+400x(x^2-y) , 200(y-x^2))");
		vector root_rosenbrock = newtonian.newton(rosenbrock_grad,vstart);
		Console.Write($"Calculated root of gradient : ");
		root_rosenbrock.print();
		Console.Write($"from starting point : ");
		vstart.print();
		Console.Write($"\n");
		Console.WriteLine($"Himmelblau's function: f(x,y)=(x^2+y-11)^2 + (x+y^2-7)^2");
		Console.WriteLine($"Gradient = (4*x*(x*x+y-11)+2*(x+y*y-7),2(x*x+y-11)+4*y*(x+y*y-7))");
		foreach(vector vstart2 in vstart2list){
			vector root_himmelblau = newtonian.newton(himmelblau_grad,vstart2);
			Console.Write($"Calculated root of gradient : ");
			root_himmelblau.print();
			Console.Write($"from starting point : \t");
			vstart2.print();	
			Console.Write($"\n");
		}
	}//Main
}//class main
