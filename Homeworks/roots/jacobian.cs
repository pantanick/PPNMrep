using System;
public class jacobi{
	public static matrix jacobian(Func<vector,vector> f,vector x,vector fx=null,vector dx=null){
		if(dx == null){
			dx = x.map(xi => System.Math.Abs(xi)*System.Math.Pow(2,-26));
		}
		if(fx == null){
			fx = f(x);
		}
		matrix J=new matrix(x.size);
		for(int j=0;j < x.size;j++){
			x[j]+=dx[j];
			vector df=f(x)-fx;
			for(int i=0;i < x.size;i++){
				J[i,j]=df[i]/dx[j];
			}
			x[j]-=dx[j];
		}
		return J;
		}//jacobian
}//class jacobi
