default: cmath.dll main.exe Out.txt

cmath.dll: cmath.cs complex.cs
	mcs -target:library -out:./cmath.dll $^

main.exe: main.cs cmath.dll
	mcs -reference:cmath.dll -target:exe -out:main.exe main.cs

Out.txt: main.exe
	@mono $< > $@

clean:
	$(RM) *.dll *.exe [Oo]ut* 
