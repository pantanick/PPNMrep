using System;

public static class leastsquares {
    public static (vector, matrix) lsfit(Func<double, double>[] fs, vector t, vector y, vector dy) {
        int n = t.size, m = fs.Length;
        matrix A = new matrix(n, m);
        vector b = new vector(n);	
        for (int i = 0; i < n; i++) {
            b[i] = y[i] / dy[i];
            for (int k = 0; k < m; k++) {
                A[i, k] = fs[k](t[i]) / dy[i];
            }
        }
        qrgs qrgsInstance2 = new qrgs(A);
        System.Console.Write($"A = ");
        A.print();
        System.Console.Write($"R = ");
        matrix R = qrgsInstance2.getR();
        matrix Q = qrgsInstance2.getQ();
        vector c = qrgs.solve(Q, R, b);
        matrix R_inv = qrgsInstance2.pseudoinverse(R); // Use pseudoinverse method
        matrix R_invT = R_inv.transpose();
        R_invT.print();
        matrix sigma = R_inv * R_invT;
        return (c, sigma);
    }
}
