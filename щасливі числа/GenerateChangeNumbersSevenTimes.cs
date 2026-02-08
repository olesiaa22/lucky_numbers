using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

/// @class GenerateChangeNumbersSevenTimes
/// @brief Генерує числа, що складаються лише з цифр 4 і 7 та змінюються рівно 7 разів.
///
/// Даний клас рекурсивно будує числа, використовуючи тільки символи '4' та '7'.
/// "Зміна" рахується тоді, коли дві сусідні цифри відрізняються.
///
/// Приклади:
/// - 444777 має 1 зміну (4 → 7)
/// - 474747 має 5 змін (4→7, 7→4, ...)
///
/// У процесі генерації всі числа, що відповідають умові,
/// зберігаються у списку, а результатом є останнє згенероване число
/// та його позиція у цьому списку.
public class GenerateChangeNumbersSevenTimes
{
    /// @brief Генерує останнє число, яке має рівно 7 змін між цифрами, та його позицію.
    ///
    /// Метод запускає рекурсивний процес побудови чисел.
    /// Усі числа, що містять рівно 7 переходів між 4 та 7,
    /// додаються у список.
    ///
    /// @return Кортеж:
    /// - Item1: позиція (індекс) останнього знайденого числа у списку
    /// - Item2: останнє згенероване коректне число типу BigInteger
    public static Tuple<int, BigInteger> GenerateNumber()
    {
        List<BigInteger> numbers = new List<BigInteger>();
        int position = GenerateNumbersHelper(new StringBuilder(), 7, numbers);
        return Tuple.Create(position, numbers[numbers.Count - 1]);
    }

    /// @brief Допоміжний рекурсивний метод для генерації чисел із заданою кількістю змін.
    ///
    /// Метод будує числа шляхом додавання цифр '4' та '7'.
    /// Якщо поточне число має потрібну кількість змін,
    /// воно додається у список результатів.
    ///
    /// @param currentNumber Поточне число, яке будується рекурсивно.
    /// @param requiredChanges Необхідна кількість змін між сусідніми цифрами.
    /// @param numbers Список для збереження всіх правильних чисел.
    ///
    /// @return Позиція останнього знайденого правильного числа або -1, якщо нічого не знайдено.
    private static int GenerateNumbersHelper(
        StringBuilder currentNumber,
        int requiredChanges,
        List<BigInteger> numbers)
    {
        int position = -1;

        // Перевірка поточного числа
        if (currentNumber.Length > 0)
        {
            int changes = CountChanges(currentNumber.ToString());
            if (changes == requiredChanges)
            {
                numbers.Add(BigInteger.Parse(currentNumber.ToString()));
                position = numbers.Count;
            }
        }

        // Продовження рекурсії, якщо довжина не перевищує ліміт
        if (currentNumber.Length < 20) // Обмеження максимальної довжини
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

    /// @brief Підраховує кількість змін цифр у числі.
    ///
    /// Зміна відбувається тоді, коли поточна цифра
    /// відрізняється від попередньої.
    ///
    /// Приклад:
    /// - "44477" має 1 зміну (4 → 7)
    /// - "4747" має 3 зміни (4→7, 7→4, 4→7)
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
