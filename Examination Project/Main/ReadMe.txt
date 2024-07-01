Examination project	— 24 —
Eigenvalues via rootfinding: Variational method with Lagrange multipliers

I decided to use a symmetric 4x4 matrix, it's a modest size that is frequently encountered in physics. I checked the results using this python(please don't grade me with a -2 for mentioning python) script:
"
import numpy as np
A = np.array([[4, 1, 2, 3],
            [1, 5, 4, 2],
            [2, 4, 6, 1],
            [3, 2, 1, 3]])
eigenvalues, eigenvectors = np.linalg.eigh(A)
print("Eigenvalues:", eigenvalues)
print("Eigenvectors:")
print(eigenvectors)
# verification
for i in range(4):
    print(f"Eigenvalue {i+1}: {eigenvalues[i]}")
    print(f"Eigenvector {i+1}: {eigenvectors[:, i]}")
    Av = A @ eigenvectors[:, i]
    λV= eigenvalues[i] * eigenvectors[:, i]
    print(f"Verification Av = λ*v: {np.allclose(Av, λV)}")
    print(f"Av = {Av}, λ*v = {λV}")
"
Results may have reversed signs, that is ok.
I decided to calculate all 4 sets of eigenvector-value, because having a 4x4 matrix and only calculating one eigenvector-eigenvalue set felt incomplete.
I don't know if I should prioritize efficency or stability, so I used a loop to make sure every solution is calculated, since I changed the values of the matrix A a lot for testing and debugging.
I understand that means that the code could be more efficient, but otherwise it felt too specialized and changing the matrix A demanded to change the starting values as well. 
After I confirmed the code was working as intended, I did not randomize matrix A since the project asks for a "given symmetric matrix A".
