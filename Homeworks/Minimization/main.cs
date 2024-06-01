using System;
public class main {
	public static Func<vector,double>rosenbrock = v =>
		{
			double x=v[0];
			double y=v[1];
			return Math.Pow(1 - x, 2) + 100 * Math.Pow(y - x * x, 2);
		};
	public static Func<vector,double>himmelblau = v =>
		{
			double x=v[0];
			double y=v[1];
			return Math.Pow(x * x + y - 11, 2) + Math.Pow(x + y * y - 7, 2);
		};
	public static void Main(){
		vector vstart=new vector (-4,5);//bad guess to check algorithm
		vector[] vstart2list=new vector[]
		{
			//new vector(2.0,1.5),//close to minimum 3,2
			//new vector(-3.0, 2.5),//-2.81,3.13
			//new vector(-3.0, -3.5),//-3.78,-3.28
			new vector(2.5, -2.0),//3.58,-1.85
			//new vector(0.0, 0.0),//because why not, and mainly to check the maxiterations filter
			new vector(5.0, 5.0),//3,2
			new vector(-10.0, -10.0),//-3.78,-3.28
			new vector(-3.0,2.5),//close to minimum -2.81,3.13
			//new vector(-3.0,-3.5),//close to minimum -3.78,-3.28
			//new vector(2.5,-2.0)//close to minimum 3.58,-1.85
//tried different points to check the algorithm, left 1 active for each minimum
		};
		Console.WriteLine($"Rosenbrock's valley function: f(x,y)=(1-x)^2 + 100(y-x^2)^2");
		vector root_rosenbrock = newtonian.newton(rosenbrock, vstart);
		Console.Write($"Calculated minimum : ");
		root_rosenbrock.print();
		Console.Write($"from starting point : ");
		vstart.print();
		Console.Write($"\n");
		Console.WriteLine($"Himmelblau's function: f(x,y)=(x^2+y-11)^2 + (x+y^2-7)^2");
		foreach(vector vstart2 in vstart2list){
			vector root_himmelblau = newtonian.newton(himmelblau,vstart2);
			Console.Write($"Calculated minimum : ");
			root_himmelblau.print();
			Console.Write($"from starting point : \t");
			vstart2.print();	
			Console.Write($"\n");
		}
	}//Main
}//class main
