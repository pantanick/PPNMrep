using System;
using static System.Console;

public class main {
	public static void Main(){
		int n = 3;
//Generating random symmetrix matrix A
		matrix A = new matrix(n);
		var random = new System.Random(DateTime.Now.Millisecond);
		for(int i = 0; i < n; i++){
			for(int j = 0; j < n; j++){
				A[i,j]=random.NextDouble();
				A[j,i]=A[i,j];
			}
		}
		A.print("Matrix A (symmetric): ");
		matrix A_initial=A.copy(); //Needed for checks later
		(vector w, matrix v)=jacobi.cyclic(A);
		w.print("\nEigenvalues: ");
		v.print("\nMatrix V - Eigenvectors (columns): ");
		matrix v_t=v.transpose();
		v_t.print("\nV_Transposed");
		matrix D=new matrix (n,n);
		for(int i=0; i<n; i++){
			D[i,i]=w[i];
		}
		D.print("\nMatrix D:");
//Checks
		Console.WriteLine("\nChecks to make sure results are correct:\n");
	//Check1
		matrix v_tAv=v_t*A_initial*v;
		bool equality = v_tAv.approx(D);
		v_tAv.print("\nMatrix V_T*A*V:");
		if (equality){
			Console.WriteLine("-> V_T*A*V is approximately equal to D\n");
		}
		else{
			Console.WriteLine("-> V_T*A*V is not approximately equal to D\n");
		}
	//Check2
		matrix vDv_t=v*D*v_t;
		bool equality2 = vDv_t.approx(A_initial);
		vDv_t.print("\nMatrix V*D*V_T:");
		if (equality2){
			Console.WriteLine("-> V*D*V_T is approximately equal to A\n");
		}
		else{
			Console.WriteLine("-> V*D*V_T is not approximately equal to A\n");
		}
	//Check3
		matrix v_tv=v_t*v;
		v_tv.print("V_T * V :");
		bool equality3 = v_tv.approx(matrix.id(n));
		if (equality3){
			Console.WriteLine("-> V_T*V is approximately equal to identity matrix\n");
		}
		else{
			Console.WriteLine("-> V_T*A*V is not approximately equal to identity matrix\n");
		}
	//Check4
		matrix vv_t=v*v_t;
		vv_t.print("V * V_T :");
		bool equality4 = vv_t.approx(matrix.id(n));
		if (equality4){
			Console.WriteLine("-> V*V_T is approximately equal to identity matrix\n");
		}
		else{
			Console.WriteLine("-> V*V_T is not approximately equal to identity matrix\n");
		}

		}//Main
}//main
