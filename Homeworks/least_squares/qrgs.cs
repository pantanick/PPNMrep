using System;

public class qrgs {
    public static int n;
    public static int m;
    private matrix Q;
    private matrix R;

    // Constructor for qrgs decomposition
    public qrgs(matrix A) {
        n = A.size1;
        m = A.size2;
        Q = A.copy();
        R = new matrix(m, m); // Ensure R is initialized to correct size
        for (int i = 0; i < m; i++) {
            R[i, i] = Q[i].norm();
            Q[i] /= R[i, i];
            for (int j = i + 1; j < m; j++) {
                R[i, j] = Q[i].dot(Q[j]);
                Q[j] -= R[i, j] * Q[i];
            }
        }
    }

    // Getter method for Q
    public matrix getQ() {
        return Q;
    }

    // Getter method for R
    public matrix getR() {
        return R;
    }

    // Solve R*x=Q^t*b
    public static vector solve(matrix Q, matrix R, vector b) {
        matrix Q_transpose = Q.transpose();
        vector c = Q_transpose * b;
        vector x = new vector(R.size2);
        for (int i = m - 1; i >= 0; i--) {
            double sum = 0;
            for (int temp = i + 1; temp < R.size2; temp++) {
                sum += R[i, temp] * x[temp];
            }
            x[i] = (c[i] - sum) / R[i, i];
        }
        return x;
    }

    // Determinant
    public static double det(matrix R) {
        if (R.size1 != R.size2) {
            System.Console.WriteLine($"Matrix must be square in order to compute determinant");
            return 0;
        } else {
            double det = 1.0;
            for (int i = 0; i < n; i++) {
                det *= R[i, i];
            }
            return det;
        }
    }

    // Pseudoinverse
    public matrix pseudoinverse(matrix A) {
        var qr = new qrgs(A);
        var R = qr.getR();
        var Q = qr.getQ();

        //Console.WriteLine($"Dimensions of R: {R.size1}x{R.size2}");
        //Console.WriteLine($"Dimensions of Q: {Q.size1}x{Q.size2}");

        if (R.size1 != R.size2) {
            // For non-square matrices, compute Moore-Penrose pseudoinverse
            matrix R_T = R.transpose();
            matrix R_T_R = R_T * R;
            Console.WriteLine($"Dimensions of R_T: {R_T.size1}x{R_T.size2}");
            Console.WriteLine($"Dimensions of R_T_R: {R_T_R.size1}x{R_T_R.size2}");
            var qrR_T_R = new qrgs(R_T_R);
            matrix R_T_R_inv = qrR_T_R.inverse(R_T_R);
            matrix R_inv = R_T_R_inv * R_T;
            return R_inv * Q.transpose();
        } else {
            return qr.inverse(R) * Q.transpose(); // Use the existing inverse method for square matrices
        }
    }

    // Inverse
    public matrix inverse(matrix A) {
        if (R.size1 != R.size2) {
            System.Console.WriteLine($"Matrix must be square in order to compute determinant");
            return new matrix(0, 0);
        } else {
            matrix inverse_A = new matrix(n, m);
            for (int i = 0; i < n; i++) {
                vector e = new vector(n);
                for (int j = 0; j < m; j++) {
                    if (j == i) {
                        e[j] = 1;
                    } else {
                        e[j] = 0;
                    }
                }
                inverse_A[i] = solve(Q, R, e);
            }
            return inverse_A;
        }
    }
}
