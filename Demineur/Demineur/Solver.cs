using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demineur
{
    class Solver
    {
        public int[] getDLL(String board)
        {
            // Obtenir l'assemblage de notre DLL
            Assembly SampleAssembly = Assembly.LoadFrom(@"MineSweeperSolver.dll");

            // Sachant que notre DLL ne contient qu'une seule classe
            // l'on va chercher l'objet "Type" correspondant
            Type myType = SampleAssembly.GetTypes()[0];

            // À partir de cet objet, obtenir la méthode désirée
            MethodInfo Method = myType.GetMethod("possiblePlay");

            // Utiliser la méthode statique de la classe Activator pour
            // créer l'instance de notre classe
            object myInstance = Activator.CreateInstance(myType);

            // Puisque notre méthode attend un paramètre, créer le tableau
            // des paramètres qui n'en contient qu'un seul (valeur : 2)
            object[] parametersArray = new object[] {board};

            // Appeler la méthode en passant l'instance de l'objet ainsi que
            // les paramètre.
            int[] result = (int[])(Method.Invoke(myInstance, parametersArray));

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Réponse du DLL : " + result[i]);
            }
            return result;

            Console.ReadLine();
        }

    }


}
