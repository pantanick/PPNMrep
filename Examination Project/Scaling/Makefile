CSC = mcs
RUN = mono

all: lib.dll out.txt plot_times.svg
	@cat out.txt

main.exe: main.cs lib.dll
	$(CSC) -reference:lib.dll -out:$@ main.cs

out.txt: main.exe
	$(RUN) main.exe > out.txt

lib.dll: vector.cs qrgs.cs newton.cs matrix.cs lagrangian_eigen_solver.cs
	$(CSC) -target:library -out:$@ $^

plot_times.svg: out_plot_data.txt Makefile
	echo '\
		set terminal svg background "white"; \
		set grid; \
		set output "plot_times.svg"; \
		set title "Time Dependency on Matrix Size"; \
		set xlabel "Matrix Dimension"; \
		set ylabel "Time (ms)"; \
		set tics out; \
		set logscale x; \
		set logscale y; \
		set xrange [1:300]; \
		set yrange [1:20000]; \
		plot "out_plot_data.txt" using 1:3 with points linecolor rgb "purple" notitle; \
	' | gnuplot

clean:
	$(RM) *.dll *.exe [Oo]ut* result* *.svg
