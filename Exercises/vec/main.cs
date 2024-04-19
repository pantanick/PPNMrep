using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(){
	vec v = new vec(1,2,3);
	vec u = new vec(2,4,6);
	v.print("v = ");
	WriteLine(v.ToString());
	u.print("u = ");
	WriteLine(u.ToString());
	(-v).print("-v = ");
	(-u).print("-u = ");
	(2 * v).print("2v = ");
	(u / 3).print("u/3 = ");
	(v + u).print("v+u = ");
	(u - v).print("u-v = ");
	(v - u).print("v-u = ");
	WriteLine($"v.u = {v.dot(u)}");
	WriteLine($"v.v = {v.dot(v)}");
	WriteLine($"u.u = {u.dot(u)}");
	WriteLine($"v = {v.dot(u)}");
	WriteLine($"v = v :{v.approx(v)}");
	WriteLine($"v = u :{v.approx(u)}");


	}//Main
}//main
