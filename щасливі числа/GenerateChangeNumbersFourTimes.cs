using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

/// @class GenerateChangeNumbersFourTimes
/// @brief Генерує числа, що складаються лише з цифр 4 і 7 та змінюються рівно 4 рази.
///
/// Даний клас рекурсивно створює числа, використовуючи тільки цифри '4' та '7'.
/// "Зміна" визначається як момент, коли дві сусідні цифри не однакові.
///
/// Приклади:
/// - 44447777 має 1 зміну (4 → 7)
/// - 474747 має 5 змін
/// - 447744 має 3 зміни
///
/// Метою є знайти всі числа, що містять рівно 4 переходи між цифрами,
/// та повернути останнє знайдене число разом із його позицією.
public class GenerateChangeNumbersFourTimes
{
    /// @brief Генерує останнє число, яке має рівно 4 зміни між цифрами, та його позицію.
    ///
    /// Метод запускає рекурсивну генерацію чисел.
    /// Усі числа, що мають рівно 4 переходи між цифрами 4 та 7,
    /// додаються у список.
    ///
    /// @return Кортеж:
    /// - Item1: позиція (індекс) останнього коректного числа у списку
    /// - Item2: останнє згенероване число типу BigInteger
    public static Tuple<int, BigInteger> GenerateNumber()
    {
        List<BigInteger> numbers = new List<BigInteger>();
        int position = GenerateNumbersHelper(new StringBuilder(), 4, numbers);
        return Tuple.Create(position, numbers[numbers.Count - 1]);
    }

    /// @brief Допоміжний рекурсивний метод для побудови чисел із заданою кількістю змін.
    ///
    /// Метод поступово додає до поточного числа цифри '4' або '7'.
    /// Якщо поточне число містить рівно необхідну кількість змін,
    /// воно додається у список результатів.
    ///
    /// @param currentNumber Поточне число, яке будується рекурсивно.
    /// @param requiredChanges Кількість змін між сусідніми цифрами, яку потрібно досягти.
    /// @param numbers Список, у який додаються всі правильні числа.
    ///
    /// @return Позиція останнього знайденого правильного числа або -1, якщо нічого не знайдено.
    private static int GenerateNumbersHelper(
        StringBuilder currentNumber,
        int requiredChanges,
        List<BigInteger> numbers)
    {
        int position = -1;

        // Перевірка, чи поточне число не порожнє
        if (currentNumber.Length > 0)
        {
            int changes = CountChanges(currentNumber.ToString());

            // Якщо кількість змін дорівнює необхідній — додаємо число
            if (changes == requiredChanges)
            {
                numbers.Add(BigInteger.Parse(currentNumber.ToString()));
                position = numbers.Count;
            }
        }

        // Продовження генерації, якщо не перевищено максимальну довжину
        if (currentNumber.Length < 20) // Обмеження довжини числа
        {
            // Додаємо цифру '4'
            currentNumber.Append("4");
            int newPosition = GenerateNumbersHelper(currentNumber, requiredChanges, numbers);

            if (newPosition != -1)
                position = newPosition;

            currentNumber.Length--; // Повернення назад (backtracking)

            // Додаємо цифру '7'
            currentNumber.Append("7");
            newPosition = GenerateNumbersHelper(currentNumber, requiredChanges, numbers);

            if (newPosition != -1)
                position = newPosition;

            currentNumber.Length--; // Повернення назад
        }

        return position;
    }

    /// @brief Підраховує кількість змін між сусідніми цифрами у числі.
    ///
    /// Зміна фіксується тоді, коли поточна цифра
    /// відрізняється від попередньої.
    ///
    /// Приклад:
    /// - "44477" має 1 зміну (4 → 7)
    /// - "4747" має 3 зміни
    ///
    /// @param number Число у вигляді рядка.
    /// @return Кількість переходів між різними цифрами.
    private static int CountChanges(string number)
    {
        int changes = 0;

        for (int i = 1; i < number.Length; i++)
        {
            if (number[i] != number[i - 1])
            {
                changes++;
            }
        }

        return changes;
    }
}
