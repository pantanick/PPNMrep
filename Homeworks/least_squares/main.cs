using System;
using static System.Math;
using System.IO;

public class main {
    public static int Main(string[] args) {
        foreach (var arg in args) {
            Console.WriteLine("- " + arg);
        }
        string infile = null;
        //string outfile = null; tried having outfile defined by user, output would not write to file because of print() function.
	//Might look into it later
        foreach (var arg in args) {
            var words = arg.Split(':');
            if (words[0] == "-input") infile = words[1];
        //    if (words[0] == "-output") outfile = words[1];
        }
        Console.WriteLine("Input file: " + infile);
        //Console.WriteLine("Output file: " + outfile);
        Console.WriteLine("\nReading data\n");
        if (infile == null /*|| outfile == null*/) {
            Console.WriteLine("Usage: main.exe -input:<inputPath> -output:<outputPath>");
            return 1;  //Error
        }
        var instream = new StreamReader(infile);
        var plotstream = new StreamWriter("plotdata.txt");
        vector t = new vector(9);
        vector y = new vector(9);
        vector dy = new vector(9);
        int temp = 0;
        var split_options = StringSplitOptions.RemoveEmptyEntries;
        char[] split_delimiters = { ' ', '\t', '\n' };
        for (string line = instream.ReadLine(); line != null; line = instream.ReadLine()) {
            var number = line.Split(split_delimiters, split_options);
            t[temp] = double.Parse(number[0]);
            y[temp] = double.Parse(number[1]);
            dy[temp] = double.Parse(number[2]);
            Console.WriteLine($"t[{temp}]: {t[temp]}, y[{temp}]: {y[temp]}, dy[{temp}] : {dy[temp]}");
            temp += 1;
        }
        vector lny = new vector(y.size);
        vector dlny = new vector(dy.size);
        for (int i = 0; i < y.size; i++) {
            lny[i] = Log(y[i]);
            dlny[i] = dy[i] / y[i];
        }
        Console.WriteLine("\nCreating random matrix A, running QR and checking result\n");
        int n = 5;
        int m = 4;
        var random = new Random();
        matrix A = new matrix(n, m);
        vector b = new vector(n);
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < m; j++) {
                A[i, j] = random.NextDouble();
            }
            b[i] = random.NextDouble();
        }
        qrgs qrgsInstance = new qrgs(A);
        Console.Write("A = ");
        A.print();
        matrix Q = qrgsInstance.getQ();
        matrix R = qrgsInstance.getR();
        Console.Write("Q = ");
        Q.print();
        Console.Write("R = ");
        R.print();
        matrix Q_transpose = Q.transpose();
        matrix Q_transpose_Q = Q_transpose * Q;
        if (Q_transpose_Q.approx(matrix.id(Q_transpose_Q.size1))) {
            Console.WriteLine("Q^T * Q is approximately equal to the identity matrix");
        } else {
            Console.WriteLine("Q^T * Q is not approximately equal to the identity matrix");
        }
        if (Q.size2 != R.size1) {
            Console.WriteLine($"Error: Dimensions of Q and R do not match for multiplication: {Q.size2} != {R.size1}");
            return 1;
        }
        matrix Q_R = Q * R;
        if (Q_R.approx(A)) {
            Console.WriteLine("Q * R is approximately equal to matrix A");
        } else {
            Console.WriteLine("Q * R is not approximately equal to matrix A");
        }
        Console.WriteLine("\nDecay of Thx\n");
        Func<double, double>[] fs = { x => 1, x => -x };
        (vector c, matrix sigma) = leastsquares.lsfit(fs, t, lny, dlny);
        Console.WriteLine("Coefficients vector c:\n");
        c.print();
        vector dc = new vector(c.size);
        for (int i = 0; i < dc.size; i++) {
            dc[i] = Sqrt(sigma[i, i]);
        }
        Func<double, double> fy = z => Exp(c[0]) * Exp(-c[1] * z);
        Func<double, double> dfy_up = z => Exp(c[0] + dc[0]) * Exp((-c[1] - dc[1]) * z);
        Func<double, double> dfy_down = z => Exp(c[0] - dc[0]) * Exp((-c[1] + dc[1]) * z);
        Console.WriteLine("Coefficients vector c - Errors:\n");
        dc.print();
        Console.WriteLine("Covariance matrix");
        sigma.print();
        for (int i = 0; i < t.size; i++) {
            plotstream.WriteLine($"{t[i]} {fy(t[i])} {y[i]} {dy[i]} {dfy_up(t[i])} {dfy_down(t[i])}");
        }
        double taf = Log(2) / c[1];
        double dtaf = taf * Sqrt((dc[1] / c[1]) * (dc[1] / (c[1])));
        Console.WriteLine($"Given half life value of ThX = 3.64 days");
        Console.WriteLine($"Measured half life value of ThX = {taf} +- {dtaf} days");
        instream.Close();
        plotstream.Close();
        return 0;
    }
}
