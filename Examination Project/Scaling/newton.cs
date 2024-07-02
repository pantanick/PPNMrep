using System;

public static class newtonian
{
	public static vector newton(
		Func<vector, vector> f,
		vector start,
		Func<vector, matrix> jacobian,
		double acc = 1e-6
	)
	{
		vector x = start.copy();
		vector fx = f(x), z, fz;
		double lambdaMin = 1e-4;
		qrgs qr = new qrgs();  // Instance of qrgs for QR decomposition

		do
		{
			if (fx.norm() < acc) break;  // Check if accuracy goal is met

			matrix J = jacobian(x);  // Use the provided Jacobian function
			var (Q, R) = qr.decomp(J);  // Perform QR decomposition on the Jacobian
			vector Δx = qrgs.solve(Q, R, -fx);  // Solve the linear system using returned Q and R

			double λ = 1.0;
			do
			{
				z = x + λ * Δx;
				fz = f(z);
				if (fz.norm() < (1 - λ / 2) * fx.norm())
				{
					x = z;
					fx = fz;
					break;
				}
				λ /= 2;
			} while (λ > lambdaMin);

		} while (true);

		return x;
	}
}
