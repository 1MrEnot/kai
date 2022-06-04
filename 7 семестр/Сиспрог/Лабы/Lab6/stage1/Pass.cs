using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Lab4.DataStorage;

namespace Lab4
{
    public partial class Pass:DataStorage.IObserver<Sector>
    {
        internal Form1 form;
        private readonly List<SourceCode> SourceCodeLines;
        private readonly List<OperationCode> OperationCodes;
        private Sector current = null;
        private readonly List<Sector> sectors = new List<Sector>();
        private int currentLineNumber = -1; // номер обрабатываемой строки кода

        internal bool StartFlag = false; // Директива "START" найдена
        internal bool EndFlag = false; // Директива "END" найдена
        internal bool SectFlag = false; // Директива "CSECT" найдена

        internal BindingList<ExtDefLine> TableExtDef
        {
            get
            {
                var temp = new BindingList<ExtDefLine>();
                foreach (var s in sectors)
                {
                    foreach (var i in s.TableExtDef)
                    {
                        temp.Add(i);
                    }
                }
                return temp;
            }
        }
        private BindingList<object[]> TableSymbolicNames
        {
            get
            {
                var temp = new BindingList<object[]>();
                foreach (var s in sectors)
                {
                    foreach (var i in s.TableSymbolicNames)
                    {
                        temp.Add(new object[] { i.Name, i.Address, i.Link, i.Type, s.Name });
                    }
                }
                return temp;
            }
        }
        private BindingList<BinCodeLine> BinCodeLines
        {
            get
            {
                var temp = new BindingList<BinCodeLine>();
                foreach (var s in sectors)
                {
                    foreach (var i in s.BinCodeLines)
                    {
                        temp.Add(i);
                    }
                }
                return temp;
            }
        }
        private BindingList<ConfigLine> TableConfig
        {
            get
            {
                var temp = new BindingList<ConfigLine>();
                foreach (var s in sectors)
                {
                    foreach (var i in s.tableConfig)
                    {
                        temp.Add(i);
                    }
                }
                return temp;
            }
        }
        internal OperationCode GetOperation(string MKOP)
        {
            foreach (OperationCode oc in OperationCodes)
                if (oc.MKOP.Equals(MKOP.ToUpper()))
                {
                    return oc;
                }
            return null;
        }

        private void NewException(string message)
        {
            throw new Exception($"[{currentLineNumber + 1}] {message}\n");
        }

        internal Pass(IList<SourceCode> sources,IList<OperationCode> operations,Form1 form)
        {
            SourceCodeLines = sources.ToList();
            OperationCodes = operations.ToList();
            this.form = form;
        }
        private void Warning(string warn)
        {
            form.AddError(warn);
        }
        public void OnChanged()
        {
            form.UpdateTSI(TableSymbolicNames);
            form.UpdateTextBoxBinCode(BinCodeLines);
            form.UpdateTableConfig(TableConfig);
            form.UpdateExtDefs(TableExtDef);
        }

        internal void OneStep()
        {
            if (currentLineNumber == -1)
            {
                if (SourceCodeLines.Count() == 0)
                {
                    NewException("Исходный текст не задан");
                    return;
                }

                if (OperationCodes.Count() == 0)
                {
                    NewException("Таблица кодов операций не задана");
                    return;
                }

                sectors.Clear();
                currentLineNumber++;
                form.SetLinePointer(currentLineNumber);
                return;
            }

            if (string.IsNullOrWhiteSpace(SourceCodeLines[currentLineNumber].Label)
                && string.IsNullOrWhiteSpace(SourceCodeLines[currentLineNumber].MKOP)
                && string.IsNullOrWhiteSpace(SourceCodeLines[currentLineNumber].Op1)
                && string.IsNullOrWhiteSpace(SourceCodeLines[currentLineNumber].Op2))
            {
               /* if (SourceCodeLines.Count() - 1 != currentLineNumber)
                {
                    currentLineNumber++;
                    form.SetLinePointer(currentLineNumber);
                }*/
            }
            else
            {

                var codeLine = SourceCodeLines[currentLineNumber];

                if (!StartFlag && !SectFlag)
                {
                    // Сюда попадем только при первой итерации
                    // Если здесь не директива START, то генерируется ошибка
                    if (!codeLine.MKOP.ToUpper().Equals("START"))
                    {
                        NewException("В начале программы директива START не была обнаружена");
                        return;
                    }
                }

                if (EndFlag)
                {
                    currentLineNumber++;
                    return;
                }

                if (codeLine.Label.Length != 0 && StartFlag && !codeLine.MKOP.Equals("CSECT"))
                {
                    SymbolicName symName;
                    if (current.ExtRefs.Contains(codeLine.Label))
                        NewException("Метка уже является внешней ссылкой: " + codeLine.Label);
                    else if (current.TsiContainsName(codeLine.Label, out symName))
                    {
                        AddAddressToSymbolicName(codeLine.Label, GetAddressCounter());
                    }
                    else
                    {
                        SymbolicName symbolicName = new SymbolicName(codeLine.Label, string.Format("{0:X6}", current.AddressCounter).ToUpper());
                        current.TableSymbolicNames.Add(symbolicName);
                    }
                    if (current.TedContainsName(codeLine.Label))
                    {
                        AddAddressToExtDef(codeLine.Label, GetAddressCounter());
                    }
                }

                if (Config.IsDirective(codeLine.MKOP))
                {
                    switch (codeLine.MKOP.ToUpper())
                    {
                        case "START":
                            {
                                if (StartFlag)
                                {
                                    NewException("Обнаружено второе появление директивы START");
                                    return;
                                }

                                StartFlag = true;

                                if (codeLine.Label.Length == 0)
                                    NewException("Имя программы не задано");
                                if (codeLine.Label.Length > 10)
                                    NewException("Превышена длина имени программы (больше чем 10 символов)");
                                current = new Sector(codeLine.Label);
                                current.Subscribe(this);
                                sectors.Add(current);

                                // Проверяем, что адрес начала программы состоит из символов шестнадцатеричной СС
                                if (!Regex.IsMatch(codeLine.Op1.ToUpper(), @"^[A-F0-9]+$"))
                                    NewException("Неверный адрес начала программы");

                                // Преобразуем в 10чную СС
                                try
                                {
                                    current.AddressCounter = Convert.ToInt32(codeLine.Op1, 16);
                                    if(current.AddressCounter > Config.maxMemoryAdr|| current.AddressCounter < 0)
                                    {
                                        throw new Exception();
                                    }
                                }
                                catch (Exception)
                                {
                                    NewException("Слишком большое число в адресе начала программы");
                                }
                                
                                current.StartAddress = current.AddressCounter;

                                if (current.StartAddress != 0)
                                {
                                    NewException("Адрес начала программы должен быть равен 0");
                                    return;
                                }
                                if (current.StartAddress > Config.maxMemoryAdr)
                                    NewException("Адрес программы выходит за диапазон памяти");

                                BinCodeLine binCodeLine = new BinCodeLine("H " + current.Name, string.Format("{0:X6}", current.AddressCounter).ToUpper(), "", "", "");
                                current.BinCodeLines.Add(binCodeLine);
                                current.ProgBodyFlag = false;
                                SectFlag = false;
                            }
                            break;
                        case "WORD":
                            {
                                // Допускаются только 16-чные числа (положительные и до maxMemoryAddr)
                                int operand = 0;
                                if (!codeLine.Op1.Equals("?")) // ? резервирует слово в памяти
                                    if (!int.TryParse(codeLine.Op1, out operand))
                                        NewException("Ошибка ввода операнда в строке: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.Op1);

                                if (operand < 0 || operand > Config.maxMemoryAdr)
                                    NewException("Число вне допустимого диапазона. Строка кода: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.Op1 + " " + codeLine.Op2);

                                BinCodeLine binCodeLine = new BinCodeLine("T", GetAddressCounter(), "03", "00", string.Format("{0:X6}", operand));
                                current.BinCodeLines.Add(binCodeLine);
                                current.AddressCounter += 3;
                                current.ProgBodyFlag = true;
                            }
                            break;
                        case "BYTE":
                            {
                                // Допускаются только числа от 0 до 255
                                int operand;
                                if (int.TryParse(codeLine.Op1, out operand))
                                {
                                    if (operand < 0 || operand > 255)
                                        NewException("Операнд вне допустимого диапазона. Строка кода: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.Op1 + " " + codeLine.Op2);
                                    BinCodeLine binCodeLine = new BinCodeLine("T", GetAddressCounter(), "01", "00", string.Format("{0:X2}", operand));
                                    current.BinCodeLines.Add(binCodeLine);
                                    current.AddressCounter++;
                                }
                                // проверяем, это строка или это шестнадцатеричное число
                                else if ((codeLine.Op1.Length > 3) && (codeLine.Op1[1].Equals('\"')) && (codeLine.Op1[codeLine.Op1.Length - 1].Equals('\"')))
                                {
                                    // если C, то это строка
                                    if (codeLine.Op1[0].Equals('C') || codeLine.Op1[0].Equals('c'))
                                    {
                                        string str = codeLine.Op1.Substring(2, codeLine.Op1.Length - 3);
                                        string temp = "";
                                        foreach (byte symb in System.Text.Encoding.ASCII.GetBytes(str))
                                            temp += symb.ToString("X");
                                        BinCodeLine binCodeLine = new BinCodeLine("T", GetAddressCounter(), string.Format("{0:X2}", str.Length), "00", temp.ToUpper());
                                        current.BinCodeLines.Add(binCodeLine);
                                        current.AddressCounter += str.Length;
                                    }
                                    // если X, то это шестнадцатеричное число
                                    else if (codeLine.Op1[0].Equals('X') || codeLine.Op1[0].Equals('x'))
                                    {
                                        string str = codeLine.Op1.Substring(2, codeLine.Op1.Length - 3);
                                        if ((str.Length % 2) != 0)
                                            str = str.Insert(0, "0");
                                        if (!Regex.IsMatch(str.ToUpper(), @"^[A-F0-9]+$"))
                                            NewException("Шестнадцатеричное число введено неверно. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.Op1 + " " + codeLine.Op2);

                                        BinCodeLine binCodeLine = new BinCodeLine("T", GetAddressCounter(), string.Format("{0:X2}", str.Length / 2), "00", str.ToUpper());
                                        current.BinCodeLines.Add(binCodeLine);
                                        current.AddressCounter += str.Length / 2;
                                    }
                                    else
                                        NewException("Ошибка ввода операнда в строке: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.Op1 + " " + codeLine.Op2);
                                }
                                // если там "?", то просто резервируем один байт
                                else if (codeLine.Op1.Equals("?"))
                                {
                                    BinCodeLine binCodeLine = new BinCodeLine("T", GetAddressCounter(), "01", "00", "00");
                                    current.BinCodeLines.Add(binCodeLine);
                                    current.AddressCounter++;
                                }
                                else
                                    NewException("Ошибка ввода операнда в строке: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.Op1 + " " + codeLine.Op2);

                                current.ProgBodyFlag = true;
                            }
                            break;
                        case "RESB":
                            {
                                if (codeLine.Op1.Length == 0)
                                {
                                    NewException("Пустой операнд");
                                }

                                if (!int.TryParse(codeLine.Op1, out int operand))
                                    NewException("Невозможно преобразовать в число. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.Op1 + " " + codeLine.Op2);
                                if (operand <= 0)
                                    NewException("Указано недопустимое количество байт. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.Op1 + " " + codeLine.Op2);
                                BinCodeLine binCodeLine = new BinCodeLine("T", GetAddressCounter(), string.Format("{0:X2}", operand), "00", string.Format("{0:X6}", operand));
                                current.BinCodeLines.Add(binCodeLine);
                                current.AddressCounter += operand;
                                current.ProgBodyFlag = true;
                            }
                            break;
                        case "RESW":
                            {
                                if (!int.TryParse(codeLine.Op1, out int operand))
                                    NewException("Невозможно преобразовать в число. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.Op1 + " " + codeLine.Op2);
                                if (operand <= 0)
                                    NewException("Недопустимое количество слов. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.Op1 + " " + codeLine.Op2);
                                BinCodeLine binCodeLine = new BinCodeLine("T", GetAddressCounter(), string.Format("{0:X2}", operand * 3), "00", string.Format("{0:X6}", operand));
                                current.BinCodeLines.Add(binCodeLine);
                                current.AddressCounter += operand * 3;
                                current.ProgBodyFlag = true;
                            }
                            break;
                        case "END":
                            {
                                // Проверяем, у всех ли СИ были найдены адреса
                                if (SectFlag)
                                {
                                    foreach (var i in current.TableSymbolicNames)
                                    {
                                        if (i.Link.Length != 0)
                                        {
                                            NewException($"Символическое имя {i.Name} в секторе {current.Name} не имеет адреса"); ;
                                        }
                                    }
                                    foreach (var i in current.TableExtDef)
                                    {
                                        if (i.Address.Length == 0)
                                        {
                                            NewException($"Внешнее имя {i.ExtDef} в секторе {current.Name} не имеет адреса"); ;
                                        }
                                    }
                                    /*foreach (var i in current.TableExtDef)
                                    {
                                       
                                        NewException($"Найдено неопределенное внешнее имя {i.ExtDef}");
                                    }
                                    /*if (!Check.EmptyAddress(lable_table))
                                    {
                                        error_message = "Исходный текст программы --> Ошибка! Найдено неопределенное внешнее имя";
                                        return false;
                                    }*/
                                }
                                foreach (var i in current.TableSymbolicNames)
                                {
                                    if (i.Link.Length != 0)
                                    {
                                        NewException($"Символическое имя {i.Name} в секторе {current.Name} не имеет адреса"); ;
                                    }
                                }
                                foreach (var i in current.TableExtDef)
                                {
                                    if (i.Address.Length == 0)
                                    {
                                        NewException($"Внешнее имя {i.ExtDef} в секторе {current.Name} не имеет адреса"); ;
                                    }
                                }
                                if (!StartFlag)
                                    NewException("Директива END раньше директивы Start");
                                if (EndFlag)
                                    NewException("Повтор директивы END");
                                EndFlag = true;
                                current.ProgBodyFlag = false;
                                SectFlag = false;
                                foreach(var i in current.tableConfig)
                                {
                                    current.BinCodeLines.Add(new BinCodeLine("M", i.Address, i.ExtRef, "", ""));
                                }
                                    

                                if (codeLine.Op1.Length == 0)
                                    current.EndAddress = current.StartAddress;
                                else
                                {
                                    if (!Regex.IsMatch(codeLine.Op1.ToUpper(), @"^[A-F0-9]+$"))
                                        NewException("Неверный адрес выхода из программы");
                                    current.EndAddress = Convert.ToInt32(codeLine.Op1.ToUpper(), 16);
                                    if (current.EndAddress < current.StartAddress || current.EndAddress > current.AddressCounter)
                                        NewException("Неверный адрес выхода из программы");
                                }
                                
                                // преобразуем обратно в 16 сс и допишем нули
                                BinCodeLine binCodeLine = new BinCodeLine("E", string.Format("{0:X6}", current.EndAddress), "", "", "");
                                current.BinCodeLines.Add(binCodeLine);

                                current.ProgramLength = current.AddressCounter - current.StartAddress;
                                current.BinCodeLines[0].Length = string.Format("{0:X6}", current.ProgramLength);
                                OnChanged();
                            }
                            break;
                        case "CSECT":
                            {
                                if (!StartFlag)
                                {
                                    /*tableConfig.Clear();
                                    dataGridViewTN.Refresh();
                                    tableSymbolicNames.Clear();
                                    dataGridViewTSI.Refresh();
                                    tableExtDef.Clear();
                                    dataGridViewExtDefs.Refresh();
                                    ExtRefs.Clear();*/

                                    StartFlag = true;

                                    if (codeLine.Label.Length == 0)
                                        NewException("Имя секции не задано");
                                    if (codeLine.Label.Length > 10)
                                        NewException("Превышена длина имени секции (больше чем 10 символов)");
                                    
                                    current = new Sector(codeLine.Label)
                                    {
                                        AddressCounter = 0
                                    };
                                    current.Subscribe(this);
                                    sectors.Add(current);
                                    current.StartAddress = current.AddressCounter;

                                    BinCodeLine binCodeLine = new BinCodeLine("H " + current.Name, string.Format("{0:X6}", current.AddressCounter).ToUpper(), "", "", "");
                                    current.BinCodeLines.Add(binCodeLine);
                                    current.ProgBodyFlag = false;
                                }
                                else
                                {
                                    // Проверяем, у всех ли СИ были найдены адреса
                                    foreach (var i in current.TableSymbolicNames)
                                    {
                                        if (i.Link.Length != 0)
                                        {
                                            NewException($"Символическое имя {i.Name} в секторе {current.Name} не имеет адреса"); ;
                                            break;
                                        }
                                    }

                                    foreach (var i in current.TableExtDef)
                                    {
                                        if (i.Address.Length == 0)
                                        {
                                            NewException($"Внешнее имя {i.ExtDef} в секторе {current.Name} не имеет адреса"); ;
                                        }
                                    }

                                    StartFlag = false;
                                    current.ProgBodyFlag = false;
                                    SectFlag = true;

                                    current.EndAddress = current.StartAddress;

                                     foreach (var i in current.tableConfig)
                                    {
                                        current.BinCodeLines.Add(new BinCodeLine("M", i.Address, i.ExtRef, "", ""));
                                    }
                                    BinCodeLine binCodeLine = new BinCodeLine("E", string.Format("{0:X6}", current.EndAddress), "", "", "");
                                    current.BinCodeLines.Add(binCodeLine);

                                    current.ProgramLength = current.AddressCounter - current.StartAddress;
                                    current.BinCodeLines[0].Length = string.Format("{0:X6}", current.ProgramLength);
                                    OnChanged();

                                    return;
                                }
                            }
                            break;
                        case "EXTDEF":
                            {
                                if (current.ProgBodyFlag)
                                {
                                    NewException("EXTDEF обнаружен в теле программы");
                                    break;
                                }
                                //codeLine.Op1 = codeLine.Op1.ToUpper();
                                //codeLine.Op2 = codeLine.Op2.ToUpper();
                                if (codeLine.Op1.Length == 0)
                                    NewException("EXTDEF не содержит операндов");
                                else
                                {
                                    if (current.TedContainsName(codeLine.Op1))
                                    {
                                        NewException("Найдена уже существующая метка среди внешних имен: " + codeLine.Op1);
                                    }
                                    else if (current.ExtRefs.Contains(codeLine.Op1))
                                    {
                                        NewException("Найдена уже существующая метка среди внешних ссылок: " + codeLine.Op1);
                                    }
                                    /*else if (codeLine.Op2.Length == 0)
                                    {
                                        NewException("Метка не объявлена в секции" + codeLine.Op1);
                                    }*/
                                    else 
                                    {
                                        var temp = new ExtDefLine(codeLine.Op1);
                                        current.TableExtDef.Add(temp);
                                        current.BinCodeLines.Add(new BinCodeLine("D", codeLine.Op1, "", "", ""));
                                    }
                                    
                                }
                                if (codeLine.Op2 != "")
                                {
                                    if (current.TedContainsName(codeLine.Op2))
                                        NewException("Найдена уже существующая метка среди внешних имен: " + codeLine.Op2);
                                    else if (current.ExtRefs.Contains(codeLine.Op2))
                                        NewException("Найдена уже существующая метка среди внешних ссылок: " + codeLine.Op2);
                                    else
                                    {
                                        var temp = new ExtDefLine(codeLine.Op1);
                                        current.TableExtDef.Add(temp);
                                        current.BinCodeLines.Add(new BinCodeLine("D", codeLine.Op1, "", "", ""));
                                    }
                                }
                                
                            }
                            break;
                        case "EXTREF":
                            {
                                if (current.ProgBodyFlag)
                                {
                                    NewException("EXTREF обнаружен в теле программы");
                                    break;
                                }
                                if (codeLine.Op1.Length == 0)
                                {
                                    NewException("EXTREF не содержит операндов");
                                    break;
                                }
                                if (current.TedContainsName(codeLine.Op1))
                                    NewException("Найдена уже существующая метка среди внешних имен: " + codeLine.Op1);
                                else if (current.ExtRefs.Contains(codeLine.Op1))
                                    NewException("Найдена уже существующая метка среди внешних ссылок: " + codeLine.Op1);
                                else
                                {
                                    current.ExtRefs.Add(codeLine.Op1);
                                    current.BinCodeLines.Add(new BinCodeLine("R", codeLine.Op1, "", "", ""));
                                }
                                if (codeLine.Op2.Length != 0)
                                {
                                    if (current.TedContainsName(codeLine.Op2))
                                        NewException("Найдена уже существующая метка среди внешних имен: " + codeLine.Op2);
                                    else if (current.ExtRefs.Contains(codeLine.Op2))
                                        NewException("Найдена уже существующая метка среди внешних ссылок: " + codeLine.Op2);
                                    else
                                    {
                                        current.ExtRefs.Add(codeLine.Op2);
                                        current.BinCodeLines.Add(new BinCodeLine("R", codeLine.Op2, "", "", ""));
                                    }
                                }
                            }
                            break;
                    }
                }
                // значит это команда
                else
                {
                    var operationCode = GetOperation(codeLine.MKOP.ToUpper());
                    if (operationCode == null)
                        NewException("МКОП не найден в таблице кодов операций. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.Op1 + " " + codeLine.Op2);
                    else
                        switch (operationCode.CodeLength)
                        {
                            case 1: // Длина команды 1
                                {
                                    if (codeLine.Op1.Length != 0)
                                    {
                                        Warning("Эта команда не требует операндов");
                                    }
                                    // Просто сдвигаем на два разряда влево, т.к. это число и адресация непосредственная
                                    int addrType = Convert.ToInt32(operationCode.HexCode, 16) << 2;
                                    // переводим обратно в 16 с.с.
                                    BinCodeLine binCodeLine = new BinCodeLine("T", GetAddressCounter(), "01", string.Format("{0:X2}", addrType).ToUpper(), "");
                                    current.BinCodeLines.Add(binCodeLine);
                                    current.AddressCounter++;
                                }
                                break;
                            case 2: // Длина команды 2
                                {
                                    int Op1 = 0;
                                    // Попробуем преобразовать операнд в число
                                    if (int.TryParse(codeLine.Op1, out Op1))
                                    {
                                        if (Op1 < 0 || Op1 > 255)
                                            NewException("Значение первого операнда вне допустимого диапазона. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.Op1 + " " + codeLine.Op2);
                                        // Просто сдвигаем на два разряда влево, т.к. это число и адресация непосредственная
                                        int addrType = Convert.ToInt32(operationCode.HexCode, 16) << 2;
                                        BinCodeLine binCodeLine = new BinCodeLine("T", GetAddressCounter(), "02", string.Format("{0:X2}", addrType).ToUpper(), string.Format("{0:X2}", Op1).ToUpper());
                                        current.BinCodeLines.Add(binCodeLine);
                                        current.AddressCounter += 2;
                                    }
                                    // Если в операнде не число, значит регистры
                                    else
                                    {
                                        if ((!Config.registers.Contains(codeLine.Op1.ToUpper())) || (!Config.registers.Contains(codeLine.Op2.ToUpper())))
                                            NewException("Ошибка в операндах. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.Op1 + " " + codeLine.Op2);
                                        // т.к. используются регистры, то это непосредственная адресация - сдвигаем на два разряда влево
                                        int addrType = Convert.ToInt32(operationCode.HexCode, 16) << 2;
                                        string operands = string.Format("{0:X2}", Config.registers.ToList().IndexOf(codeLine.Op1)) + " " + string.Format("{0:X2}", Config.registers.ToList().IndexOf(codeLine.Op2));
                                        BinCodeLine binCodeLine = new BinCodeLine("T", GetAddressCounter(), "02", string.Format("{0:X2}", addrType).ToUpper(), operands.ToUpper());
                                        current.BinCodeLines.Add(binCodeLine);
                                        current.AddressCounter += 2;
                                    }
                                }
                                break;
                            case 3: // Длина команды 3
                                {
                                    // Используется прямая адресация
                                    //int addrType = Convert.ToInt32(operationCode.HexCode, 16) * 4 + 1;
                                    //string mkop = string.Format("{0:X2}", addrType).ToUpper();
                                    //BinCodeLine supportLine = new BinCodeLine(GetAddressCounter(), mkop, codeLine.Op1, "");
                                    //binCodeLines.Add(supportLine);
                                    //addressCounter += 3;
                                    NewException($"В операции {operationCode.MKOP} недопустимая длина команды 3");
                                }
                                break;
                            case 4: // Длина команды 4
                                {
                                    if (codeLine.Op2.Length != 0)
                                    {
                                        Warning("В команде лишний операнд: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.Op1 + " " + codeLine.Op2 + "\n");
                                        Warning("Второй операнд не учитывается" + "\n");
                                    }

                                    bool relAddr = false;
                                    if (Regex.IsMatch(codeLine.Op1, @"^\[.+\]$"))
                                        relAddr = true;

                                    int addrType;

                                    string str;
                                    // Используется относительная адресация
                                    if (relAddr)
                                    {
                                        addrType = Convert.ToInt32(operationCode.HexCode, 16) * 4 + 2;
                                        str = codeLine.Op1.Trim(new char[] { '[', ']' });
                                    }
                                    // Используется прямая адресация
                                    else
                                    {
                                        addrType = Convert.ToInt32(operationCode.HexCode, 16) * 4 + 1;
                                        str = codeLine.Op1;
                                    }
                                    string mkop = string.Format("{0:X2}", addrType).ToUpper();
                                    string operand;

                                    SymbolicName temp;
                                    if (current.ExtRefs.Contains(str))
                                    {
                                        if (relAddr)
                                            NewException("Внешняя ссылка не может использоваться при относительной адресации");
                                        operand = "000000";
                                        current.tableConfig.Add(new ConfigLine(GetAddressCounter(), str));
                                    }
                                    else if (current.TsiContainsName(str, out temp) && temp.Address.Length != 0)
                                    {
                                        if (relAddr)
                                        {
                                            int address = Convert.ToInt32(temp.Address, 16);
                                            address -= current.AddressCounter + 4;
                                            operand = string.Format("{0:X6}", address);
                                            if(operand.Length > 6)
                                            {
                                                operand = operand.Substring(2,6);
                                            }
                                        }
                                        else
                                        {
                                            operand = temp.Address;
                                            current.tableConfig.Add(new ConfigLine(GetAddressCounter()));
                                        }
                                    }
                                    else
                                    {
                                        int type = 0;
                                        if (current.TedContainsName(str))
                                            type = 1;
                                        if (!current.TsiContainsName(str,out temp))
                                        {
                                            var buff = new SymbolicName(str, "", GetAddressCounter(), type);
                                            current.TableSymbolicNames.Add(buff);
                                        }
                                        if(codeLine.Op1.Length == 0)
                                        {
                                            NewException("Пустой операнд");
                                        }
                                        operand = "$" + codeLine.Op1.ToUpper();
                                        if(!relAddr)
                                            current.tableConfig.Add(new ConfigLine(GetAddressCounter()));
                                    }

                                    BinCodeLine supportLine = new BinCodeLine("T", GetAddressCounter(), "04", mkop, operand);
                                    current.BinCodeLines.Add(supportLine);
                                    current.AddressCounter += 4;
                                }
                                break;
                            default:
                                NewException("Превышен размер команды. Строка: " + codeLine.Label + " " + codeLine.MKOP + " " + codeLine.Op1 + " " + codeLine.Op2);
                                break;
                        }
                    current.ProgBodyFlag = true;
                }

                
            }
            // проверка памяти
            if (current.AddressCounter > Config.maxMemoryAdr || current.AddressCounter < 0)
                NewException("Произошло переполнение памяти");
            if (!EndFlag)
            {
                if (currentLineNumber >= SourceCodeLines.Count - 1)
                {
                    NewException("Точка выхода из программы не была найдена");
                    return;
                }

                currentLineNumber++;
                form.SetLinePointer(currentLineNumber);
            }
        }

        internal void FirstPass()
        {
            while (currentLineNumber < SourceCodeLines.Count())
            {
                if (EndFlag)
                    break;
                OneStep();
            }
        }

        private string GetAddressCounter()
        {
            return string.Format("{0:X6}", current.AddressCounter).ToUpper();
        }
       
        private void AddAddressToSymbolicName(string name, string Address)
        {
            bool first = true;
            List<SymbolicName> toRemove = new List<SymbolicName>();
            foreach (SymbolicName item in current.TableSymbolicNames)
            {
                if (item.Name.Equals(name))
                {
                    if (item.Address.Length != 0)
                    {
                        NewException("Метка уже существует: " + name);
                        break;
                    }

                    item.Address = Address;

                    // ищем номер строки в textBoxBinCode только в текущей секции, в которую надо добавить адрес
                    /*int i = current.BinCodeLines.IndexOf(current.BinCodeLines.FirstOrDefault(x => x.Address.Equals(item.Link)));
                    if (i == -1)
                    {
                        NewException("Ошибка метки: " + ExtDef);
                        break;
                    }*/
                    bool flag = false;
                    foreach(var row in current.BinCodeLines)
                    {
                        if(Regex.IsMatch(row.Operands, @"^\$\[" + item.Name + @"\]$"))
                        {
                            int addr = Convert.ToInt32(Address, 16);
                            addr -= Convert.ToInt32(row.Address, 16) + 4;
                            row.Operands = string.Format("{0:X6}", addr);
                            flag = true;
                        }
                        else if(row.Operands == "$"+item.Name)
                        {
                            row.Address = Address;
                            row.Operands = string.Format("{0:X6}", Address);
                            flag = true;
                        }
                    }

                    if (!flag)
                    {
                        NewException("Ошибка метки: " + item.Name);
                    }

                    item.Link = "";
                    if (!first)
                        toRemove.Add(item);
                    first = false;
                }
            }
            foreach (SymbolicName item in toRemove)
                current.TableSymbolicNames.Remove(item);
            form.UpdateTSI(TableSymbolicNames);
        }
        private void AddAddressToExtDef(string ExtDef, string Address)
        {
            SymbolicName temp;
            if (current.TsiContainsName(ExtDef, out temp))
                temp.Type = 1;

            foreach (ExtDefLine item in current.TableExtDef)
            {
                if (item.ExtDef.Equals(ExtDef))
                {
                    if (item.Address.Length != 0)
                    {
                        NewException("Метка уже существует: " + ExtDef);
                        break;
                    }

                    item.Address = Address;

                    int i = current.BinCodeLines.IndexOf(current.BinCodeLines.FirstOrDefault(x => x.Address.Equals(ExtDef)));
                    if (i == -1)
                    {
                        NewException("Ошибка внешнего имени: " + ExtDef);
                        break;
                    }

                    current.BinCodeLines[i].Length = Address;
                    OnChanged();
                    break;
                }
            }
        }
    }
}