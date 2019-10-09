using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsApplication1.Moduli
{

    class CFGReader
    {

        //Parametri
       public static List<String> Param = new List<string>();
        public static List<String> Attr = new List<string>();

        public static void lmain()
        {
            //Caricamento dei parametri di lettura configurabili
            ///////////////////////////////////////////////////

            //Header
            
            Param.Add("header");  //val bool bar on off
            Param.Add("himg");  //val img bg bar

        }

      

        //Algoritmo per La lettura della configurazione da file
        //in ordine in base alla lista Param

        public static void ReadCFG(String Path)
        {
            System.IO.StreamReader R = new System.IO.StreamReader(Path + "\\data\\Default\\config.cfg");
            String riga;

            while ((riga = R.ReadLine()) != null)
            {
                if (riga.StartsWith("//"))
                {
                    continue;
                }
                else
                {
                    int i = 0;
                    for (i = 0; i < Param.Count; i++)
                    {
                        if(riga.StartsWith(Param[i])) {
                            String prm = riga.Remove(0, riga.IndexOf(":") + 1);
                            Attr.Add(prm);
                        }
                    }
                 }
             }
             
        }


        


    }
}
