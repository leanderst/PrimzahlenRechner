using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrimzahlenRechner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Inizialisierung
            string modus = string.Empty, startNummerString = string.Empty, zielNummerString = string.Empty, zahlEingabeString = string.Empty, spaltenAnzahlString = string.Empty; ;
            double zahlEingabe = 1, startNummer = 0, zielNummer = 0, primzahlenZahler = 0, spaltenAnzahl = 0, primzahlenMitZahler = 0;
            bool istPrimzahl = true;
            int[] primzahlenListe = new int[26003];
            Console.CursorVisible = false;

            //Primzahlen Liste exestiert
            if (File.Exists($@"C:\Users\" + Environment.UserName + "\\Documents\\Primzahlenliste.txt"))
            {
                try
                {
                    string[] primzahlenListeString = File.ReadAllLines($@"C:\Users\" + Environment.UserName + "\\Documents\\Primzahlenliste.txt");
                    for (int i = 0; i <= 26002; i++)
                    {
                        primzahlenListe[i] = Convert.ToInt32(primzahlenListeString[i]);
                    }
                }
                catch (InvalidCastException)
                {
                    PrimZahlenListeErstellen(primzahlenListe);
                }

            }
            //Primzahlen List exestiert NICHT
            else
            {
                PrimZahlenListeErstellen(primzahlenListe);
            }

            do
            {
                //Auzählung oder Abfrage Eingabe
                Console.Clear();
                Console.CursorVisible = false;
                Console.WriteLine("Zahl nach Primeigenschaften überprüfen   ->  [1]"); //48
                Console.WriteLine("Primzahlen aufzählen                     ->  [2]");
                Console.WriteLine("Primzahlen Matrix                        ->  [3]");
                Console.WriteLine("Primzahlen Liste erstellen               ->  [4]");
                Console.WriteLine("Programm beenden                         ->  [5]");
                do
                {
                    modus = Console.ReadKey(true).KeyChar.ToString();
                    if (modus != "1" && modus != "2" && modus != "3" && modus != "4" && modus != "5" && modus != "!"/*1*/ && modus != "\""/*2*/ && modus != "²"/*2*/ && modus != "³"/*3*/ && modus != "§"/*3*/ && modus != "$"/*4*/ && modus != "%"/*5*/)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(45, 0);
                        Console.WriteLine("[1]");
                        Console.SetCursorPosition(45, 1);
                        Console.WriteLine("[2]");
                        Console.SetCursorPosition(45, 2);
                        Console.WriteLine("[3]");
                        Console.SetCursorPosition(45, 3);
                        Console.WriteLine("[4]");
                        Console.SetCursorPosition(45, 4);
                        Console.WriteLine("[5]");
                        Thread.Sleep(50);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(45, 0);
                        Console.WriteLine("[1]");
                        Console.SetCursorPosition(45, 1);
                        Console.WriteLine("[2]");
                        Console.SetCursorPosition(45, 2);
                        Console.WriteLine("[3]");
                        Console.SetCursorPosition(45, 3);
                        Console.WriteLine("[4]");
                        Console.SetCursorPosition(45, 4);
                        Console.WriteLine("[5]");
                    }
                } while (modus != "1" && modus != "2" && modus != "3" && modus != "4" && modus != "5" && modus != "!"/*1*/ && modus != "\""/*2*/ && modus != "²"/*2*/ && modus != "³"/*3*/ && modus != "§"/*3*/ && modus != "$"/*4*/ && modus != "%"/*5*/);
                Console.Clear();


                //Primeigeschaften Überprüfung
                if (modus == "1" || modus == "!")
                {
                    //Zahl Eingabe
                    do
                    {
                        Console.Write("Zahl: ");
                        Console.CursorVisible = true;
                        zahlEingabeString = Console.ReadLine();
                        Console.CursorVisible = false;
                        if (zahlEingabeString == "0")
                        {
                            zahlEingabe = 0;
                            break;
                        }
                        else
                        {
                            double.TryParse(zahlEingabeString, out zahlEingabe);
                            if (zahlEingabe <= 0)
                            {
                                Console.Clear();
                            }
                        }
                    } while (zahlEingabe <= 0);

                    //Zahl Überprüfung
                    istPrimzahl = IstPrimzahlNeu(zahlEingabe, primzahlenListe);

                    //Ergebnis Ausgabe
                    Console.Write("\nIhre Zahl ist ");
                    if (istPrimzahl)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("eine");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" Primzahl.");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("keine");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" Primzahl.");
                    }
                    Console.WriteLine();
                }

                //Primzahlen Aufzählung
                if (modus == "2" || modus == "\"" || modus == "²")
                {
                    Console.WriteLine("     ( Drücke [Esc] um abzubrechen. )\n");

                    //Startnummer Eingabe
                    do
                    {
                        Console.Write("Start Primzahl: ");
                        Console.CursorVisible = true;
                        startNummerString = Console.ReadLine();
                        Console.CursorVisible = false;
                        double.TryParse(startNummerString, out startNummer);
                        if (startNummer <= 0)
                        {
                            Console.Clear();
                            Console.WriteLine("     ( Drücke [Esc] um abzubrechen. )\n");
                        }
                    } while (startNummer <= 0);
                    startNummer = (double)Math.Round(startNummer, 0);

                    //Zielnummer Eingabe
                    do
                    {
                        Console.Write("Ziel Primzahl:  ");
                        Console.CursorVisible = true;
                        zielNummerString = Console.ReadLine();
                        Console.CursorVisible = false;
                        double.TryParse(zielNummerString, out zielNummer);
                        if (zielNummer <= 0)
                        {
                            Console.Clear();
                            Console.WriteLine("     ( Drücke [Esc] um abzubrechen. )\n");
                            Console.WriteLine("Start Primzahl: " + startNummer);
                        }
                    } while (zielNummer <= 0);
                    zielNummer = (double)Math.Round(zielNummer, 0);
                    Console.Clear();

                    //Primzahlen Aufzählung
                    /*
                    1. Primzahl =       1         | 1stellig | * 1.0
                    6. Primzahl =       11        | 2stellig | * 1.8  (+0.8)
                    27. Primzahl =      101       | 3stellig | * 3.7  (+1.9)
                    170. Primzahl =     1009      | 4stellig | * 5.9  (+2.2)
                    1231. Primzahl =    10007     | 5stellig | * 8.1  (+2.2)
                    9594. Primzahl =    100003    | 6stellig | * 10.4 (+2.3)
                    78500. Primzahl =   1000003   | 7stellig | * 12.7 (+2.3)
                    664581. Primzahl =  10000019  | 8stellig | * 15.0 (+2.3)
                    5761457. Primzahl = 100000007 | 9stellig | * 17.3 (+2.3)
                    */
                    switch (startNummer.ToString().Length)
                    {
                        case 1:
                            zahlEingabe = startNummer * 1.0;
                            break;
                        case 2:
                            zahlEingabe = startNummer * 2;
                            break;
                        case 3:
                            zahlEingabe = startNummer * 4;
                            break;
                        case 4:
                            zahlEingabe = startNummer * 6;
                            break;
                        case 5:
                            zahlEingabe = startNummer * 8;
                            break;
                        case 6:
                            zahlEingabe = startNummer * 10;
                            break;
                        case 7:
                            zahlEingabe = startNummer * 13;
                            break;
                        case 8:
                            zahlEingabe = startNummer * 15;
                            break;
                        case 9:
                            zahlEingabe = startNummer * 17;
                            break;
                        default:
                            startNummer = startNummer * 8;
                            double multiplikator = 0;
                            for (int i = 0; i < startNummer.ToString().Length - 5; i++)
                            {
                                multiplikator += 2;
                            }
                            zahlEingabe = startNummer * multiplikator;
                            break;
                    }

                    primzahlenZahler = 0;
                    primzahlenMitZahler = startNummer;

                    //Ausrechnung der Primzahlen mit der Primzahlenliste
                    if (startNummer <= 26002)
                    {
                        while (primzahlenMitZahler <= zielNummer)
                        {
                            //Escape Abbruch Knopf
                            if (Console.KeyAvailable)
                            {
                                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                                {
                                    break;
                                }
                            }
                            //Console.WriteLine("zahlEingabe = " + zahlEingabe + "  |  primzahlenZahler = " + primzahlenZahler + "  |  primzahlenMItZahler = " + primzahlenMitZahler);
                            //Ausrechnung
                            if (primzahlenMitZahler < 13852)
                            {
                                if (Convert.ToInt32(primzahlenListe[Convert.ToInt32(primzahlenMitZahler - 1)]) == zahlEingabe)
                                {
                                    if (primzahlenMitZahler >= startNummer && primzahlenMitZahler <= zielNummer)
                                    {

                                        Console.WriteLine(primzahlenMitZahler + ". Primzahllll");
                                        Console.SetBufferSize(Console.BufferWidth, (Console.BufferHeight + 1));
                                        Console.SetCursorPosition(50, Convert.ToInt32(primzahlenZahler));
                                        primzahlenZahler++;
                                        Console.WriteLine("=     " + zahlEingabe);
                                    }
                                    primzahlenMitZahler++;
                                }
                                zahlEingabe++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    //Eigenausrechnung der Primzahlen
                    if (zielNummer > 26002)
                    {
                        while (primzahlenMitZahler <= zielNummer)
                        {
                            //Escape Abbruch Knopf
                            if (Console.KeyAvailable)
                            {
                                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                                {
                                    break;
                                }
                            }

                            //Ausrechnung
                            if (IstPrimzahlNeu(zahlEingabe, primzahlenListe))
                            {
                                if (primzahlenZahler >= startNummer && primzahlenZahler <= zielNummer)
                                {
                                    Console.WriteLine(primzahlenMitZahler + ". Primzahl");
                                    Console.SetBufferSize(Console.BufferWidth, (Console.BufferHeight + 1));
                                    Console.SetCursorPosition(50, Convert.ToInt32(primzahlenZahler));

                                    primzahlenZahler++;
                                    Console.WriteLine("=     " + zahlEingabe);
                                }
                                primzahlenMitZahler++;
                            }
                            zahlEingabe++;
                        }
                    }
                }

                //Primzahlen Matrix
                if (modus == "3" || modus == "§" || modus == "³")
                {
                    Console.WriteLine("     ( Drücke [Esc] um abzubrechen. )\n");
                    //Startnummer Eingabe
                    do
                    {
                        Console.Write("Start Zahl:      ");
                        Console.CursorVisible = true;
                        startNummerString = Console.ReadLine();
                        Console.CursorVisible = false;
                        double.TryParse(startNummerString, out startNummer);
                        if (startNummer <= 0)
                        {
                            Console.Clear();
                            Console.WriteLine("     ( Drücke [Esc] um abzubrechen. )\n");
                        }
                    } while (startNummer <= 0);
                    startNummer = (double)Math.Round(startNummer, 0);

                    //Zielnummer Eingabe
                    do
                    {
                        Console.Write("Ziel Zahl:       ");
                        Console.CursorVisible = true;
                        zielNummerString = Console.ReadLine();
                        Console.CursorVisible = false;
                        double.TryParse(zielNummerString, out zielNummer);
                        if (zielNummer <= 0)
                        {
                            Console.Clear();
                            Console.WriteLine("     ( Drücke [Esc] um abzubrechen. )\n");
                            Console.WriteLine("Start Zahl:      " + startNummer);
                        }
                    } while (zielNummer <= 0);
                    zielNummer = (double)Math.Round(zielNummer, 0);

                    //Spalten Anzahl Eingabe
                    do
                    {
                        Console.Write("Spalten Anzahl:  ");
                        Console.CursorVisible = true;
                        spaltenAnzahlString = Console.ReadLine();
                        Console.CursorVisible = false;
                        double.TryParse(spaltenAnzahlString, out spaltenAnzahl);
                        if (spaltenAnzahl <= 0)
                        {
                            Console.Clear();
                            Console.WriteLine("     ( Drücke [Esc] um abzubrechen. )\n");
                            Console.WriteLine("Start Zahl:      " + startNummer);
                            Console.WriteLine("Ziel Zahl:       " + zielNummer);
                        }
                    } while (spaltenAnzahl <= 0);
                    spaltenAnzahl = (double)Math.Round(spaltenAnzahl, 0);
                    Console.Clear();

                    //Berechnung der Anfangs Abstände
                    string spaltenNuller = "1";
                    for (int i = 1; i <= spaltenAnzahl.ToString().Length; i++)
                    {
                        spaltenNuller = spaltenNuller + "0";
                    }
                    while (startNummer % Convert.ToInt32(spaltenNuller) > spaltenAnzahl)
                    {
                        spaltenNuller.Remove(spaltenNuller.Length - 2, 1);
                    }
                    for (int i = Convert.ToInt32(startNummer % Convert.ToInt32(spaltenNuller)); i > 1; i--)
                    {
                        for (int j = 0; j <= zielNummer.ToString().Length; j++)
                        {
                            Console.Write(" ");
                        }
                    }

                    //Ausgabe der Matrix
                    for (double i = startNummer; i <= zielNummer; i++)
                    {

                        //Escape Abbruch Knopf
                        if (Console.KeyAvailable)
                        {
                            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                            {
                                break;
                            }
                        }
                        if (IstPrimzahlNeu(i, primzahlenListe))
                        {
                            for (int j = i.ToString().Length; j < zielNummer.ToString().Length; j++)
                            {
                                Console.Write(" ");
                            }
                            Console.Write(" ");
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(i);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            for (int j = i.ToString().Length; j < zielNummer.ToString().Length; j++)
                            {
                                Console.Write(" ");
                            }
                            Console.Write(" ");
                            Console.Write(i);
                        }
                        if (i % spaltenAnzahl == 0)
                        {
                            Console.WriteLine();
                        }
                    }
                    if (zielNummer % spaltenAnzahl != 0)
                    {
                        Console.WriteLine();
                    }
                }

                //Primzahlen Liste erstellen
                if (modus == "4" || modus == "$")
                {
                    Console.WriteLine("     ( Drücke [Esc] um abzubrechen. )\n");

                    //Startnummer Eingabe
                    do
                    {
                        Console.Write("Start Primzahl: ");
                        Console.CursorVisible = true;
                        startNummerString = Console.ReadLine();
                        Console.CursorVisible = false;
                        double.TryParse(startNummerString, out startNummer);
                    } while (startNummer <= 0);
                    startNummer = (double)Math.Round(startNummer, 0);

                    //Zielnummer Eingabe
                    do
                    {
                        Console.Write("Ziel Primzahl:  ");
                        Console.CursorVisible = true;
                        zielNummerString = Console.ReadLine();
                        Console.CursorVisible = false;
                        double.TryParse(zielNummerString, out zielNummer);
                    } while (zielNummer <= 0);
                    zielNummer = (double)Math.Round(zielNummer, 0);
                    Console.Clear();

                    //Mit Userpfad oder ohne auswählen
                    Console.CursorVisible = false;
                    Console.WriteLine("\"C:\\Users\\" + Environment.UserName + "\\\"  ->  [1] | Pfad selber ausschreiben  ->  [2]");
                    do
                    {
                        modus = Console.ReadKey(true).KeyChar.ToString();
                    } while (modus != "1" && modus != "2" && modus != "!"/*1*/ && modus != "\""/*2*/ && modus != "²"/*2*/);

                    bool Wiederholen = false;
                    do
                    {
                        Console.WriteLine("\n     ( Gebe \"STOPP\" ein um abb zu brechen. )\n");
                        string Pfad;
                        Console.CursorVisible = true;
                        if (modus == "1" || modus == "!"/*1*/)
                        {
                            Pfad = "C:\\Users\\" + Environment.UserName + "\\";
                            Console.Write("Pfad: " + Pfad);
                            Pfad = Pfad + Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("Pfad: ");
                            Pfad = Console.ReadLine();
                        }
                        Console.CursorVisible = false;
                        if (Pfad.Substring(Pfad.Length - 5, 5) == "STOPP") break;

                        //try
                        //{
                            PersönlichePrimzahlenListeErstellen(startNummer, zielNummer, Pfad, primzahlenListe);
                        //}
                        /*
                        catch
                        {
                            Wiederholen = true;
                            Console.WriteLine("Beim Erstellen der Liste ist leider ein Fehler passiert.");
                        }
                        */
                    } while (Wiederholen);
                }

                //Programm beenden
                if (modus == "5" || modus == "%")
                {
                    Console.WriteLine("Auf Wiedersehen!");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                }

                //Ende
                Console.WriteLine("\n     ( Drücke [Enter] wenn Sie bereit sind. )");
                Console.ReadKey(true);
                Console.Clear();
            } while (true);
        }
        static bool IstPrimzahlAlt(double i)
        {
            for (int j = 2; j <= (i / 2); j++)
            {
                if (i % j == 0)
                {
                    return false;
                }
            }
            return true;
        }
        static bool IstPrimzahlNeu(double i, int[] primzahlenListe)
        {
            int k = 1;
            int j = primzahlenListe[k];
            while (j < (i / 2))
            {
                if (i % j == 0)
                {
                    return false;
                }
                k++;
                if (k > 26002)
                {
                    for (; j <= (i / 2); j++)
                    {
                        if (i % j == 0)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                j = primzahlenListe[k];
                //Console.WriteLine(k + " | " + (i / 2) + " | " + j);
            }
            return true;
        }
        static void PrimZahlenListeErstellen(int[] primzahlenListe)
        {
            Console.WriteLine("                                          ( Das Progamm wird kurz vorbereitet. )");
            Console.WriteLine("\n     ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");

            //Dekleration der ersten 26000 Primzahlen
            Console.SetCursorPosition(Convert.ToInt32(5), 2);
            int zahler = 0, ladeBalken = 0, primZahlenZahler = 0;
            while (zahler <= 300080)
            {
                zahler++;
                ladeBalken++;
                if (ladeBalken == 2728)
                {
                    Console.Write("█");
                    ladeBalken = 0;
                }
                if (IstPrimzahlAlt(zahler))
                {
                    primzahlenListe[primZahlenZahler] = zahler;
                    primZahlenZahler++;
                }
            }
            File.WriteAllText($@"C:\Users\" + Environment.UserName + "\\Documents\\Primzahlenliste.txt", "");
            File.WriteAllLines($@"C:\Users\" + Environment.UserName + "\\Documents\\Primzahlenliste.txt", Array.ConvertAll(primzahlenListe, x => x.ToString()));
            Console.Clear();
        }

        static void PersönlichePrimzahlenListeErstellen(double startNummer, double zielNummer, string Pfad, int[] primzahlenListe)
        {
            Console.WriteLine("                          ( Dies wird je nach der Menge von gewünschten Primzahlen lange brachen )");
            Console.WriteLine("\n     ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");
            Console.SetCursorPosition(5, Console.CursorTop - 1);
            
            double temp = 0;
            switch (startNummer.ToString().Length)
            {
                case 1:
                    temp = startNummer * 1.0;
                    break;
                case 2:
                    temp = startNummer * 2;
                    break;
                case 3:
                    temp = startNummer * 4;
                    break;
                case 4:
                    temp = startNummer * 6;
                    break;
                case 5:
                    temp = startNummer * 8;
                    break;
                case 6:
                    temp = startNummer * 10;
                    break;
                case 7:
                    temp = startNummer * 13;
                    break;
                case 8:
                    temp = startNummer * 15;
                    break;
                case 9:
                    temp = startNummer * 17;
                    break;
                default:
                    startNummer = startNummer * 8;
                    double multiplikator = 0;
                    for (int i = 0; i < startNummer.ToString().Length - 5; i++)
                    {
                        multiplikator += 2;
                    }
                    temp = startNummer * multiplikator;
                    break;
            }

            double primzahlenZahler = 0;
            double primzahlenMitZahler = startNummer;
            double ladebalken = 0;
            double[] PersoenlichePrimzahlenListe = new double[Convert.ToInt32(zielNummer + 1 - startNummer)];

            //Ausrechnung der Primzahlen mit der Primzahlenliste
            if (startNummer <= 26002)
            {
                while (primzahlenMitZahler <= zielNummer)
                {
                    //Escape Abbruch Knopf
                    if (Console.KeyAvailable)
                    {
                        if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                        {
                            break;
                        }
                    }
                    //Console.WriteLine("zahlEingabe = " + zahlEingabe + "  |  primzahlenZahler = " + primzahlenZahler + "  |  primzahlenMItZahler = " + primzahlenMitZahler);
                    //Ausrechnung
                    if (primzahlenMitZahler < 26004)
                    {
                        if (Convert.ToInt32(primzahlenListe[Convert.ToInt32(primzahlenMitZahler - 1)]) == temp)
                        {
                            if (primzahlenMitZahler >= startNummer && primzahlenMitZahler <= zielNummer)
                            {
                                PersoenlichePrimzahlenListe[Convert.ToInt32(primzahlenZahler)] = temp;
                                ladebalken++;
                                if (ladebalken == Convert.ToInt64((zielNummer - (startNummer - 1)) / 110))
                                {
                                    ladebalken = 0;
                                    Console.Write("█");
                                }
                                primzahlenZahler++;
                            }
                            primzahlenMitZahler++;
                        }
                        temp++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (zielNummer > 26002)
                {
                    while (primzahlenMitZahler <= zielNummer)
                    {
                        //Escape Abbruch Knopf
                        if (Console.KeyAvailable)
                        {
                            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                            {
                                break;
                            }
                        }

                        //Ausrechnung
                        if (IstPrimzahlNeu(temp, primzahlenListe))
                        {
                            if (primzahlenZahler >= startNummer && primzahlenZahler <= zielNummer)
                            {
                                PersoenlichePrimzahlenListe[Convert.ToInt32(primzahlenZahler)] = temp;
                                ladebalken++;
                                if (ladebalken == Convert.ToInt64((zielNummer - (startNummer - 1)) / 110))
                                {
                                    ladebalken = 0;
                                    Console.Write("█");
                                }
                                primzahlenZahler++;
                            }
                            primzahlenMitZahler++;
                        }
                        temp++;
                    }
                }
            }
            File.WriteAllText(Pfad + "Primzahlenliste.txt", "");
            File.WriteAllLines(Pfad + "Primzahlenliste.txt", Array.ConvertAll(PersoenlichePrimzahlenListe, x => x.ToString()));
            Console.Clear();
        }
    }
}
