using System.Linq; // to use Sum() method on objects
class main {

	public static int Main(string[] args){
		int nterms=(int)1e8;
		foreach(string arg in args){
			var words = arg.Split(':');
			if(words[0]=="-nterms")nterms=(int)double.Parse(words[1]);
			}
		System.Console.Write($"Main: nterms={nterms}\n");
		var sum = new
			System.Threading.ThreadLocal<double>(()=>0,trackAllValues:true);
		System.Threading.Tasks.Parallel.For(1,nterms+1,delegate(int i){sum.Value+=1.0/i;});
		double total=0;
		foreach(double s in sum.Values)total+=s;
		System.Console.Write($"Main: total sum        = {total}\n");
		System.Console.Write($"Main: sum.Values.Sum() = {sum.Values.Sum()}\n");

	return 0;
	}//Main

}//main
