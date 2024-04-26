class main_lngamma{
        public static void Main(){
        for(double x=1.0/32;x<=5;x+=1.0/32){
                System.Console.WriteLine($"{x} {sfuns.lngamma(x)}");
                }
        }//Main

}//main_gamma
