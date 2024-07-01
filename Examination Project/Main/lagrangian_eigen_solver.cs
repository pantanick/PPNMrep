using System;

public static class LagrangianEigenSolver
{
	public static (vector, double) SolveEigen(vector start, matrix A, double λStart, double acc = 1e-6)
	{
		int n = A.size1;
		vector x = new vector(n + 1);
		// initializing the vector x with the starting vector and eigenvalue
		for (int i = 0; i < n; i++)
		{
			x[i] = start[i];
		}
		x[n] = λStart;
		// defining the function f(v, λ) = {Av-λv, v^Tv-1}
		Func<vector, vector> f = v => {
			vector vSub = new vector(n);// subvector for v
			for (int i = 0; i < n; i++)
			{
				vSub[i] = v[i];
			}
			double λ = v[n]; // eigenvalue
			vector result = new vector(n + 1);
			// calculating Av , Av-λv
			vector Av = A * vSub;
			for (int i = 0; i < n; i++)
			{
				result[i] = Av[i] - λ * vSub[i];
			}
			// calculating v^Tv-1
			result[n] = vSub.dot(vSub) - 1;
			return result;
		};

		// defining the jacobian matrix (J) of f 
		Func<vector, matrix> jacobian = v => {
			vector vSub = new vector(n);// subvector for v
			for (int i = 0; i < n; i++)
			{
				vSub[i] = v[i];
			}
			double λ = v[n];
			matrix J = new matrix(n + 1, n + 1);
			// calculating the elements of the jacobian matrix
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					J[i, j] = A[i, j] - λ * (i == j ? 1 : 0);
				}
				J[i, n] = -vSub[i];
				J[n, i] = 2 * vSub[i];
			}
			J[n, n] = 0;// derivative of constraint with respect to λ
			return J;
		};

		// rootfinding using the newton's method
		vector root = newtonian.newton(f, x, jacobian, acc);
		// extracting results
		vector eigenvector = new vector(n);
		for (int i = 0; i < n; i++)
		{
			eigenvector[i] = root[i];
		}
		double eigenvalue = root[n];

		return (eigenvector, eigenvalue);
	}//SolveEigen
}//class LagrangianEigenSolver
