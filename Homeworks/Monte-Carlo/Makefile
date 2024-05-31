CSC = mcs
RUN = mono

all: lib.dll out.txt plot_circle.svg;
	cat out.txt

lib.dll: montecarlo.cs vector.cs
	$(CSC) -target:library -out:$@ $^

main.exe: main.cs lib.dll
	$(CSC) -reference:lib.dll -out:$@ main.cs

out.txt: main.exe
	$(RUN) main.exe > out.txt

plot_circle.svg: Makefile
	echo '\
		set terminal svg background "white"; \
		set grid; \
		set output "plot_circle.svg"; \
		set title "Monte Carlo Integration Error Scaling"; \
		set xlabel "Number of sampling points (N)"; \
		set tics out; \
		set logscale x; \
		set logscale y; \
		set xtics (1000, 10000, 100000, 1000000, 10000000); \
		set xrange [500:20000000]; \
		set ylabel "Error"; \
		f(x)=1.0/sqrt(x); \
		plot	"out_plotdata.txt" using 1:2 with points linecolor rgb "purple" title "Estimated Error,Plain MC",\
			"out_plotdata.txt" using 1:3 with points linecolor rgb "purple" title "Real Error,Plain MC",\
			"out_plotdata.txt" using 1:4 with points linecolor rgb "green" title "Estimated Error,Quasi-Random MC",\
			"out_plotdata.txt" using 1:5 with points linecolor rgb "green" title "Real Error,Quasi-Random MC",\
			f(x) with lines linecolor rgb "blue" title "1/sqrt(N)"; \
	'| gnuplot

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg
