using System;
using System.Reflection;

namespace Lab6
{
    delegate double PlusOrMinus(double p1, double p2);//Обычный делегат
    class Program
    {
        static double Plus(double p1, double p2)//Метод типа делегата PlusOrMinus
        {
            return p1 + p2;
        }
        static double Minus(double p1, double p2)//Метод типа делегата PlusOrMinus
        {
            return p1 - p2;
        }

        //Метод, принимающий разработанный делегат PlusOrMinus
        static void PlusOrMinusMethod(string str, double d1, double d2, PlusOrMinus PlusOrMinusParam)
        {
            double result = PlusOrMinusParam(d1, d2);
            Console.WriteLine(str + result.ToString());
        }

        //Использования обобщенного делегата Func
        static void PlusOrMinusFunc(string str, double d1, double d2, Func<double, double, double> PlusOrMinusParam)
        {
            double result = PlusOrMinusParam(d1, d2);
            Console.WriteLine(str + result.ToString());
        }

        /// <summary>   
        /// Проверка, что у свойства есть атрибут заданного типа 
        /// </summary>        
        /// <returns>Значение атрибута</returns>      
        public static bool GetPropertyAttribute(PropertyInfo checkType, Type attributeType, out object attribute)
        {
            bool Result = false;
            attribute = null;

            //Поиск атрибутов с заданным типом       
            var isAttribute = checkType.GetCustomAttributes(attributeType, false);
            if (isAttribute.Length > 0)
            {
                Result = true;
                attribute = isAttribute[0];
            }

            return Result;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("\t\t1)Работа с делегатами");
            double d1 = 1.1, d2 = 2;
            Console.WriteLine("Числа: " + d1.ToString() + " и " + d2.ToString());

            Console.WriteLine("\t1.Вызов метода с разработанным делегатом");
            PlusOrMinusMethod("Сумма: ", 1.1, 2, Plus);
            PlusOrMinus pm1 = Minus;//экземпляр делегата
            PlusOrMinusMethod("Разность: ", 1.1, 2, pm1);
            PlusOrMinusMethod("Создание экземпляра делегата на основе лямбда-выражения\n", d1, d2, (x, y) => x + y);

            Console.WriteLine("\t2.Вызов метода с обобщенным делегатом Func");
            PlusOrMinusFunc("Сумма: ", 1.1, 2, Plus);
            Func<double, double, double> pm2 = Minus;//экземпляр делегата
            PlusOrMinusFunc("Разность: ", 1.1, 2, pm2);
            PlusOrMinusMethod("Создание экземпляра делегата на основе лямбда-выражения\n", d1, d2, (x, y) => x + y);

            Console.WriteLine("\t\t2)Работа с рефлексией");

            Console.WriteLine("\t1.Информация о конструкторах, свойствах и методах");
            Type t = typeof(ForInspection);
            Console.WriteLine("Конструкторы:");
            foreach (var x in t.GetConstructors())
            {
                Console.WriteLine(x);
            }
            Console.WriteLine("Свойства:");
            foreach (var x in t.GetProperties())
            {
                Console.WriteLine(x);
            }
            Console.WriteLine("Методы:");
            foreach (var x in t.GetMethods())
            {
                Console.WriteLine(x);
            }

            Console.WriteLine("\t2.Свойства, помеченные атрибутом:");
            foreach (var x in t.GetProperties())
            {
                object attrObj;
                if (GetPropertyAttribute(x, typeof(NewAttribute), out attrObj))
                {
                    NewAttribute attr = attrObj as NewAttribute;
                    Console.WriteLine(x.Name + " - " + attr.Description);
                }
            }

            Console.WriteLine("\t3.Вызов метода класса:");
            ForInspection fi = (ForInspection)t.InvokeMember(null, BindingFlags.CreateInstance, null, null, new object[] { });
            object[] parameters = new object[] { 1.2, 3.5 };
            object Result = t.InvokeMember("Plus", BindingFlags.InvokeMethod, null, fi, parameters);
            Console.WriteLine("Plus(1.2,3.5)={0}", Result);

            Console.ReadLine();
        }
    }
}
