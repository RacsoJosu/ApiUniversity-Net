using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace LinQSnipets
{
    public class Snippets
    {

        static public void BasicLinq()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat León"
            };

            // selecionar los carros 

            var carlist = from car in cars select car;

            foreach ( var car in carlist )
            {
                Console.WriteLine(car);
            }

            // seleccionar con filtro 
            var audiList = from car in cars where car.Contains("Audi") select car;

            foreach( var audi in audiList)
            {
                Console.WriteLine(audi);
            }
            


        }

        // ejemplo con numeros 

        static public void LinqNumbers() {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 8, 9, 9, 9, 10, 11, 12, 13, 13, 13, 14, 15, 16, 17, 18, 19, 20, };
            // iterar y multiplicar por 3 
            // tomar los numeros menos 9
            // ordenar los datos 
            var processedNumberList =
                numbers
                    .Select(num => num * 3)
                    .Where(num => num != 9 || num != 13)
                    .OrderBy(num => num);


        
        }
        //
        static public void SearchExamples()
        {
            List<string> textList = new List<string> { 
                   "a",
                   "bx",
                   "c",
                   "d",
                   "e",
                   "cj",
                   "f", 
                   "c"
            
            };

            // encontrar el primero d elos elementos 
            var first = textList.First();
            // el ultimi elemento
            var last = textList.Last();
            // primer elemeto que contenga "C"
            var cText = textList.First(text => text.Equals("c"));
            // el primer elemento que contenga "J"
            var jText = textList.First(text => text.Contains("j"));
            // enctroar un elemento o por defecto 
            var fistOrDefaulText = textList.FirstOrDefault(text => text.Contains("z"));
            // elegir valor unico 
            var uniqueText = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8, 10, 12 , 14 };
            int[] otherEvenNumber = { 0, 2, 6, 10 };
            var myEvenNumbers = evenNumbers.Except(otherEvenNumber);     


        }

        static void MultipleSelect()
        {
            string[] myOpinions =
            {
                "Opinion 1, text 1",
                "Opinion 2, text 2",
                "Opinion 3, text 3",
                "Opinion 4, text 4",
                "Opinion 5, text 5",
                "Opinion 6, text 6",
            };

            var opinionSelections = myOpinions.SelectMany(myOpinion => myOpinion.Split(","));

            Enterprise[] empresas = new Enterprise[]
            {
                new Enterprise{
                Id = 1,
                Name = "Empresa 1",
                employees = new Employee[]
                {
                    new Employee
                    {
                        Id= 1,
                        Name="Oscar Vallecillo",
                        email="oscar@correo.com",
                        Salary=4000
                    },
                    new Employee
                    {
                        Id= 2,
                        Name="Daniel Vallecillo",
                        email="daniel@correo.com",
                        Salary=6000
                    },
                    new Employee
                    {
                        Id= 3,
                        Name="Daniela Vallecillo",
                        email="Daniela@correo.com",
                        Salary=8000
                    },
                }

                },
                new Enterprise
                {
                    Id = 2,
                Name = "Empresa 2",
                employees = new Employee[]
                {
                    new Employee
                    {
                        Id= 3,
                        Name="Greysi Aguilar",
                        email="greysi@correo.com",
                        Salary=4000
                    },
                    new Employee
                    {
                        Id= 4,
                        Name="Doris Aguilar",
                        email="doris@correo.com",
                        Salary=6000
                    },
                    new Employee
                    {
                        Id= 5,
                        Name="Armando Rodirguez",
                        email="armando@correo.com",
                        Salary=8000
                    },
                }

            }

            };


            // obtener todos los empleados de todas las empresas

            var employeeList = empresas.SelectMany(enterprise => enterprise.employees);

            // saber si tenemos una lista vacia 
            bool hasEnterprices = empresas.Any();

            // empresas que tienen empleados 
            bool hasEmployees = empresas.Any(enterprise => enterprise.employees.Any());

            // todas as empresas con salios mayores a 1000
            bool hasEmployeeWithSalaryMoreThan1000 = empresas.Any(
                    enterprise => enterprise.employees.Any(employee => employee.Salary >= 1000)
                );


        }

        static public void linqCollections()
        {
            var firstList = new List<string> { "a", "b", "c" };
            var secondList = new List<string> { "a", "b", "c", "d", "e", "f" };

            // inner join 
            var commonList1 = from element in firstList
                              join secondElemet in secondList
                              on element equals secondElemet
                              select new { element, secondElemet };
            var commonList2 = firstList.Join(
                secondList, // la lista con la que vamos a mergear
                element => element, // la pk de la primera lista o el valor que nos relaiona
                secondElement => secondElement, // el valor que relaciona una lista con otra
                (element, secondElement)=> new {element, secondElement} // la cción que vamos a hacer
                );
            // outer join - left

            var commonList3 = from element in firstList
                              join secondElement in secondList
                              on element equals secondElement
                              into temporalList
                              from temporalElement in temporalList.DefaultIfEmpty()
                              where element != temporalElement
                              select new { Element = element };

            var leftOuterJoin = from element in firstList
                                from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                select new { Element = element, SecondElement = secondElement };

            // right outer join
            var commonList4 = from secondElement in secondList
                              join element in firstList
                              on secondElement equals element
                              into temporalList
                              from temporalElement in temporalList.DefaultIfEmpty()
                              where secondElement != temporalElement
                              select new { Element = secondElement };

            // full outer join o concatenacion union 
            var unionList = commonList3.Union(commonList4);

        }


        public static void SkipTakeLinq()
        {
            //lista de numeros del 1 al 10
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10,11,12,13,14
            };

            // funciones de skip
            var skipTwoElement = myList.Skip(2);

            var skipLastTwo = myList.SkipLast(2);

            var skipWile = myList.SkipWhile(num => num < 4);
            //funciones de take 

            var takeFirstTwoValues = myList.Take(2);

            var takeLastTwoValues = myList.TakeLast(2);

            var takeWhile = myList.TakeWhile(num => num < 4);


        }




    }
}
