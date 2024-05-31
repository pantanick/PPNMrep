using System;
public static class main{
	static void Main(string[] args){
		unitcircle_area();
		extra_calc();
	}
	static void unitcircle_area(){
		Func<vector, double> unitcircle = v =>
		{
			double x=v[0];
			double y=v[1];
			return (x*x + y*y <=1) ? 1.0 : 0.0;
		};
		//square bounds
		vector a = new vector(-1,-1); 
		vector b = new vector(1,1);
		int m = 100000; //Number of random points
		var (area_test,error_test)=montecarlo.plainmc(unitcircle,a,b,m);
		Console.WriteLine("Test calculation:");
		Console.WriteLine($"Estimated area of the unit circle: {area_test}, Error : {error_test}, Number of sampling points : {m}");
		Console.WriteLine("Proceeding to calculating plot data\n");
		int[] samplepoints = { 1000, 10000, 100000, 1000000, 10000000 };
		double realarea = System.Math.PI;
		double[] calcerrors = new double[samplepoints.Length];	
		double[] realerrors = new double[samplepoints.Length];
		double[] calcerrors_quasi = new double[samplepoints.Length];
		double[] realerrors_quasi = new double[samplepoints.Length];
		for(int i=0;i<samplepoints.Length;i++){
			int N = samplepoints[i];
			var(area,error)=montecarlo.plainmc(unitcircle,a,b,N);
			calcerrors[i]=error;
			realerrors[i]=System.Math.Abs(area-realarea);
			Console.WriteLine($"\nPlain M-C :");
			Console.WriteLine($"N: {N}, Estimated Area = {area}, Estimated Error = {error}, Actual Error = {realerrors[i]}");	
			var (area_quasi, error_quasi)=montecarlo.quasimc(unitcircle,a,b,N);
			calcerrors_quasi[i]=error_quasi;
			realerrors_quasi[i]=Math.Abs(area_quasi-realarea);
			Console.WriteLine($"Halton M-C :");
	        	Console.WriteLine($"N: {N}, Estimated Area: {area_quasi}, Estimated Error: {error_quasi}, Actual Error: {realerrors_quasi[i]}");
		}
		Console.Write("\n");
		using (var plotwriter = new System.IO.StreamWriter("out_plotdata.txt")){
			 for (int i = 0; i < samplepoints.Length; i++){
				 plotwriter.WriteLine($"{samplepoints[i]}	{calcerrors[i]}	{realerrors[i]}	{calcerrors_quasi[i]}  {realerrors_quasi[i]}");
			 }
		}
	}//unitcircle
	static void extra_calc(){
		Func<vector,double> anIntegral = v =>
		{
			double x = v[0];
			double y = v[1];
			double z = v[2];
			return (1 / (Math.PI * Math.PI * Math.PI)) *1/((1 - Math.Cos(x) * Math.Cos(y) * Math.Cos(z)));
		};
		//cubic bounds
		vector a = new vector (0,0,0);
		vector b = new vector (Math.PI,Math.PI,Math.PI);
		int N = 1000000;
		double realvalue = 1.3932039296856768591842462603255;
		var (result,error)=montecarlo.plainmc(anIntegral,a,b,N);
		Console.WriteLine($"\nCalculating value of the given complex integral, number of sampling points: N = {N}");
		Console.WriteLine($"Calculated value = {result}, Actual value = {realvalue}, Estimated error = {error}, Actual error = {Math.Abs(realvalue-result)}");
		var (resultquasi,errorquasi)=montecarlo.quasimc(anIntegral,a,b,N);
		Console.WriteLine($"\nCalculating value of the given complex integral using the Halton sequence, number of sampling points: N = {N}");
		Console.WriteLine($"Calculated value = {resultquasi}, Actual value = {realvalue}, Estimated error = {errorquasi}, Actual error = {Math.Abs(realvalue-resultquasi)}");

	}//extra_calc
}//class main
