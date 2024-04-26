class main_gamma{
	public static void Main(){
	for(double x=-5;x<=5;x+=1.0/32){
		System.Console.WriteLine($"{x} {sfuns.gamma(x)}");
		}
	}//Main

}//main_gamma

