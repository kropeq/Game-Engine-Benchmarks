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

Ze względu na chęć porównania części wspólnej tych silników, pierwszy test polega na wielokrotnym renderowaniu tego samego obrazka 2D bez wykorzystania fizyki, jedynie z symulacją kinematyki w postaci przesuwania obrazka góra-dół.

Drugi test polega na replikowaniu obrazków i zrzucaniu ich do pudełka, tym razem wykorzystując elementy silnika fizycznego 2D takie jak grawitacja, odbicia, kolizje i rotacje.