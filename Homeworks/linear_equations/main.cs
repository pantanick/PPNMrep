using System;
public class main{
	public static void Main(){
		/*Test for user input for matrix dimensions.
		System.Console.WriteLine("Number of rows (n) of matrix A:");
		int n=int.Parse(Console.ReadLine());
		System.Console.WriteLine("Number of columns (m) of matrix A:");
		int m=int.Parse(Console.ReadLine());
		*/
		int n=8;
		int m=8;
		var random = new System.Random(DateTime.Now.Millisecond);
		vector b = new vector(n);
		matrix A = new matrix(n,m);
		for (int i=0;i<n;i++){
			for(int j=0;j<m;j++){
				A[i,j]=random.NextDouble();
			}
			b[i]=random.NextDouble();
		}
		qrgs qrgsInstance = new qrgs(A);
		System.Console.Write($"A = ");
		A.print();
		System.Console.Write($"Q = ");
		qrgsInstance.getQ().print();
		System.Console.Write($"R = ");
		qrgsInstance.getR().print();
//Checking if R is upper triangular
		matrix R = qrgsInstance.getR();
		matrix Q = qrgsInstance.getQ();
		int check = 0;
		for (int i = 0; i < R.size1; i++) {
		    for (int j = 0; j < i; j++) {
		        if (R[i, j] != 0) {
		            check+= 1;
        		    break;
       			 }
   		 }
   		 if (check!=0) {
   		     break;
		    }
		}
		if (check==0) {
		    System.Console.WriteLine("R is upper triangular");
		} else {
    		System.Console.WriteLine("R is not upper triangular");
		}

//Checking if Q^T*Q=1
		matrix Q_transpose = Q.transpose();
		matrix Q_transpose_Q = Q_transpose * Q;
		if (Q_transpose_Q.approx(matrix.id(Q_transpose_Q.size1))) {
		    System.Console.WriteLine("Q^T * Q is approximately equal to the identity matrix");
		} else {
		    System.Console.WriteLine("Q^T * Q is not approximately equal to the identity matrix");
		}
//Checking if Q*R=A
		matrix Q_R = Q * R;
		if (Q_R.approx(A)) {
		    System.Console.WriteLine("Q * R is approximately equal to matrix A");
		} else {
		    System.Console.WriteLine("Q * R is not approximately equal to matrix A");
		}
//Square matrix condition
		if (n > m) {
			Console.WriteLine("\nSkipping execution of methods 'solve', 'det' and 'inverse' because matrix A is not square");
			return;
		}

//Solve
		System.Console.WriteLine($"b = ");
		b.print();
		vector x=qrgs.solve(Q,R,b);
		System.Console.WriteLine($"Solution vector\nx= ");
		x.print();

//Checking if A*x = b
		vector Ax=A*x;
		System.Console.Write($"A*x = ");
		Ax.print();
		if (Ax.approx(b)) {
		    System.Console.WriteLine("Ax is approximately equal to b.");
		} else {
		    System.Console.WriteLine("Ax is not approximately equal to b.");
		}
//Determinant
		double det=qrgs.det(R);
		System.Console.Write($"Determinant(A)=Determinant(R)={det}\n");

//Inverse
		matrix matrix_inverted = qrgsInstance.inverse(A);
		System.Console.Write($"A^(-1)= ");
		matrix_inverted.print();
//Checking if A*A^(-1)=1
		matrix Aproduct = A*matrix_inverted;
		if (Aproduct.approx(matrix.id(A.size1))) {
			Console.WriteLine("A * A^(-1) is approximately equal to the identity matrix");
			} else {
			Console.WriteLine("A * A^(-1) is not approximately equal to the identity matrix");
			}
	}//Main
}//class main
