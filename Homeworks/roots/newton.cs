using System;
public static class newtonian{
	public static vector newton(
		Func<vector,vector>f,
		vector start,
		double acc=1e-2,
		vector δx=null
		){
	vector x=start.copy();
	vector fx=f(x),z,fz;
	double λmin=1e-4;
	qrgs qr = new qrgs(); //instance
	do{ // Newton
		if(fx.norm() < acc) break; /* job done */
		matrix J=jacobi.jacobian(f,x,fx,δx);
		var (QRofJ, RofJ) = qr.decomp(J);
		vector Dx = qrgs.solve(QRofJ,RofJ,-fx); /* Newton's step */
		double λ=1;
		do{ /* linesearch */
			z=x+λ*Dx;
			fz=f(z);
			if( fz.norm() < (1-λ/2)*fx.norm() ){
				break;
			}
			if( λ < λmin ){
				break;
			}
			λ/=2;
		}while(true);
		x=z; fx=fz;
	}while(true);
	return x;
	}//newton
}//class newton
