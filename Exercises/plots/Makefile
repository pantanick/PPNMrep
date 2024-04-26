CODE= $(filter %.cs,$^)
LIBS= $(addprefix -reference:,$(filter %.dll,$^))
MKEXE= mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB= mcs -target:library -out:$@ $(LIBS) $(CODE)

default: Out.erf.gnuplot.svg Out.gamma.gnuplot.svg Out.lngamma.gnuplot.svg

Out.erf.gnuplot.svg: Out.txt Makefile
	echo '\
		set terminal svg background "white" ;\
		set out "$@" ;\
		set key left ;\
		set xlabel "x" ;\
		set ylabel "erf(x)" ;\
		set tics out ;\
		set grid ;\
		set title "Error function" ;\
		plot [-3:3]\
			 "$<" with lines notitle \
			,"erf.data.txt" using 1:2 with points pointtype 6 notitle \
			,"erf.data.txt" using (-($$1)):(-($$2)) with points pointtype 6 notitle ;\
	' | tee log.gpi  | gnuplot

Out.txt: main.exe
	mono $< > $@

sfuns.dll: sfuns.cs ; $(MKLIB)

main.exe: main.cs sfuns.dll; $(MKEXE)

Out.gamma.gnuplot.svg: Out.gamma.txt Makefile
	echo '\
		set terminal svg background "white";\
		set out "$@" ;\
		set key left ;\
		set xlabel "x" ;\
		set ylabel "gamma (x)";\
		set tics out ;\
		set grid ;\
		set title "Gamma function" ;\
		plot [0:5] \
			"$<" with lines notitle \
			' | tee log_gamma.gpi | gnuplot

Out.gamma.txt: main_gamma.exe
	mono $< > $@

main_gamma.exe: main_gamma.cs sfuns.dll ; $(MKEXE)

Out.lngamma.gnuplot.svg: Out.lngamma.txt Makefile
	echo '\
                set terminal svg background "white";\
                set out "$@" ;\
                set key left ;\
                set xlabel "x" ;\
                set ylabel "Ln(gamma(x))";\
                set tics out ;\
                set grid ;\
                set title "Ln(gamma function)" ;\
                plot [0:5] \
                        "$<" with lines notitle \
                        ' | tee log_lngamma.gpi | gnuplot


Out.lngamma.txt: main_lngamma.exe
	mono $< > $@

main_lngamma.exe: main_lngamma.cs sfuns.dll ; $(MKEXE)


clean:
	$(RM) *.exe *.dll [Oo]ut* [Ll]og*
