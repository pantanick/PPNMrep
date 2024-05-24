CSC = mcs
RUN = mono

all: qplot.svg linplot.svg out2.txt out.txt;
	cat out.txt out2.txt

out.txt: main.exe
	$(RUN) $< > $@ 2> out_linplotdata.txt

out_qplotdata.txt: qspline.exe
	$(RUN) qspline.exe 2> $@

out_linplotdata.txt: main.exe
	$(RUN) main.exe 2> $@

out2.txt: qspline.exe
	$(RUN) $< > $@ 2> out_qplotdata.txt

qspline.exe: qspline.cs lib.dll
	$(CSC) -reference:lib.dll $< 2> out_compiler_errors.txt

main.exe: main.cs lib.dll
	$(CSC) -reference:lib.dll $< 2> out_compiler_errors.txt

lib.dll: vector.cs matrix.cs
	$(CSC) -target:library -out:$@ $^ 2> out_compiler_errors.txt

linplot.svg: out_linplotdata.txt out_cospoints.txt Makefile
	echo '\
       		set terminal svg background "white"; \
 		set grid; \
		set output "plot.svg"; \
		set title "Linear spline of cos(x)"; \
		set xlabel "x"; \
		set tics out; \
		set key bottom right; \
		set ylabel "cos(x)"; \
		plot 	"out_linplotdata.txt" using 1:2 title "Calculated spline" with dot, \
			"out_linplotdata.txt" using 1:3 with lines linecolor rgb "blue" title "Antiderivative (analytic)", \
			"out_cospoints.txt" using 1:2 with points pointtype 6 title "Given cos(x)", \
		'| gnuplot

qplot.svg: out_qplotdata.txt out_sinpoints.txt Makefile
	echo '\
		set terminal svg background "white"; \
		set grid; \
		set output "qplot.svg"; \
		set title "Quadratic spline of sin(x)"; \
		set xlabel "x"; \
		set tics out; \
		set key bottom right; \
		set ylabel "sin(x)"; \
		plot	"out_qplotdata.txt" using 1:2 title "Calculated spline" with dot, \
			"out_qplotdata.txt" using 1:3 with lines linecolor rgb "blue" title "Spline derivative", \
			"out_qplotdata.txt" using 1:4 with lines linecolor rgb "green" title "Spline integral", \
			"out_sinpoints.txt" using 1:2 with points pointtype 6 title "Given sin(x)", \
		'| gnuplot
clean:

	$(RM) *.dll *.exe [Oo]ut* *.svg
