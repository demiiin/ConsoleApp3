// This file is part of ProjectName, and is subject to the project license.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace PvsStudioDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Возможное разыменование null-ссылки (V3042)
            string str = null;
            if (str.Length > 0) // <- Ошибка: NullReferenceException
            {
                Console.WriteLine("String is not empty!");
            }

            // 2. Утечка памяти из-за неправильной подписки на событие (V3119)
            var publisher = new EventPublisher();
            for (int i = 0; i < 10; i++)
            {
                var subscriber = new EventSubscriber(publisher);
                // Подписчик не отписывается, что может привести к утечке
            }

            // 3. Деление на ноль (V3064)
            int a = 10, b = 0;
            int result = a / b; // <- DivideByZeroException

            // 4. Переполнение буфера (V3106)
            int[] numbers = new int[5];
            for (int i = 0; i <= 5; i++) // <- Выход за границы массива
            {
                numbers[i] = i;
            }

            // 5. Бесполезное условие (V3022)
            bool condition = true;
            if (condition == true) // <- Избыточная проверка
            {
                Console.WriteLine("Condition is always true!");
            }

            // 6. Неиспользуемая переменная (V3018)
            int unusedVar = 42;

            // 7. Опасное сравнение double (V3024)
            double x = 0.1 + 0.2;
            if (x == 0.3) // <- Неточное сравнение double
            {
                Console.WriteLine("Unexpected comparison result!");
            }
        }
    }

    class EventPublisher
    {
        public event EventHandler SomethingHappened;

        public void RaiseEvent()
        {
            SomethingHappened?.Invoke(this, EventArgs.Empty);
        }
    }

    class EventSubscriber
    {
        public EventSubscriber(EventPublisher publisher)
        {
            publisher.SomethingHappened += HandleEvent; // Подписка без отписки
        }

        private void HandleEvent(object sender, EventArgs e)
        {
            Console.WriteLine("Event handled!");
        }
    }
}