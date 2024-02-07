Out.txt: main.exe #main
	mono main.exe > Out.txt
#	./main >> Out.txt

main.exe: main.cs
	mcs -out:main.exe -target:exe main.cs

#main: main.cc
#	$(CXX) -o main main.cc

#test:
#	echo $(CXX)

.PHONEY: clean
clean:
	rm -f main.exe main Out.txt
