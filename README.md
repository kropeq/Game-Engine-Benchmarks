# Porównanie silników gier

### Silniki

Do porównania wykorzystałem różne rodzaje silników, aby poddać obserwacji różne kategorie silników: 3D ze wsparciem 2D - Unity, 2D - Cocos2d-x oraz bibliotekę graficzną SDL.

Wersje wykorzystanych narzędzi:
* Unity 5.4.2f
* Cocos2d-x 3.15.1
* Cocos Creator 1.5.0
* SDL 2.0.5

### Benchmarki

Do wykonania porównań wykorzystałem dwa benchmarki: graficzny i fizyczny. Wyniki benchmarków to maksymalna liczba obrazków do utrzymaniu zadanej granicy FPS( Klatek na sekundę ).

##### BunnyMark

BunnyMark test polega na wielokrotnym renderowaniu tego samego obrazka 2D bez wykorzystania fizyki, jedynie z symulacją kinematyki w postaci przesuwania obrazka góra-dół.

##### PhysicsMark

PhysicsMark test polega na replikowaniu obrazków i zrzucaniu ich do pudełka, tym razem wykorzystując elementy silnika fizycznego 2D takie jak grawitacja, odbicia, kolizje i rotacje.

### Wyniki

#### Komputer stacjonarny
|Element|Specyfikacja||Wartość|
|--------|-------------------------|:---:|---|
|System|Windows 7 Professional Service Pack 1|Typ|32 bitowy|
|Procesor|Inter(R) Core(TM)2 Quad CPU Q8400 @ 2.66GHz|Rdzenie|4|
|Karta graficzna|NVIDIA GeForce GTX 275|Pamięć|896 MB|
|Pamięć RAM|4 GB(3.25GB dostępne)|||

##### BunnyMark - 60 FPS 

|Narzędzie|Wynik(60FPS)|Narzędzie|Wynik(50FPS)|
|---------|------------|---------|------------|
|**SDL2**|*19 200*|**SDL2**|*23 100*|
|**Cocos2d-x**|*8 400*|**Cocos2d-x**|*9 300*|
|**Unity 5**|*5 200*|**Unity 5**|*6 500*|
|**Cocos Creator**|TODO|**Cocos Creator**|TODO|

#### Smartfon Sony Xperia M2 D2303
|Element|Specyfikacja||Wartość|
|--------|-------------------------|:---:|---|
|System|Android OS 5.1.1|Typ|32 bitowy|
|Procesor|Qualcomm Snapdragon 400 MSM8926 @ 1,2GHz|Rdzenie|4|
|Karta graficzna|Qualcomm Adreno (TM) 305|Zegar rdzenia|450 MHz|
|Pamięć RAM|864 MB|||

##### BunnyMark

|Narzędzie|Wynik(60FPS)|Narzędzie|Wynik(50FPS)|
|---------|------------|---------|------------|
|**Unity 5**|*860*|**Unity 5**|*1 070*|
|**Cocos2d-x**|*470*|**SDL2**|*750*|
|**Cocos Creator**|*300*|**Cocos2d-x**|*570*|
|**SDL2**|*250*|**Cocos Creator**|*375*|




