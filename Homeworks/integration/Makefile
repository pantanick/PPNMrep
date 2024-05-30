CSC = mcs
RUN = mono

all: out.txt plot_erf.svg lib.dll;
	cat out.txt

main.exe: main.cs lib.dll
	$(CSC) -reference:lib.dll -out:$@ main.cs

out.txt: main.exe
	$(RUN) main.exe > out.txt

lib.dll: integrate.cs
	$(CSC) -target:library -out:$@ $^

plot_erf.svg: Makefile
	echo '\
		set terminal svg background "white"; \
		set grid; \
		set output "plot_erf.svg"; \
		set title "Error function"; \
		set xlabel "z"; \
		set tics out; \
		set ylabel "erf(z)"; \
		unset key; \
		plot	"out_erf.txt" using 1:2 with points,\
	'| gnuplot

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg
