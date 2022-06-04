namespace Sys
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    class FirstSecondPass
    {
        string ProgName;
        public string ErrorMessage = "";
        int StartAddress = 0; // Адрес начала программы
        int EndAddress = 0;   //Адрес точки входа в программу
        int AddressCount = 0;
        const int MAX_MemmoryAdr = 16777215; //максимальное значение адреса  оперативной памяти

        //| Метка| МКОП | Операнд1 | Операнд2 | Вспомогательная таблица
        List<List<string>> SupportTable = new List<List<string>>();

        //| Метка| Адрес | Таблица символических имен
        List<List<string>> SymbolTable = new List<List<string>>();

        //Таблица исходного кода
        List<string> BinaryCode = new List<string>();

        Form1 form1 = new Form1();

        public void AddStringToSupportTable(string n1, string n2, string n3, string n4)
        {
            SupportTable[0].Add(n1);
            SupportTable[1].Add(n2);
            SupportTable[2].Add(n3);
            SupportTable[3].Add(n4);
        }

        public bool MemoryCheck()
        {
            if (AddressCount < 0 || AddressCount > MAX_MemmoryAdr)
            {
                ErrorMessage = "Исходный текст программы --> Строка {1} Ошибка! Выход за границы доступной памяти";
                return false;
            }
            return true;
        }

        //передается массив, номер строки который надо разложить по переменным
        //проверяется допустимость введеных символов
        public bool GetRow(string[,] mas, int number, out string label, out string command, 
                           out string dir1, out string dir2)
        {
            label = mas[number, 0];
            command = mas[number, 1].ToUpper();
            dir1 = mas[number, 2];
            dir2 = mas[number, 3];

            if (CheckData.IsDirective(label) || CheckData.OnlyRegisters(label))
            {
                return false;
            }

            if (number > 0 && ProgName == label.ToUpper())
                return false;


            if ((CheckData.OnlySymbolsAndNumbers(label) || label.Length == 0) && 
                (CheckData.OnlySymbolsAndNumbers(command)) && 
                (CheckData.OnlySymbolsAndNumbers(dir2) || dir2.Length == 0))
            {
                if (label.Length > 0)
                {
                    //метка должна начинаться с символа
                    if (CheckData.OnlySymbols(Convert.ToString(label[0])))
                        return true;
                    else
                        return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        //Процедура проверяет таблицу кодов операций 
        public bool CheckOperationCodeTable(ref string[,] OperationCodeTable) //проверяем таблицу  операций
        {
            int rows = OperationCodeTable.GetLength(0);

            for (int i = 0; i < rows; i++)
            {

                if (OperationCodeTable[i, 0] == "" || 
                    OperationCodeTable[i, 1] == "" || 
                    OperationCodeTable[i, 2] == "")
                {
                    ErrorMessage = "Строка {" + (i + 1) + "} Ошибка! Пустая ячейка в ТАБЛИЦЕ КОДОВ ОПЕРАЦИЙ недопустима";
                    return false;
                }

                if (OperationCodeTable[i, 0].Length > 6 ||
                    OperationCodeTable[i, 1].Length > 2 ||
                    OperationCodeTable[i, 2].Length > 1)
                {
                    ErrorMessage = "Строка {" + (i + 1) + "} Ошибка в размере строки в ТАБЛИЦЕ КОДОВ ОПЕРАЦИЙ " +
                                   "(Команда (от 1 до 6),МКОП (от 1 до 2), Длина(не более 1))";
                    return false;
                }

                if (OperationCodeTable[i, 2] == "1" ||
                    OperationCodeTable[i, 2] == "2" ||
                    OperationCodeTable[i, 2] == "4")
                { }
                else
                {
                    ErrorMessage = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! Нет команды такого размера:" + 
                        OperationCodeTable[i, 2] + " байт";
                    return false;
                }

                if (!CheckData.OnlySymbolsAndNumbers(OperationCodeTable[i, 0]))
                {
                    ErrorMessage = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! В поле команды недопустимый символ";
                    return false;
                }

                //changed code
                if(CheckData.OnlyNumbers(OperationCodeTable[i, 0]))
                {
                    ErrorMessage = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! Некорректный МКОп";
                    return false;
                }

                //проверяем поле МКОП команды, что там были только 16чные цифры
                if (CheckData.IsAdressPossible(OperationCodeTable[i, 1]))
                {

                    if (CheckData.IsDirective(OperationCodeTable[i, 0]) || 
                        CheckData.OnlyRegisters(OperationCodeTable[i, 0]))
                    {
                        ErrorMessage = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! Поле код команды " + OperationCodeTable[i, 0] + " является зарезервированным словом";
                        return false;
                    }
                    //преобразуем их в число
                    int count = Converting.HexToDec(OperationCodeTable[i, 1]);
                    if (count > 63)
                    {
                        ErrorMessage = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! Поле код команды " + OperationCodeTable[i, 0] + " не должен превышать 3F";
                        return false;
                    }
                    else
                    {
                        //если оно в пределах байта, но записано просто как ... "F" поправляем его на "0F"
                        if (OperationCodeTable[i, 1].Length == 1)
                            OperationCodeTable[i, 1] = Converting.ToTwoChars(OperationCodeTable[i, 1]);
                    }
                }
                else
                {
                    ErrorMessage = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! Посторонние символы в поле МКОп";
                    return false;
                }


                int value = 0;
                if (CheckData.OnlyNumbers(OperationCodeTable[i, 2]))
                {
                    bool result = Int32.TryParse(OperationCodeTable[i, 2], out value);
                    if (result)
                    {
                        if (value <= 0 || value > 4)
                        {
                            ErrorMessage = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! Проверьте размер команды (от 1 до 4)";
                            return false;
                        }
                    }
                }
                else
                {
                    ErrorMessage = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! В поле размер операции недопустимый символ";
                    return false;
                }

                //проверяем уникальность поля названия команды
                for (int k = i + 1; k < rows; k++)
                {
                    string cmp_str1 = OperationCodeTable[i, 0];
                    string cmp_str2 = OperationCodeTable[k, 0];
                    if (Equals(cmp_str1, cmp_str2))
                    {
                        ErrorMessage = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! Найдены совпадения в команде";
                        return false;
                    }
                }

                //проверяем уникальность поля код операции
                for (int k = i + 1; k < rows; k++)
                {
                    string cmp_str1 = Convert.ToString(Converting.HexToDec(OperationCodeTable[i, 1]));
                    string cmp_str2 = Convert.ToString(Converting.HexToDec(OperationCodeTable[k, 1]));
                    if (Equals(cmp_str1, cmp_str2))
                    {
                        ErrorMessage = "Таблица кодов операций --> Строка {" + (i + 1) + "} Ошибка! Найдены совпадения в коде команды";
                        return false;
                    }
                }
            }

            return true;
        }

        //Метод проверяет каждую ячейку таблицы, имеются ли в ней записи, если в строке нет данных, то она удаляется
        public void DeleteEmptyRows(DataGridView DBGrid_source_code)
        {
            for (int i = 0; i < DBGrid_source_code.Rows.Count - 1; i++)
            {
                bool empty = true;
                for (int j = 0; j < DBGrid_source_code.Rows[i].Cells.Count; j++)
                    if ((DBGrid_source_code.Rows[i].Cells[j].Value != null) && (DBGrid_source_code.Rows[i].Cells[j].Value.ToString() != ""))
                        empty = false;

                if (empty)
                {
                    DBGrid_source_code.Rows.Remove(DBGrid_source_code.Rows[i]);
                }
            }
        }

        //проверяет есть ли такая команда в массиве команд
        public int FindCodeInCodeTable(string label, string[,] code_table)
        {
            for (int i = 0; i < code_table.GetLength(0); i++)
            {
                if (label == code_table[i, 0])
                    return i;
            }
            return -1;
        }

        //проверяет есть ли такая метка в массиве меток
        public int FindLabelInLabelTable(string label)
        {
            for (int i = 0; i < SymbolTable[0].Count; i++)
            {
                if (label == SymbolTable[0][i])
                    return i;
            }
            return -1;
        }

        //проверка опреанда во втором проходе
        //Если в операнде метка - возращает адрес метки
        //Если в операнде регистр - возвращает номер регистра
        //Если там строка типа C"????" - возвращает ASCII код
        //Если там строка типа X"????" - возвращает строку
        //Если что-то в 10ричном формате - то вернет это же число в 16ричном формате
        //иначе возращает пустую строку
        public string CheckingOperandSecondPass(string operand1, out int error, out int operandIslabel, int adrType, out string tune_address, int index)
        {
            operandIslabel = 0;
            tune_address = "";
            string result = "";
            error = 0;

            if (operand1 != "")
            {
                //что нам делать с Операндом-меткой зависит от типа адресации
                switch (adrType)
                {
                    //прямая адресация
                    case 0:
                        {
                            int LabelStringNum = FindLabelInLabelTable(operand1);
                            if (LabelStringNum > -1)
                            {
                                operandIslabel = 1; //ключ ,что операндом является символьное имя
                                tune_address = SupportTable[0][index];
                                return result = SymbolTable[1][LabelStringNum];
                            }
                            break;
                        }
                    //относительная адресация

                    case 1:
                        {
                            //убираем [] у операнда
                            if (operand1[0] == '[' && operand1[operand1.Length - 1] == ']') //убираем [] у операнда
                            {

                                string temp = operand1;
                                temp = temp.Substring(1, temp.Length - 2);
                                int LabelStringNum = FindLabelInLabelTable(temp);
                                if (LabelStringNum > -1)
                                {
                                    operandIslabel = 1; //ключ ,что операндом является символьное имя
                                    return Converting.SubHex(SymbolTable[1][LabelStringNum], SupportTable[0][index + 1]);
                                }
                            }
                            break;
                        }
                    //смешанная адресация
                    case 2:
                        {
                            int LabelStringNum = FindLabelInLabelTable(operand1);
                            if (LabelStringNum > -1)
                            {
                                operandIslabel = 1; //ключ ,что операндом является символьное имя
                                tune_address = SupportTable[0][index];
                                return result = SymbolTable[1][LabelStringNum];
                            }

                            if (operand1[0] == '[' && operand1[operand1.Length - 1] == ']')//убираем [] у операнда
                            {
                                string temp = operand1;
                                temp = temp.Substring(1, temp.Length - 2);
                                LabelStringNum = FindLabelInLabelTable(temp);
                                if (LabelStringNum > -1)
                                {
                                    operandIslabel = 1; //ключ ,что операндом является символьное имя
                                    return result = Converting.SubHex(SymbolTable[1][LabelStringNum], SupportTable[0][index + 1]);
                                }
                            }
                            break;
                        }
                }

                //если в операнде регистр
                int regnum = CheckData.RegisterNumber(operand1);
                int maxValue = (int)Math.Pow(2, 31) - 1;
                    
                if (regnum > -1)
                {
                    return result = Converting.DecToHex(regnum);
                }
                else
                    //если в операнде только цифры
                    if (CheckData.OnlyNumbers(operand1))
                    {
                        //если число слишком большое то ошибка
                        if (Convert.ToDouble(operand1) > maxValue || Convert.ToDouble(operand1) < (maxValue * -1) + 1)
                            error = 1;
                        else
                            return result = Converting.DecToHex(Convert.ToInt32(operand1));
                    }
                    else
                    {
                        string sentence = "";
                        sentence = CheckData.String(operand1);

                        if (sentence != "")
                        {
                            return Converting.ToASCII(sentence);
                        }

                        sentence = CheckData.ByteString(operand1);

                        if (sentence != "")
                        {
                            return sentence;
                        }

                        //Если перепробованы все комбинации, значит ошибка
                        error = 1;
                    }
            }

            return result;
        }

        //Процедура проверяет таблицу исходного кода, ищет директиву Старт,End. Проверяет адрес памяти, не выходим ли за границу  и т.д.
        //какие символы используются для названия программы
        public bool FirstPass(string[,] SourceCodeTable, string[,] operation_code_table, DataGridView dataGrid_supportTable, DataGridView dataGrid_symbol_table)
        {
            AddressCount = 0;
            StartAddress = 0;
            EndAddress = 0;

            //Добавляем "столбцы" в списки списков "Таблица символьных имен"
            SymbolTable.Add(new List<string>());
            SymbolTable.Add(new List<string>());

            //Добавляем "столбцы" в списки списков "Вспомогательная таблица"
            SupportTable.Add(new List<string>());
            SupportTable.Add(new List<string>());
            SupportTable.Add(new List<string>());
            SupportTable.Add(new List<string>());

            int rows = SourceCodeTable.GetLength(0) - 1;
            
            int StartFlag = 0;                      //флаг найденной директивы старт
            int EndFlag = 0;                        //флаг найденной директивы энд

            string[,] code_table_mas = operation_code_table;

            string label, MKOP, Operand1, Operand2;

            int countStart = 0; //

            //ЗАПУСКАЕМ ЦИКЛ ОБРАБОТКИ КАЖДОЙ СТРОКИ ИСХОДНОГО КОДА
            for (int i = 0; i <= rows; i++)
            {
                //если директива старт найдена
                if (StartFlag == 1)
                {
                    //то адрес уже записан в переменную 
                    //и надо проверить чтобы он не выходил за диапазон
                    if (!MemoryCheck())
                        return false;
                }

                //Проверяем, если директива END найдена, то можно выходить из цикла
                if (EndFlag == 1)
                {
                    break;
                }

                //берем строку из массива и сразу же проверяем корректность данных
                //строка состоит из Label MKOP Operand1 Operand2
                if (!GetRow(SourceCodeTable, i, out label, out MKOP, out Operand1, out Operand2))
                {
                    ErrorMessage = "Исходный текст программы --> Синтаксическая ошибка строка = " + (i + 1);
                    return false;
                }

                // Смотрим сперва  на метку, есть ли она в таблице меток
                // укажет на строку в которой она находится
                if (FindLabelInLabelTable(label) != -1)
                {
                    ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Найдена уже существующая метка " + label;
                    return false;
                }
                // если не найдена,то добавляем её и смотрим на МКОП
                else
                {
                    //если метка это не пустая строка и встречена после директивы старт
                    //то добавляем её в таблицу меток
                    if (label != "" && StartFlag == 1)
                    {
                        SymbolTable[0].Add(label);
                        SymbolTable[1].Add(Converting.ToSixChars(Converting.DecToHex(AddressCount)));
                    }


                    if (CheckData.IsDirective(MKOP))
                        switch (MKOP)
                        {
                            case "START":
                                {
                                    countStart++;
                                    //если у нас старт не в начале массива, и найден в массиве еще раз то ошибка
                                    if (i == 0 && StartFlag == 0)
                                    {
                                        StartFlag = 1;
                                        //смотрим на операнд, символы соответствуют 16ричной сс
                                        if (CheckData.IsAdressPossible(Operand1) || Operand1 == "")
                                        {
                                            //если адрес оставить пустым, то он инициализируется нулём
                                            Operand1 = (Operand1 == "") ? "0" : Operand1;
                                            //если да то преобразуем 16ричное число в 10чное
                                            AddressCount = Converting.HexToDec(Operand1);

                                            StartAddress = AddressCount;
                                            //адрес начала программы не может быть равен 0
                                            if (AddressCount > 0)
                                            {
                                                ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Адрес начала программы должен быть = 0";
                                                return false;
                                            }
                                            //адрес начала программы не может превышать объем памяти
                                            if (AddressCount > MAX_MemmoryAdr)
                                            {
                                                ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Адрес программы выходит за диапазон памяти";
                                                return false;
                                            }

                                            if (label == "")
                                            {
                                                ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Не задано имя программы";
                                                return false;
                                            }
                                            if (label.Length > 10)
                                            {
                                                ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Превышена длина имени программы( > 10 символов)";
                                                return false;
                                            }

                                            //Когда мы задали имя программы, нужно проверить, а совпадает ли оно с названием команды
                                            //так как таблица команд формируется раньше
                                            //до первого прохода
                                            for (int k = 0; k <= code_table_mas.GetUpperBound(0); k++)
                                            {
                                                if (Equals(label, code_table_mas[k, 0]))
                                                {
                                                    ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Имя программы не может совпадать с названием команды";
                                                    return false;
                                                }
                                            }

                                            //теперь помещаем это в выходной массив
                                            AddStringToSupportTable(label, MKOP, Converting.ToSixChars(Operand1), "");
                                            ProgName = label;
                                            //выводим предупреждение если такое имеется
                                            if (Operand2.Length > 0)
                                                ErrorMessage = ErrorMessage + "Исходный текст программы --> Строка {" + (i + 1) + "} Второй операнд директивы START не рассматривается.  \n";

                                        }
                                        else
                                        {
                                            ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Неверный адрес начала программы";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        if (i != 0)
                                        {
                                            if(countStart == 1)
                                            {
                                                ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Директива START должна находится в " +
                                                "1-ой строке во 2-ом столбце.";
                                                return false;
                                            }
                                            else
                                            {
                                                ErrorMessage = "Исходный текст программы --> Ошибка! Директива START встречается несколько раз.";
                                                return false;
                                            }
                                        }                                        
                                    }
                                }

                                break;
                            case "WORD":
                                {
                                    int number;
                                    //В WORD у нас могут быть записаны только числа (в данной проге только положительные)
                                    //преобразовываем операнд в число
                                    if (int.TryParse(Operand1, out number))
                                    {
                                        if (number >= 0 && number <= MAX_MemmoryAdr)
                                        {
                                            if (AddressCount + 3 > MAX_MemmoryAdr)
                                            {
                                                ErrorMessage = "Произошло переполнение";
                                                return false;
                                            }
                                            if (!MemoryCheck())
                                            {
                                                return false;
                                            }

                                            AddStringToSupportTable(Converting.ToSixChars(Converting.DecToHex(AddressCount)), MKOP, 
                                                                    Convert.ToString(number), "");
                                            AddressCount = AddressCount + 3;
                                            if (!MemoryCheck()) { return false; }

                                            if (Operand2.Length > 0)
                                                ErrorMessage = ErrorMessage + "Исходный текст программы --> Строка {" + (i + 1) + "} Второй операнд директивы WORD не рассматривается. \n";
                                        }
                                        else
                                        {
                                            ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Отрицательное число, либо превышено максимальное значение числа";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        //символ вопроса, резервирует 1 слово в памяти
                                        if (Operand1.Length == 1 && Operand1 == "?")
                                        {

                                            if (AddressCount + 3 > MAX_MemmoryAdr)
                                            {
                                                ErrorMessage = "Произошло переполнение";
                                                return false;
                                            }

                                            AddStringToSupportTable(Converting.ToSixChars(Converting.DecToHex(AddressCount)), MKOP, Operand1, "");

                                            AddressCount = AddressCount + 3;

                                            if (!MemoryCheck()) 
                                            { return false; }

                                            if (Operand2.Length > 0)
                                                ErrorMessage = ErrorMessage + "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Второй операнд директивы WORD не рассматривается.  \n";
                                        }
                                        else
                                        {
                                            ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Невозможно выполнить преобразование в число " + Operand1;
                                            return false;
                                        }
                                    }
                                }
                                break;
                            case "BYTE":
                                {
                                    int number;
                                    //пытаемся преобразовать операнд в число (разрешено только положительное 0 до 255)

                                    if (int.TryParse(Operand1, out number))
                                    {
                                        if (number >= 0 && number <= 255)
                                        {

                                            if (AddressCount + 1 > MAX_MemmoryAdr)
                                            {
                                                ErrorMessage = "Произошло переполнение";
                                                return false;
                                            }

                                            //BYTE = 1 байт, увеличиваем адрес
                                            AddStringToSupportTable(Converting.ToSixChars(Converting.DecToHex(AddressCount)), MKOP, 
                                                                    Convert.ToString(number), "");
                                            AddressCount = AddressCount + 1;

                                            if (!MemoryCheck()) 
                                            { return false; }


                                            if (Operand2.Length > 0)
                                                ErrorMessage = ErrorMessage + "Исходный текст программы --> Строка {" + (i + 1) + "} Второй операнд директивы BYTE не рассматривается.  \n";
                                        }

                                        else
                                        {
                                            ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Отрицательное число, либо превышеноо максимальное значение числа";
                                            return false;
                                        }
                                    }
                                    //если преобразование в число не получилось, значит разбираем строку
                                    else
                                    {
                                        //первый символ 'C' второй и последний символ это кавычки и длина строки >3
                                        string symbols = CheckData.String(Operand1);
                                        if (symbols != "")
                                        {

                                            if (AddressCount + symbols.Length > MAX_MemmoryAdr)
                                            {
                                                ErrorMessage = "Произошло переполнение";
                                                return false;
                                            }

                                            AddStringToSupportTable(Converting.ToSixChars(Converting.DecToHex(AddressCount)), MKOP, Operand1, "");

                                            AddressCount = AddressCount + symbols.Length;

                                            if (!MemoryCheck()) 
                                            { return false; }


                                            if (Operand2.Length > 0)
                                                ErrorMessage = ErrorMessage + "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Второй операнд директивы BYTE не рассматривается.  \n";
                                            continue;
                                        }

                                        //первый символ 'X' второй и последний символ это кавычки и длина строки >3
                                        symbols = "";
                                        symbols = CheckData.ByteString(Operand1);
                                        if (symbols != "")
                                        {
                                            int lenght = symbols.Length;
                                            //1 символ = 1 байт = 2 цифры в 16ричной системе = четное число символов
                                            if ((lenght % 2) == 0)
                                            {
                                                if (AddressCount + symbols.Length / 2 > MAX_MemmoryAdr)
                                                {
                                                    ErrorMessage = "Произошло переполнение";
                                                    return false;
                                                }

                                                AddStringToSupportTable(Converting.ToSixChars(Converting.DecToHex(AddressCount)), MKOP, Operand1, "");
                                                AddressCount = AddressCount + symbols.Length / 2;
                                                if (!MemoryCheck()) { return false; }

                                                if (Operand2.Length > 0)
                                                    ErrorMessage = ErrorMessage + "Исходный текст программы --> Строка {" + (i + 1) + "} Второй операнд директивы BYTE не рассматривается.  \n";
                                                continue;
                                            }
                                            else
                                            {
                                                ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Невозможно преобразовать BYTE нечетное количество символов";
                                                return false;
                                            }
                                        }

                                        //если там всего один символ "?"
                                        if (Operand1.Length == 1 && Operand1 == "?")
                                        {
                                            if (AddressCount + 1 > MAX_MemmoryAdr)
                                            {
                                                ErrorMessage = "Произошло переполнение";
                                                return false;
                                            }

                                            AddStringToSupportTable(Converting.ToSixChars(Converting.DecToHex(AddressCount)), MKOP, Operand1, "");
                                            AddressCount = AddressCount + 1;
                                            if (!MemoryCheck()) { return false; }

                                            if (Operand2.Length > 0)
                                                ErrorMessage = ErrorMessage + "Исходный текст программы --> Строка {" + (i + 1) + "} Второй операнд директивы BYTE не рассматривается.  \n";
                                            continue;
                                        }
                                        else
                                        {
                                            ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Неверный формат строки " + Operand1;
                                            return false;
                                        }
                                    }
                                }
                                break;

                            case "RESB":
                                {
                                    int number;
                                    if (int.TryParse(Operand1, out number))
                                    {
                                        if (number > 0)
                                        {

                                            if (AddressCount > MAX_MemmoryAdr)
                                            {
                                                ErrorMessage = "Переполнение памяти";
                                                return false;
                                            }
                                            else
                                            {
                                                if (AddressCount + number > MAX_MemmoryAdr)
                                                {
                                                    ErrorMessage = "Произошло переполнение";
                                                    return false;
                                                }
                                                AddStringToSupportTable(Converting.ToSixChars(Converting.DecToHex(AddressCount)), MKOP, Convert.ToString(number), "");
                                                AddressCount = AddressCount + number;//WORD = 3 байта, увеличиваем адрес
                                                if (!MemoryCheck()) { return false; }

                                                if (Operand2.Length > 0)
                                                    ErrorMessage = ErrorMessage + "Исходный текст программы --> Строка {" + (i + 1) + "} Второй операнд директивы RESB не рассматривается.  \n";
                                            }

                                        }
                                        else
                                        {
                                            ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Количество байт равно нулю или меньше нуля";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Невозможно выполнить преобразование в число " + Operand1;
                                        return false;
                                    }
                                }
                                break;
                            case "RESW":
                                {
                                    int number;
                                    if (int.TryParse(Operand1, out number))
                                    {
                                        if (number > 0)
                                        {

                                            if (AddressCount + number * 3 > MAX_MemmoryAdr)
                                            {
                                                ErrorMessage = "Произошло переполнение";
                                                return false;
                                            }
                                            AddStringToSupportTable(Converting.ToSixChars(Converting.DecToHex(AddressCount)), MKOP, Convert.ToString(number), "");
                                            //WORD = 3 байта, увеличиваем адрес
                                            AddressCount = AddressCount + number * 3;
                                            if (!MemoryCheck()) { return false; }
                                            if (AddressCount < 0 || AddressCount > MAX_MemmoryAdr)
                                            {
                                                ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Выход за границы доступной памяти";
                                                return false;
                                            }


                                            if (Operand2.Length > 0)
                                                ErrorMessage = ErrorMessage + "Исходный текст программы --> Строка {" + (i + 1) + "} Второй операнд директивы RESW не рассматривается.  \n";
                                        }
                                        else
                                        {
                                            ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Количество слов равно нулю или меньше нуля";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Невозможно выполнить преобразование в число " + Operand1;
                                        return false;
                                    }
                                }
                                break;

                            case "END":
                                {
                                    if (StartFlag == 1 && EndFlag == 0)
                                    {
                                        EndFlag = 1;
                                        if (Operand1.Length == 0)
                                        {
                                            EndAddress = StartAddress;
                                            AddStringToSupportTable(Converting.ToSixChars(Converting.DecToHex(AddressCount)), MKOP, Convert.ToString("0"), "");
                                        }
                                        else
                                        {
                                            if (CheckData.IsAdressPossible(Operand1))
                                            {
                                                //если да то преобразуем 16ричное число в 10чное
                                                EndAddress = Converting.HexToDec(Operand1);
                                                if (EndAddress >= StartAddress && EndAddress <= AddressCount)
                                                {
                                                    AddStringToSupportTable(Converting.ToSixChars(Converting.DecToHex(AddressCount)), MKOP, 
                                                                            Convert.ToString(Operand1), "");
                                                    break;
                                                }
                                                else
                                                {
                                                    ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Адрес точки входа неверен";
                                                    return false;
                                                }

                                            }
                                            else
                                            {
                                                ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Неверный адрес входа в программу";
                                                return false;
                                            }
                                        }

                                    }
                                    else
                                    {                                        
                                        ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка в директиве END";
                                        return false;
                                    }
                                }
                                break;
                        }
                    //значит в строке команда, обрабатываем тут
                    else
                    {
                        //Если в строке МКОП что-то написано
                        if (MKOP.Length > 0)
                        {
                            //Смотрим есть такой МКОП в таблице
                            int num = FindCodeInCodeTable(MKOP, code_table_mas);
                            if (num > -1)
                            {
                                //если он есть, то смотрим на длину команды
                                //ДЛИНА КОМАНДЫ = 1
                                //например NOP, операндов нет, а если и есть то не смотрим на них
                                if (code_table_mas[num, 2] == "1")
                                {
                                    if (AddressCount + 1 > MAX_MemmoryAdr)
                                    {
                                        ErrorMessage = "Произошло переполнение";
                                        return false;
                                    }

                                    int AddressationType = Converting.HexToDec(code_table_mas[num, 1]) * 4;

                                    AddStringToSupportTable(Converting.ToSixChars(Converting.DecToHex(AddressCount)), 
                                                            Converting.ToTwoChars(Converting.DecToHex(AddressationType)), "", "");

                                    AddressCount = AddressCount + 1;

                                    if (!MemoryCheck()) 
                                    { return false; }

                                    if (Operand1.Length > 0 || Operand2.Length > 0)
                                        ErrorMessage = ErrorMessage + "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Операнды не рассматриваются в команде " + code_table_mas[num, 0] + ". \n";
                                }
                                else
                                    //ДЛИНА КОМАНДЫ = 2
                                    //ADD r1,r2   операнды это регистры, либо число //INT 200
                                    if (code_table_mas[num, 2] == "2")
                                    {
                                        int number;
                                        //сначала пытаемся преобразовать первый операнд в число
                                        if (int.TryParse(Operand1, out number))
                                        {
                                            if (number >= 0 && number <= 255)
                                            {
                                                //так как операнд является числом, то это непосредственная адресация
                                                //просто  сдвигаем на два разряда влево
                                                int AddressationType = Converting.HexToDec(code_table_mas[num, 1]) * 4;
                                                if (AddressCount + 2 > MAX_MemmoryAdr)
                                                {
                                                    ErrorMessage = "Произошло переполнение";
                                                    return false;
                                                }
                                                AddStringToSupportTable(Converting.ToSixChars(Converting.DecToHex(AddressCount)), 
                                                                        Converting.ToTwoChars(Converting.DecToHex(AddressationType)), Operand1, "");
                                                AddressCount = AddressCount + 2;
                                                if (!MemoryCheck()) { return false; }

                                                if (Operand2.Length > 0)
                                                    ErrorMessage = ErrorMessage + "Исходный текст программы -->  Строка {" + (i + 1) + "} Ошибка! Второй операнд команды " + code_table_mas[num, 0] + " не рассматривается.  \n";
                                            }

                                            else
                                            {
                                                ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Отрицательное число, либо превышено максимальное значение числа";
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            //если первый и второй операнд - регистры
                                            if (CheckData.OnlyRegisters(Operand1) && CheckData.OnlyRegisters(Operand2))
                                            {
                                                //так как оба операнда регистры то это регистровая(регистровой==непосредственной) адресация
                                                //просто  сдвигаем на два разряда влево
                                                int AddressationType = Converting.HexToDec(code_table_mas[num, 1]) * 4;
                                                if (AddressCount + 2 > MAX_MemmoryAdr)
                                                {
                                                    ErrorMessage = "Произошло переполнение";
                                                    return false;
                                                }
                                                AddStringToSupportTable(Converting.ToSixChars(Converting.DecToHex(AddressCount)), 
                                                                        Converting.ToTwoChars(Converting.DecToHex(AddressationType)), Operand1, Operand2);
                                                AddressCount = AddressCount + 2;
                                                if (!MemoryCheck()) { return false; }
                                            }
                                            else
                                            {
                                                ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка в команде " + code_table_mas[num, 0];
                                                return false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //ДЛИНА КОМАНДЫ = 4
                                        if (code_table_mas[num, 2] == "4")
                                        {
                                            if (AddressCount + 4 > MAX_MemmoryAdr)
                                            {
                                                ErrorMessage = "Произошло переполнение";
                                                return false;
                                            }

                                            int AddressationType;
                                            if (Operand1.Length > 0)
                                            {
                                                if (Operand1[0] == '[' && Operand1[Operand1.Length - 1] == ']')
                                                    AddressationType = Converting.HexToDec(code_table_mas[num, 1]) * 4 + 2;
                                                else
                                                    AddressationType = Converting.HexToDec(code_table_mas[num, 1]) * 4 + 1;
                                            }
                                            else
                                            {
                                                ErrorMessage = "Исходный текст программы --> Не найден операнд в строке:" + (i + 1);
                                                return false;
                                            }

                                            AddStringToSupportTable(Converting.ToSixChars(Converting.DecToHex(AddressCount)), 
                                                                    Converting.ToTwoChars(Converting.DecToHex(AddressationType)), Operand1, Operand2);

                                            AddressCount = AddressCount + 4;
                                            if (!MemoryCheck()) 
                                            { return false; }

                                            if (Operand2.Length > 0)
                                                ErrorMessage = ErrorMessage + "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Второй операнд команды " + code_table_mas[num, 0] + " не рассматривается.  \n";
                                        }
                                        else
                                        {
                                            ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Размер команды больше установленного";
                                            return false;
                                        }
                                    }    
                                            /*  //ДЛИНА КОМАНДЫ = 3
                                              if (code_table_mas[num,2]=="3")
                                              {
                                                  if (addressCounter +3> maxMemmoryAdr)
                                                  {
                                                      error_message = "Произошло переполнение";
                                                      return false;
                                                  }
                                                  int AddressationType = Converting.HexToDec(code_table_mas[num, 1]) * 4+1;
                                                  AddStringToSupportTable(Converting.ToSixChars(Converting.DecToHex(addressCounter)), Converting.ToTwoChars(Converting.DecToHex(AddressationType)), Operand1, Operand2);
                                                  addressCounter = addressCounter + 3;
                                                  if (!MemoryCheck()) { return false; }

                                                  if (Operand2.Length > 0)
                                                      error_message = error_message + "Второй операнд команды" + code_table_mas[num, 0] + "  не рассматривается\n";

                                              }
                                              else*/          
                            }
                            else
                            {
                                ErrorMessage = "Строка {" + (i + 1) + "} Ошибка! МКОП " + MKOP + " не найден в ТАБЛИЦЕ КОДОВ ОПЕРАЦИИ";
                                return false;
                            }
                        }
                        else
                        {
                            if(i == rows)
                            {
                                ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Отсутствует директива END!";
                                return false;
                            }
                            else
                            {
                                ErrorMessage = "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка в МКОП";
                                return false;
                            }
                        }
                    }
                }

            }

            if (EndFlag == 0)
            {
                ErrorMessage = ErrorMessage + "Исходный текст программы --> Строка {" + (rows + 2) + "} Ошибка! Не найдена точка входа в программу \n";
                return false;
            }

            //Помещаем сформированную Вспомагательную таблицу в датагрид
            for (int i = 0; i < SupportTable[0].Count; i++)
            {
                dataGrid_supportTable.Rows.Add();
                dataGrid_supportTable.Rows[i].Cells[0].Value = SupportTable[0][i];
                dataGrid_supportTable.Rows[i].Cells[1].Value = SupportTable[1][i];
                dataGrid_supportTable.Rows[i].Cells[2].Value = SupportTable[2][i];
                dataGrid_supportTable.Rows[i].Cells[3].Value = SupportTable[3][i];
            }

            //Помещаем сформированную Таблицу вспомогательных имен в датагрид
            for (int j = 0; j < SymbolTable[1].Count; j++)
            {
                dataGrid_symbol_table.Rows.Add();
                dataGrid_symbol_table.Rows[j].Cells[0].Value = SymbolTable[0][j];
                dataGrid_symbol_table.Rows[j].Cells[1].Value = SymbolTable[1][j];
            }

            return true;
        }

        //Второй проход
        public bool SecondPass(ListBox BinaryCode, int adrType, DataGridView dataGrid_TuningTable)
        {
            ErrorMessage = "";

            //Создаем массив для хранения строк настройки
            List<string> TableSetting = new List<string>();

            //запускаем его для каждой строки Вспомогательной таблицы
            for (int i = 0; i < SupportTable[0].Count; i++)
            {
                string address = SupportTable[0][i];
                string MKOP = SupportTable[1][i];
                string operand1 = SupportTable[2][i];
                string operand2 = SupportTable[3][i];

                //Если строка первая, то это директива Старт
                if (i == 0)
                {
                    string str = Converting.EditingString("H", SupportTable[0][0], SupportTable[2][0], "", Convert.ToString(AddressCount - StartAddress), "");
                    BinaryCode.Items.Add(str);
                }
                //Если строка не первая, то снова смотрим, команда там или директива. Интересуют RESB и  RESW, т.к. из значение операндов отражается только в длинне записи
                else
                {
                    int error, label1, label2;
                    string tune_address;

                    string result1 = CheckingOperandSecondPass(operand1, out error, out label1, adrType, out tune_address, i);

                    if (error == 1) 
                    { ErrorMessage = ErrorMessage + "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка в операнде, недопустимое выражение: " + operand1; break; }
                    AddTo.SettingTable(tune_address, TableSetting);

                    string result2 = CheckingOperandSecondPass(operand2, out error, out label2, adrType, out tune_address, i);

                    if (error == 1) 
                    { ErrorMessage = ErrorMessage + "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка в операнде, недопустимое выражение: " + operand2; break; }
                    AddTo.SettingTable(tune_address, TableSetting);

                    if (CheckData.IsDirective(MKOP) == true)
                    {
                        if (MKOP == "RESB")
                        {
                            MKOP = "";
                            string str1 = Converting.EditingString("T", address, MKOP, result1, "", "");
                            BinaryCode.Items.Add(str1);
                            continue;
                        }

                        if (MKOP == "RESW")
                        {
                            MKOP = "";
                            string str2 = Converting.EditingString("T", address, MKOP, Converting.ToTwoChars(Converting.DecToHex(Convert.ToInt32(operand1) * 3)), "", "");
                            BinaryCode.Items.Add(str2);
                            continue;
                        }


                        if (MKOP == "BYTE")
                        {
                            MKOP = "";
                            string str2 = Converting.EditingString("T", address, MKOP, Converting.ToTwoChars(Converting.DecToHex(result1.Length + result2.Length)), result1, result2);
                            BinaryCode.Items.Add(str2);
                            continue;
                        }

                        if (MKOP == "WORD")
                        {
                            MKOP = "";
                            result1 = Converting.ToSixChars(result1);
                            string str2 = Converting.EditingString("T", address, MKOP, Converting.ToTwoChars(Converting.DecToHex(result1.Length + result2.Length)), result1, result2);
                            BinaryCode.Items.Add(str2);
                            continue;
                        }


                        if (MKOP == "BYTE" && operand1 == "?")
                        {
                            MKOP = "";
                            string str2 = Converting.EditingString("T", address, MKOP, Converting.ToTwoChars(Converting.DecToHex(1)), "", "");
                            BinaryCode.Items.Add(str2);
                            continue;
                        }

                        if (MKOP == "WORD" && operand1 == "?")
                        {
                            MKOP = "";
                            string str2 = Converting.EditingString("T", address, MKOP, Converting.ToTwoChars(Converting.DecToHex(3)), "", "");
                            BinaryCode.Items.Add(str2);
                            continue;
                        }
                    }
                    else
                    {
                        // Проверяем что команда работает с тем, что разрешено адресацией
                        // сначала смотрим на тип адресации, если там  01 , значит это прямая
                        //и в операндах может быть только метка
                        int Type_of_adr = (byte)Converting.HexToDec(MKOP) & 0x03;
                        if (Type_of_adr == 1)
                        {
                            if (label1 != 1)
                            {
                                ErrorMessage = ErrorMessage + "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Для данного типа адресации операнд должен быть меткой " + MKOP;
                                BinaryCode.Items.Clear();
                                return false;

                            }
                            if (result2 != "")
                            {
                                ErrorMessage = ErrorMessage + "Исходный текст программы --> Строка {" + (i + 1) + "} Ошибка! Данный тип адрессации поддерживает 1 операнд";
                                BinaryCode.Items.Clear();
                                return false;
                            }
                        }

                        String RecordLength = Converting.ToTwoChars(Converting.DecToHex(MKOP.Length + result1.Length + result2.Length));
                        string str5 = Converting.EditingString("T", address, MKOP, RecordLength, result1, result2);
                        BinaryCode.Items.Add(str5);

                    }

                }

                //после каждой проийденной строки, обновляем таблицу настройки
                if (TableSetting.Count > 0)
                {
                    dataGrid_TuningTable.Rows.Clear();
                    for (int j = 0; j < TableSetting.Count; j++)
                        dataGrid_TuningTable.Rows.Add(TableSetting[j]);
                }

            }

            //добавляем в объектный модуль всё из таблицы настройки
            for (int j = 0; j < TableSetting.Count; j++)
            {
                string str4 = Converting.EditingString("M", TableSetting[j], "", "", "", "");
                BinaryCode.Items.Add(str4);
            }

            string str3 = Converting.EditingString("E", Converting.ToSixChars(Converting.DecToHex(EndAddress)), "", "", "", "");
            BinaryCode.Items.Add(str3);

            if (ErrorMessage != "")
                BinaryCode.Items.Clear();

            return true;
        }
    }
}
