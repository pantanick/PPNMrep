using System;

public class main {
	public static void Main(){
		double a=0;
		double b=1;
		double δ = 0.001;
		double ε = 0.001;
		Func<double,double> sqrtFunction = x => System.Math.Sqrt(x);
		double realres1 = 2.0/3.0;
		double testresult1 = integration.integrate(sqrtFunction,a,b);
		Console.WriteLine($"Testing the implementation by calculating known integrals: \n");
		Console.WriteLine($"Absolute error tolerance: δ = {δ}, Relative error tolerance: ε = {ε}\n");
		Console.WriteLine($"The integral of sqrt(x) from {a} to {b} is approximately: {testresult1}");
		Console.WriteLine($"Accurate value = {realres1} , Calculation error = {System.Math.Abs(realres1-testresult1)}\n");
		Func<double,double> InvsqrtFunction = x => 1.0/System.Math.Sqrt(x);
		double realres2 = 2.0;
		double testresult2 = integration.integrate(InvsqrtFunction,a,b);
		Console.WriteLine($"The integral of 1/sqrt(x) from {a} to {b} is approximately: {testresult2}");
		Console.WriteLine($"Accurate value = {realres2} , Calculation error = {System.Math.Abs(realres2-testresult2)}\n");
		Func<double,double> blahFunction = x => 4.0*System.Math.Sqrt(1-(x*x));
		double realres3 = System.Math.PI;
		double testresult3 = integration.integrate(blahFunction,a,b);
		Console.WriteLine($"The integral of 4*sqrt(1-x^2) from {a} to {b} is approximately: {testresult3}");
		Console.WriteLine($"Accurate value = {realres3} , Calculation error = {System.Math.Abs(realres3-testresult3)}\n");
		Func<double,double> blahFunction2 = x => System.Math.Log(x)/System.Math.Sqrt(x);
		double realres4 = -4;
		double testresult4 = integration.integrate(blahFunction2,a,b);
		Console.WriteLine($"The integral of ln(x)/sqrt(x) from {a} to {b} is approximately: {testresult4}");
		Console.WriteLine($"Accurate value = {realres4} , Calculation error = {System.Math.Abs(realres4-testresult4)}\n");
		Console.WriteLine($"Proceeding to calculating and plotting the error function for -3≤z≤3");
		using (var erfout = new System.IO.StreamWriter("out_erf.txt")){
			for(double z=-3.00;z<=3;z+=0.1){
				erfout.WriteLine($"{z}	{integration.erf(z)}");
			}
		}
		double[] exactErfValues = {
			-0.99997790950300141464,
			-0.99532226501895273416,
		        -0.84270079294971486934,
			0,
			0.84270079294971486934,
			0.99532226501895273416,
			0.99997790950300141464
		};	
		Console.WriteLine($"Comparing to result for integer z to exact values");
		for(int k=-3;k<=3;k++){
			Console.WriteLine($"z={k}: Calculated erf(z)={integration.erf(k)} , Exact value={exactErfValues[k+3]}, Error = {System.Math.Abs(integration.erf(k) - exactErfValues[k+3])}");
		}
	}//Main
}//class main
