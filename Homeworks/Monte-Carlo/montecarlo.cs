using System ; 
public static class montecarlo{
	public static (double,double) plainmc(Func<vector,double> f,vector a,vector b,int N){
		int dim=a.size;
		double V=1;
		for(int i=0;i<dim;i++){
			V*=b[i]-a[i];
		}
		double sum=0,sum2=0;
		var x=new vector(dim);
		var rnd=new Random();
		for(int i=0;i<N;i++){
			for(int k=0;k<dim;k++){
				x[k]=a[k]+rnd.NextDouble()*(b[k]-a[k]);
			}
			double fx=f(x); sum+=fx; sum2+=fx*fx;
		}
		double mean=sum/N, sigma=System.Math.Sqrt(sum2/N-mean*mean);
		var result=(mean*V,sigma*V/System.Math.Sqrt(N));
		return result;
	}//plainmc
	 //Halton calculation
	public static (double, double) quasimc(Func<vector, double> f, vector a, vector b, int N){
		int dim = a.size;
		double V = 1;
		for (int i = 0; i < dim; i++){
			V*=b[i]-a[i];
		}
		double sumx1=0,sumx2=0;
		double sum2x1=0, sum2x2=0;
		var x1 = new vector(dim);
		var x2 = new vector(dim);
		for (int i = 0; i < N; i++){
			double[] haltonpoint1 = haltonsequence(i + 1, dim,1);
			double[] haltonpoint2 = haltonsequence(i + 1, dim,2);
			for (int k = 0; k < dim; k++){
				x1[k]=a[k]+haltonpoint1[k]*(b[k]-a[k]);
				x2[k]=a[k]+haltonpoint2[k]*(b[k]-a[k]);
			}
			double fx1=f(x1); 
			double fx2=f(x2); 
			sumx1+=fx1; sumx2+=fx2;
			sum2x1+=fx1*fx1; sum2x2=fx2*fx2;
		}
		double mean1=sumx1/N;
		double mean2=sumx2/N;
		double variance1 = sum2x1 / N - mean1 * mean1;
		double variance2 = sum2x2 / N - mean2 * mean2;
		variance1=System.Math.Max(variance1,0);
		variance2=System.Math.Max(variance2,0);
		double sigma1=System.Math.Sqrt(variance1);
		double sigma2=System.Math.Sqrt(variance2);
		double result=(mean1+mean2)*V/2;
		double error=System.Math.Sqrt((sigma1*sigma1+sigma2*sigma2)/2)*V/System.Math.Sqrt(N);
		return (result,error) ;
	}//Halton calculatio
	private static double[] haltonsequence(int index, int dimension, int offset){
		int[] bases = { 2, 3, 5, 7, 11};
		if (dimension > bases.Length){
			throw new ArgumentException("Dimension too high for available bases");
		}
		double[] result = new double[dimension];
		for (int i = 0; i < dimension; i++){
			result[i]=vandercorput(index+offset,bases[i]);
		}
		return result;
	}//haltonsequence
	private static double vandercorput(int index, int basenum){
		double f=1.0;
		double r=0.0;
		while(index>0){
			f/=basenum;
			r+=f*(index % basenum);
			index/=basenum;
		}
		return r;
	}//vandercorput
	

}//class montecarlo
