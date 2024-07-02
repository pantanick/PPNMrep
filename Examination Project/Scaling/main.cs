using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

public class main
{
	public static void Main()
	{
		// range of matrix sizes to test
		int[] sizes = { 10, 30, 50, 80, 100, 120, 130, 142, 150, 160};
		// StreamWriter to write the results to results.txt
		using (StreamWriter writer = new StreamWriter("results.txt"))
		// StreamWriter to write the plot data to out_plot_data.txt
		using (StreamWriter plotDataWriter = new StreamWriter("out_plot_data.txt"))
		{
			plotDataWriter.WriteLine("Dimension\tTotal Elements\tTime (ms)");

			foreach (int n in sizes)
			{
				// random symmetric matrix A with a fixed seed for reproducibility
				matrix A = new matrix(n, n);
				Random rand = new Random(42); // seed for reproducibility, since matrix size is our only variable

				for (int i = 0; i < n; i++)
				{
					for (int j = i; j < n; j++)
					{
						A[i, j] = rand.NextDouble() * 10; // random values between 0 and 10
						A[j, i] = A[i, j];
					}
				}

				// defining multiple initial guesses for eigenvector and eigenvalue
				vector[] vStarts = {
					new vector(n) { [0] = 1.0, [1] = 1.0, [2] = 1.0, [3] = 1.0 },
					new vector(n) { [0] = -1.0, [1] = 1.0, [2] = 1.0, [3] = 1.0 },
					new vector(n) { [0] = 1.0, [1] = -1.0, [2] = 1.0, [3] = 1.0 },
					new vector(n) { [0] = 1.0, [1] = 1.0, [2] = -1.0, [3] = 1.0 },
					new vector(n) { [0] = 1.0, [1] = 1.0, [2] = 1.0, [3] = -1.0 }
				};
				// added negative values of λStarts in order to not miss a root
				double[] λStarts = { -10.0, -5.0, -1.0, 1.0, 5.0, 10.0 };

				// keep track of printed eigenvalues and eigenvectors
				List<string> printedResults = new List<string>();

				// measuring time taken for each size-loop
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();

				foreach (var vStart in vStarts)
				{
					foreach (var λStart in λStarts)
					{
						// finding eigenvector and eigenvalue
						var (eigenvector, eigenvalue) = LagrangianEigenSolver.SolveEigen(vStart, A, λStart);

						// normalizing the eigenvector
						double norm = eigenvector.norm();
						for (int i = 0; i < eigenvector.size; i++)
						{
							eigenvector[i] /= norm;
						}

						// creating a unique key for the current result
						string resultKey = $"{Math.Round(eigenvalue, 5):F5} " + string.Join(" ", eigenvector);

						// checking if the result has already been printed
						if (!printedResults.Contains(resultKey))
						{
							// adding the result to the list
							printedResults.Add(resultKey);

							// writing result to file
							writer.WriteLine($"Matrix size: {n}x{n}");
							writer.WriteLine("Eigenvalue: " + eigenvalue);
							writer.WriteLine("Eigenvector:");
							for (int i = 0; i < eigenvector.size; i++)
							{
								writer.Write($"{eigenvector[i]} ");
							}
							writer.WriteLine();

							// checking that Av = λ*v
							vector Av = A * eigenvector;
							vector λV = eigenvalue * eigenvector;
							bool verification = true;
							for (int i = 0; i < eigenvector.size; i++)
							{
								if (Math.Abs(Av[i] - λV[i]) > 1e-6)
								{
									verification = false;
									break;
								}
							}

							writer.WriteLine($"Verification: Av = λv = {verification}");
							writer.WriteLine();
						}
					}
				}

				stopwatch.Stop();
				long elapsedTime = stopwatch.ElapsedMilliseconds;

				// writing the matrix size, total number of elements, and time to out_plot_data.txt
				plotDataWriter.WriteLine($"{n}\t{n * n}\t{elapsedTime}");
				Console.WriteLine($"\nTime taken for matrix size {n}x{n}: {elapsedTime} ms");
				writer.WriteLine($"Time taken for matrix size {n}x{n}: {elapsedTime} ms");
				writer.WriteLine();
			}
		}
	}//Main
}//class main
