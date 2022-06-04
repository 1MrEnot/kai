namespace Sys
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    internal class FirstSecondPass
    {
        private const int MaxMemoryAdr = 16777215; //максимальное значение адреса  оперативной памяти
        private int _addressCount;

        //Таблица исходного кода
        private int _endAddress; //Адрес точки входа в программу
        public string ErrorMessage = "";
        private string _nameProg;
        private int _startAddress; // Адрес начала программы

        //| Метка| МКОП | Операнд1 | Операнд2 | Вспомогательная таблица
        private readonly List<List<string>> _supportTable = new List<List<string>>();

        //| Метка| Адрес | Таблица символических имен
        private readonly List<List<string>> _symbolNameTable = new List<List<string>>();

        private void AddStringToSupportTable(string n1, string n2, string n3, string n4)
        {
            _supportTable[0].Add(n1);
            _supportTable[1].Add(n2);
            _supportTable[2].Add(n3);
            _supportTable[3].Add(n4);
        }

        private bool MemoryCheck()
        {
            if (_addressCount < 0 || _addressCount > MaxMemoryAdr)
            {
                ErrorMessage = "Ошибка! Выход за границы доступной памяти";
                return false;
            }

            return true;
        }

        //передается массив, номер строки который надо разложить по переменным
        //проверяется допустимость введеных символов
        private bool GetRow(string[,] mas, int number, out string label, out string command, out string dir1,
            out string dir2)
        {
            label = mas[number, 0];
            command = mas[number, 1].ToUpper();
            dir1 = mas[number, 2];
            dir2 = mas[number, 3];

            if (CheckHelper.IsDirective(label) || CheckHelper.OnlyRegisters(label))
            {
                return false;
            }

            if (number > 0 && _nameProg == label.ToUpper())
            {
                return false;
            }


            if ((CheckHelper.OnlySymbolsAndNumbers(label) || label.Length == 0) &&
                CheckHelper.OnlySymbolsAndNumbers(command) &&
                (CheckHelper.OnlySymbolsAndNumbers(dir2) || dir2.Length == 0))
            {
                if (label.Length > 0)
                {
                    //метка должна начинаться с символа
                    if (CheckHelper.OnlySymbols(Convert.ToString(label[0])))
                    {
                        return true;
                    }

                    return false;
                }

                return true;
            }

            return false;
        }

        //Проверяет таблицу кодов операций
        public bool CheckOperationCodeTable(ref string[,] operationCodeTable)
        {
            var rows = operationCodeTable.GetLength(0);

            for (var i = 0; i < rows; i++)
            {
                if (operationCodeTable[i, 0] == "" ||
                    operationCodeTable[i, 1] == "" ||
                    operationCodeTable[i, 2] == "")
                {
                    ErrorMessage = "Строка {" + (i + 1) +
                                   "} Ошибка! Пустая ячейка в таблице кодов операций недопустима";
                    return false;
                }

                if (operationCodeTable[i, 0].Length > 6 ||
                    operationCodeTable[i, 1].Length > 2 ||
                    operationCodeTable[i, 2].Length > 1)
                {
                    ErrorMessage = "Строка {" + (i + 1) +
                                   "} Ошибка в размере строки в таблице кодов операций (Команда (от 1 до 6),МКОП (от 1 до 2), Длина(не более 1))";
                    return false;
                }

                if (!CheckHelper.OnlySymbolsAndNumbers(operationCodeTable[i, 0]))
                {
                    ErrorMessage = "Строка {" + (i + 1) + "} Ошибка! В поле команды недопустимый символ";
                    return false;
                }

                //проверяем поле МКОП команды, что там были только hex цифры
                if (CheckHelper.IsAdressPossible(operationCodeTable[i, 1]))
                {
                    if (CheckHelper.IsDirective(operationCodeTable[i, 0]) ||
                        CheckHelper.OnlyRegisters(operationCodeTable[i, 0]))
                    {
                        ErrorMessage = "Строка {" + (i + 1) +
                                       "} Ошибка! Поле код команды является зарезервированным словом";
                        return false;
                    }

                    //преобразуем их в число
                    var count = ConverterHelper.HexToDec(operationCodeTable[i, 1]);
                    if (count > 63)
                    {
                        ErrorMessage = "Строка {" + (i + 1) + "} Ошибка! Поле код команды не должен превышать 3F";
                        return false;
                    }

                    //если оно в пределах байта, но записано просто как ... "F" поправляем его на "0F"
                    if (operationCodeTable[i, 1].Length == 1)
                    {
                        operationCodeTable[i, 1] = ConverterHelper.ToTwoChars(operationCodeTable[i, 1]);
                    }
                }
                else
                {
                    ErrorMessage = "Строка {" + (i + 1) + "} Ошибка! Посторонние символы в поле МКОП";
                    return false;
                }

                if (CheckHelper.OnlyNumbers(operationCodeTable[i, 2]))
                {
                    if (int.TryParse(operationCodeTable[i, 2], out var value))
                    {
                        if (value <= 0 || value > 4)
                        {
                            ErrorMessage = "Строка {" + (i + 1) + "} Ошибка! Проверьте размер команды (от 1 до 4)";
                            return false;
                        }
                    }
                }
                else
                {
                    ErrorMessage = "Строка {" + (i + 1) + "} Ошибка! В поле размер операции недопустимый символ";
                    return false;
                }

                //проверяем уникальность поля названия команды
                for (var k = i + 1; k < rows; k++)
                {
                    var cmpStr1 = operationCodeTable[i, 0];
                    var cmpStr2 = operationCodeTable[k, 0];
                    if (Equals(cmpStr1, cmpStr2))
                    {
                        ErrorMessage = "Строка {" + (i + 1) + "} Ошибка! Найдены совпадения в команде";
                        return false;
                    }
                }

                //проверяем уникальность поля код операции
                for (var k = i + 1; k < rows; k++)
                {
                    var str1 = Convert.ToString(ConverterHelper.HexToDec(operationCodeTable[i, 1]));
                    var str2 = Convert.ToString(ConverterHelper.HexToDec(operationCodeTable[k, 1]));
                    if (Equals(str1, str2))
                    {
                        ErrorMessage = "Строка {" + (i + 1) + "} Ошибка! Найдены совпадения в коде команды";
                        return false;
                    }
                }
            }

            return true;
        }

        //Проверяет каждую ячейку таблицы, имеются ли в ней записи, если в строке нет данных, то она удаляется
        public static void DeleteEmptyRows(DataGridView dbGridSourceCode)
        {
            for (var i = 0; i < dbGridSourceCode.Rows.Count - 1; i++)
            {
                var empty = true;
                for (var j = 0; j < dbGridSourceCode.Rows[i].Cells.Count; j++)
                {
                    if (dbGridSourceCode.Rows[i].Cells[j].Value != null &&
                        dbGridSourceCode.Rows[i].Cells[j].Value.ToString() != "")
                    {
                        empty = false;
                    }
                }

                if (empty)
                {
                    dbGridSourceCode.Rows.Remove(dbGridSourceCode.Rows[i]);
                }
            }
        }

        //Проверяет есть ли такая команда в массиве команд
        private static int FindCodeInCodeTable(string label, string[,] codeTable)
        {
            for (var i = 0; i < codeTable.GetLength(0); i++)
            {
                if (label == codeTable[i, 0])
                {
                    return i;
                }
            }

            return -1;
        }

        //Проверяет есть ли такая метка в массиве меток
        private int FindLabelInLabelTable(string label)
        {
            for (var i = 0; i < _symbolNameTable[0].Count; i++)
            {
                if (label == _symbolNameTable[0][i])
                {
                    return i;
                }
            }

            return -1;
        }

        //Проверяет таблицу исходного кода, ищет директиву START,END.
        //Проверяет адрес памяти, не выходим ли за границу  и т.д.
        //какие символы используются для названия программы
        public bool FirstPass(string[,] sourceCodeTable, string[,] operCodeTable,
            DataGridView dgSupportTable, DataGridView dgSymbolTable)
        {
            _addressCount = 0;
            _startAddress = 0;
            _endAddress = 0;
            //Добавляем "столбцы" в списки списков
            _symbolNameTable.Add(new List<string>());
            _symbolNameTable.Add(new List<string>());

            _supportTable.Add(new List<string>());
            _supportTable.Add(new List<string>());
            _supportTable.Add(new List<string>());
            _supportTable.Add(new List<string>());

            var rows = sourceCodeTable.GetLength(0) - 1;

            var startFlag = 0; //флаг найденной директивы START
            var endFlag = 0; //флаг найденной директивы END

            // Организуем цикл по обработке строк ИСХОДНОГО ТЕКСТА ПРОГРАММЫ
            for (var i = 0; i <= rows; i++)
            {
                //если директива старт найдена
                if (startFlag == 1)
                {
                    //то адрес уже записан в переменную
                    //и надо проверить чтобы он не выходил за диапазон
                    if (_addressCount > MaxMemoryAdr)
                    {
                        ErrorMessage = "Строка {" + (i + 1) + "Ошибка! Произошло переполнение";
                        return false;
                    }
                }

                //Проверяем, если директива END найдена, то можно выходить из цикла
                if (endFlag == 1)
                {
                    break;
                }

                //берем строку из массива и сразу же проверяем корректность данных
                //строка состоит из Label MKOP Operand1 Operand2
                if (!GetRow(sourceCodeTable, i, out var label, out var mkop, out var operand1, out var operand2))
                {
                    ErrorMessage = "Синтаксическая ошибка строка = " + (i + 1);
                    return false;
                }

                // Смотрим сперва  на метку, есть ли она в таблице меток
                //number_in_LabelTable  - укажет на строку в которой она находится
                var numberInLabelTable = FindLabelInLabelTable(label);

                if (numberInLabelTable != -1)
                {
                    ErrorMessage = "Строка {" + (i + 1) + "} Ошибка! Найдена уже существующая метка " + label;
                    return false;
                }
                // если не найдена,то добавляем её и смотрим на МКОП

                //если метка это не пустая строка и встречена после директивы старт
                //то добавляем её в таблицу меток
                if (label != "" && startFlag == 1)
                {
                    _symbolNameTable[0].Add(label);
                    _symbolNameTable[1].Add(ConverterHelper.ToSixChars(ConverterHelper.DecToHex(_addressCount)));
                }


                if (CheckHelper.IsDirective(mkop))
                {
                    switch (mkop)
                    {
                        case "START":
                        {
                            //если у нас старт не в начале массива, и найден в массиве еще раз то ошибка
                            if (i == 0 && startFlag == 0)
                            {
                                startFlag = 1;
                                //смотрим на операнд, символы соответствуют 16ричной сс
                                if (CheckHelper.IsAdressPossible(operand1))
                                {
                                    //если да то преобразуем 16ричное число в 10чное
                                    _addressCount = ConverterHelper.HexToDec(operand1);

                                    _startAddress = _addressCount;
                                    //адрес начала программы не может быть равен 0

                                    if (_addressCount == 0)
                                    {
                                        ErrorMessage = "Строка 1 Ошибка! Адрес начала программы не может быть равен 0";
                                        return false;
                                    }

                                    //адрес начала программы не может превышать объем памяти
                                    if (_addressCount > MaxMemoryAdr || _addressCount < 0)
                                    {
                                        ErrorMessage = "Строка 1 Ошибка! Неправильный адрес загрузки.";
                                        return false;
                                    }

                                    if (label == "")
                                    {
                                        ErrorMessage = "Строка 1 Ошибка! Не задано имя программы";
                                        return false;
                                    }

                                    if (label.Length > 10)
                                    {
                                        ErrorMessage = "Строка 1 Ошибка! Превышена длина имени программы( > 10 символов)";
                                        return false;
                                    }


                                    //теперь помещаем это в выходной массив
                                    AddStringToSupportTable(label, mkop, ConverterHelper.ToSixChars(operand1), "");
                                    _nameProg = label;
                                    //выводим предупреждение если такое имеется
                                    if (operand2.Length > 0)
                                    {
                                        ErrorMessage = ErrorMessage + "Строка {" + (i + 1) +
                                                       "} Второй операнд директивы START не рассматривается\n";
                                    }
                                }
                                else
                                {
                                    ErrorMessage = "Строка 1 Ошибка! Неверный адрес начала программы";
                                    return false;
                                }
                            }
                            else
                            {
                                ErrorMessage = "Строка {" + (i + 1) + "} Ошибка в директиве START";
                                return false;
                            }
                        }
                            break;

                        case "WORD":
                        {
                            //В WORD у нас могут быть записаны только числа (в данной проге только положительные)
                            //преобразовываем операнд в число
                            if (int.TryParse(operand1, out var number))
                            {
                                if (number >= 0 && number <= MaxMemoryAdr)
                                {
                                    if (_addressCount + 3 > MaxMemoryAdr)
                                    {
                                        ErrorMessage = "Ошибка! Произошло переполнение";
                                        return false;
                                    }

                                    if (!MemoryCheck())
                                    {
                                        return false;
                                    }

                                    AddStringToSupportTable(ConverterHelper.ToSixChars(ConverterHelper.DecToHex(_addressCount)),
                                        mkop, Convert.ToString(number), "");

                                    _addressCount = _addressCount + 3;

                                    if (!MemoryCheck())
                                    {
                                        return false;
                                    }

                                    if (_addressCount < 0 || _addressCount > MaxMemoryAdr)
                                    {
                                        ErrorMessage = "Строка {" + (i + 1) +
                                                       "} Ошибка! Выход за границы доступной памяти";
                                        return false;
                                    }

                                    if (operand2.Length > 0)
                                    {
                                        ErrorMessage = ErrorMessage + "Строка {" + (i + 1) +
                                                       "} Второй операнд директивы WORD не рассматривается\n";
                                    }
                                }
                                else
                                {
                                    ErrorMessage = "Строка {" + (i + 1) +
                                                   "} Ошибка! Отрицательное число, либо превышено максимальное значение числа";
                                    return false;
                                }
                            }
                            else
                            {
                                //символ вопроса, резервирует 1 слово в памяти
                                if (operand1.Length == 1 && operand1 == "?")
                                {
                                    if (_addressCount + 3 > MaxMemoryAdr)
                                    {
                                        ErrorMessage = "Ошибка! Произошло переполнение";
                                        return false;
                                    }

                                    AddStringToSupportTable(ConverterHelper.ToSixChars(ConverterHelper.DecToHex(_addressCount)),
                                        mkop, operand1, "");

                                    _addressCount += 3;

                                    if (!MemoryCheck())
                                    {
                                        return false;
                                    }

                                    if (operand2.Length > 0)
                                    {
                                        ErrorMessage = ErrorMessage + "Строка {" + (i + 1) +
                                                       "} Второй операнд директивы WORD не рассматривается\n";
                                    }
                                }
                                else
                                {
                                    ErrorMessage = "Строка {" + (i + 1) +
                                                   "} Ошибка! Невозможно выполнить преобразование в число " + operand1;
                                    return false;
                                }
                            }
                        }
                            break;
                        case "BYTE":
                        {
                            //пытаемся преобразовать операнд в число (разрешено только положительное 0 до 255)

                            if (int.TryParse(operand1, out var number))
                            {
                                if (number >= 0 && number <= 255)
                                {
                                    if (_addressCount + 1 > MaxMemoryAdr)
                                    {
                                        ErrorMessage = "Ошибка! Произошло переполнение";
                                        return false;
                                    }

                                    //BYTE = 1 байт, увеличиваем адрес
                                    AddStringToSupportTable(ConverterHelper.ToSixChars(ConverterHelper.DecToHex(_addressCount)),
                                        mkop, Convert.ToString(number), "");

                                    _addressCount = _addressCount + 1;

                                    if (!MemoryCheck())
                                    {
                                        return false;
                                    }


                                    if (operand2.Length > 0)
                                    {
                                        ErrorMessage = ErrorMessage + "Строка {" + (i + 1) +
                                                       "} Второй операнд директивы BYTE не рассматривается\n";
                                    }
                                }

                                else
                                {
                                    ErrorMessage = "Строка {" + (i + 1) +
                                                   "} Ошибка! Отрицательное число, либо превышеноо максимальное значение числа";
                                    return false;
                                }
                            }
                            //если преобразование в число не получилось, значит разбираем строку
                            else
                            {
                                //первый символ 'C' второй и последний символ это кавычки и длина строки >3
                                var symbols = CheckHelper.String(operand1);
                                if (symbols != "")
                                {
                                    if (_addressCount + symbols.Length > MaxMemoryAdr)
                                    {
                                        ErrorMessage = "Ошибка! Произошло переполнение";
                                        return false;
                                    }

                                    AddStringToSupportTable(ConverterHelper.ToSixChars(ConverterHelper.DecToHex(_addressCount)),
                                        mkop, operand1, "");

                                    _addressCount += symbols.Length;

                                    if (!MemoryCheck())
                                    {
                                        return false;
                                    }


                                    if (operand2.Length > 0)
                                    {
                                        ErrorMessage = ErrorMessage + "Строка {" + (i + 1) +
                                                       "} Второй операнд директивы BYTE не рассматривается\n";
                                    }

                                    continue;
                                }

                                //первый символ 'X' второй и последний символ это кавычки и длина строки >3
                                symbols = CheckHelper.ByteString(operand1);

                                if (symbols != "")
                                {
                                    var lenght = symbols.Length;
                                    //1 символ = 1 байт = 2 цифры в 16ричной системе = четное число символов
                                    if (lenght % 2 == 0)
                                    {
                                        if (_addressCount + symbols.Length / 2 > MaxMemoryAdr)
                                        {
                                            ErrorMessage = "Ошибка! Произошло переполнение";
                                            return false;
                                        }

                                        AddStringToSupportTable(
                                            ConverterHelper.ToSixChars(ConverterHelper.DecToHex(_addressCount)),
                                            mkop, operand1, "");

                                        _addressCount = _addressCount + symbols.Length / 2;

                                        if (!MemoryCheck())
                                        {
                                            return false;
                                        }

                                        if (operand2.Length > 0)
                                        {
                                            ErrorMessage = ErrorMessage + "Строка {" + (i + 1) +
                                                           "} Второй операнд директивы BYTE не рассматривается \n";
                                        }

                                        continue;
                                    }

                                    ErrorMessage = "Строка {" + (i + 1) +
                                                   "} Ошибка! Невозможно преобразовать BYTE нечетное количество символов";
                                    return false;
                                }

                                //если там всего один символ "?"
                                if (operand1.Length == 1 && operand1 == "?")
                                {
                                    if (_addressCount + 1 > MaxMemoryAdr)
                                    {
                                        ErrorMessage = "Ошибка! Произошло переполнение";
                                        return false;
                                    }

                                    AddStringToSupportTable(ConverterHelper.ToSixChars(ConverterHelper.DecToHex(_addressCount)),
                                        mkop, operand1, "");

                                    _addressCount = _addressCount + 1;

                                    if (!MemoryCheck())
                                    {
                                        return false;
                                    }

                                    if (operand2.Length > 0)
                                    {
                                        ErrorMessage = ErrorMessage + "Строка {" + (i + 1) +
                                                       "} Второй операнд директивы BYTE не рассматривается \n";
                                    }

                                    continue;
                                }

                                ErrorMessage = "Строка {" + (i + 1) + "} Ошибка! Неверный формат строки " + operand1;
                                return false;
                            }
                        }
                            break;

                        case "RESB":
                        {
                            if (int.TryParse(operand1, out var number))
                            {
                                if (number > 0)
                                {
                                    if (_addressCount > MaxMemoryAdr)
                                    {
                                        ErrorMessage = "Ошибка! Переполнение памяти";
                                        return false;
                                    }

                                    if (_addressCount + number > MaxMemoryAdr)
                                    {
                                        ErrorMessage = "Ошибка! Произошло переполнение";
                                        return false;
                                    }

                                    AddStringToSupportTable(ConverterHelper.ToSixChars(ConverterHelper.DecToHex(_addressCount)),
                                        mkop, Convert.ToString(number), "");

                                    _addressCount = _addressCount + number; //WORD = 3 байта, увеличиваем адрес

                                    if (!MemoryCheck())
                                    {
                                        return false;
                                    }

                                    if (operand2.Length > 0)
                                    {
                                        ErrorMessage = ErrorMessage + "Строка {" + (i + 1) +
                                                       "} Второй операнд директивы RESB не рассматривается \n";
                                    }
                                }
                                else
                                {
                                    ErrorMessage = "Строка {" + (i + 1) +
                                                   "} Ошибка! Количество байт равно нулю или меньше нуля";
                                    return false;
                                }
                            }
                            else
                            {
                                ErrorMessage = "Строка {" + (i + 1) +
                                               "} Ошибка! Невозможно выполнить преобразование в число " + operand1;
                                return false;
                            }
                        }
                            break;
                        case "RESW":
                        {
                            if (int.TryParse(operand1, out var number))
                            {
                                if (number > 0)
                                {
                                    if (_addressCount + number * 3 > MaxMemoryAdr)
                                    {
                                        ErrorMessage = "Произошло переполнение";
                                        return false;
                                    }

                                    AddStringToSupportTable(ConverterHelper.ToSixChars(ConverterHelper.DecToHex(_addressCount)),
                                        mkop, Convert.ToString(number), "");

                                    //WORD = 3 байта, увеличиваем адрес
                                    _addressCount = _addressCount + number * 3;

                                    if (!MemoryCheck())
                                    {
                                        return false;
                                    }

                                    if (_addressCount < 0 || _addressCount > MaxMemoryAdr)
                                    {
                                        ErrorMessage = "Строка {" + (i + 1) + "} Выход за границы доступной памяти";
                                        return false;
                                    }

                                    if (operand2.Length > 0)
                                    {
                                        ErrorMessage = ErrorMessage + "Строка {" + (i + 1) +
                                                       "} Второй операнд директивы RESW не рассматривается \n";
                                    }
                                }
                                else
                                {
                                    ErrorMessage = "Строка {" + (i + 1) +
                                                   "} Количество слов равно нулю или меньше нуля";
                                    return false;
                                }
                            }
                            else
                            {
                                ErrorMessage = "Строка {" + (i + 1) + "} Невозможно выполнить преобразование в число " +
                                               operand1;
                                return false;
                            }
                        }
                            break;

                        case "END":
                        {
                            if (startFlag == 1)
                            {
                                endFlag = 1;
                                if (operand1.Length == 0)
                                {
                                    _endAddress = _startAddress;
                                }
                                else
                                {
                                    if (CheckHelper.IsAdressPossible(operand1))
                                    {
                                        //если да то преобразуем 16ричное число в 10чное
                                        _endAddress = ConverterHelper.HexToDec(operand1);
                                        if (_endAddress >= _startAddress && _endAddress <= _addressCount)
                                        {
                                            break;
                                        }

                                        ErrorMessage = "Строка {" + (i + 1) + "} Адрес точки входа неверен";
                                        return false;
                                    }

                                    ErrorMessage = "Строка {" + (i + 1) + "} Неверный адрес входа в программу";
                                    return false;
                                }
                            }
                            else
                            {
                                ErrorMessage = "Строка {" + (i + 1) + "} Ошибка в директиве END";
                                return false;
                            }
                        }
                            break;
                    }
                }

                //значит в строке команда, обрабатываем тут
                else
                {
                    //Если в строке МКОП что-то написано
                    if (mkop.Length > 0)
                    {
                        //Смотрим есть такой МКОП в таблице
                        var num = FindCodeInCodeTable(mkop, operCodeTable);
                        if (num > -1)
                        {
                            //если он есть, то смотрим на длину команды
                            //ДЛИНА КОМАНДЫ = 1
                            //например NOP, операндов нет, а если и есть то не смотрим на них
                            if (operCodeTable[num, 2] == "1")
                            {
                                if (_addressCount + 1 > MaxMemoryAdr)
                                {
                                    ErrorMessage = "Произошло переполнение";
                                    return false;
                                }

                                var addressationType = ConverterHelper.HexToDec(operCodeTable[num, 1]) * 4;

                                AddStringToSupportTable(ConverterHelper.ToSixChars(ConverterHelper.DecToHex(_addressCount)),
                                    ConverterHelper.ToTwoChars(ConverterHelper.DecToHex(addressationType)), "", "");

                                _addressCount = _addressCount + 1;

                                if (!MemoryCheck())
                                {
                                    return false;
                                }

                                if (operand1.Length > 0 || operand2.Length > 0)
                                {
                                    ErrorMessage = ErrorMessage + "Строка {" + (i + 1) +
                                                   "} Операнды не рассматриваются в команде " + operCodeTable[num, 0] +
                                                   "\n";
                                }
                            }
                            else
                                //ДЛИНА КОМАНДЫ = 2
                                //ADD r1,r2   операнды это регистры, либо число //INT 200
                            if (operCodeTable[num, 2] == "2")
                            {
                                //сначала пытаемся преобразовать первый операнд в число
                                if (int.TryParse(operand1, out var number))
                                {
                                    if (number >= 0 && number <= 255)
                                    {
                                        //так как операнд является числом, то это непосредственная адресация
                                        //просто  сдвигаем на два разряда влево
                                        var addressationType = ConverterHelper.HexToDec(operCodeTable[num, 1]) * 4;

                                        if (_addressCount + 2 > MaxMemoryAdr)
                                        {
                                            ErrorMessage = "Произошло переполнение";
                                            return false;
                                        }

                                        AddStringToSupportTable(
                                            ConverterHelper.ToSixChars(ConverterHelper.DecToHex(_addressCount)),
                                            ConverterHelper.ToTwoChars(ConverterHelper.DecToHex(addressationType)), operand1, "");

                                        _addressCount = _addressCount + 2;

                                        if (!MemoryCheck())
                                        {
                                            return false;
                                        }

                                        if (operand2.Length > 0)
                                        {
                                            ErrorMessage = ErrorMessage + "Строка {" + (i + 1) +
                                                           "} Второй операнд команды" + operCodeTable[num, 0] +
                                                           " не рассматривается \n";
                                        }
                                    }

                                    else
                                    {
                                        ErrorMessage = "Строка {" + (i + 1) +
                                                       "} Отрицательное число, либо превышено максимальное значение числа";
                                        return false;
                                    }
                                }
                                else
                                {
                                    //если первый и второй операнд - регистры
                                    if (CheckHelper.OnlyRegisters(operand1) && CheckHelper.OnlyRegisters(operand2))
                                    {
                                        //так как оба операнда регистры то это регистровая(регистровой==непосредственной) адресация
                                        //просто  сдвигаем на два разряда влево
                                        var addressationType = ConverterHelper.HexToDec(operCodeTable[num, 1]) * 4;
                                        if (_addressCount + 2 > MaxMemoryAdr)
                                        {
                                            ErrorMessage = "Произошло переполнение";
                                            return false;
                                        }

                                        AddStringToSupportTable(
                                            ConverterHelper.ToSixChars(ConverterHelper.DecToHex(_addressCount)),
                                            ConverterHelper.ToTwoChars(ConverterHelper.DecToHex(addressationType)), operand1,
                                            operand2);

                                        _addressCount = _addressCount + 2;

                                        if (!MemoryCheck())
                                        {
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage = "Строка {" + (i + 1) + "} Ошибка в команде " +
                                                       operCodeTable[num, 0];
                                        return false;
                                    }
                                }
                            }
                            else
                                //ДЛИНА КОМАНДЫ = 3
                            if (operCodeTable[num, 2] == "3")
                            {
                                if (_addressCount + 3 > MaxMemoryAdr)
                                {
                                    ErrorMessage = "Произошло переполнение";
                                    return false;
                                }

                                var addressationType = ConverterHelper.HexToDec(operCodeTable[num, 1]) * 4 + 1;

                                AddStringToSupportTable(ConverterHelper.ToSixChars(ConverterHelper.DecToHex(_addressCount)),
                                    ConverterHelper.ToTwoChars(ConverterHelper.DecToHex(addressationType)), operand1, operand2);

                                _addressCount = _addressCount + 3;

                                if (!MemoryCheck())
                                {
                                    return false;
                                }

                                if (operand2.Length > 0)
                                {
                                    ErrorMessage = ErrorMessage + "Строка {" + (i + 1) + "} Второй операнд команды" +
                                                   operCodeTable[num, 0] + "  не рассматривается\n";
                                }
                            }
                            else
                                //ДЛИНА КОМАНДЫ = 4
                            if (operCodeTable[num, 2] == "4")
                            {
                                if (_addressCount + 4 > MaxMemoryAdr)
                                {
                                    ErrorMessage = "Произошло переполнение";
                                    return false;
                                }

                                var addressationType = ConverterHelper.HexToDec(operCodeTable[num, 1]) * 4 + 1;

                                AddStringToSupportTable(ConverterHelper.ToSixChars(ConverterHelper.DecToHex(_addressCount)),
                                    ConverterHelper.ToTwoChars(ConverterHelper.DecToHex(addressationType)), operand1, operand2);

                                _addressCount = _addressCount + 4;

                                if (!MemoryCheck())
                                {
                                    return false;
                                }

                                if (operand2.Length > 0)
                                {
                                    ErrorMessage = ErrorMessage + "Строка {" + (i + 1) + "} Второй операнд команды" +
                                                   operCodeTable[num, 0] + " не рассматривается\n";
                                }
                            }
                            else
                            {
                                ErrorMessage = "Строка {" + (i + 1) + "} Размер команды больше установленного";
                                return false;
                            }
                        }
                        else
                        {
                            ErrorMessage = "Строка {" + (i + 1) +
                                           "} МКОП не найден в таблице исходный текст программы " + mkop;
                            return false;
                        }
                    }
                    else
                    {
                        ErrorMessage = "Строка {" + (i + 1) + "} Ошибка в МКОП";
                        return false;
                    }
                }
            }

            if (endFlag == 0)
            {
                ErrorMessage = ErrorMessage + "Не найдена точка входа в программу \n";
                return false;
            }

            //Помещаем сформированную Вспомагательную таблицу в датагрид
            for (var i = 0; i < _supportTable[0].Count; i++)
            {
                dgSupportTable.Rows.Add();
                dgSupportTable.Rows[i].Cells[0].Value = _supportTable[0][i];
                dgSupportTable.Rows[i].Cells[1].Value = _supportTable[1][i];
                dgSupportTable.Rows[i].Cells[2].Value = _supportTable[2][i];
                dgSupportTable.Rows[i].Cells[3].Value = _supportTable[3][i];
            }

            //Помещаем сформированную Таблицу вспомогательных имен в датагрид
            for (var j = 0; j < _symbolNameTable[1].Count; j++)
            {
                dgSymbolTable.Rows.Add();
                dgSymbolTable.Rows[j].Cells[0].Value = _symbolNameTable[0][j];
                dgSymbolTable.Rows[j].Cells[1].Value = _symbolNameTable[1][j];
            }


            return true;
        }

        //проверка опреанда во втором проходе
        //Если в операнде метка - возращает адрес метки
        //Если в операнде регистр - возвращает номер регистра
        //Если там строка типа C"????" - возвращает ASCII код
        //Если там строка типа X"????" - возвращает строку
        //Если что-то в 10ричном формате - то вернет это же число в 16ричном формате
        //иначе возращает пустую строку
        private string CheckingOperandSecondPass(string operand1, out int error, out int label)
        {
            label = 0;
            var result = "";
            error = 0;
            if (operand1 != "")
            {
                //если там метка - то возвращаем адрес метки
                var labelStringNum = FindLabelInLabelTable(operand1);
                if (labelStringNum > -1)
                {
                    label = 1;
                    return _symbolNameTable[1][labelStringNum];
                }

                //если в операнде регистр
                var regnum = CheckHelper.RegisterNumber(operand1);
                if (regnum > -1)
                {
                    return ConverterHelper.DecToHex(regnum);
                }

                //если в операнде только цифры
                if (CheckHelper.OnlyNumbers(operand1))
                {
                    return ConverterHelper.DecToHex(Convert.ToInt32(operand1));
                }

                var sentence = CheckHelper.String(operand1);
                if (sentence != "")
                {
                    return ConverterHelper.ToAscii(sentence);
                }

                sentence = CheckHelper.ByteString(operand1);
                if (sentence != "")
                {
                    return sentence;
                }

                //Если перепробованы все комбинации, значит ошибка
                error = 1;
            }

            return result;
        }

        //Второй проход
        public bool Second_pass(ListBox binaryCode)
        {
            ErrorMessage = "";
            //запускаем его для каждой строки Вспомогательной таблицы
            for (var i = 0; i < _supportTable[0].Count; i++)
            {
                var address = _supportTable[0][i];
                var mkop = _supportTable[1][i];
                var operand1 = _supportTable[2][i];
                var operand2 = _supportTable[3][i];

                //Если строка первая, то это директива Старт
                if (i == 0)
                {
                    var str = ConverterHelper.EditingString("H", _supportTable[0][0], _supportTable[2][0], "",
                        Convert.ToString(_addressCount - _startAddress), "");
                    binaryCode.Items.Add(str);
                }
                //Если строка не первая, то снова смотрим, команда там или директива. Интересуют RESB и  RESW, т.к. из значение операндов отражается только в длинне записи
                else
                {
                    var result1 = CheckingOperandSecondPass(operand1, out var error, out var label1);

                    if (error == 1)
                    {
                        ErrorMessage = ErrorMessage + "Строка {" + (i + 1) +
                                       "} Ошибка в операнде, код отсутствует в ТСИ " + operand1;
                        break;
                    }

                    var result2 = CheckingOperandSecondPass(operand2, out error, out _);

                    if (error == 1)
                    {
                        ErrorMessage = ErrorMessage + "Строка {" + (i + 1) +
                                       "} Ошибка в операнде,код отсутствует в ТСИ " + operand2;
                        break;
                    }

                    if (CheckHelper.IsDirective(mkop))
                    {
                        if (mkop == "RESB")
                        {
                            mkop = "";
                            var str1 = ConverterHelper.EditingString("T", address, mkop, result1, "", "");
                            binaryCode.Items.Add(str1);
                            continue;
                        }

                        if (mkop == "RESW")
                        {
                            mkop = "";
                            var str2 = ConverterHelper.EditingString("T", address, mkop,
                                ConverterHelper.ToTwoChars(ConverterHelper.DecToHex(Convert.ToInt32(operand1) * 3)), "", "");
                            binaryCode.Items.Add(str2);
                            continue;
                        }

                        if (mkop == "BYTE")
                        {
                            mkop = "";
                            var str2 = ConverterHelper.EditingString("T", address, mkop,
                                ConverterHelper.ToTwoChars(ConverterHelper.DecToHex(result1.Length + result2.Length)), result1,
                                result2);
                            binaryCode.Items.Add(str2);
                            continue;
                        }

                        if (mkop == "WORD")
                        {
                            mkop = "";
                            result1 = ConverterHelper.ToSixChars(result1);
                            var str2 = ConverterHelper.EditingString("T", address, mkop,
                                ConverterHelper.ToTwoChars(ConverterHelper.DecToHex(result1.Length + result2.Length)), result1,
                                result2);
                            binaryCode.Items.Add(str2);
                            continue;
                        }

                        if (mkop == "BYTE" && operand1 == "?")
                        {
                            mkop = "";
                            var str2 = ConverterHelper.EditingString("T", address, mkop,
                                ConverterHelper.ToTwoChars(ConverterHelper.DecToHex(1)), "", "");
                            binaryCode.Items.Add(str2);
                            continue;
                        }

                        if (mkop == "WORD" && operand1 == "?")
                        {
                            mkop = "";
                            var str2 = ConverterHelper.EditingString("T", address, mkop,
                                ConverterHelper.ToTwoChars(ConverterHelper.DecToHex(3)), "", "");
                            binaryCode.Items.Add(str2);
                        }
                    }
                    else
                    {
                        // Проверяем что команда работает с тем, что разрешено адресацией
                        // сначала смотрим на тип адресации, если там  01 , значит это прямая
                        //и в операндах может быть только метка
                        var typeOfAdr = (byte)ConverterHelper.HexToDec(mkop) & 0x03;
                        if (typeOfAdr == 1)
                        {
                            if (label1 != 1)
                            {
                                ErrorMessage = ErrorMessage + "Строка {" + (i + 1) +
                                               "} Для данного типа адресации операнд должен быть меткой " + mkop;
                                binaryCode.Items.Clear();
                                return false;
                            }

                            if (result2 != "")
                            {
                                ErrorMessage = ErrorMessage + "Строка {" + (i + 1) +
                                               "} Данный тип адрессации поддерживает 1 операнд";
                                binaryCode.Items.Clear();
                                return false;
                            }
                        }

                        var recordLength
                            = ConverterHelper.ToTwoChars(ConverterHelper.DecToHex(mkop.Length + result1.Length + result2.Length));
                        var str5 = ConverterHelper.EditingString("T", address, mkop, recordLength, result1, result2);
                        binaryCode.Items.Add(str5);
                    }
                }
            }

            var str3 = ConverterHelper.EditingString("E", ConverterHelper.ToSixChars(ConverterHelper.DecToHex(_endAddress)), "", "", "",
                "");
            binaryCode.Items.Add(str3);

            if (ErrorMessage != "")
            {
                binaryCode.Items.Clear();
            }

            return true;
        }
    }
}