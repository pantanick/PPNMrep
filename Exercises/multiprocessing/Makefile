CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)
TIME = time --output=$@ --append --format "$$nthreads %e %U"

Out.times.svg: out.times.txt Makefile
	echo '\
		set terminal svg background "white" ;\
		set out "$@" ;\
		set xlabel "number of threads";\
		set ylabel "real time (s)";\
		set title "Running time as function of numnber of threads";\
		plot "$<" with lp ;\
		'| tee log.gpi | gnuplot

N = 2e8
out.times.txt: main.exe
	for nthreads in 1 2 3 4; do\
		$(TIME) mono main.exe -nthreads:$$nthreads -nterms:$(N); \
	done

main.exe: main.cs ; $(MKEXE)
main-for.exe: main-for.cs ; $(MKEXE)

test: out1 out2 out3 out4
out1: main.exe ; mono $< -nthreads:1 -nterms:2e8 > $@
out2: main.exe ; mono $< -nthreads:1 -nterms:2e8 > $@
out3: main.exe ; mono $< -nthreads:1 -nterms:2e8 > $@
out4: main.exe ; mono $< -nthreads:1 -nterms:2e8 > $@

test2: main.exe
	mono $< -nthreads:1 -nterms:8e8 > out1 &
	mono $< -nthreads:1 -nterms:8e8 > out2 &
	mono $< -nthreads:1 -nterms:8e8 > out3 &
	mono $< -nthreads:1 -nterms:8e8 > out4 &


clean:
	$(RM) *.exe *.dll [Oo]ut* [Ll]og*
