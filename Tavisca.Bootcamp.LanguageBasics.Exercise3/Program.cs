using System;
using System.Linq;
using System.Collections.Generic; 

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            int num_dishes = protein.Length;
            int[] cal = new int[num_dishes]; 
            int num_diets = dietPlans.Length;
            int[] res = new int[num_diets];

            for(int i=0;i< num_dishes;i++)
                cal[i]= (protein[i]+carbs[i])*5 + fat[i]*9;
            
            int[] hi_pro= Maxs(protein,num_dishes);
            int[] hi_fat= Maxs(fat,num_dishes);
            int[] hi_carb= Maxs(carbs,num_dishes);
            int[] hi_cal= Maxs(cal,num_dishes);

            int[] lo_pro= Mins(protein,num_dishes);
            int[] lo_fat= Mins(fat,num_dishes);
            int[] lo_carb= Mins(carbs,num_dishes);
            int[] lo_cal= Mins(cal,num_dishes);


            for(int i =0; i<num_diets;i++)
            {
              
                string diet = dietPlans[i];
                if(diet.Length==0)
                {
                    res[i]=0;
                    continue;
                }
                
                switch(diet[0])
                {
                    case 'p': if(lo_pro.Length>1)
                        {
                            res[i]=plan(1,diet,lo_pro,protein,cal,carbs,fat);
                        }
                        else
                            res[i]=lo_pro[0];
                        break;
                    case 'P': if(hi_pro.Length>1)
                                res[i]=plan(1,diet,hi_pro,protein,cal,carbs,fat);
                            else
                                res[i]=hi_pro[0];
                            break;


                    case 't': if(lo_cal.Length>1)
                                res[i]=plan(1,diet,lo_cal,protein,cal,carbs,fat);
                            else
                                res[i]=lo_cal[0];break;

                    case 'T': if(hi_cal.Length>1)
                                res[i]=plan(1,diet,hi_cal,protein,cal,carbs,fat);
                            else
                                res[i]=hi_cal[0];break;


                    case 'f': if(lo_fat.Length>1)
                                res[i]=plan(1,diet,lo_fat,protein,cal,carbs,fat);
                            else
                                res[i]=lo_fat[0];break;

                    case 'F': if(hi_fat.Length>1)
                                res[i]=plan(1,diet,hi_fat,protein,cal,carbs,fat);
                            else
                                res[i]=hi_fat[0];break;


                    case 'c': if(lo_carb.Length>1)
                                res[i]=plan(1,diet,lo_carb,protein,cal,carbs,fat);
                            else
                                res[i]=lo_carb[0];break;

                    case 'C': if(hi_carb.Length>1)
                                res[i]=plan(1,diet,hi_carb,protein,cal,carbs,fat);
                            else
                                res[i]=hi_carb[0];break;

                }

                
                

            }

            //for(int z=0;z<num_diets;z++)
                //System.Console.Write(res[z]+" ");
            
            return res;
            

        }

        public static int plan(int i,string diet,int[] indx, int[] protein, int[] cal, int[] carbs, int[] fat)
       {
           if(indx.Length>1)
           {

               int l=indx.Length;
               List<int> indxList = new List<int>();
                int min,max,res=0;
                int[] nindx;

                if(i>=diet.Length)
                {
                    res=indx[0];
                    return res;
                }

               switch(diet[i])
                {
                    case 'p': 
                        min=9999;
                        for(int j=0;j<l;j++)
                            if(protein[indx[j]]<min)
                                min= protein[indx[j]];

                        for(int j=0;j<l;j++)
                            if(protein[indx[j]]==min)
                                indxList.Add(indx[j]);
                        nindx= indxList.ToArray();

                        res=plan(i+1,diet,nindx,protein,cal,carbs,fat);
                        break;

                    case 'P': 
                        max=-1;
                        for(int j=0;j<l;j++)
                            if(protein[indx[j]]>max)
                                max= protein[indx[j]];

                        for(int j=0;j<l;j++)
                            if(protein[indx[j]]==max)
                                indxList.Add(indx[j]);
                        nindx= indxList.ToArray();

                        res=plan(i+1,diet,nindx,protein,cal,carbs,fat);
                        break;
                            


                    case 't':
                        min=9999;
                        for(int j=0;j<l;j++)
                            if(cal[indx[j]]<min)
                                min= cal[indx[j]];

                        for(int j=0;j<l;j++)
                            if(cal[indx[j]]==min)
                                indxList.Add(indx[j]);
                        nindx= indxList.ToArray();

                        res=plan(i+1,diet,nindx,protein,cal,carbs,fat);
                        break;

                    case 'T': 
                        max=-1;
                        for(int j=0;j<l;j++)
                            if(cal[indx[j]]>max)
                                max= cal[indx[j]];

                        for(int j=0;j<l;j++)
                            if(cal[indx[j]]==max)
                                indxList.Add(indx[j]);
                        nindx= indxList.ToArray();

                        res=plan(i+1,diet,nindx,protein,cal,carbs,fat);
                        break;


                    case 'f': 
                        min=9999;
                        for(int j=0;j<l;j++)
                            if(fat[indx[j]]<min)
                                min= fat[indx[j]];

                        for(int j=0;j<l;j++)
                            if(fat[indx[j]]==min)
                                indxList.Add(indx[j]);
                        nindx= indxList.ToArray();

                        res=plan(i+1,diet,nindx,protein,cal,carbs,fat);
                        break;

                    case 'F':
                        max=-1;
                        for(int j=0;j<l;j++)
                            if(fat[indx[j]]>max)
                                max= fat[indx[j]];

                        for(int j=0;j<l;j++)
                            if(fat[indx[j]]==max)
                                indxList.Add(indx[j]);
                        nindx= indxList.ToArray();

                        res=plan(i+1,diet,nindx,protein,cal,carbs,fat);
                        break;


                    case 'c':
                        min=9999;
                        for(int j=0;j<l;j++)
                            if(carbs[indx[j]]<min)
                                min= carbs[indx[j]];

                        for(int j=0;j<l;j++)
                            if(carbs[indx[j]]==min)
                                indxList.Add(indx[j]);
                        nindx= indxList.ToArray();

                        res=plan(i+1,diet,nindx,protein,cal,carbs,fat);
                        break;

                    case 'C': 
                        max=-1;
                        for(int j=0;j<l;j++)
                            if(carbs[indx[j]]>max)
                                max= carbs[indx[j]];

                        for(int j=0;j<l;j++)
                            if(carbs[indx[j]]==max)
                                indxList.Add(indx[j]);
                        nindx= indxList.ToArray();

                        res=plan(i+1,diet,nindx,protein,cal,carbs,fat);
                        break;

                }
                //System.Console.WriteLine(res);
            return res;
           }
           else
                return indx[0];
       }

               public static int[] Maxs(int[] arr, int lnth)
       {
           int max = arr.Max();
           List<int> index = new List<int>();

           for(int i=0;i<lnth;i++)
           {
               if(arr[i]==max)
               index.Add(i);
           }
           int[] res= index.ToArray();
           return res;
       }

        public static int[] Mins(int[] arr, int lnth)
       {
           int min = arr.Min();
           List<int> index = new List<int>();

           for(int i=0;i<lnth;i++)
           {
               if(arr[i]==min)
               index.Add(i);
           }
           int[] res= index.ToArray();
           return res;
       }
       
    
    }
}
