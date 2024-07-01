using System;
using System.Collections.Generic;
using System.IO;

public class main
{
	public static void Main()
	{
		// defining a 4x4 symmetric matrix A
		matrix A = new matrix(4,4);
		A[0, 0] = 4;	
		A[0, 1] = 1;
		A[0, 2] = 2;
		A[0, 3] = 3;
		A[1, 0] = 1;
		A[1, 1] = 5;
		A[1, 2] = 4;
		A[1, 3] = 2;
		A[2, 0] = 2;
		A[2, 1] = 4;
		A[2, 2] = 6;
		A[2, 3] = 1;
		A[3, 0] = 3;
		A[3, 1] = 2;
		A[3, 2] = 1;
		A[3, 3] = 3;

		// sanity check
		Console.WriteLine("Matrix A:");
		for (int i = 0; i < A.size1; i++)
		{
			for (int j = 0; j < A.size2; j++)
			{
				Console.Write($"{A[i, j]} ");
			}
			Console.WriteLine();
		}

		// sanity check vol.2
		Console.WriteLine($"Matrix A size: {A.size1}x{A.size2}\n");

		// defining multiple initial guesses for eigenvector and eigenvalue
		vector[] vStarts = {
			new vector(1, 1, 1, 1),
			new vector(-1, 1, 1, 1),
			new vector(1, -1, 1, 1),
			new vector(1, 1, -1, 1),
			new vector(1, 1, 1, -1)
		};

		double[] λStarts = { 1.0, 5.0, 10.0 };

		// keep track of printed eigenvalues and eigenvectors
		List<string> printedResults = new List<string>();

		// StreamWriter to write the results to results.txt
		using (StreamWriter writer = new StreamWriter("results.txt"))
		{
			foreach (var vStart in vStarts)
			{
				foreach (var λStart in λStarts)
				{
					Console.WriteLine($"\nInitial guess for eigenvalue λStart: {λStart}");
					Console.WriteLine($"Initial guess for eigenvector vStart: {vStart[0]}, {vStart[1]}, {vStart[2]}");

					// finding eigenvector and eigenvalue
					var (eigenvector, eigenvalue) = LagrangianEigenSolver.SolveEigen(vStart, A, λStart);

					// printing eigenvalue and eigenvector before normalization
					Console.WriteLine("Eigenvalue: " + eigenvalue);
					Console.WriteLine("Eigenvector before normalization:");
					Console.Write($"\t");
					for (int i = 0; i < eigenvector.size; i++)
					{
						Console.Write($"{eigenvector[i]} ");
					}
					Console.WriteLine();

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
						writer.WriteLine("Eigenvalue: " + eigenvalue);
						writer.WriteLine("Eigenvector:");
						for (int i = 0; i < eigenvector.size; i++)
						{
							writer.Write($"{eigenvector[i]} ");
						}
						writer.WriteLine();
						writer.WriteLine();

						// printing eigenvector after normalization
						Console.WriteLine("Eigenvector after normalization:");
						Console.Write($"\t");
						for (int i = 0; i < eigenvector.size; i++)
						{
							Console.Write($"{eigenvector[i]} ");
						}
						Console.WriteLine();

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

						Console.WriteLine("Verification: Av = λ*v ?");
						for (int i = 0; i < eigenvector.size; i++)
						{
							Console.WriteLine($"\tAv[{i}] = {Av[i]}, λ*v[{i}] = {λV[i]}");
						}
						Console.WriteLine("Verification result: " + verification);

						// printing results
						Console.WriteLine("Eigenvalue: " + eigenvalue);
						Console.WriteLine("Eigenvector:");
						eigenvector.print();
					}
					else
					{
						Console.WriteLine("Result calculated and printed already.");
					}
				}
			}
		}

		// reading and printing the unique results for better readability without
		// needing to open results.txt
		string results = File.ReadAllText("results.txt");
		Console.WriteLine("\n\n\nUnique results calculated (saved in results.txt):\n");
		Console.WriteLine(results);
	}
}
