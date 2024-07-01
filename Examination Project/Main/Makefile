CSC = mcs
RUN = mono

all: lib.dll out.txt;
	cat out.txt

main.exe: main.cs lib.dll
	$(CSC) -reference:lib.dll -out:$@ main.cs

out.txt: main.exe
	$(RUN) main.exe > out.txt


lib.dll: vector.cs qrgs.cs newton.cs matrix.cs lagrangian_eigen_solver.cs
	$(CSC) -target:library -out:$@ $^

clean:
	$(RM) *.dll *.exe [Oo]ut* result*
