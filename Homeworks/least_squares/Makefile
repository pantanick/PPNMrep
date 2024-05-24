CSC = mcs
RUN = mono

all: plot.svg out.txt; cat out.txt

out.txt: main.exe
	$(RUN) main.exe -input:data.txt > out.txt

main.exe: main.cs lib.dll leastsquares.dll
	$(CSC) -reference:lib.dll -reference:leastsquares.dll $<

lib.dll: vector.cs matrix.cs qrgs.cs
	$(CSC) -target:library -out:$@ $^

leastsquares.dll: leastsquares.cs
	$(CSC) -target:library -reference:lib.dll -out:$@ $^

plot.svg: plotdata.txt Makefile
	echo '\
		set terminal svg background "white"; \
		set grid; \
		set output "plot.svg"; \
		set title "Decay of ThX"; \
		set xlabel "Time (Days)"; \
		set ylabel "Per-cent Activity"; \
		plot "plotdata.txt" using 1:2 title "Fitted Curve (least squares)" with lines, \
		     "plotdata.txt" using 1:3:4 title "Original Data" with yerrorbars, \
		     "plotdata.txt" using 1:5 title "Upper error bound of fit" with lines, \
		     "plotdata.txt" using 1:6 title "Lower error bound of fit" with lines; \
	' | gnuplot

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg *.gpi
