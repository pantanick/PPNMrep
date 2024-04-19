CODE= $(filter %.cs,$^)
LIBS= $(addprefix -reference:,$(filter %.dll,$^))
MKEXE= mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB= mcs -target:library -out:$@ $(LIBS) $(CODE)

Out.txt: main.exe
	mono $< > $@

main.exe: main.cs vec.dll; $(MKEXE)

vec.dll: vec.cs
	$(MKLIB)

	
clean:
	$(RM) *.exe *.dll [Oo]ut* [Ll]og*
