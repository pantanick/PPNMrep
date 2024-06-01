using System;

public static class newtonian
{
    public static vector newton(
        Func<vector, double> φ,
        vector x,
        double acc = 1e-3,
        double λmin = 1.0/1024,
        int maxIterations = 1000)
    {
        qrgs qr = new qrgs(); // instance
        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            Console.Write($"\nIteration {iteration}:	 x = ");
	    x.print();
	    //Console.WriteLine($"\n");
            var gradφ = gradient(φ, x);
            Console.Write($"Gradient(φ) : ");
	    gradφ.print();
	    //Console.WriteLine($"\n");
            if (gradφ.norm() < acc)
            {
                Console.WriteLine($"\nConvergence reached after {iteration} iterations");
                break; /* job done */
            }
            var H = hessian(φ, x);
            var (QofJ, RofJ) = qr.decomp(H);
            vector dx = qrgs.solve(QofJ, RofJ, -gradφ); /* Newton's step */
            double λ = 1;
            double φx = φ(x);
            do
            {
                // Linesearch
                if (φ(x + λ * dx) < φx)
                {
                    break;
                }
                if (λ < λmin)
                {
                    break;
                }
                λ /= 2;
            } while (true);
            x += λ * dx;
	    if(iteration+1==maxIterations){
        	Console.WriteLine($"\nReached max amount of iterations, convergence not achieved");
	    }
	}
        return x;
    }

    public static vector gradient(Func<vector, double> φ, vector x)
    {
        vector gradφ = new vector(x.size);
        double φx = φ(x);
        for (int i = 0; i < x.size; i++)
        {
            double dx = Math.Max(Math.Abs(x[i]), 1) * Math.Pow(2, -26);
            x[i] += dx;
            gradφ[i] = (φ(x) - φx) / dx;
            x[i] -= dx;
        }
        return gradφ;
    }

    public static matrix hessian(Func<vector, double> φ, vector x)
    {
        int n = x.size;
        matrix H = new matrix(n, n);
        vector gradφx = gradient(φ, x);
        for (int j = 0; j < n; j++)
        {
            double dx = Math.Max(Math.Abs(x[j]), 1) * Math.Pow(2, -13);
            x[j] += dx;
            vector dgradφ = gradient(φ, x) - gradφx;
            for (int i = 0; i < n; i++)
            {
                H[i, j] = dgradφ[i] / dx;
            }
            x[j] -= dx;
        }
        return (H + H.transpose()) / 2.0;//to make sure it's symmetric
    }
}
