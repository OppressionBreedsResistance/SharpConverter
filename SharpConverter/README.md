# SharpConverter 
## IDEA
Każda binarka napisana w .NET (tzw. assembly) może zostać przedstawione w postaci bajtów i jej klasy mogą zostać załadowane do skryptu PowerShellowego. Skrypt PowerShellowy natomiast może być bardzo prosto umieszczony na zewnętrznym URL i uruchomiony w pamięci PowerShella. Kombinacja tych dwóch czynników oznacza, że możemy uruchomić dowolne Assembly .NETowe z zewnętrznego zasobu w pamięci PowerShella co zapewnia nam dośc wysoki poziom AV Evasion. 


Do stworzenia programu nakłonił mnie projekt Fabiana Moscha (S3cur3Th1sSh1t) dostepny pod tym adresem:
https://github.com/S3cur3Th1sSh1t/PowerSharpPack/tree/master a także program DotNetToJSCript, który zamienia .NET assembly na JSCript w względnie podobny sposób (https://github.com/tyranid/DotNetToJScript)

Generalnie chodzi o to, że S3cur3Th1sSh1t udostępnia gotowego .execa który pobiera plik napisany w .NET i udostepnia nam jego reprezentację w formie bajtów, skompresowaną gzipem oraz zakodowaną na Base64. Problem w tym, że nie udostępnia do niej kodu źródłowego - a my nie lubimy execów bez kodu źródłowego. 

Postanowiłem też zamienić algorytm kompresji GZIP na Deflate, być może ominie to tez jakieś statyczne rule detekcji. 

## Jak używać? 
Program może pobierac jeden lub dwa argumenty. 
Argument 1 jest wymagany i jest to ścieżka do .NET binarki którą chcemy zamienić na zakodowany strumień bajtów. 
Argument 2 jest opcjonalny i jeśli chcemy wygenerować gotowy skrypt PowerShellowy uruchamiający naszą binarkę to wpisujemy w drugim argumencie słowo "save". Wtedy w katalogu roboczym tworzy się gotowy skrypt o nazwie script.ps1. 

## Przykłady użycia 

### Zamiana .exe na zakodowany strumień bajtów, wyświetlenie i zapisanie gotowego skryptu PowerShell do pliku script.ps1 
`.\SharpConverter.exe "C:\Users\piotr\Documents\repos\HelloWorld\HelloWorld\bin\x64\Release\HelloWorld.exe" save`

### Zamiana .exe na zakodowany strumień bajtów i wyświetlenie go
`.\SharpConverter.exe "C:\Users\piotr\Documents\repos\HelloWorld\HelloWorld\bin\x64\Release\HelloWorld.exe"`


