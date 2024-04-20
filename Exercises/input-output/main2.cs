using System;
using static System.Console;
using static System.Math;
using System.IO;

public class main2{
	public static void Main(string[] args){
		char[] split_delimiters = {' ','\t','\n'};
		var split_options = StringSplitOptions.RemoveEmptyEntries;
		string[] lines = File.ReadAllLines("input.txt");
		foreach (string line in lines) {
			var numbers = line.Split(split_delimiters,split_options);
			foreach(var number in numbers){
				double x = double.Parse(number);
				Error.WriteLine($"x = {x} Sin(x) = {Sin(x)} Cos(x) = {Cos(x)}");
                		}
        		}
	}//Main
}//main2
