CODE= $(filter %.cs,$^)
LIBS= $(addprefix -reference:,$(filter %.dll,$^))
MKEXE= mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB= mcs -target:library -out:$@ $(LIBS) $(CODE)

default: Makefile Out.txt Out2.txt Out3.txt

Out.txt: main.exe Makefile
	mono main.exe -numbers:-2,-1,0,1,2 1> $@

main.exe: main.cs
	$(MKEXE)

Out2.txt: main2.exe input.txt
	mono main2.exe < input.txt 2> $@

main2.exe: main2.cs
	$(MKEXE)

Out3.txt: main3.exe input3.txt
	mono main3.exe -input:input3.txt -output:$@ $<

main3.exe: main3.cs
	$(MKEXE)



clean:
	$(RM) *.exe *.dll [Oo]ut* [Ll]og*
