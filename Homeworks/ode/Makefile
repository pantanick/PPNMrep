CSC = mcs
RUN = mono

all: plot_friction.svg plot_harmonic.svg lib.dll out.txt;
	cat out.txt

main.exe: main.cs lib.dll
	$(CSC) -reference:lib.dll -out:$@ main.cs

out.txt: main.exe
	$(RUN) main.exe > out.txt


lib.dll: vector.cs matrix.cs ode.cs
	$(CSC) -target:library -out:$@ $^

plot_harmonic.svg: out.txt Makefile
	echo '\
		set terminal svg background "white"; \
		set grid; \
		set output "plot_harmonic.svg"; \
		set title "Harmonic oscillator"; \
		set xlabel "x"; \
		set tics out; \
		set ylabel "RK12 value"; \
		plot	"out_harmonic.txt" using 1:2 with lines title "y",\
			"out_harmonic.txt" using 1:3 with lines title "dy/dx"; \
	'| gnuplot

plot_friction.svg: out.txt Makefile
	echo '\
		set terminal svg background "white"; \
		set grid; \
		set output "plot_friction.svg"; \
		set title "Oscillator with friction"; \
		set xlabel "x"; \
		set tics out; \
		set ylabel "RK12 value"; \
		plot	"out.txt" using 1:2 with lines title "y",\
			"out.txt" using 1:3 with lines title "dy/dx"; \
	'| gnuplot

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg
